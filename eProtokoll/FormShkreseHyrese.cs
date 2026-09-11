using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace eProtokoll
{
    public partial class FormShkreseHyrese : Form
    {
        private int vitiAktiv;
        private int numriFillestar;

        private string dokumentiOrigjinal =
            string.Empty;

        private bool dokumentiEshteSkanim;

        public FormShkreseHyrese()
        {
            InitializeComponent();

            Load += FormShkreseHyrese_Load;

            btnSkanoDokumentin.Click +=
      btnSkanoDokumentin_Click;

            


        }

       
        private void FormShkreseHyrese_Load(
            object? sender,
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
                    "Vetëm Menaxheri mund të regjistrojë " +
                    "shkresa hyrëse!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                Close();
                return;
            }

            txtViti.ReadOnly = true;
            txtNumriProtokollit.ReadOnly = true;
            txtPathDokumenti.ReadOnly = true;

            NgarkoVitinAktiv();
            NgarkoInstitucionet();
            NgarkoKlasifikimet();
        }

        private void NgarkoVitinAktiv()
        {
            try
            {
                string query = @"
                    SELECT
                        Viti,
                        NumriFillestar
                    FROM VitetProtokollare
                    WHERE Mbyllur = 0;";

                DataTable dt =
                    DBHelper.ExecuteQuery(query);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Nuk ka vit protokollar të hapur!",
                        "Kujdes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    btnRuaj.Enabled = false;
                    return;
                }

                if (dt.Rows.Count > 1)
                {
                    MessageBox.Show(
                        "Ekzistojnë disa vite protokollare " +
                        "të hapura!",
                        "Gabim",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    btnRuaj.Enabled = false;
                    return;
                }

                vitiAktiv =
                    Convert.ToInt32(
                        dt.Rows[0]["Viti"]);

                numriFillestar =
                    Convert.ToInt32(
                        dt.Rows[0]["NumriFillestar"]);

                txtViti.Text =
                    vitiAktiv.ToString();

                ParashikoNumrin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gabim gjatë ngarkimit të vitit: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnRuaj.Enabled = false;
            }
        }

        private void ParashikoNumrin()
        {
            try
            {
                string query = @"
                    SELECT
                        ISNULL(
                            MAX(NumriProtokolli),
                            @fillimi - 1
                        ) + 1
                    FROM Shkresat
                    WHERE Viti = @viti;";

                SqlParameter[] parameters =
                {
                    new SqlParameter(
                        "@fillimi",
                        numriFillestar),

                    new SqlParameter(
                        "@viti",
                        vitiAktiv)
                };

                DataTable dt =
                    DBHelper.ExecuteQuery(
                        query,
                        parameters);

                int numri =
                    Convert.ToInt32(
                        dt.Rows[0][0]);

                txtNumriProtokollit.Text =
                    $"{numri}/1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gabim gjatë gjenerimit të numrit: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnRuaj.Enabled = false;
            }
        }

        private void NgarkoInstitucionet()
        {
            try
            {
                string query = @"
                    SELECT
                        InstitucioniId,
                        Emri
                    FROM Institucionet
                    ORDER BY Emri;";

                DataTable dt =
                    DBHelper.ExecuteQuery(query);

                cmbInstitucioni.DataSource = dt;
                cmbInstitucioni.DisplayMember = "Emri";
                cmbInstitucioni.ValueMember =
                    "InstitucioniId";

                cmbInstitucioni.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gabim gjatë ngarkimit të institucioneve: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void NgarkoKlasifikimet()
        {
            try
            {
                string query = @"
                    SELECT
                        KlasifikimiId,
                        Emri,
                        Niveli
                    FROM Klasifikimet
                    ORDER BY Niveli;";

                DataTable dt =
                    DBHelper.ExecuteQuery(query);

                cmbKlasifikimi.DataSource = dt;
                cmbKlasifikimi.DisplayMember = "Emri";
                cmbKlasifikimi.ValueMember =
                    "KlasifikimiId";

                cmbKlasifikimi.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gabim gjatë ngarkimit të klasifikimeve: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnZgjidhDokument_Click(
            object? sender,
            EventArgs e)
        {
            using OpenFileDialog dialog =
                new OpenFileDialog();

            dialog.Title =
                "Zgjidh dokumentin e shkresës hyrëse";

            dialog.Filter =
                "Dokumente të lejuara|" +
                "*.pdf;*.doc;*.docx;*.jpg;*.jpeg;*.png|" +
                "Dokument PDF|*.pdf|" +
                "Dokument Word|*.doc;*.docx|" +
                "Imazhe|*.jpg;*.jpeg;*.png|" +
                "Të gjithë skedarët|*.*";

            dialog.Multiselect = false;

            if (dialog.ShowDialog() ==
                DialogResult.OK)
            {
                PastroSkaniminEPerkohshem();

                dokumentiOrigjinal =
                    dialog.FileName;

                dokumentiEshteSkanim = false;

                txtPathDokumenti.Text =
                    dialog.FileName;
            }
        }

        private void btnSkanoDokumentin_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                string? pathISkanimit =
                    ScannerHelper.SkanoDokumentin(this);

                if (string.IsNullOrWhiteSpace(
                        pathISkanimit))
                {
                    return;
                }

                PastroSkaniminEPerkohshem();

                dokumentiOrigjinal =
                    pathISkanimit;

                dokumentiEshteSkanim = true;

                txtPathDokumenti.Text =
                    pathISkanimit;

                MessageBox.Show(
                    "Dokumenti u skanua me sukses!",
                    "Sukses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Gabim gjatë skanimit",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnRuaj_Click(
            object? sender,
            EventArgs e)
        {
            if (UserSession.PerdoruesiId <= 0)
            {
                MessageBox.Show(
                    "Sesioni nuk është i vlefshëm!",
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (cmbInstitucioni.SelectedValue == null)
            {
                MessageBox.Show(
                    "Zgjidhni institucionin dërgues!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbInstitucioni.Focus();
                return;
            }

            if (cmbKlasifikimi.SelectedItem
                is not DataRowView klasifikimiRow)
            {
                MessageBox.Show(
                    "Zgjidhni klasifikimin!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbKlasifikimi.Focus();
                return;
            }

            string permbajtja =
                txtPermbajtja.Text.Trim();

            //permbajta e detyrueshme
            if (string.IsNullOrWhiteSpace(permbajtja))
            {
                MessageBox.Show(
                    "Plotësoni përmbajtjen e shkresës!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPermbajtja.Focus();
                return;
            }

            // Dokumenti eshte opsional.
            // Nese eshte zgjedhur, kontrollojme qe ekziston.
            if (!string.IsNullOrWhiteSpace(
                    dokumentiOrigjinal) &&
                !File.Exists(dokumentiOrigjinal))
            {
                MessageBox.Show(
                    "Dokumenti i zgjedhur nuk u gjet!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int institucioniId =
                Convert.ToInt32(
                    cmbInstitucioni.SelectedValue);

            int niveli =
                Convert.ToInt32(
                    klasifikimiRow["Niveli"]);


            string klasifikimi = niveli switch
            {   //Switch expression
                1 => "Publik",
                2 => "Kufizuar",
                3 => "Sekret",

                _ => throw new Exception(
                    "Klasifikimi është i pavlefshëm.")
            };

            DialogResult konfirmim =
                MessageBox.Show(
                    string.IsNullOrWhiteSpace(
                        dokumentiOrigjinal)
                        ? "Shkresa nuk ka dokument të " +
                          "ngarkuar.\nDëshironi ta ruani?"
                        : "Dëshironi ta regjistroni këtë " +
                          "shkresë hyrëse?",
                    "Konfirmo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (konfirmim != DialogResult.Yes)
            {
                return;
            }

            RuajShkresen(
                institucioniId,
                klasifikimi,
                permbajtja);
        }

        private void RuajShkresen(
            int institucioniId,
            string klasifikimi,
            string permbajtja)
        {
            string? pathIRuajtur = null;

            using SqlConnection connection =
                DBHelper.GetConnection();

            connection.Open();

            using SqlTransaction transaction =
                connection.BeginTransaction(
                    IsolationLevel.Serializable);

            try
            {
                string queryViti = @"
                    SELECT
                        Viti,
                        NumriFillestar
                    FROM VitetProtokollare
                        WITH (UPDLOCK, HOLDLOCK)
                    WHERE Mbyllur = 0;";

                using SqlCommand cmdViti =
                    new SqlCommand(
                        queryViti,
                        connection,
                        transaction);

                int viti;
                int fillimi;

                using (SqlDataReader reader =
                       cmdViti.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new Exception(
                            "Nuk ka vit protokollar të hapur.");
                    }

                    viti =
                        Convert.ToInt32(
                            reader["Viti"]);

                    fillimi =
                        Convert.ToInt32(
                            reader["NumriFillestar"]);

                    if (reader.Read())
                    {
                        throw new Exception(
                            "Ekzistojnë disa vite protokollare " +
                            "të hapura.");
                    }
                }

                string queryNumri = @"
                    SELECT
                        ISNULL(
                            MAX(NumriProtokolli),
                            @fillimi - 1
                        ) + 1

                    FROM Shkresat
                        WITH (UPDLOCK, HOLDLOCK)

                    WHERE Viti = @viti;";

                using SqlCommand cmdNumri =
                    new SqlCommand(
                        queryNumri,
                        connection,
                        transaction);

                cmdNumri.Parameters.AddWithValue(
                    "@fillimi",
                    fillimi);

                cmdNumri.Parameters.AddWithValue(
                    "@viti",
                    viti);

                int numriKryesor =
                    Convert.ToInt32(
                        cmdNumri.ExecuteScalar());

                const int numriKorrespondences = 1;

                
                if (!string.IsNullOrWhiteSpace(
                        dokumentiOrigjinal))
                {
                    string folderi =
                        Path.Combine(
                            AppContext.BaseDirectory,
                            "Dokumente",
                            viti.ToString());

                    Directory.CreateDirectory(folderi);

                    string extension =
                        Path.GetExtension(
                            dokumentiOrigjinal);

                    string emriIRuajtur =
                        Guid.NewGuid().ToString("N") +
                        extension.ToLowerInvariant();

                    pathIRuajtur =
                        Path.Combine(
                            folderi,
                            emriIRuajtur);

                    File.Copy(
                        dokumentiOrigjinal,
                        pathIRuajtur,
                        false);
                }

                string insertQuery = @"
                    INSERT INTO Shkresat
                    (
                        NumriProtokolli,
                        NumriKorrespondences,
                        Viti,
                        DataRegjistrimit,
                        LlojiShkreses,
                        Klasifikimi,
                        InstitucioniId,
                        PerdoruesiId,
                        Permbajtja,
                        PathDokumenti,
                        Mbyllur,
                        DataMbylljes,
                        MbyllurNga
                    )
                    VALUES
                    (
                        @numriProtokollit,
                        @numriKorrespondences,
                        @viti,
                        SYSDATETIME(),
                        N'Hyrese',
                        @klasifikimi,
                        @institucioniId,
                        @perdoruesiId,
                        @permbajtja,
                        @pathDokumenti,
                        0,
                        NULL,
                        NULL
                    );";

                using SqlCommand cmdInsert =
                    new SqlCommand(
                        insertQuery,
                        connection,
                        transaction);

                cmdInsert.Parameters.AddWithValue(
                    "@numriProtokollit",
                    numriKryesor);

                cmdInsert.Parameters.AddWithValue(
                    "@numriKorrespondences",
                    numriKorrespondences);

                cmdInsert.Parameters.AddWithValue(
                    "@viti",
                    viti);

                cmdInsert.Parameters.AddWithValue(
                    "@klasifikimi",
                    klasifikimi);

                cmdInsert.Parameters.AddWithValue(
                    "@institucioniId",
                    institucioniId);

                cmdInsert.Parameters.AddWithValue(
                    "@perdoruesiId",
                    UserSession.PerdoruesiId);

                cmdInsert.Parameters.AddWithValue(
                    "@permbajtja",
                    permbajtja);

                SqlParameter pathParameter =
                    new SqlParameter(
                        "@pathDokumenti",
                        SqlDbType.NVarChar,
                        500);

                pathParameter.Value =
                    string.IsNullOrWhiteSpace(pathIRuajtur)
                        ? DBNull.Value
                        : pathIRuajtur;

                cmdInsert.Parameters.Add(
                    pathParameter);

                cmdInsert.ExecuteNonQuery();

                transaction.Commit();

                MessageBox.Show(
                    $"Shkresa hyrëse u regjistrua me " +
                    $"numrin {numriKryesor}/1.",
                    "Sukses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                PastroSkaniminPasRuajtjes();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch
                {
                }

                if (!string.IsNullOrWhiteSpace(pathIRuajtur) &&
                    File.Exists(pathIRuajtur))
                {
                    try
                    {
                        File.Delete(pathIRuajtur);
                    }
                    catch
                    {
                    }
                }

                MessageBox.Show(
                    "Shkresa nuk u regjistrua: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void PastroSkaniminEPerkohshem()
        {
            if (!dokumentiEshteSkanim)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(
                    dokumentiOrigjinal))
            {
                return;
            }

            if (!File.Exists(dokumentiOrigjinal))
            {
                return;
            }

            try
            {
                File.Delete(dokumentiOrigjinal);
            }
            catch
            {
                
            }

            dokumentiOrigjinal =
                string.Empty;

            dokumentiEshteSkanim = false;
        }

        private void PastroSkaniminPasRuajtjes()
        {
            if (!dokumentiEshteSkanim)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(
                    dokumentiOrigjinal) &&
                File.Exists(dokumentiOrigjinal))
            {
                try
                {
                    File.Delete(dokumentiOrigjinal);
                }
                catch
                {
                }
            }

            dokumentiOrigjinal =
                string.Empty;

            dokumentiEshteSkanim = false;
        }

        protected override void OnFormClosed(
            FormClosedEventArgs e)
        {
            PastroSkaniminEPerkohshem();
            base.OnFormClosed(e);
        }
    }
}