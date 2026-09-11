using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace eProtokoll
{
    public partial class FormAdministrator : Form
    {
        public FormAdministrator()
        {
            InitializeComponent();
            this.Load += FormAdministrator_Load;

            lblMiresevini.Text =
       $"Mirë se erdhët, {UserSession.EmriPlote}";
        }

        private void FormAdministrator_Load(object? sender, EventArgs e)
        {
            NgarkoInstitucionet();
        }

        private void NgarkoInstitucionet()
        {
            string query = "SELECT InstitucioniId, Emri, Adresa, Telefon, Email FROM Institucionet ORDER BY Emri";
            var dt = DBHelper.ExecuteQuery(query);
            dgvInstitucionet.DataSource = dt;
        }


        private void btnShtoInstitucion_Click(object sender, EventArgs e)
        {
            FormInstitucioni f = new FormInstitucioni();
            if (f.ShowDialog() == DialogResult.OK)
            {
                NgarkoInstitucionet(); 
            }
        }

        private void btnModifikoInstitucion_Click(object sender, EventArgs e)
        {
            if (dgvInstitucionet.CurrentRow == null)
            {
                MessageBox.Show("Zgjidhni nje institucion per te modifikuar!", "Kujdes",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvInstitucionet.CurrentRow.Cells["InstitucioniId"].Value);

            FormInstitucioni f = new FormInstitucioni(id); 
            if (f.ShowDialog() == DialogResult.OK)
            {
                NgarkoInstitucionet();
            }
        }

        private void btnFshiInstitucion_Click(object sender, EventArgs e)
        {
            if (dgvInstitucionet.CurrentRow == null)
            {
                MessageBox.Show("Zgjidhni nje institucion per te fshire!", "Kujdes",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvInstitucionet.CurrentRow.Cells["InstitucioniId"].Value);
            string emri = dgvInstitucionet.CurrentRow.Cells["Emri"].Value.ToString();

            var konfirmim = MessageBox.Show($"Jeni te sigurt qe doni te fshini '{emri}'?",
                "Konfirmo fshirjen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmim == DialogResult.Yes)
            {
                string query = "DELETE FROM Institucionet WHERE InstitucioniId = @id";
                SqlParameter[] parameters = { new SqlParameter("@id", id) };

                try
                {
                    DBHelper.ExecuteNonQuery(query, parameters);
                    NgarkoInstitucionet();
                }
                catch (SqlException ex) when (ex.Number == 547)
                {
                    MessageBox.Show(
                    "Institucioni nuk mund të fshihet sepse është përdorur në një ose më shumë shkresa dhe është pjesë e historikut të protokollit.",
                    "Fshirja nuk lejohet",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Ndodhi një gabim gjatë fshirjes:\n" + ex.Message,
                        "Gabim",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void btnVitetProtokollare_Click(object sender, EventArgs e)
        {
            using (FormVitetProtokollare forma =
           new FormVitetProtokollare())
            {
                forma.ShowDialog();
            }
        }

        private void btnPerdoruesit_Click(object sender, EventArgs e)
        {
            using FormPerdoruesit forma =
        new FormPerdoruesit();

            forma.ShowDialog();
        }

        private void btnDil_Click(object sender, EventArgs e)
        {
            DialogResult pergjigjja = MessageBox.Show(
                "Dëshironi të dilni nga llogaria?",
                "Konfirmo daljen",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (pergjigjja != DialogResult.Yes)
            {
                return;
            }

            UserSession.Pastro();
            Close();
        }
    }
}
