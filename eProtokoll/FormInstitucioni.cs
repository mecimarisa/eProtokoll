using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace eProtokoll
{
    public partial class FormInstitucioni : Form
    {
        private int? institucioniId = null; 

        
        public FormInstitucioni()
        {
            InitializeComponent();
        }

       
        public FormInstitucioni(int id)
        {
            InitializeComponent();
            institucioniId = id;
            this.Load += (s, e) => NgarkoTeDhenat(id);
        }

        private void NgarkoTeDhenat(int id)
        {
            string query = "SELECT Emri, Adresa, Telefon, Email FROM Institucionet WHERE InstitucioniId = @id";
            SqlParameter[] parameters = { new SqlParameter("@id", id) };
            var dt = DBHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                txtEmri.Text = dt.Rows[0]["Emri"].ToString();
                txtAdresa.Text = dt.Rows[0]["Adresa"].ToString();
                txtTelefon.Text = dt.Rows[0]["Telefon"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
            }
        }

        private void btnRuaj_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmri.Text))
            {
                MessageBox.Show("Emri i institucionit eshte i detyrueshem!", "Kujdes",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

      
            try
            {
                if (institucioniId == null)
                {
                    // Shtim
                    string query = @"INSERT INTO Institucionet (Emri, Adresa, Telefon, Email)
                                      VALUES (@emri, @adresa, @telefon, @email)";
                    SqlParameter[] parameters = {
                        new SqlParameter("@emri", txtEmri.Text.Trim()),
                        new SqlParameter("@adresa", (object)txtAdresa.Text.Trim() ?? DBNull.Value),
                        new SqlParameter("@telefon", (object)txtTelefon.Text.Trim() ?? DBNull.Value),
                        new SqlParameter("@email", (object)txtEmail.Text.Trim() ?? DBNull.Value)
                    };
                    DBHelper.ExecuteNonQuery(query, parameters);
                }
                else
                {
                    // Modifikim
                    string query = @"UPDATE Institucionet 
                                      SET Emri = @emri, Adresa = @adresa, Telefon = @telefon, Email = @email
                                      WHERE InstitucioniId = @id";
                    SqlParameter[] parameters = {
                        new SqlParameter("@emri", txtEmri.Text.Trim()),
                        new SqlParameter("@adresa", txtAdresa.Text.Trim()),
                        new SqlParameter("@telefon", txtTelefon.Text.Trim()),
                        new SqlParameter("@email", txtEmail.Text.Trim()),
                        new SqlParameter("@id", institucioniId.Value)
                    };
                    DBHelper.ExecuteNonQuery(query, parameters);
                }

                this.DialogResult = DialogResult.OK; 
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim: " + ex.Message, "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}