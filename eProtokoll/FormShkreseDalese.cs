using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace eProtokoll
{
    public partial class FormShkreseDalese : Form
    {
        private int vitiAktiv;
        private int numriFillestar;
        private string dokumentiOrigjinal = string.Empty;
        private bool eshtePergjigje;
        private int vitiPergjigjes;
        private int numriKryesorPergjigjes;
        private int institucioniIdPergjigjes;
        private string klasifikimiPergjigjes = string.Empty;
        private string permbajtjaPergjigjes = string.Empty;
        private string dokumentiPergjigjes = string.Empty;

        
        public FormShkreseDalese()
        {
            InitializeComponent();

            Load += FormShkreseDalese_Load;
            btnZgjidhDokument.Click +=
                btnZgjidhDokument_Click;

            btnRuaj.Click += btnRuaj_Click;
        }

       
        public FormShkreseDalese(
            int viti,
            int numriKryesor,
            int institucioniId,
            string klasifikimi,
            string permbajtja,
            string pathDokumenti)
            : this()
        {
            //constructor chaining
            eshtePergjigje = true;
            vitiPergjigjes = viti;
            numriKryesorPergjigjes = numriKryesor;
            institucioniIdPergjigjes = institucioniId;
            klasifikimiPergjigjes = klasifikimi;
            permbajtjaPergjigjes = permbajtja;
            dokumentiPergjigjes = pathDokumenti;
        }

        private void FormShkreseDalese_Load(
            object? sender,
            EventArgs e)
        {
            NgarkoVitinAktiv();
            NgarkoInstitucionet();
            NgarkoKlasifikimet();



            if (eshtePergjigje)
            {
                PlotesoTeDhenatEPergjigjes();
            }
            else
            {
                ParashikoNumerTeRi();
            }
        }

        private void NgarkoVitinAktiv()
        {
            try
            {
                string query = @"
                    SELECT Viti, NumriFillestar
                    FROM VitetProtokollare
                    WHERE Mbyllur = 0";

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
                        "Ekzistojnë disa vite protokollare të hapura!",
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

                txtViti.Text = vitiAktiv.ToString();
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

        private void NgarkoInstitucionet()
        {
            try
            {
                string query = @"
                    SELECT InstitucioniId, Emri
                    FROM Institucionet
                    ORDER BY Emri";

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
                    SELECT KlasifikimiId, Emri, Niveli
                    FROM Klasifikimet
                    ORDER BY Niveli";

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

        private void ParashikoNumerTeRi()
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
                    WHERE Viti = @viti";

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

                int numriKryesor =
                    Convert.ToInt32(dt.Rows[0][0]);

                txtNumriProtokollit.Text =
                    $"{numriKryesor}/1";
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

        private void PlotesoTeDhenatEPergjigjes()
        {
            if (vitiPergjigjes != vitiAktiv)
            {
                MessageBox.Show(
                    "Shkresa i përket një viti protokollar " +
                    "që nuk është më i hapur!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                btnRuaj.Enabled = false;
                return;
            }

            try
            {
                string query = @"
                    SELECT
                        ISNULL(
                            MAX(NumriKorrespondences),
                            0
                        ) + 1
                    FROM Shkresat
                    WHERE Viti = @viti
                      AND NumriProtokolli =
                          @numriProtokollit";

                SqlParameter[] parameters =
                {
                    new SqlParameter(
                        "@viti",
                        vitiPergjigjes),

                    new SqlParameter(
                        "@numriProtokollit",
                        numriKryesorPergjigjes)
                };

                DataTable dt =
                    DBHelper.ExecuteQuery(
                        query,
                        parameters);

                int korrespondenca =
                    Convert.ToInt32(dt.Rows[0][0]);

                txtViti.Text =
                    vitiPergjigjes.ToString();

                txtNumriProtokollit.Text =
                    $"{numriKryesorPergjigjes}/" +
                    $"{korrespondenca}";

                cmbInstitucioni.SelectedValue =
                    institucioniIdPergjigjes;

                ZgjidhKlasifikimin(
                    klasifikimiPergjigjes);

                txtPermbajtja.Text =
                    permbajtjaPergjigjes;

                dokumentiOrigjinal =
                    dokumentiPergjigjes;

                txtPathDokumenti.Text =
                    dokumentiPergjigjes;

                Text =
                    $"Përgjigje dalëse për " +
                    $"{numriKryesorPergjigjes}/1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gabim gjatë përgatitjes së përgjigjes: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnRuaj.Enabled = false;
            }
        }

        private void ZgjidhKlasifikimin(
            string klasifikimi)
        {
            int niveli = klasifikimi switch
            {
                "Publik" => 1,
                "Kufizuar" => 2,
                "Sekret" => 3,
                _ => 0
            };

            if (niveli == 0)
            {
                cmbKlasifikimi.SelectedIndex = -1;
                return;
            }

            for (int i = 0;
                 i < cmbKlasifikimi.Items.Count;
                 i++)
            {
                if (cmbKlasifikimi.Items[i]
                    is DataRowView row &&
                    Convert.ToInt32(row["Niveli"]) ==
                    niveli)
                {
                    cmbKlasifikimi.SelectedIndex = i;
                    break;
                }
            }
        }

        private void btnZgjidhDokument_Click(
            object? sender,
            EventArgs e)
        {
            using OpenFileDialog dialog =
                new OpenFileDialog();

            dialog.Title =
                "Zgjidh dokumentin e shkresës dalëse";

            dialog.Filter =
                "Dokumente të lejuara|" +
                "*.pdf;*.doc;*.docx;*.jpg;*.jpeg;*.png|" +
                "Dokument PDF|*.pdf|" +
                "Dokument Word|*.doc;*.docx|" +
                "Imazhe|*.jpg;*.jpeg;*.png";

            dialog.Multiselect = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                dokumentiOrigjinal =
                    dialog.FileName;

                txtPathDokumenti.Text =
                    dialog.FileName;
            }
        }

        private void btnRuaj_Click(
            object? sender,
            EventArgs e)
        {
            if (UserSession.PerdoruesiId <= 0 ||
                UserSession.Roli != "Menaxher")
            {
                MessageBox.Show(
                    "Vetëm Menaxheri mund të regjistrojë " +
                    "shkresa të jashtme dalëse!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (cmbInstitucioni.SelectedValue == null)
            {
                MessageBox.Show(
                    "Zgjidhni institucionin marrës!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

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

                return;
            }

            string permbajtja =
                txtPermbajtja.Text.Trim();

            if (string.IsNullOrWhiteSpace(permbajtja))
            {
                MessageBox.Show(
                    "Plotësoni përmbajtjen!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(
                    dokumentiOrigjinal) ||
                !File.Exists(dokumentiOrigjinal))
            {
                MessageBox.Show(
                    "Zgjidhni dokumentin dalës!",
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
            {
                1 => "Publik",
                2 => "Kufizuar",
                3 => "Sekret",
                _ => throw new Exception(
                    "Klasifikimi është i pavlefshëm.")
            };

            DialogResult konfirmim =
                MessageBox.Show(
                    eshtePergjigje
                        ? "Dëshironi ta regjistroni këtë " +
                          "përgjigje dalëse?"
                        : "Dëshironi ta regjistroni këtë " +
                          "shkresë të re dalëse?",
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
                    SELECT Viti, NumriFillestar
                    FROM VitetProtokollare
                        WITH (UPDLOCK, HOLDLOCK)
                    WHERE Mbyllur = 0";

                using SqlCommand cmdViti =
                    new SqlCommand(
                        queryViti,
                        connection,
                        transaction);

                using SqlDataReader reader =
                    cmdViti.ExecuteReader();

                if (!reader.Read())
                {
                    throw new Exception(
                        "Nuk ka vit protokollar të hapur.");
                }

                int viti =
                    Convert.ToInt32(reader["Viti"]);

                int fillimi =
                    Convert.ToInt32(
                        reader["NumriFillestar"]);

                if (reader.Read())
                {
                    throw new Exception(
                        "Ekzistojnë disa vite të hapura.");
                }

                reader.Close();

                int numriKryesor;
                int numriKorrespondences;

                if (eshtePergjigje)
                {
                    if (viti != vitiPergjigjes)
                    {
                        throw new Exception(
                            "Viti i shkresës nuk është aktiv.");
                    }

                    numriKryesor =
                        numriKryesorPergjigjes;

                    string queryKorrespondenca = @"
                        SELECT
                            ISNULL(
                                MAX(NumriKorrespondences),
                                0
                            ) + 1
                        FROM Shkresat
                            WITH (UPDLOCK, HOLDLOCK)
                        WHERE Viti = @viti
                          AND NumriProtokolli =
                              @numriProtokollit";

                    using SqlCommand cmdKorrespondenca =
                        new SqlCommand(
                            queryKorrespondenca,
                            connection,
                            transaction);

                    cmdKorrespondenca.Parameters.AddWithValue(
                        "@viti",
                        viti);

                    cmdKorrespondenca.Parameters.AddWithValue(
                        "@numriProtokollit",
                        numriKryesor);

                    numriKorrespondences =
                        Convert.ToInt32(
                            cmdKorrespondenca.ExecuteScalar());
                }
                else
                {
                    string queryNumri = @"
                        SELECT
                            ISNULL(
                                MAX(NumriProtokolli),
                                @fillimi - 1
                            ) + 1
                        FROM Shkresat
                            WITH (UPDLOCK, HOLDLOCK)
                        WHERE Viti = @viti";

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

                    numriKryesor =
                        Convert.ToInt32(
                            cmdNumri.ExecuteScalar());

                    numriKorrespondences = 1;
                }

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
                        PathDokumenti
                    )
                    VALUES
                    (
                        @numriProtokollit,
                        @numriKorrespondences,
                        @viti,
                        GETDATE(),
                        'Dalese',
                        @klasifikimi,
                        @institucioniId,
                        @perdoruesiId,
                        @permbajtja,
                        @pathDokumenti
                    )";

                using SqlCommand command =
                    new SqlCommand(
                        insertQuery,
                        connection,
                        transaction);

                command.Parameters.AddWithValue(
                    "@numriProtokollit",
                    numriKryesor);

                command.Parameters.AddWithValue(
                    "@numriKorrespondences",
                    numriKorrespondences);

                command.Parameters.AddWithValue(
                    "@viti",
                    viti);

                command.Parameters.AddWithValue(
                    "@klasifikimi",
                    klasifikimi);

                command.Parameters.AddWithValue(
                    "@institucioniId",
                    institucioniId);

                command.Parameters.AddWithValue(
                    "@perdoruesiId",
                    UserSession.PerdoruesiId);

                command.Parameters.AddWithValue(
                    "@permbajtja",
                    permbajtja);

                command.Parameters.AddWithValue(
                    "@pathDokumenti",
                    pathIRuajtur);

                command.ExecuteNonQuery();

                // Kur kjo shkrese dalese eshte pergjigje
                // ndaj nje ceshtjeje ekzistuese, ceshtja
                // mbyllet automatikisht.
                if (eshtePergjigje)
                {
                    string mbyllCeshtjenQuery = @"
                    UPDATE Shkresat
                    SET
                        Mbyllur = 1,
                        DataMbylljes = SYSDATETIME(),
                        MbyllurNga = @mbyllurNga,
                        KomentMbylljeje =
                            @komentMbylljeje
                    WHERE Viti = @viti
                      AND NumriProtokolli =
                          @numriProtokollit
                      AND Mbyllur = 0;

                    UPDATE d
                    SET d.Statusi = N'Përfunduar'
                    FROM Delegimet d

                    INNER JOIN Shkresat s
                        ON d.ShkresaId = s.ShkresaId

                    WHERE s.Viti = @viti
                      AND s.NumriProtokolli =
                          @numriProtokollit;";

                    using SqlCommand cmdMbyll =
                        new SqlCommand(
                            mbyllCeshtjenQuery,
                            connection,
                            transaction);

                    cmdMbyll.Parameters.AddWithValue(
                        "@mbyllurNga",
                        UserSession.PerdoruesiId);

                    cmdMbyll.Parameters.AddWithValue(
                        "@viti",
                        viti);

                    cmdMbyll.Parameters.AddWithValue(
                        "@numriProtokollit",
                        numriKryesor);

                    cmdMbyll.Parameters.Add(
                        "@komentMbylljeje",
                        SqlDbType.NVarChar,
                        1000).Value =
                            "Çështja u mbyll automatikisht " +
                            "pas regjistrimit të përgjigjes dalëse.";

                    cmdMbyll.ExecuteNonQuery();
                }

                transaction.Commit();

                MessageBox.Show(
                    $"Shkresa dalëse u regjistrua me numrin " +
                    $"{numriKryesor}/" +
                    $"{numriKorrespondences}.",
                    "Sukses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

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

        private void btnSkanoDokumentin_Click(
     object sender,
     EventArgs e)
        {
            try
            {
                string? pathISkanimit =
                    ScannerHelper.SkanoDokumentin(this);

                if (string.IsNullOrWhiteSpace(pathISkanimit))
                {
                    return;
                }

                dokumentiOrigjinal = pathISkanimit;
                txtPathDokumenti.Text = pathISkanimit;

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
    }
}