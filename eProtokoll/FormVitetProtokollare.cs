using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace eProtokoll
{
    public partial class FormVitetProtokollare : Form
    {
        public FormVitetProtokollare()
        {
            InitializeComponent();
            this.Load += FormVitetProtokollare_Load;
        }

        private void FormVitetProtokollare_Load(
            object? sender,
            EventArgs e)
        {
            NgarkoVitet();
        }

        private void NgarkoVitet()
        {
            try
            {
                string query = @"
                    SELECT
                        Viti,
                        NumriFillestar,
                        NumriFundit,
                        CASE
                            WHEN Mbyllur = 1 THEN 'Mbyllur'
                            ELSE 'Hapur'
                        END AS Statusi
                    FROM VitetProtokollare
                    ORDER BY Viti DESC";

                dgvVitet.DataSource =
                    DBHelper.ExecuteQuery(query);

                dgvVitet.Columns["Viti"].HeaderText =
                    "Viti";

                dgvVitet.Columns["NumriFillestar"].HeaderText =
                    "Numri fillestar";

                dgvVitet.Columns["NumriFundit"].HeaderText =
                    "Numri i fundit";

                dgvVitet.Columns["Statusi"].HeaderText =
                    "Statusi";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gabim gjatë ngarkimit të viteve protokollare: "
                    + ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnHapVit_Click(object sender, EventArgs e)
        {
            int viti = Convert.ToInt32(numViti.Value);
            int numriFillestar =
                Convert.ToInt32(numNumriFillestar.Value);

            try
            {
                
                string queryVitHapur = @"
            SELECT COUNT(*)
            FROM VitetProtokollare
            WHERE Mbyllur = 0";

                var dtVitHapur =
                    DBHelper.ExecuteQuery(queryVitHapur);

                int numerViteshTeHapura =
                    Convert.ToInt32(dtVitHapur.Rows[0][0]);

                if (numerViteshTeHapura > 0)
                {
                    MessageBox.Show(
                        "Ekziston një vit protokollar i hapur. " +
                        "Duhet ta mbyllni përpara se të hapni një vit të ri.",
                        "Kujdes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                
                string queryEkziston = @"
            SELECT COUNT(*)
            FROM VitetProtokollare
            WHERE Viti = @viti";

                SqlParameter[] parametratEkziston =
                {
            new SqlParameter("@viti", viti)
        };

                var dtEkziston = DBHelper.ExecuteQuery(
                    queryEkziston,
                    parametratEkziston);

                int ekziston =
                    Convert.ToInt32(dtEkziston.Rows[0][0]);

                if (ekziston > 0)
                {
                    MessageBox.Show(
                        $"Viti protokollar {viti} ekziston tashmë!",
                        "Kujdes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                var konfirmim = MessageBox.Show(
                    $"Dëshironi të hapni vitin {viti} " +
                    $"me numër fillestar {numriFillestar}?",
                    "Konfirmo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (konfirmim != DialogResult.Yes)
                    return;

                string insertQuery = @"
            INSERT INTO VitetProtokollare
                (Viti, NumriFillestar, NumriFundit, Mbyllur)
            VALUES
                (@viti, @numriFillestar, NULL, 0)";

                SqlParameter[] parametratInsert =
                {
            new SqlParameter("@viti", viti),
            new SqlParameter(
                "@numriFillestar",
                numriFillestar)
        };

                DBHelper.ExecuteNonQuery(
                    insertQuery,
                    parametratInsert);

                MessageBox.Show(
                    $"Viti protokollar {viti} u hap me sukses!",
                    "Sukses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                NgarkoVitet();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gabim gjatë hapjes së vitit: " + ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnMbyllVit_Click(object sender, EventArgs e)
        {
            if (dgvVitet.CurrentRow == null)
            {
                MessageBox.Show(
                    "Zgjidhni një vit protokollar për ta mbyllur!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int viti = Convert.ToInt32(
                dgvVitet.CurrentRow.Cells["Viti"].Value);

            string statusi = Convert.ToString(
                dgvVitet.CurrentRow.Cells["Statusi"].Value) ?? "";

            if (statusi == "Mbyllur")
            {
                MessageBox.Show(
                    $"Viti {viti} është mbyllur më parë!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var konfirmim = MessageBox.Show(
                $"Jeni të sigurt që dëshironi të mbyllni vitin {viti}?\n\n" +
                "Pas mbylljes nuk mund të regjistrohen më shkresa në këtë vit.",
                "Konfirmo mbylljen",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (konfirmim != DialogResult.Yes)
                return;

            try
            {
                string query = @"
            UPDATE VitetProtokollare
            SET
                NumriFundit =
                (
                    SELECT MAX(NumriProtokolli)
                    FROM Shkresat
                    WHERE Shkresat.Viti = @viti
                ),
                Mbyllur = 1
            WHERE Viti = @viti
              AND Mbyllur = 0";

                SqlParameter[] parameters =
                {
            new SqlParameter("@viti", viti)
        };

                int rreshtaTeNdryshuar =
                    DBHelper.ExecuteNonQuery(query, parameters);

                if (rreshtaTeNdryshuar == 0)
                {
                    MessageBox.Show(
                        "Viti nuk u mbyll. Mund të jetë mbyllur më parë.",
                        "Kujdes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                MessageBox.Show(
                    $"Viti protokollar {viti} u mbyll me sukses!",
                    "Sukses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                NgarkoVitet();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gabim gjatë mbylljes së vitit: " + ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}