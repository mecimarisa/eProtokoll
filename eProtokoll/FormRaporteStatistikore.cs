using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace eProtokoll
{
    public partial class FormRaporteStatistikore : Form
    {
        public FormRaporteStatistikore()
        {
            InitializeComponent();
        }

        private void FormRaporteStatistikore_Load(
            object sender,
            EventArgs e)
        {
            if (UserSession.PerdoruesiId <= 0)
            {
                MessageBox.Show(
                    "Sesioni nuk është i vlefshëm!",
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Close();
                return;
            }

            if (UserSession.Roli != "Menaxher")
            {
                MessageBox.Show(
                    "Vetëm Menaxheri mund të shikojë raportet statistikore!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                Close();
                return;
            }

            KonfiguroTabelat();
            NgarkoVitet();
        }

        private void KonfiguroTabelat()
        {
            KonfiguroTabelen(dgvSipasInstitucionit);
            KonfiguroTabelen(dgvSipasMuajit);
            KonfiguroTabelen(dgvSipasLlojit);
        }

        private void KonfiguroTabelen(
            DataGridView tabela)
        {
            tabela.ReadOnly = true;
            tabela.AllowUserToAddRows = false;
            tabela.AllowUserToDeleteRows = false;
            tabela.MultiSelect = false;

            tabela.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            tabela.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            tabela.RowHeadersVisible = false;
        }

        private void NgarkoVitet()
        {
            try
            {
                string query = @"
                    SELECT Viti
                    FROM VitetProtokollare
                    ORDER BY Viti DESC;";

                DataTable dt =
                    DBHelper.ExecuteQuery(query);

                cmbViti.DataSource = dt;
                cmbViti.DisplayMember = "Viti";
                cmbViti.ValueMember = "Viti";

                if (dt.Rows.Count > 0)
                {
                    cmbViti.SelectedIndex = 0;
                    NgarkoRaportin();
                }
                else
                {
                    MessageBox.Show(
                        "Nuk u gjet asnjë vit protokollar.",
                        "Kujdes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    btnNgarko.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gabim gjatë ngarkimit të viteve: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnNgarko_Click(
            object sender,
            EventArgs e)
        {
            NgarkoRaportin();
        }

        private void NgarkoRaportin()
        {
            if (cmbViti.SelectedValue == null)
            {
                MessageBox.Show(
                    "Zgjidhni vitin protokollar!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int viti;

            if (cmbViti.SelectedValue is DataRowView row)
            {
                viti = Convert.ToInt32(row["Viti"]);
            }
            else
            {
                viti =
                    Convert.ToInt32(
                        cmbViti.SelectedValue);
            }

            try
            {
                NgarkoPermbledhjen(viti);
                NgarkoSipasInstitucionit(viti);
                NgarkoSipasMuajit(viti);
                NgarkoSipasLlojit(viti);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Raporti nuk mund të ngarkohej: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void NgarkoPermbledhjen(
            int viti)
        {
            string query = @"
                SELECT
                    COUNT(*) AS Totali,

                    SUM(
                        CASE
                            WHEN LlojiShkreses = N'Hyrese'
                            THEN 1
                            ELSE 0
                        END
                    ) AS Hyrese,

                    SUM(
                        CASE
                            WHEN LlojiShkreses = N'Dalese'
                            THEN 1
                            ELSE 0
                        END
                    ) AS Dalese,

                    SUM(
                        CASE
                            WHEN LlojiShkreses = N'e Brendshme'
                            THEN 1
                            ELSE 0
                        END
                    ) AS Brendshme

                FROM Shkresat
                WHERE Viti = @viti;";

            SqlParameter[] parameters =
            {
                new SqlParameter("@viti", viti)
            };

            DataTable dt =
                DBHelper.ExecuteQuery(
                    query,
                    parameters);

            int totali = 0;
            int hyrese = 0;
            int dalese = 0;
            int brendshme = 0;

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                totali =
                    row["Totali"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(
                            row["Totali"]);

                hyrese =
                    row["Hyrese"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(
                            row["Hyrese"]);

                dalese =
                    row["Dalese"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(
                            row["Dalese"]);

                brendshme =
                    row["Brendshme"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(
                            row["Brendshme"]);
            }

            lblTotalShkresa.Text =
                $"Totali i shkresave: {totali}";

            lblHyrese.Text =
                $"Hyrëse: {hyrese}";

            lblDalese.Text =
                $"Dalëse: {dalese}";

            lblBrendshme.Text =
                $"Të brendshme: {brendshme}";
        }

        private void NgarkoSipasInstitucionit(
            int viti)
        {
            string query = @"
                SELECT
                    i.Emri AS Institucioni,
                    COUNT(*) AS [Numri i shkresave]

                FROM Shkresat s

                INNER JOIN Institucionet i
                    ON s.InstitucioniId =
                       i.InstitucioniId

                WHERE
                    s.Viti = @viti
                    AND s.LlojiShkreses =
                        N'Hyrese'

                GROUP BY
                    i.InstitucioniId,
                    i.Emri

                ORDER BY
                    COUNT(*) DESC,
                    i.Emri;";

            SqlParameter[] parameters =
            {
                new SqlParameter("@viti", viti)
            };

            dgvSipasInstitucionit.DataSource =
                DBHelper.ExecuteQuery(
                    query,
                    parameters);

            dgvSipasInstitucionit.ClearSelection();
        }

        private void NgarkoSipasMuajit(
            int viti)
        {
            string query = @"
                WITH Muajt AS
                (
                    SELECT 1 AS NumriMuajit, N'Janar' AS Muaji
                    UNION ALL SELECT 2, N'Shkurt'
                    UNION ALL SELECT 3, N'Mars'
                    UNION ALL SELECT 4, N'Prill'
                    UNION ALL SELECT 5, N'Maj'
                    UNION ALL SELECT 6, N'Qershor'
                    UNION ALL SELECT 7, N'Korrik'
                    UNION ALL SELECT 8, N'Gusht'
                    UNION ALL SELECT 9, N'Shtator'
                    UNION ALL SELECT 10, N'Tetor'
                    UNION ALL SELECT 11, N'Nëntor'
                    UNION ALL SELECT 12, N'Dhjetor'
                )

                SELECT
                    m.Muaji,
                    COUNT(s.ShkresaId)
                        AS [Numri i shkresave]

                FROM Muajt m

                LEFT JOIN Shkresat s
                    ON MONTH(s.DataRegjistrimit) =
                       m.NumriMuajit
                    AND s.Viti = @viti

                GROUP BY
                    m.NumriMuajit,
                    m.Muaji

                ORDER BY
                    m.NumriMuajit;";

            SqlParameter[] parameters =
            {
                new SqlParameter("@viti", viti)
            };

            dgvSipasMuajit.DataSource =
                DBHelper.ExecuteQuery(
                    query,
                    parameters);

            dgvSipasMuajit.ClearSelection();
        }

        private void NgarkoSipasLlojit(
            int viti)
        {
            string query = @"
                WITH Llojet AS
                (
                    SELECT
                        1 AS Renditja,
                        N'Hyrese' AS Vlera,
                        N'Hyrëse' AS Emri

                    UNION ALL

                    SELECT
                        2,
                        N'Dalese',
                        N'Dalëse'

                    UNION ALL

                    SELECT
                        3,
                        N'e Brendshme',
                        N'E brendshme'
                )

                SELECT
                    l.Emri AS [Lloji i shkresës],
                    COUNT(s.ShkresaId) AS Sasia

                FROM Llojet l

                LEFT JOIN Shkresat s
                    ON s.LlojiShkreses = l.Vlera
                    AND s.Viti = @viti

                GROUP BY
                    l.Renditja,
                    l.Emri

                ORDER BY
                    l.Renditja;";

            SqlParameter[] parameters =
            {
                new SqlParameter("@viti", viti)
            };

            dgvSipasLlojit.DataSource =
    DBHelper.ExecuteQuery(
        query,
        parameters);

            dgvSipasLlojit.ClearSelection();
        }
    }
}