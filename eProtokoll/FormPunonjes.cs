using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace eProtokoll
{
    public partial class FormPunonjes : Form
    {
        private string dokumentiPergjigjesOrigjinal =
            string.Empty;

        public FormPunonjes()
        {
            InitializeComponent();

            Load += FormPunonjes_Load;

            dgvDetyrat.SelectionChanged +=
                dgvDetyrat_SelectionChanged;

            btnRifresko.Click +=
                btnRifresko_Click;

            btnHapDokumentin.Click +=
                btnHapDokumentin_Click;

            btnFilloTrajtimin.Click +=
                btnFilloTrajtimin_Click;

            btnZgjidhDokumentPergjigje.Click +=
                btnZgjidhDokumentPergjigje_Click;

            btnPerfundo.Click +=
                btnPerfundo_Click;

            btnShkreseBrendshme.Click +=
                btnShkreseBrendshme_Click;

            btnMbyllCeshtjen.Click +=
                btnMbyllCeshtjen_Click;
        }

        private void FormPunonjes_Load(
            object? sender,
            EventArgs e)
        {
            lblPerdoruesi.Text =
                $"Mirë se vini, {UserSession.EmriPlote}";

            NgarkoShkresat();
        }

        private void NgarkoShkresat()
        {
            if (UserSession.PerdoruesiId <= 0)
            {
                MessageBox.Show(
                    "Sesioni i përdoruesit nuk është i vlefshëm!",
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            try
            {
                string query = @"
                    SELECT
                        d.DelegimiId,
                        s.ShkresaId,
                        s.PerdoruesiId AS KrijuesiId,
                        s.Viti,
                        s.NumriProtokolli,
                        s.NumriKorrespondences,
                        s.PathDokumenti,
                        d.PathDokumentPergjigje,
                        s.Mbyllur,

                        CAST(s.NumriProtokolli AS NVARCHAR(20))
                            + '/' +
                        CAST(
                            s.NumriKorrespondences
                            AS NVARCHAR(20)
                        ) AS Shkresa,

                        CONVERT(
                            NVARCHAR(10),
                            s.DataRegjistrimit,
                            104
                        ) AS [Data e shkresës],

                        s.LlojiShkreses AS Lloji,

                        CASE
                            WHEN s.LlojiShkreses =
                                 N'e Brendshme'
                                THEN CONCAT(
                                    ISNULL(derguesi.Emri, ''),
                                    ' ',
                                    ISNULL(derguesi.Mbiemri, ''),
                                    N' - Institucioni ynë'
                                )

                            ELSE ISNULL(i.Emri, '')
                        END AS [Dërguesi/Burimi],

                        s.Klasifikimi,

                        CASE
                            WHEN s.LlojiShkreses = N'e Brendshme'
                                THEN ISNULL(
                                    mesazhiPare.Permbajtja,
                                    N''
                                )

                            ELSE ISNULL(
                                s.Permbajtja,
                                N''
                            )
                        END AS [Përmbajtja],

                        CASE
                            WHEN d.DelegimiId IS NULL
                                THEN ''
                            ELSE
                                CONVERT(
                                    NVARCHAR(10),
                                    d.DataDelegimit,
                                    104
                                )
                                + ' ' +
                                CONVERT(
                                    NVARCHAR(5),
                                    d.DataDelegimit,
                                    108
                                )
                        END AS [Data e delegimit],

                        CASE
                            WHEN d.DelegimiId IS NULL
                                THEN ''

                            WHEN afatiCeshtjes.Afati IS NULL
                                THEN ''

                            ELSE
                                CONVERT(
                                    NVARCHAR(10),
                                    afatiCeshtjes.Afati,
                                    104
                                )
                        END AS Afati,

                        CASE
                            WHEN d.DelegimiId IS NULL
                                THEN N'Vetëm për lexim'

                            WHEN d.Statusi = N'Përfunduar'
                                THEN N'Përfunduar'

                            WHEN d.Statusi = N'Mbyllur'
                                THEN N'Mbyllur'

                            WHEN afatiCeshtjes.Afati IS NULL
                                THEN N'Pa afat'

                            WHEN afatiCeshtjes.Afati <
                                 CAST(GETDATE() AS DATE)
                                THEN N'Me vonesë'

                            WHEN afatiCeshtjes.Afati =
                                 CAST(GETDATE() AS DATE)
                                THEN N'Afati sot'

                            ELSE N'Në afat'
                        END AS [Gjendja e afatit],

                        CASE
                            WHEN d.DelegimiId IS NULL
                                THEN ''
                            ELSE ISNULL(d.Shenim, '')
                        END AS [Udhëzimi],

                        CASE
                            WHEN d.DelegimiId IS NULL
                                THEN N'Vetëm për lexim'
                            ELSE d.Statusi
                        END AS Statusi,

                      CASE
                        WHEN s.LlojiShkreses = N'e Brendshme'
                             AND s.NumriKorrespondences > 1
                            THEN ISNULL(
                                mesazhiFundit.Permbajtja,
                                N''
                            )

                        WHEN s.LlojiShkreses <> N'e Brendshme'
                            THEN ISNULL(
                                d.Pergjigja,
                                N''
                            )

                        ELSE N''
                    END AS [Përgjigjja],

                    CASE
                        WHEN s.LlojiShkreses = N'e Brendshme'
                             AND s.NumriKorrespondences > 1
                             AND mesazhiFundit.DataRegjistrimit IS NOT NULL
                            THEN
                                CONVERT(
                                    NVARCHAR(10),
                                    mesazhiFundit.DataRegjistrimit,
                                    104
                                )
                                + N' ' +
                                CONVERT(
                                    NVARCHAR(5),
                                    mesazhiFundit.DataRegjistrimit,
                                    108
                                )

                        WHEN s.LlojiShkreses <> N'e Brendshme'
                             AND d.DataPergjigjes IS NOT NULL
                            THEN
                                CONVERT(
                                    NVARCHAR(10),
                                    d.DataPergjigjes,
                                    104
                                )
                                + N' ' +
                                CONVERT(
                                    NVARCHAR(5),
                                    d.DataPergjigjes,
                                    108
                                )

                        ELSE N''
                    END AS [Data e përgjigjes]



                    FROM Shkresat s

                    LEFT JOIN Institucionet i
                        ON s.InstitucioniId =
                           i.InstitucioniId

                    LEFT JOIN Perdoruesit derguesi
                        ON s.PerdoruesiId =
                           derguesi.PerdoruesiId

                    OUTER APPLY
                    (
                        SELECT TOP 1
                            de.DelegimiId,
                            de.DataDelegimit,
                            de.Afati,
                            de.Shenim,
                            de.Statusi,
                            de.Pergjigja,
                            de.DataPergjigjes,
                            de.PathDokumentPergjigje

                        FROM Delegimet de

                        WHERE de.ShkresaId =
                              s.ShkresaId

                          AND
                          (
                              de.PunonjesiId = @punonjesiId
                              OR s.PerdoruesiId = @punonjesiId
                          )

                        ORDER BY
                            de.DelegimiId DESC
                    ) d

                    OUTER APPLY
                    (
                        SELECT TOP 1
                            sf.Permbajtja,
                            sf.DataRegjistrimit

                        FROM Shkresat sf

                        WHERE sf.Viti = s.Viti
                          AND sf.NumriProtokolli =
                              s.NumriProtokolli

                        ORDER BY
                            sf.NumriKorrespondences DESC,
                            sf.DataRegjistrimit DESC,
                            sf.ShkresaId DESC
                    ) mesazhiFundit

                    OUTER APPLY
                    (
                        SELECT TOP 1
                            spare.Permbajtja,
                            spare.DataRegjistrimit

                        FROM Shkresat spare

                        WHERE spare.Viti = s.Viti
                          AND spare.NumriProtokolli =
                              s.NumriProtokolli

                        ORDER BY
                            spare.NumriKorrespondences ASC,
                            spare.DataRegjistrimit ASC,
                            spare.ShkresaId ASC
                    ) mesazhiPare

                    OUTER APPLY
                    (
                        SELECT TOP 1
                            da.Afati

                        FROM Delegimet da

                        INNER JOIN Shkresat sa
                            ON da.ShkresaId = sa.ShkresaId

                        WHERE sa.Viti = s.Viti
                          AND sa.NumriProtokolli =
                              s.NumriProtokolli
                          AND da.Afati IS NOT NULL

                        ORDER BY
                            sa.NumriKorrespondences DESC,
                            da.DelegimiId DESC
                    ) afatiCeshtjes

                    WHERE s.Mbyllur = 0

                      -- Në panel shfaqet vetëm mesazhi më i ri
                      -- i çdo çështjeje. Të gjithë mesazhet e
                      -- tjerë mbeten në databazë dhe historik.
                      AND s.NumriKorrespondences =
                      (
                          SELECT MAX(sfundi.NumriKorrespondences)
                          FROM Shkresat sfundi
                          WHERE sfundi.Viti = s.Viti
                            AND sfundi.NumriProtokolli =
                                s.NumriProtokolli
                            AND sfundi.Mbyllur = 0
                      )

                     AND
                        (
                            -- Shkresat jo sekrete kalojnë normalisht.
                            s.Klasifikimi <> N'Sekret'

                            -- Krijuesi e shikon gjithmonë shkresën e vet sekrete.
                            OR s.PerdoruesiId = @punonjesiId

                            -- Marrësi i zgjedhur e shikon shkresën sekrete.
                            OR EXISTS
                            (
                                SELECT 1
                                FROM ShkresaPunonjesit sekretSp

                                WHERE sekretSp.ShkresaId = s.ShkresaId
                                  AND sekretSp.PerdoruesiId = @punonjesiId
                            )
                        )

                      AND
                      (
                          -- Publik: shihet nga të gjithë.
                          s.Klasifikimi = N'Publik'

                          -- Shkresa është krijuar nga përdoruesi aktual.
                          OR s.PerdoruesiId = @punonjesiId

                          -- Shkresë e deleguar.
                          OR d.DelegimiId IS NOT NULL

                          -- Shkresë e brendshme e dërguar
                          -- drejtpërdrejt te përdoruesi.
                          OR EXISTS
                          (
                              SELECT 1
                              FROM ShkresaPunonjesit sp

                              WHERE sp.ShkresaId =
                                    s.ShkresaId

                                AND sp.PerdoruesiId =
                                    @punonjesiId
                          )
                      )

                    ORDER BY
                        CASE
                            WHEN d.DelegimiId IS NULL
                                THEN 1

                            WHEN d.Statusi = N'Përfunduar'
                                THEN 2

                            ELSE 0
                        END,

                        d.Afati,

                        s.Viti DESC,
                        s.NumriProtokolli DESC,
                        s.NumriKorrespondences DESC;";

                SqlParameter[] parameters =
                {
                    new SqlParameter(
                        "@punonjesiId",
                        UserSession.PerdoruesiId)
                };

                DataTable dt =
                    DBHelper.ExecuteQuery(
                        query,
                        parameters);

                dgvDetyrat.DataSource = dt;

                FshihKolonatTeknike();
                NgjyrosRreshtat();
                PastroPerzgjedhjen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gabim gjatë ngarkimit të shkresave: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FshihKolonatTeknike()
        {
            string[] kolonat =
            {
                "DelegimiId",
                "ShkresaId",
                "KrijuesiId",
                "Viti",
                "NumriProtokolli",
                "NumriKorrespondences",
                "PathDokumenti",
                "PathDokumentPergjigje",
                "Mbyllur"
            };

            foreach (string kolona in kolonat)
            {
                if (dgvDetyrat.Columns.Contains(kolona))
                {
                    dgvDetyrat
                        .Columns[kolona]
                        .Visible = false;
                }
            }
        }

        private void NgjyrosRreshtat()
        {
            foreach (DataGridViewRow row
                     in dgvDetyrat.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                string gjendja =
                    Convert.ToString(
                        row.Cells[
                            "Gjendja e afatit"].Value)
                    ?? string.Empty;

                if (gjendja == "Me vonesë")
                {
                    row.DefaultCellStyle.BackColor =
                        System.Drawing.Color.MistyRose;
                }
                else if (gjendja == "Afati sot")
                {
                    row.DefaultCellStyle.BackColor =
                        System.Drawing.Color.LightYellow;
                }
                else if (gjendja == "Përfunduar")
                {
                    row.DefaultCellStyle.BackColor =
                        System.Drawing.Color.Honeydew;
                }
                else if (gjendja == "Vetëm për lexim")
                {
                    row.DefaultCellStyle.BackColor =
                        System.Drawing.Color.AliceBlue;
                }
            }
        }

        private void PastroPerzgjedhjen()
        {
            dgvDetyrat.ClearSelection();
            dgvDetyrat.CurrentCell = null;

            dokumentiPergjigjesOrigjinal =
                string.Empty;

            txtPergjigja.Clear();
            txtPergjigja.Enabled = false;
            txtPergjigja.ReadOnly = true;

            txtPathPergjigje.Clear();
            txtPathPergjigje.Enabled = false;

            btnHapDokumentin.Enabled = false;
            btnFilloTrajtimin.Enabled = false;
            btnZgjidhDokumentPergjigje.Enabled = false;
            btnSkanoDokumentPergjigje.Enabled = false;
            btnPerfundo.Enabled = false;
            btnMbyllCeshtjen.Enabled = false;
            btnHistorikuBisedes.Enabled = false;
        }
        //zgjedhja e detyres
        private void dgvDetyrat_SelectionChanged(
            object? sender,
            EventArgs e)
        {
            if (dgvDetyrat.CurrentRow == null)
            {
                return;
            }

            object delegimiValue =
                dgvDetyrat.CurrentRow
                    .Cells["DelegimiId"].Value;

            bool kaDelegim =
                delegimiValue != null &&
                delegimiValue != DBNull.Value;

            string statusi =
                Convert.ToString(
                    dgvDetyrat.CurrentRow
                        .Cells["Statusi"].Value)
                ?? string.Empty;



            string pergjigjja =
                Convert.ToString(
                    dgvDetyrat.CurrentRow
                        .Cells["Përgjigjja"].Value)
                ?? string.Empty;

            string pathPergjigje =
                Convert.ToString(
                    dgvDetyrat.CurrentRow
                        .Cells[
                            "PathDokumentPergjigje"
                        ].Value)
                ?? string.Empty;

            dokumentiPergjigjesOrigjinal =
                string.Empty;

            txtPergjigja.Text =
                pergjigjja;

            txtPathPergjigje.Text =
                pathPergjigje;

            btnHapDokumentin.Enabled = true;

            int krijuesiId = Convert.ToInt32(
                dgvDetyrat.CurrentRow.Cells["KrijuesiId"].Value);

            bool eshteKrijuesi =
                krijuesiId == UserSession.PerdoruesiId;

            string lloji =
                Convert.ToString(
                    dgvDetyrat.CurrentRow
                        .Cells["Lloji"].Value)
                ?? string.Empty;

            btnHistorikuBisedes.Enabled =
                lloji == "e Brendshme";

            // Ne korrespondencen e brendshme, secili nga
            // dy pjesemarresit ne shkrese , mund ta mbylle ceshtjen.
            btnMbyllCeshtjen.Enabled =
                lloji == "e Brendshme" &&
                (eshteKrijuesi || kaDelegim);

            btnFilloTrajtimin.Enabled =
                kaDelegim &&
                !eshteKrijuesi &&
                statusi == "Në pritje";

            bool mundTePergjigjet =
                kaDelegim &&
                !eshteKrijuesi &&
                (statusi == "Në pritje" ||
                 statusi == "Në proces");

            txtPergjigja.Enabled =
                mundTePergjigjet;

            txtPergjigja.ReadOnly =
                !mundTePergjigjet;

            txtPathPergjigje.Enabled =
                mundTePergjigjet;

            btnZgjidhDokumentPergjigje.Enabled =
                mundTePergjigjet;

            btnSkanoDokumentPergjigje.Enabled =
                mundTePergjigjet;

            btnPerfundo.Enabled =
                mundTePergjigjet;
        }

        private void btnRifresko_Click(
            object? sender,
            EventArgs e)
        {
            NgarkoShkresat();
        }

        private void btnShkreseBrendshme_Click(
            object? sender,
            EventArgs e)
        {
            using FormShkreseBrendshme forma =
                new FormShkreseBrendshme();

            forma.ShowDialog();

            NgarkoShkresat();
        }

        private void btnHapDokumentin_Click(
            object? sender,
            EventArgs e)
        {
            if (dgvDetyrat.CurrentRow == null)
            {
                MessageBox.Show(
                    "Zgjidhni një shkresë!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string path =
                Convert.ToString(
                    dgvDetyrat.CurrentRow
                        .Cells["PathDokumenti"].Value)
                ?? string.Empty;

            HapDokumentin(
                path,
                "Dokumenti i shkresës");
        }

        private void btnZgjidhDokumentPergjigje_Click(
            object? sender,
            EventArgs e)
        {
            if (!MerrDelegiminEZgjedhur(out _))
            {
                return;
            }

            using OpenFileDialog dialog =
                new OpenFileDialog();

            dialog.Title =
                "Zgjidh dokumentin e përgjigjes";

            dialog.Filter =
                "Dokumente të lejuara|" +
                "*.pdf;*.doc;*.docx;*.jpg;*.jpeg;*.png|" +
                "Dokument PDF|*.pdf|" +
                "Dokument Word|*.doc;*.docx|" +
                "Imazhe|*.jpg;*.jpeg;*.png";

            dialog.Multiselect = false;

            if (dialog.ShowDialog() ==
                DialogResult.OK)
            {
                dokumentiPergjigjesOrigjinal =
                    dialog.FileName;

                txtPathPergjigje.Text =
                    dialog.FileName;
            }
        }

        private void HapDokumentin(
            string path,
            string pershkrimi)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show(
                    $"{pershkrimi} nuk është ngarkuar!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!File.Exists(path))
            {
                MessageBox.Show(
                    $"{pershkrimi} nuk u gjet në këtë adresë:\n" +
                    path,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            try
            {
                Process.Start(
                    new ProcessStartInfo
                    {
                        FileName = path,
                        UseShellExecute = true
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Dokumenti nuk mund të hapet: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnFilloTrajtimin_Click(
            object? sender,
            EventArgs e)
        {
            if (!MerrDelegiminEZgjedhur(
                    out int delegimiId))
            {
                return;
            }

            string statusi =
                Convert.ToString(
                    dgvDetyrat.CurrentRow!
                        .Cells["Statusi"].Value)
                ?? string.Empty;

            if (statusi != "Në pritje")
            {
                MessageBox.Show(
                    "Kjo detyrë nuk është në statusin " +
                    "\"Në pritje\".",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                string query = @"
                    UPDATE Delegimet
                    SET Statusi = N'Në proces'
                    WHERE DelegimiId = @delegimiId
                      AND PunonjesiId = @punonjesiId
                      AND Statusi = N'Në pritje';";

                SqlParameter[] parameters =
                {
                    new SqlParameter(
                        "@delegimiId",
                        delegimiId),

                    new SqlParameter(
                        "@punonjesiId",
                        UserSession.PerdoruesiId)
                };

                int ndryshuar =
                    DBHelper.ExecuteNonQuery(
                        query,
                        parameters);

                if (ndryshuar == 0)
                {
                    MessageBox.Show(
                        "Detyra nuk mund të kalojë në proces.",
                        "Kujdes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    NgarkoShkresat();
                    return;
                }

                MessageBox.Show(
                    "Trajtimi i shkresës filloi.",
                    "Sukses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                NgarkoShkresat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gabim gjatë ndryshimit të statusit: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnPerfundo_Click(
            object? sender,
            EventArgs e)
        {
            if (!MerrDelegiminEZgjedhur(
                    out int delegimiId))
            {
                return;
            }

     
            string pergjigjja =
                txtPergjigja.Text.Trim();

            if (string.IsNullOrWhiteSpace(pergjigjja))
            {
                MessageBox.Show(
                    "Shkruani përgjigjen përpara se " +
                    "ta përfundoni detyrën!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPergjigja.Focus();
                return;
            }

            bool kaDokument =
                !string.IsNullOrWhiteSpace(dokumentiPergjigjesOrigjinal);

            if (kaDokument && !File.Exists(dokumentiPergjigjesOrigjinal))
            {
                MessageBox.Show(
                    "Dokumenti i zgjedhur nuk u gjet. Zgjidheni përsëri ose dërgojeni përgjigjen pa dokument.",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult konfirmim =
                MessageBox.Show(
                    kaDokument
                        ? "Dëshironi ta dërgoni përgjigjen me dokument?"
                        : "Dëshironi ta dërgoni përgjigjen pa dokument?",
                    "Konfirmo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (konfirmim != DialogResult.Yes)
            {
                return;
            }

            DataGridViewRow row = dgvDetyrat.CurrentRow!;

            string lloji =
                Convert.ToString(row.Cells["Lloji"].Value)
                ?? string.Empty;

            // Shkresa hyrese: pergjigjja ruhet te Delegimet,
            // qe Menaxheri ta ktheje ne shkrese dalese.
            // Shkresa e brendshme: krijohet /2, /3, ...
            // dhe vazhdon historiku i bisedes.
            if (lloji != "e Brendshme")
            {
                RuajPergjigjenPerShkreseHyrese(
                    delegimiId,
                    pergjigjja,
                    kaDokument);

                return;
            }

            int viti =
                Convert.ToInt32(row.Cells["Viti"].Value);

            int numriProtokolli =
                Convert.ToInt32(row.Cells["NumriProtokolli"].Value);

            int marresiId =
                Convert.ToInt32(row.Cells["KrijuesiId"].Value);

            string klasifikimi =
                Convert.ToString(row.Cells["Klasifikimi"].Value)
                ?? "Kufizuar";

            if (marresiId == UserSession.PerdoruesiId)
            {
                MessageBox.Show(
                    "Nuk mund t'ia dërgoni përgjigjen vetes.",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string? pathIRuajtur = null;

            using SqlConnection connection =
                DBHelper.GetConnection();

            connection.Open();

            using SqlTransaction transaction =
                connection.BeginTransaction();

            try
            {
                if (kaDokument)
                {
                    string folderi =
                        Path.Combine(
                            AppContext.BaseDirectory,
                            "DokumentePergjigje",
                            viti.ToString());

                    Directory.CreateDirectory(folderi);

                    string extension =
                        Path.GetExtension(
                            dokumentiPergjigjesOrigjinal);

                    string emriIRuajtur =
                        Guid.NewGuid().ToString("N") +
                        extension.ToLowerInvariant();

                    pathIRuajtur =
                        Path.Combine(
                            folderi,
                            emriIRuajtur);

                    File.Copy(
                        dokumentiPergjigjesOrigjinal,
                        pathIRuajtur,
                        false);
                }

                int numriKorrespondences;

                string queryNumri = @"
                    SELECT ISNULL(MAX(NumriKorrespondences), 0) + 1
                    FROM Shkresat WITH (UPDLOCK, HOLDLOCK)
                    WHERE Viti = @viti
                      AND NumriProtokolli = @numriProtokolli;";

                using (SqlCommand cmdNumri = new SqlCommand(
                           queryNumri,
                           connection,
                           transaction))
                {
                    cmdNumri.Parameters.AddWithValue("@viti", viti);
                    cmdNumri.Parameters.AddWithValue(
                        "@numriProtokolli",
                        numriProtokolli);

                    numriKorrespondences =
                        Convert.ToInt32(cmdNumri.ExecuteScalar());
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
                        Mbyllur
                    )
                    OUTPUT INSERTED.ShkresaId
                    VALUES
                    (
                        @numriProtokolli,
                        @numriKorrespondences,
                        @viti,
                        SYSDATETIME(),
                        N'e Brendshme',
                        @klasifikimi,
                        NULL,
                        @derguesiId,
                        @permbajtja,
                        @pathDokumenti,
                        0
                    );";

                int shkresaReId;

                using (SqlCommand cmdShkresa = new SqlCommand(
                           insertShkresa,
                           connection,
                           transaction))
                {
                    cmdShkresa.Parameters.AddWithValue(
                        "@numriProtokolli",
                        numriProtokolli);
                    cmdShkresa.Parameters.AddWithValue(
                        "@numriKorrespondences",
                        numriKorrespondences);
                    cmdShkresa.Parameters.AddWithValue("@viti", viti);
                    cmdShkresa.Parameters.AddWithValue(
                        "@klasifikimi",
                        klasifikimi);
                    cmdShkresa.Parameters.AddWithValue(
                        "@derguesiId",
                        UserSession.PerdoruesiId);
                    cmdShkresa.Parameters.AddWithValue(
                        "@permbajtja",
                        pergjigjja);
                    cmdShkresa.Parameters.Add(
                        "@pathDokumenti",
                        SqlDbType.NVarChar,
                        1000).Value =
                            string.IsNullOrWhiteSpace(pathIRuajtur)
                                ? DBNull.Value
                                : pathIRuajtur;

                    shkresaReId =
                        Convert.ToInt32(cmdShkresa.ExecuteScalar());
                }

                string insertMarresi = @"
                    INSERT INTO ShkresaPunonjesit
                        (ShkresaId, PerdoruesiId)
                    VALUES
                        (@shkresaId, @marresiId);";

                using (SqlCommand cmdMarresi = new SqlCommand(
                           insertMarresi,
                           connection,
                           transaction))
                {
                    cmdMarresi.Parameters.AddWithValue(
                        "@shkresaId",
                        shkresaReId);
                    cmdMarresi.Parameters.AddWithValue(
                        "@marresiId",
                        marresiId);
                    cmdMarresi.ExecuteNonQuery();
                }

                string insertDelegimi = @"
                    INSERT INTO Delegimet
                    (
                        ShkresaId,
                        PunonjesiId,
                        DeleguarNga,
                        Afati,
                        Shenim,
                        Statusi,
                        DataDelegimit
                    )
                    VALUES
                    (
                        @shkresaId,
                        @marresiId,
                        @derguarNga,
                        (
                            SELECT TOP 1 dv.Afati
                            FROM Delegimet dv
                            INNER JOIN Shkresat sv
                                ON dv.ShkresaId = sv.ShkresaId
                            WHERE sv.Viti = @vitiCeshtjes
                              AND sv.NumriProtokolli =
                                  @numriCeshtjes
                              AND dv.Afati IS NOT NULL
                            ORDER BY
                                sv.NumriKorrespondences DESC,
                                dv.DelegimiId DESC
                        ),
                        N'Përgjigje në korrespondencë',
                        N'Në pritje',
                        SYSDATETIME()
                    );";

                using (SqlCommand cmdDelegimi = new SqlCommand(
                           insertDelegimi,
                           connection,
                           transaction))
                {
                    cmdDelegimi.Parameters.AddWithValue(
                        "@shkresaId",
                        shkresaReId);
                    cmdDelegimi.Parameters.AddWithValue(
                        "@marresiId",
                        marresiId);
                    cmdDelegimi.Parameters.AddWithValue(
                        "@derguarNga",
                        UserSession.PerdoruesiId);
                    cmdDelegimi.Parameters.AddWithValue(
                        "@vitiCeshtjes",
                        viti);
                    cmdDelegimi.Parameters.AddWithValue(
                        "@numriCeshtjes",
                        numriProtokolli);
                    cmdDelegimi.ExecuteNonQuery();
                }

                string perfundoDelegiminEVjeter = @"
                    UPDATE Delegimet
                    SET
                        Pergjigja = @pergjigjja,
                        PathDokumentPergjigje = @path,
                        DataPergjigjes = SYSDATETIME(),
                        Statusi = N'Përfunduar'
                    WHERE DelegimiId = @delegimiId
                      AND PunonjesiId = @punonjesiId
                      AND Statusi IN (N'Në pritje', N'Në proces');";

                using (SqlCommand cmdVjeter = new SqlCommand(
                           perfundoDelegiminEVjeter,
                           connection,
                           transaction))
                {
                    cmdVjeter.Parameters.AddWithValue(
                        "@pergjigjja",
                        pergjigjja);
                    cmdVjeter.Parameters.Add(
                        "@path",
                        SqlDbType.NVarChar,
                        1000).Value =
                            string.IsNullOrWhiteSpace(pathIRuajtur)
                                ? DBNull.Value
                                : pathIRuajtur;
                    cmdVjeter.Parameters.AddWithValue(
                        "@delegimiId",
                        delegimiId);
                    cmdVjeter.Parameters.AddWithValue(
                        "@punonjesiId",
                        UserSession.PerdoruesiId);

                    if (cmdVjeter.ExecuteNonQuery() == 0)
                    {
                        throw new Exception(
                            "Detyra është trajtuar më parë.");
                    }
                }

                transaction.Commit();

                MessageBox.Show(
                    $"Përgjigjja u dërgua si korrespondenca " +
                    $"{numriProtokolli}/{numriKorrespondences}.",
                    "Sukses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                dokumentiPergjigjesOrigjinal =
                    string.Empty;

                NgarkoShkresat();
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
                    "Përgjigjja nuk u ruajt: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void RuajPergjigjenPerShkreseHyrese(
            int delegimiId,
            string pergjigjja,
            bool kaDokument)
        {
            DataGridViewRow row = dgvDetyrat.CurrentRow!;

            int viti =
                Convert.ToInt32(row.Cells["Viti"].Value);

            string? pathIRuajtur = null;

            try
            {
                if (kaDokument)
                {
                    string folderi =
                        Path.Combine(
                            AppContext.BaseDirectory,
                            "DokumentePergjigje",
                            viti.ToString());

                    Directory.CreateDirectory(folderi);

                    string extension =
                        Path.GetExtension(
                            dokumentiPergjigjesOrigjinal);

                    string emriIRuajtur =
                        Guid.NewGuid().ToString("N") +
                        extension.ToLowerInvariant();

                    pathIRuajtur =
                        Path.Combine(folderi, emriIRuajtur);

                    File.Copy(
                        dokumentiPergjigjesOrigjinal,
                        pathIRuajtur,
                        false);
                }

                string query = @"
                    UPDATE Delegimet
                    SET
                        Pergjigja = @pergjigjja,
                        PathDokumentPergjigje = @path,
                        DataPergjigjes = SYSDATETIME(),
                        Statusi =
                            N'P' + NCHAR(235) + N'rfunduar'
                    WHERE DelegimiId = @delegimiId
                      AND PunonjesiId = @punonjesiId
                      AND Statusi IN
                      (
                          N'N' + NCHAR(235) + N' pritje',
                          N'N' + NCHAR(235) + N' proces'
                      );";

                SqlParameter[] parameters =
                {
                    new SqlParameter(
                        "@pergjigjja",
                        SqlDbType.NVarChar,
                        -1)
                    {
                        Value = pergjigjja
                    },

                    new SqlParameter(
                        "@path",
                        SqlDbType.NVarChar,
                        1000)
                    {
                        Value = string.IsNullOrWhiteSpace(
                                    pathIRuajtur)
                            ? DBNull.Value
                            : pathIRuajtur
                    },

                    new SqlParameter(
                        "@delegimiId",
                        delegimiId),

                    new SqlParameter(
                        "@punonjesiId",
                        UserSession.PerdoruesiId)
                };

                int ndryshuar =
                    DBHelper.ExecuteNonQuery(query, parameters);

                if (ndryshuar == 0)
                {
                    throw new Exception(
                        "Detyra eshte trajtuar me pare ose " +
                        "nuk i perket perdoruesit aktual.");
                }

                MessageBox.Show(
                    "Pergjigjja iu kthye Menaxherit. " +
                    "Tani ai mund ta perdore per te " +
                    "krijuar shkresen dalese.",
                    "Sukses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                dokumentiPergjigjesOrigjinal =
                    string.Empty;

                NgarkoShkresat();
            }
            catch (Exception ex)
            {
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
                    "Pergjigjja nuk u ruajt: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnMbyllCeshtjen_Click(
            object? sender,
            EventArgs e)
        {
            if (dgvDetyrat.CurrentRow == null)
            {
                MessageBox.Show(
                    "Zgjidhni çështjen që dëshironi të mbyllni!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DataGridViewRow row =
                dgvDetyrat.CurrentRow;

            string lloji = Convert.ToString(
                row.Cells["Lloji"].Value) ?? string.Empty;

            if (lloji != "e Brendshme")
            {
                MessageBox.Show(
                    "Ky buton përdoret për mbylljen e korrespondencave të brendshme.",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int viti =
                Convert.ToInt32(
                    row.Cells["Viti"].Value);

            int numriProtokolli =
                Convert.ToInt32(
                    row.Cells["NumriProtokolli"].Value);

            string? komentMbylljeje =
                KerkoKomentinEMbylljes();

            if (komentMbylljeje == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(komentMbylljeje))
            {
                MessageBox.Show(
                    "Shkruani komentin përfundimtar të mbylljes!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult konfirmim =
                MessageBox.Show(
                    $"Dëshironi ta mbyllni çështjen " +
                    $"{numriProtokolli} të vitit {viti}?\n\n" +
                    "Ajo do të hiqet nga detyrat aktive, " +
                    "por do të mbetet në databazë.",
                    "Konfirmo mbylljen",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (konfirmim != DialogResult.Yes)
            {
                return;
            }

            using SqlConnection connection =
                DBHelper.GetConnection();

            connection.Open();

            using SqlTransaction transaction =
                connection.BeginTransaction();

            try
            {
                string updateDelegimet = @"
                    UPDATE d
                    SET d.Statusi = N'Përfunduar'

                    FROM Delegimet d

                    INNER JOIN Shkresat s
                        ON d.ShkresaId = s.ShkresaId

                    WHERE s.Viti = @viti
                      AND s.NumriProtokolli =
                          @numriProtokolli

                      AND d.Statusi <> N'Përfunduar';";

                using (SqlCommand cmdDelegimet =
                       new SqlCommand(
                           updateDelegimet,
                           connection,
                           transaction))
                {
                    cmdDelegimet.Parameters.AddWithValue(
                        "@viti",
                        viti);

                    cmdDelegimet.Parameters.AddWithValue(
                        "@numriProtokolli",
                        numriProtokolli);

                    cmdDelegimet.ExecuteNonQuery();
                }

                string updateShkresat = @"
                    UPDATE Shkresat
                    SET
                        Mbyllur = 1,
                        DataMbylljes = SYSDATETIME(),
                        MbyllurNga = @mbyllurNga,
                        KomentMbylljeje = @komentMbylljeje

                    WHERE Viti = @viti
                      AND NumriProtokolli =
                          @numriProtokolli

                      AND Mbyllur = 0;";

                using (SqlCommand cmdShkresat =
                       new SqlCommand(
                           updateShkresat,
                           connection,
                           transaction))
                {
                    cmdShkresat.Parameters.AddWithValue(
                        "@mbyllurNga",
                        UserSession.PerdoruesiId);

                    cmdShkresat.Parameters.AddWithValue(
                        "@komentMbylljeje",
                        komentMbylljeje);

                    cmdShkresat.Parameters.AddWithValue(
                        "@viti",
                        viti);

                    cmdShkresat.Parameters.AddWithValue(
                        "@numriProtokolli",
                        numriProtokolli);

                    int ndryshuar =
                        cmdShkresat.ExecuteNonQuery();

                    if (ndryshuar == 0)
                    {
                        throw new Exception(
                            "Çështja është mbyllur më parë " +
                            "ose nuk ekziston.");
                    }
                }

                transaction.Commit();

                MessageBox.Show(
                    $"Çështja {numriProtokolli} u mbyll.\n" +
                    "Ajo mbetet në databazë dhe historik.",
                    "Sukses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                NgarkoShkresat();
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

                MessageBox.Show(
                    "Çështja nuk u mbyll: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string? KerkoKomentinEMbylljes()
        {
            using Form dialog = new Form();
            using Label label = new Label();
            using TextBox tekst = new TextBox();
            using Button btnRuaj = new Button();
            using Button btnAnulo = new Button();

            dialog.Text = "Komenti i mbylljes";
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
            dialog.MinimizeBox = false;
            dialog.MaximizeBox = false;
            dialog.ClientSize = new System.Drawing.Size(480, 245);

            label.AutoSize = true;
            label.Location = new System.Drawing.Point(20, 18);
            label.Text = "Shkruani komentin përfundimtar të çështjes:";

            tekst.Location = new System.Drawing.Point(20, 52);
            tekst.Size = new System.Drawing.Size(440, 120);
            tekst.Multiline = true;
            tekst.ScrollBars = ScrollBars.Vertical;
            tekst.MaxLength = 1000;

            btnRuaj.Text = "Mbyll çështjen";
            btnRuaj.Location = new System.Drawing.Point(190, 192);
            btnRuaj.Size = new System.Drawing.Size(130, 34);
            btnRuaj.DialogResult = DialogResult.OK;

            btnAnulo.Text = "Anulo";
            btnAnulo.Location = new System.Drawing.Point(330, 192);
            btnAnulo.Size = new System.Drawing.Size(130, 34);
            btnAnulo.DialogResult = DialogResult.Cancel;

            dialog.Controls.Add(label);
            dialog.Controls.Add(tekst);
            dialog.Controls.Add(btnRuaj);
            dialog.Controls.Add(btnAnulo);
            dialog.AcceptButton = btnRuaj;
            dialog.CancelButton = btnAnulo;

            while (true)
            {
                DialogResult rezultat = dialog.ShowDialog(this);

                if (rezultat != DialogResult.OK)
                {
                    return null;
                }

                string koment = tekst.Text.Trim();

                if (!string.IsNullOrWhiteSpace(koment))
                {
                    return koment;
                }

                MessageBox.Show(
                    dialog,
                    "Komenti i mbylljes nuk mund të jetë bosh!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                tekst.Focus();
            }
        }

        private bool MerrDelegiminEZgjedhur(
            out int delegimiId)
        {
            delegimiId = 0;

            if (dgvDetyrat.CurrentRow == null)
            {
                MessageBox.Show(
                    "Zgjidhni një shkresë!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            object value =
                dgvDetyrat.CurrentRow
                    .Cells["DelegimiId"].Value;

            if (value == null ||
                value == DBNull.Value)
            {
                MessageBox.Show(
                    "Kjo shkresë është vetëm për lexim. " +
                    "Duhet t'ju jetë dërguar ose deleguar " +
                    "si detyrë që ta trajtoni.",
                    "Informacion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return false;
            }

            delegimiId =
                Convert.ToInt32(value);

            return true;
        }

        private void btnCeshtjeMbyllura_Click(object sender, EventArgs e)
        {
            using FormCeshtjeMbyllura forma =
       new FormCeshtjeMbyllura();

            forma.ShowDialog();
        }


        private void btnHistorikuBisedes_Click(object sender, EventArgs e)
        {
            if (dgvDetyrat.CurrentRow == null)
            {
                MessageBox.Show(
                    "Zgjidhni fillimisht një shkresë!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string lloji = Convert.ToString(
                dgvDetyrat.CurrentRow.Cells["Lloji"].Value)
                ?? string.Empty;

            if (lloji != "e Brendshme")
            {
                MessageBox.Show(
                    "Historiku i bisedes perdoret vetem " +
                    "per shkresat e brendshme.",
                    "Informacion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            int viti = Convert.ToInt32(
                dgvDetyrat.CurrentRow.Cells["Viti"].Value);

            int numriProtokollit = Convert.ToInt32(
                dgvDetyrat.CurrentRow.Cells["NumriProtokolli"].Value);

            using (FormHistorikuBisedes forma =
                   new FormHistorikuBisedes(viti, numriProtokollit))
            {
                forma.ShowDialog(this);
            }
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

        private void btnSkanoDokumentPergjigje_Click(
     object sender,
     EventArgs e)
        {
            if (!MerrDelegiminEZgjedhur(out _))
            {
                return;
            }

            try
            {
                string? pathISkanimit =
                    ScannerHelper.SkanoDokumentin(this);

                if (string.IsNullOrWhiteSpace(pathISkanimit))
                {
                    return;
                }

                dokumentiPergjigjesOrigjinal =
                    pathISkanimit;

                txtPathPergjigje.Text =
                    pathISkanimit;

                MessageBox.Show(
                    "Dokumenti i përgjigjes u skanua me sukses!",
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
