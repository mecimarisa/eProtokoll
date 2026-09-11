using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace eProtokoll
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Ju lutem plotësoni përdoruesin dhe fjalëkalimin!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string query = @"
                SELECT
                    p.PerdoruesiId,
                    p.Emri,
                    p.Mbiemri,
                    r.EmriRolit
                FROM Perdoruesit p
                INNER JOIN Rolet r
                    ON p.RolId = r.RolId
                WHERE p.Username = @username
                  AND p.Password = @password
                  AND p.Aktiv = 1";

            SqlParameter[] parameters =
            {
                new SqlParameter("@username", username),
                new SqlParameter("@password", password)
            };

            try
            {
                var result =
                    DBHelper.ExecuteQuery(query, parameters);

                if (result.Rows.Count > 0)
                {
                    int perdoruesiId = Convert.ToInt32(
                        result.Rows[0]["PerdoruesiId"]);

                    string roli =
                        result.Rows[0]["EmriRolit"]
                            .ToString() ?? "";

                    string emriPlote =
                        result.Rows[0]["Emri"] + " " +
                        result.Rows[0]["Mbiemri"];

                    // Ruajm perdoruesin e identifikuar
                    // per ta perdorur ne format e tjera.
                    UserSession.PerdoruesiId =
                        perdoruesiId;

                    UserSession.EmriPlote =
                        emriPlote;

                    UserSession.Roli =
                        roli;

                    Form formeTjeter;

                  

                    switch (roli)
                    {
                        case "Administrator":
                            formeTjeter =
                                new FormAdministrator();
                            break;

                        case "Menaxher":
                            formeTjeter =
                                new FormMenaxher();
                            break;

                        case "Punonjes":
                            formeTjeter =
                                new FormPunonjes();
                           
                            break;

                        default:
                            UserSession.Pastro();

                            MessageBox.Show(
                                "Rol i panjohur!",
                                "Gabim",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                            return;
                    }

                    // Kur mbyllet forma tjeter,
                    // pastrojme sesionin dhe rihapim login-in.
                    formeTjeter.FormClosed += (s, args) =>
                    {
                        UserSession.Pastro();

                        txtUsername.Clear();
                        txtPassword.Clear();

                        this.Show();
                        this.Activate();
                        txtUsername.Focus();
                    };

                    this.Hide();
                    formeTjeter.Show();
                }
                else
                {
                    MessageBox.Show(
                        "Përdoruesi ose fjalëkalimi është gabim!",
                        "Gabim",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gabim në lidhjen me databazën: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}