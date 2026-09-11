using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace eProtokoll
{
    public partial class FormShkreseBrendshme : Form
    {
        private int vitiAktiv;
        private int numriFillestar;
        private string dokumentiOrigjinal = string.Empty;
        private bool eshtePergjigje;
        private int vitiPergjigjes;
        private int numriKryesorPergjigjes;
        private int marresiPergjigjesId;
        private string klasifikimiPergjigjes = string.Empty;

        public FormShkreseBrendshme()
        {
            InitializeComponent();

            Load += FormShkreseBrendshme_Load;

            cmbKlasifikimi.SelectedIndexChanged +=
                cmbKlasifikimi_SelectedIndexChanged;

            btnZgjidhDokument.Click +=
                btnZgjidhDokument_Click;

            btnRuaj.Click +=
                btnRuaj_Click;
        }

        
        public FormShkreseBrendshme(
            int viti,
            int numriKryesor,
            int marresiId,
            string klasifikimi)
            : this()
        {
            eshtePergjigje = true;
            vitiPergjigjes = viti;
            numriKryesorPergjigjes = numriKryesor;
            marresiPergjigjesId = marresiId;
            klasifikimiPergjigjes = klasifikimi;
        }

        private void FormShkreseBrendshme_Load(
            object? sender,
            EventArgs e)
        {
            dtpAfati.MinDate = DateTime.Today;
            dtpAfati.Value = DateTime.Today;
            dtpAfati.Checked = false;

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

            if (UserSession.Roli != "Menaxher" &&
                UserSession.Roli != "Punonjes")
            {
                MessageBox.Show(
                    "Vetëm Menaxheri dhe Punonjësit mund " +
                    "të krijojnë shkresa të brendshme!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                Close();
                return;
            }

            lblPunonjesit.Visible = true;
            clbPunonjesit.Visible = true;

            NgarkoKlasifikimet();

            if (eshtePergjigje)
            {
                PergatitFormenPerPergjigje();
                NgarkoAfatinECeshtjes();
            }
            else
            {
                Text =
                    $"Shkresë e brendshme - " +
                    $"Dërgues: {UserSession.EmriPlote}";

                lblPunonjesit.Text = "Marrësi/it:";
                NgarkoVitinAktiv();
            }
        }

        private void NgarkoAfatinECeshtjes()
        {
            try
            {
                string query = @"
                    SELECT TOP 1 d.Afati
                    FROM Delegimet d
                    INNER JOIN Shkresat s
                        ON d.ShkresaId = s.ShkresaId
                    WHERE s.Viti = @viti
                      AND s.NumriProtokolli = @numriProtokolli
                      AND d.Afati IS NOT NULL
                    ORDER BY
                        s.NumriKorrespondences DESC,
                        d.DelegimiId DESC;";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@viti", vitiPergjigjes),
                    new SqlParameter(
                        "@numriProtokolli",
                        numriKryesorPergjigjes)
                };

                DataTable dt =
                    DBHelper.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0 &&
                    dt.Rows[0]["Afati"] != DBNull.Value)
                {
                    DateTime afati = Convert.ToDateTime(
                        dt.Rows[0]["Afati"]);

                    dtpAfati.MinDate =
                        DateTimePicker.MinimumDateTime;
                    dtpAfati.Value = afati;
                    dtpAfati.Checked = true;
                    dtpAfati.Enabled = false;
                }
                else
                {
                    dtpAfati.Checked = false;
                    dtpAfati.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Afati i çështjes nuk u ngarkua: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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

                txtViti.Text =
                    vitiAktiv.ToString();

                txtViti.ReadOnly = true;
                txtNumriProtokollit.ReadOnly = true;

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

                btnRuaj.Enabled = false;
            }
        }

        private void PergatitFormenPerPergjigje()
        {
            try
            {
                string query = @"
                    SELECT
                        COUNT(*) AS NumriShkresave,

                        ISNULL(
                            MAX(NumriKorrespondences),
                            0
                        ) + 1 AS NumriIRadhes,

                        ISNULL(
                            MAX(
                                CASE
                                    WHEN Mbyllur = 1
                                        THEN 1
                                    ELSE 0
                                END
                            ),
                            0
                        ) AS EshteMbyllur

                    FROM Shkresat

                    WHERE Viti = @viti
                      AND NumriProtokolli =
                          @numriProtokolli;";

                SqlParameter[] parameters =
                {
                    new SqlParameter(
                        "@viti",
                        vitiPergjigjes),

                    new SqlParameter(
                        "@numriProtokolli",
                        numriKryesorPergjigjes)
                };

                DataTable dt =
                    DBHelper.ExecuteQuery(
                        query,
                        parameters);

                int numriShkresave =
                    Convert.ToInt32(
                        dt.Rows[0]["NumriShkresave"]);

                int numriIRadhes =
                    Convert.ToInt32(
                        dt.Rows[0]["NumriIRadhes"]);

                bool eshteMbyllur =
                    Convert.ToInt32(
                        dt.Rows[0]["EshteMbyllur"]) == 1;

                if (numriShkresave == 0)
                {
                    MessageBox.Show(
                        "Çështja që kërkoni nuk u gjet!",
                        "Gabim",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    btnRuaj.Enabled = false;
                    return;
                }

                if (eshteMbyllur)
                {
                    MessageBox.Show(
                        "Kjo çështje është mbyllur. " +
                        "Nuk mund të shtohet përgjigje e re!",
                        "Kujdes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    btnRuaj.Enabled = false;
                    return;
                }

                if (!VitiPergjigjesEshteHapur())
                {
                    MessageBox.Show(
                        "Viti protokollar i kësaj çështjeje " +
                        "është mbyllur!",
                        "Kujdes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    btnRuaj.Enabled = false;
                    return;
                }

                Text =
                    $"Përgjigje e brendshme - " +
                    $"{numriKryesorPergjigjes}/" +
                    $"{numriIRadhes}";

                txtViti.Text =
                    vitiPergjigjes.ToString();

                txtNumriProtokollit.Text =
                    $"{numriKryesorPergjigjes}/" +
                    $"{numriIRadhes}";

                txtViti.ReadOnly = true;
                txtNumriProtokollit.ReadOnly = true;

                ZgjidhKlasifikiminEPergjigjes();
                NgarkoVetemMarresinEPergjigjes();

                cmbKlasifikimi.Enabled = false;
                clbPunonjesit.Enabled = false;

                lblPunonjesit.Enabled = true;
                lblPunonjesit.Text =
                    "Marrësi i përgjigjes:";

                txtPermbajtja.Text =
                    $"Përgjigje për shkresën " +
                    $"{numriKryesorPergjigjes}/" +
                    $"{numriIRadhes - 1}:\r\n";

                txtPermbajtja.SelectionStart =
                    txtPermbajtja.Text.Length;

                txtPermbajtja.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Përgjigjja nuk mund të përgatitet: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnRuaj.Enabled = false;
            }
        }

        private bool VitiPergjigjesEshteHapur()
        {
            string query = @"
                SELECT COUNT(*)
                FROM VitetProtokollare
                WHERE Viti = @viti
                  AND Mbyllur = 0;";

            SqlParameter[] parameters =
            {
                new SqlParameter(
                    "@viti",
                    vitiPergjigjes)
            };

            DataTable dt =
                DBHelper.ExecuteQuery(
                    query,
                    parameters);

            return Convert.ToInt32(
                dt.Rows[0][0]) > 0;
        }

        private void ZgjidhKlasifikiminEPergjigjes()
        {
            if (cmbKlasifikimi.DataSource
                is not DataTable dt)
            {
                return;
            }

            int niveli =
                klasifikimiPergjigjes switch
                {
                    "Publik" => 1,
                    "Kufizuar" => 2,
                    "Sekret" => 3,
                    _ => 1
                };

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(
                        dt.Rows[i]["Niveli"]) == niveli)
                {
                    cmbKlasifikimi.SelectedIndex = i;
                    return;
                }
            }

            throw new Exception(
                "Klasifikimi i shkresës nuk u gjet.");
        }

        private void NgarkoVetemMarresinEPergjigjes()
        {
            string query = @"
                SELECT
                    p.PerdoruesiId,

                    p.Emri + ' ' + p.Mbiemri
                    + ' - ' + r.EmriRolit
                        AS EmriPlote

                FROM Perdoruesit p

                INNER JOIN Rolet r
                    ON p.RolId = r.RolId

                WHERE p.PerdoruesiId =
                      @perdoruesiId

                  AND p.Aktiv = 1;";

            SqlParameter[] parameters =
            {
                new SqlParameter(
                    "@perdoruesiId",
                    marresiPergjigjesId)
            };

            DataTable dt =
                DBHelper.ExecuteQuery(
                    query,
                    parameters);

            if (dt.Rows.Count == 0)
            {
                throw new Exception(
                    "Marrësi i përgjigjes nuk ekziston " +
                    "ose nuk është aktiv.");
            }

            clbPunonjesit.DataSource = null;
            clbPunonjesit.Items.Clear();

            clbPunonjesit.DataSource = dt;
            clbPunonjesit.DisplayMember =
                "EmriPlote";

            clbPunonjesit.ValueMember =
                "PerdoruesiId";

            clbPunonjesit.SetItemChecked(
                0,
                true);
        }

        private void NgarkoMarresit(
            bool vetemMenaxher)
        {
            try
            {
                string query;

                if (vetemMenaxher)
                {
                    query = @"
                        SELECT
                            p.PerdoruesiId,

                            p.Emri + ' ' + p.Mbiemri
                            + ' - ' + r.EmriRolit
                                AS EmriPlote

                        FROM Perdoruesit p

                        INNER JOIN Rolet r
                            ON p.RolId = r.RolId

                        WHERE r.EmriRolit = N'Menaxher'
                          AND p.Aktiv = 1
                          

                        ORDER BY
                            p.Emri,
                            p.Mbiemri;";
                }
                else
                {
                    query = @"
                        SELECT
                            p.PerdoruesiId,

                            p.Emri + ' ' + p.Mbiemri
                            + ' - ' + r.EmriRolit
                                AS EmriPlote

                        FROM Perdoruesit p

                        INNER JOIN Rolet r
                            ON p.RolId = r.RolId

                        WHERE r.EmriRolit IN
                              (N'Menaxher', N'Punonjes')

                          AND p.Aktiv = 1

                          AND p.PerdoruesiId <>
                              @perdoruesiId

                        ORDER BY
                            p.Emri,
                            p.Mbiemri;";
                }

                SqlParameter[] parameters =
                {
                    new SqlParameter(
                        "@perdoruesiId",
                        UserSession.PerdoruesiId)
                };

                DataTable dt =
                    DBHelper.ExecuteQuery(
                        query,
                        parameters);

                clbPunonjesit.DataSource = null;
                clbPunonjesit.Items.Clear();

                clbPunonjesit.DataSource = dt;
                clbPunonjesit.DisplayMember =
                    "EmriPlote";

                clbPunonjesit.ValueMember =
                    "PerdoruesiId";

                if (vetemMenaxher &&
                    clbPunonjesit.Items.Count == 1)
                {
                    clbPunonjesit.SetItemChecked(
                        0,
                        true);
                }

                clbPunonjesit.ClearSelected();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gabim gjatë ngarkimit të marrësve: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void cmbKlasifikimi_SelectedIndexChanged(
            object? sender,
            EventArgs e)
        {
            // Kur eshte pergjigje, marresi dhe
            // klasifikimi jane percaktuar automatikisht
            if (eshtePergjigje)
            {
                return;
            }

            if (cmbKlasifikimi.SelectedItem
                is not DataRowView row)
            {
                return;
            }

            int niveli =
                Convert.ToInt32(
                    row["Niveli"]);

            if (niveli == 1)
            {
                lblPunonjesit.Text =
                    "Përgjegjësi (opsional):";

                lblPunonjesit.Enabled = true;
                clbPunonjesit.Enabled = true;

                NgarkoMarresit(false);
            }
            else if (niveli == 2)
            {
                lblPunonjesit.Text =
                    "Marrësi/it:";

                lblPunonjesit.Enabled = true;
                clbPunonjesit.Enabled = true;

                NgarkoMarresit(false);
            }
            else if (niveli == 3)
            {
                lblPunonjesit.Text =
                    "Menaxheri marrës:";

                lblPunonjesit.Enabled = true;
                clbPunonjesit.Enabled = true;

                NgarkoMarresit(true);
            }
        }

        private void btnZgjidhDokument_Click(
            object? sender,
            EventArgs e)
        {
            using OpenFileDialog dialog =
                new OpenFileDialog();

            dialog.Title =
                eshtePergjigje
                    ? "Zgjidh dokumentin e përgjigjes"
                    : "Zgjidh dokumentin e brendshëm";

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
            if (UserSession.PerdoruesiId <= 0)
            {
                MessageBox.Show(
                    "Sesioni nuk është i vlefshëm!",
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

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

            int niveli =
                Convert.ToInt32(
                    klasifikimiRow["Niveli"]);

            
            if ((eshtePergjigje || niveli != 1) &&
                clbPunonjesit.CheckedItems.Count == 0)
            {
                MessageBox.Show(
                    "Zgjidhni të paktën një marrës!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            
            if (!eshtePergjigje &&
                niveli == 1 &&
                clbPunonjesit.CheckedItems.Count > 1)
            {
                MessageBox.Show(
                    "Për shkresën publike mund të zgjidhni vetëm një person përgjegjës!",
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

                txtPermbajtja.Focus();
                return;
            }

            if (dtpAfati.Checked &&
                dtpAfati.Value.Date < DateTime.Today)
            {
                MessageBox.Show(
                    "Afati nuk mund të jetë një datë e kaluar!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                dtpAfati.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(dokumentiOrigjinal) &&
                !File.Exists(dokumentiOrigjinal))
            {
                MessageBox.Show(
                    "Dokumenti i zgjedhur nuk u gjet. Zgjidheni përsëri ose vazhdoni pa dokument.",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string klasifikimi = niveli switch
            {
                1 => "Publik",
                2 => "Kufizuar",
                3 => "Sekret",

                _ => throw new Exception(
                    "Klasifikimi është i pavlefshëm.")
            };

            string pyetja;

            if (eshtePergjigje)
            {
                pyetja =
                    "Dëshironi t'ia dërgoni këtë " +
                    "përgjigje dërguesit?";
            }
            else if (niveli == 1)
            {
                pyetja =
                    "Dëshironi ta dërgoni këtë shkresë " +
                    "publike për të gjithë?";
            }
            else
            {
                pyetja =
                    "Dëshironi ta dërgoni këtë shkresë " +
                    "te marrësit e zgjedhur?";
            }

            DialogResult konfirmim =
                MessageBox.Show(
                    pyetja,
                    "Konfirmo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (konfirmim != DialogResult.Yes)
            {
                return;
            }

            RuajShkresen(
                niveli,
                klasifikimi,
                permbajtja);
        }

        private void RuajShkresen(
            int niveli,
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
                int viti;
                int numriKryesor;
                int numriKorrespondences;

                if (eshtePergjigje)
                {
                    viti = vitiPergjigjes;
                    numriKryesor =
                        numriKryesorPergjigjes;

                    string queryCeshtja = @"
                        SELECT
                            COUNT(*) AS NumriShkresave,

                            ISNULL(
                                MAX(
                                    CASE
                                        WHEN Mbyllur = 1
                                            THEN 1
                                        ELSE 0
                                    END
                                ),
                                0
                            ) AS EshteMbyllur,

                            ISNULL(
                                MAX(NumriKorrespondences),
                                0
                            ) + 1 AS NumriIRadhes

                        FROM Shkresat
                            WITH (UPDLOCK, HOLDLOCK)

                        WHERE Viti = @viti
                          AND NumriProtokolli =
                              @numriProtokolli;";

                    using SqlCommand cmdCeshtja =
                        new SqlCommand(
                            queryCeshtja,
                            connection,
                            transaction);

                    cmdCeshtja.Parameters.AddWithValue(
                        "@viti",
                        viti);

                    cmdCeshtja.Parameters.AddWithValue(
                        "@numriProtokolli",
                        numriKryesor);

                    using SqlDataReader reader =
                        cmdCeshtja.ExecuteReader();

                    reader.Read();

                    int numriShkresave =
                        Convert.ToInt32(
                            reader["NumriShkresave"]);

                    bool mbyllur =
                        Convert.ToInt32(
                            reader["EshteMbyllur"]) == 1;

                    numriKorrespondences =
                        Convert.ToInt32(
                            reader["NumriIRadhes"]);

                    reader.Close();

                    if (numriShkresave == 0)
                    {
                        throw new Exception(
                            "Çështja nuk ekziston.");
                    }

                    if (mbyllur)
                    {
                        throw new Exception(
                            "Çështja është mbyllur.");
                    }

                    string kontrollViti = @"
                        SELECT COUNT(*)
                        FROM VitetProtokollare
                        WHERE Viti = @viti
                          AND Mbyllur = 0;";

                    using SqlCommand cmdKontrollViti =
                        new SqlCommand(
                            kontrollViti,
                            connection,
                            transaction);

                    cmdKontrollViti.Parameters.AddWithValue(
                        "@viti",
                        viti);

                    int vitiHapur =
                        Convert.ToInt32(
                            cmdKontrollViti.ExecuteScalar());

                    if (vitiHapur == 0)
                    {
                        throw new Exception(
                            "Viti protokollar është mbyllur.");
                    }
                }
                else
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
                                "Ekzistojnë disa vite të hapura.");
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

                    numriKryesor =
                        Convert.ToInt32(
                            cmdNumri.ExecuteScalar());

                    numriKorrespondences = 1;
                }

                if (!string.IsNullOrWhiteSpace(dokumentiOrigjinal))
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

                string insertShkresa = @"
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
                    OUTPUT INSERTED.ShkresaId
                    VALUES
                    (
                        @numriProtokollit,
                        @numriKorrespondences,
                        @viti,
                        SYSDATETIME(),
                        N'e Brendshme',
                        @klasifikimi,
                        NULL,
                        @perdoruesiId,
                        @permbajtja,
                        @pathDokumenti,
                        0,
                        NULL,
                        NULL
                    );";

                using SqlCommand cmdShkresa =
                    new SqlCommand(
                        insertShkresa,
                        connection,
                        transaction);

                cmdShkresa.Parameters.AddWithValue(
                    "@numriProtokollit",
                    numriKryesor);

                cmdShkresa.Parameters.AddWithValue(
                    "@numriKorrespondences",
                    numriKorrespondences);

                cmdShkresa.Parameters.AddWithValue(
                    "@viti",
                    viti);

                cmdShkresa.Parameters.AddWithValue(
                    "@klasifikimi",
                    klasifikimi);

                cmdShkresa.Parameters.AddWithValue(
                    "@perdoruesiId",
                    UserSession.PerdoruesiId);

                cmdShkresa.Parameters.AddWithValue(
                    "@permbajtja",
                    permbajtja);

                cmdShkresa.Parameters.Add(
                    "@pathDokumenti",
                    SqlDbType.NVarChar,
                    1000).Value =
                        string.IsNullOrWhiteSpace(pathIRuajtur)
                            ? DBNull.Value
                            : pathIRuajtur;

                int shkresaId =
                    Convert.ToInt32(
                        cmdShkresa.ExecuteScalar());

                
                if (clbPunonjesit.CheckedItems.Count > 0)
                {
                    RuajMarresitDheDetyrat(
                        shkresaId,
                        connection,
                        transaction);
                }

                transaction.Commit();

                string mesazhi;

                if (eshtePergjigje)
                {
                    mesazhi =
                        $"Përgjigjja u dërgua me numrin " +
                        $"{numriKryesor}/" +
                        $"{numriKorrespondences}.";
                }
                else if (niveli == 1)
                {
                    mesazhi =
                        $"Shkresa publike u regjistrua me " +
                        $"numrin {numriKryesor}/1 dhe është " +
                        $"e dukshme për të gjithë.";
                }
                else
                {
                    mesazhi =
                        $"Shkresa e brendshme u dërgua me " +
                        $"numrin {numriKryesor}/1.";
                }

                MessageBox.Show(
                    mesazhi,
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
                    "Shkresa nuk u dërgua: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void RuajMarresitDheDetyrat(
            int shkresaId,
            SqlConnection connection,
            SqlTransaction transaction)
        {
            string insertMarresi = @"
                INSERT INTO ShkresaPunonjesit
                (
                    ShkresaId,
                    PerdoruesiId
                )
                VALUES
                (
                    @shkresaId,
                    @perdoruesiId
                );";

            string insertDetyra = @"
                INSERT INTO Delegimet
                (
                    ShkresaId,
                    PunonjesiId,
                    DeleguarNga,
                    Afati,
                    Shenim,
                    Statusi,
                    Pergjigja,
                    DataPergjigjes,
                    DataDelegimit,
                    PathDokumentPergjigje
                )
                VALUES
                (
                    @shkresaId,
                    @punonjesiId,
                    @deleguarNga,
                    @afati,
                    @shenim,
                    N'Në pritje',
                    NULL,
                    NULL,
                    SYSDATETIME(),
                    NULL
                );";

            foreach (object item
                     in clbPunonjesit.CheckedItems)
            {
                if (item is not DataRowView row)
                {
                    continue;
                }

                int marresiId =
                    Convert.ToInt32(
                        row["PerdoruesiId"]);

                using (SqlCommand cmdMarresi =
                       new SqlCommand(
                           insertMarresi,
                           connection,
                           transaction))
                {
                    cmdMarresi.Parameters.AddWithValue(
                        "@shkresaId",
                        shkresaId);

                    cmdMarresi.Parameters.AddWithValue(
                        "@perdoruesiId",
                        marresiId);

                    cmdMarresi.ExecuteNonQuery();
                }

                using (SqlCommand cmdDetyra =
                       new SqlCommand(
                           insertDetyra,
                           connection,
                           transaction))
                {
                    cmdDetyra.Parameters.AddWithValue(
                        "@shkresaId",
                        shkresaId);

                    cmdDetyra.Parameters.AddWithValue(
                        "@punonjesiId",
                        marresiId);

                    cmdDetyra.Parameters.AddWithValue(
                        "@deleguarNga",
                        UserSession.PerdoruesiId);

                    cmdDetyra.Parameters.Add(
                        "@afati",
                        SqlDbType.Date).Value =
                            dtpAfati.Checked
                                ? dtpAfati.Value.Date
                                : DBNull.Value;

                    cmdDetyra.Parameters.AddWithValue(
                        "@shenim",
                        eshtePergjigje
                            ? $"Përgjigje e brendshme nga " +
                              $"{UserSession.EmriPlote}"
                            : $"Shkresë e brendshme nga " +
                              $"{UserSession.EmriPlote}");

                    cmdDetyra.ExecuteNonQuery();
                }
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
