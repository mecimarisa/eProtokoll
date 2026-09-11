using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace eProtokoll
{
    public partial class FormDelegimet : Form
    {
        public FormDelegimet()
        {
            InitializeComponent();

            Load += FormDelegimet_Load;

            dgvShkresat.SelectionChanged +=
                dgvShkresat_SelectionChanged;

            dgvDelegimet.SelectionChanged +=
                dgvDelegimet_SelectionChanged;

            btnDelego.Click += btnDelego_Click;

            btnHapDokumentPergjigje.Click +=
                btnHapDokumentPergjigje_Click;

            btnKrijoPergjigjeDalese.Click +=
                btnKrijoPergjigjeDalese_Click;

            btnHapDokumentin.Click +=
                btnHapDokumentin_Click;

            btnPergjigjuBrendshme.Click +=
                btnPergjigjuBrendshme_Click;

            btnMbyllCeshtjen.Click +=
                btnMbyllCeshtjen_Click;

            btnHistorikuBisedes.Click +=
                btnHistorikuBisedes_Click;

        }

        private void FormDelegimet_Load(
            object? sender,
            EventArgs e)
        {
            btnHapDokumentPergjigje.Enabled = false;
            btnKrijoPergjigjeDalese.Enabled = false;
            btnHapDokumentin.Enabled = false;
            btnPergjigjuBrendshme.Enabled = false;
            btnMbyllCeshtjen.Enabled = false;
            btnHistorikuBisedes.Enabled = false;

            NgarkoShkresat();
        }

        private void NgarkoShkresat()
        {
            try
            {
                string query = @"
                    SELECT
                        s.ShkresaId,
                        s.Viti,
                        s.NumriProtokolli,
                        s.NumriKorrespondences,
                        s.PerdoruesiId AS DerguesiId,
                        s.InstitucioniId,
                        s.PathDokumenti,
                        s.Mbyllur,

                        CAST(
                            CASE
                                WHEN EXISTS
                                (
                                    SELECT 1
                                    FROM ShkresaPunonjesit spAktual

                                    WHERE spAktual.ShkresaId =
                                          s.ShkresaId

                                      AND spAktual.PerdoruesiId =
                                          @perdoruesiId
                                )
                                    THEN 1
                                ELSE 0
                            END
                            AS BIT
                        ) AS EshteMarres,

                        CAST(s.NumriProtokolli AS NVARCHAR(20))
                            + '/' +
                        CAST(
                            s.NumriKorrespondences
                            AS NVARCHAR(20)
                        ) AS [Nr. Protokolli],

                        CONVERT(
                            NVARCHAR(10),
                            s.DataRegjistrimit,
                            104
                        ) AS Data,

                        s.LlojiShkreses AS Lloji,

                        CASE
                            WHEN s.LlojiShkreses =
                                 N'e Brendshme'
                                THEN CONCAT(
                                    ISNULL(derguesi.Emri, ''),
                                    ' ',
                                    ISNULL(derguesi.Mbiemri, '')
                                )

                            ELSE ISNULL(i.Emri, '')
                        END AS [Dërguesi/Burimi],

                        s.Klasifikimi,

                        CASE
                        WHEN s.LlojiShkreses = N'e Brendshme'
                            THEN ISNULL(
                                (
                                    SELECT TOP 1
                                        mesazhiPare.Permbajtja

                                    FROM Shkresat mesazhiPare

                                    WHERE mesazhiPare.Viti = s.Viti
                                      AND mesazhiPare.NumriProtokolli =
                                          s.NumriProtokolli

                                    ORDER BY
                                        mesazhiPare.NumriKorrespondences ASC,
                                        mesazhiPare.ShkresaId ASC
                                ),
                                N''
                            )

                        ELSE ISNULL(
                            s.Permbajtja,
                            N''
                        )
                    END AS [Përmbajtja]

                    FROM Shkresat s

                    LEFT JOIN Institucionet i
                        ON s.InstitucioniId =
                           i.InstitucioniId

                    LEFT JOIN Perdoruesit derguesi
                        ON s.PerdoruesiId =
                           derguesi.PerdoruesiId

                    WHERE s.Mbyllur = 0

                      -- Në tabelën kryesore paraqitet vetëm
                      -- korrespondenca më e re e çështjes.
                      AND s.NumriKorrespondences =
                      (
                          SELECT MAX(sf.NumriKorrespondences)
                          FROM Shkresat sf
                          WHERE sf.Viti = s.Viti
                            AND sf.NumriProtokolli =
                                s.NumriProtokolli
                            AND sf.Mbyllur = 0
                      )

                             AND
        (
            -- Të gjitha shkresat hyrëse shfaqen te menaxheri.
            s.LlojiShkreses = N'Hyrese'

            OR

            (
                s.LlojiShkreses = N'e Brendshme'

                AND
                (
                    -- Shkresa publike shfaqet te çdo menaxher.
                    s.Klasifikimi = N'Publik'

            -- Kufizuar dhe Sekret shfaqen vetëm kur
            -- menaxheri është krijues ose marrës.
            OR EXISTS
            (
                SELECT 1
                FROM Shkresat sc

                WHERE sc.Viti = s.Viti
                  AND sc.NumriProtokolli =
                      s.NumriProtokolli

                  AND
                  (
                      sc.PerdoruesiId =
                          @perdoruesiId

                      OR EXISTS
                      (
                          SELECT 1
                          FROM ShkresaPunonjesit sp

                          WHERE sp.ShkresaId =
                                sc.ShkresaId

                            AND sp.PerdoruesiId =
                                @perdoruesiId
                      )
                                      )
                                )
                            )
                        )
                    )

                    ORDER BY
                        s.Viti DESC,
                        s.NumriProtokolli DESC,
                        s.NumriKorrespondences DESC;";

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

                dgvShkresat.DataSource = dt;

                string[] kolonatEFshehura =
                {
                    "ShkresaId",
                    "Viti",
                    "NumriProtokolli",
                    "NumriKorrespondences",
                    "DerguesiId",
                    "InstitucioniId",
                    "PathDokumenti",
                    "Mbyllur",
                    "EshteMarres"
                };

                foreach (string kolona
                         in kolonatEFshehura)
                {
                    if (dgvShkresat.Columns.Contains(
                            kolona))
                    {
                        dgvShkresat
                            .Columns[kolona]
                            .Visible = false;
                    }
                }

                cmbPunonjesi.DataSource = null;
                dgvDelegimet.DataSource = null;

                btnHapDokumentPergjigje.Enabled = false;
                btnKrijoPergjigjeDalese.Enabled = false;

                btnHapDokumentin.Enabled = false;
                btnPergjigjuBrendshme.Enabled = false;
                btnMbyllCeshtjen.Enabled = false;
                btnHistorikuBisedes.Enabled = false;

                dgvShkresat.ClearSelection();
                dgvShkresat.CurrentCell = null;
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

        private void dgvShkresat_SelectionChanged(
            object? sender,
            EventArgs e)
        {
            if (dgvShkresat.CurrentRow == null)
            {
                cmbPunonjesi.DataSource = null;
                dgvDelegimet.DataSource = null;

                btnHapDokumentPergjigje.Enabled = false;
                btnKrijoPergjigjeDalese.Enabled = false;

                btnHapDokumentin.Enabled = false;
                btnPergjigjuBrendshme.Enabled = false;
                btnMbyllCeshtjen.Enabled = false;
                btnHistorikuBisedes.Enabled = false;

                return;
            }

            object shkresaValue =
                dgvShkresat.CurrentRow
                    .Cells["ShkresaId"].Value;

            if (shkresaValue == null ||
                shkresaValue == DBNull.Value)
            {
                return;
            }

            int shkresaId =
                Convert.ToInt32(shkresaValue);

            string lloji =
                Convert.ToString(
                    dgvShkresat.CurrentRow
                        .Cells["Lloji"].Value)
                ?? string.Empty;

            string klasifikimi =
                Convert.ToString(
                    dgvShkresat.CurrentRow
                        .Cells["Klasifikimi"].Value)
                ?? string.Empty;

            bool eshteSekret =
                klasifikimi.StartsWith(
                    "Sekret",
                    StringComparison.OrdinalIgnoreCase);

            string path =
                Convert.ToString(
                    dgvShkresat.CurrentRow
                        .Cells["PathDokumenti"].Value)
                ?? string.Empty;

            int derguesiId =
                Convert.ToInt32(
                    dgvShkresat.CurrentRow
                        .Cells["DerguesiId"].Value);
            bool eshteMarres =
    Convert.ToBoolean(
        dgvShkresat.CurrentRow
            .Cells["EshteMarres"].Value);

            bool eshteKrijuesi =
                derguesiId == UserSession.PerdoruesiId;

            btnHapDokumentin.Enabled =
                !string.IsNullOrWhiteSpace(path);

            btnPergjigjuBrendshme.Enabled =
     (
         lloji == "e Brendshme" &&
         !eshteKrijuesi &&
         eshteMarres
     )
     ||
     (
         lloji == "Hyrese" &&
         eshteSekret
     );

            btnMbyllCeshtjen.Enabled =
                lloji == "Hyrese"
                ||
                (
                    lloji == "e Brendshme" &&
                    (eshteKrijuesi || eshteMarres)
                );

            btnHistorikuBisedes.Enabled =
                lloji == "e Brendshme";

            if (eshteSekret)
            {
               
                cmbPunonjesi.DataSource = null;
                cmbPunonjesi.Enabled = false;

                txtShenim.Clear();
                txtShenim.Enabled = false;

                dtpAfati.Checked = false;
                dtpAfati.Enabled = false;

                btnDelego.Enabled = false;
            }
            else
            {
                cmbPunonjesi.Enabled = true;
                txtShenim.Enabled = true;
                dtpAfati.Enabled = true;

                NgarkoPunonjesit();
            }

            NgarkoHistorikun(shkresaId);
        }

        private void NgarkoPunonjesit()
        {
            try
            {
                string query = @"
                    SELECT
                        p.PerdoruesiId,

                        p.Emri + ' ' + p.Mbiemri
                            AS EmriPlote

                    FROM Perdoruesit p

                    INNER JOIN Rolet r
                        ON p.RolId = r.RolId

                    WHERE r.EmriRolit = N'Punonjes'
                      AND p.Aktiv = 1

                    ORDER BY
                        p.Emri,
                        p.Mbiemri;";

                DataTable dt =
                    DBHelper.ExecuteQuery(query);

                cmbPunonjesi.DataSource = dt;
                cmbPunonjesi.DisplayMember =
                    "EmriPlote";

                cmbPunonjesi.ValueMember =
                    "PerdoruesiId";

                cmbPunonjesi.SelectedIndex =
                    dt.Rows.Count > 0 ? 0 : -1;

                btnDelego.Enabled =
                    dt.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gabim gjatë ngarkimit të punonjësve: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void NgarkoHistorikun(
            int shkresaId)
        {
            try
            {
                string query = @"
                    SELECT
                        d.DelegimiId,
                        s.ShkresaId,
                        s.Viti,
                        s.NumriProtokolli,
                        s.NumriKorrespondences,
                        s.InstitucioniId,
                        s.Klasifikimi,
                        d.PathDokumentPergjigje,

                        CAST(s.NumriProtokolli AS NVARCHAR(20))
                            + '/' +
                        CAST(
                            s.NumriKorrespondences
                            AS NVARCHAR(20)
                        ) AS Shkresa,

                        p.Emri + ' ' + p.Mbiemri
                            AS Punonjësi,

                        m.Emri + ' ' + m.Mbiemri
                            AS [Deleguar nga],

                        CONVERT(
                            NVARCHAR(10),
                            d.DataDelegimit,
                            104
                        ) + ' ' +
                        CONVERT(
                            NVARCHAR(5),
                            d.DataDelegimit,
                            108
                        ) AS [Data e delegimit],

                        CASE
                            WHEN d.Afati IS NULL
                                THEN ''
                            ELSE CONVERT(
                                NVARCHAR(10),
                                d.Afati,
                                104
                            )
                        END AS Afati,

                        ISNULL(d.Shenim, '')
                            AS Shënimi,

                        d.Statusi,

                        CASE

                        WHEN s.LlojiShkreses = N'e Brendshme'
                             AND s.NumriKorrespondences > 1
                            THEN ISNULL(
                                s.Permbajtja,
                                N''
                            )

                        ELSE ISNULL(
                            d.Pergjigja,
                            N''
                        )
                    END AS Përgjigjja,

                    CASE
                        WHEN s.LlojiShkreses = N'e Brendshme'
                             AND s.NumriKorrespondences > 1
                            THEN
                                CONVERT(
                                    NVARCHAR(10),
                                    s.DataRegjistrimit,
                                    104
                                )
                                + N' ' +
                                CONVERT(
                                    NVARCHAR(5),
                                    s.DataRegjistrimit,
                                    108
                                )

                        WHEN d.DataPergjigjes IS NOT NULL
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

                    FROM Delegimet d

                    INNER JOIN Shkresat s
                        ON d.ShkresaId =
                           s.ShkresaId

                    INNER JOIN Perdoruesit p
                        ON d.PunonjesiId =
                           p.PerdoruesiId

                    INNER JOIN Perdoruesit m
                        ON d.DeleguarNga =
                           m.PerdoruesiId

                    WHERE s.ShkresaId = @shkresaId


                    ORDER BY
                        d.DelegimiId DESC;";

                SqlParameter[] parameters =
                {
                    new SqlParameter(
                        "@shkresaId",
                        shkresaId)
                };

                DataTable dt =
                    DBHelper.ExecuteQuery(
                        query,
                        parameters);

                dgvDelegimet.DataSource = dt;

                string[] kolonatEFshehura =
                {
                    "DelegimiId",
                    "ShkresaId",
                    "Viti",
                    "NumriProtokolli",
                    "NumriKorrespondences",
                    "InstitucioniId",
                    "Klasifikimi",
                    "PathDokumentPergjigje"
                };

                foreach (string kolona
                         in kolonatEFshehura)
                {
                    if (dgvDelegimet.Columns.Contains(
                            kolona))
                    {
                        dgvDelegimet
                            .Columns[kolona]
                            .Visible = false;
                    }
                }

                dgvDelegimet.ClearSelection();
                dgvDelegimet.CurrentCell = null;

                btnHapDokumentPergjigje.Enabled = false;
                btnKrijoPergjigjeDalese.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gabim gjatë ngarkimit të historikut: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dgvDelegimet_SelectionChanged(
            object? sender,
            EventArgs e)
        {
            if (dgvDelegimet.CurrentRow == null)
            {
                btnHapDokumentPergjigje.Enabled = false;
                btnKrijoPergjigjeDalese.Enabled = false;
                return;
            }

            string statusi =
                Convert.ToString(
                    dgvDelegimet.CurrentRow
                        .Cells["Statusi"].Value)
                ?? string.Empty;

            string path =
                Convert.ToString(
                    dgvDelegimet.CurrentRow
                        .Cells["PathDokumentPergjigje"].Value)
                ?? string.Empty;

            object institucioniValue =
                dgvDelegimet.CurrentRow
                    .Cells["InstitucioniId"].Value;

            bool kaInstitucion =
                institucioniValue != null &&
                institucioniValue != DBNull.Value;

            bool kaDokument =
                !string.IsNullOrWhiteSpace(path);

            string pergjigjja =
                Convert.ToString(
                    dgvDelegimet.CurrentRow
                        .Cells["P\u00ebrgjigjja"].Value)
                ?? string.Empty;

            bool kaPergjigje =
                !string.IsNullOrWhiteSpace(pergjigjja);

            btnHapDokumentPergjigje.Enabled =
                kaDokument;

            // Pergjigjja dalese behet vetem per
            // shkresat qe kane institucion te jashtem
            btnKrijoPergjigjeDalese.Enabled =
                statusi == "Përfunduar" &&
                kaPergjigje &&
                kaInstitucion;
        }

        private void btnHapDokumentin_Click(
            object? sender,
            EventArgs e)
        {
            if (dgvShkresat.CurrentRow == null)
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
                    dgvShkresat.CurrentRow
                        .Cells["PathDokumenti"].Value)
                ?? string.Empty;

            HapDokumentin(
                path,
                "Dokumenti i shkresës");
        }

        private void btnPergjigjuBrendshme_Click(
            object? sender,
            EventArgs e)
        {
            if (dgvShkresat.CurrentRow == null)
            {
                MessageBox.Show(
                    "Zgjidhni shkresën e brendshme!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DataGridViewRow row =
                dgvShkresat.CurrentRow;

            string lloji =
                Convert.ToString(
                    row.Cells["Lloji"].Value)
                ?? string.Empty;

            string klasifikimi =
                Convert.ToString(
                    row.Cells["Klasifikimi"].Value)
                ?? string.Empty;

            bool eshteSekret =
                klasifikimi.StartsWith(
                    "Sekret",
                    StringComparison.OrdinalIgnoreCase);

            if (lloji == "Hyrese" && eshteSekret)
            {
                PergjigjuDirektShkresesSekrete(row);
                return;
            }

            if (lloji != "e Brendshme")
            {
                MessageBox.Show(
                    "Ky funksion përdoret vetëm për " +
                    "shkresat e brendshme.",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int derguesiId =
                Convert.ToInt32(
                    row.Cells["DerguesiId"].Value);

            if (derguesiId == UserSession.PerdoruesiId)
            {
                MessageBox.Show(
                    "Nuk mund t'i përgjigjeni vetes!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int viti =
                Convert.ToInt32(
                    row.Cells["Viti"].Value);

            int numriKryesor =
                Convert.ToInt32(
                    row.Cells["NumriProtokolli"].Value);

            using FormShkreseBrendshme forma =
                new FormShkreseBrendshme(
                    viti,
                    numriKryesor,
                    derguesiId,
                    klasifikimi);

            if (forma.ShowDialog() ==
                DialogResult.OK)
            {
                NgarkoShkresat();
            }
        }

        private void btnMbyllCeshtjen_Click(
            object? sender,
            EventArgs e)
        {
            if (dgvShkresat.CurrentRow == null)
            {
                MessageBox.Show(
                    "Zgjidhni çështjen që dëshironi të mbyllni!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DataGridViewRow row =
                dgvShkresat.CurrentRow;

            int viti =
                Convert.ToInt32(
                    row.Cells["Viti"].Value);

            int numriKryesor =
                Convert.ToInt32(
                    row.Cells["NumriProtokolli"].Value);

            string? komentMbylljeje =
                KerkoKomentinEMbylljes();

            if (komentMbylljeje == null)
            {
                return;
            }

            DialogResult konfirmim =
            MessageBox.Show(
            $"Dëshironi ta mbyllni çështjen " +
            $"{numriKryesor} të vitit {viti}?",
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
                    SET d.Statusi = N'Mbyllur'

                    FROM Delegimet d

                    INNER JOIN Shkresat s
                        ON d.ShkresaId = s.ShkresaId

                    WHERE s.Viti = @viti
                      AND s.NumriProtokolli =
                          @numriProtokolli

                      AND d.Statusi IN
                          (N'Në pritje', N'Në proces');";

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
                        numriKryesor);

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
                        numriKryesor);

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
                $"Çështja {numriKryesor} u mbyll me sukses.\n\n" +
                "Çështja është hequr nga lista e detyrave aktive.",
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
            }
        }

        private void PergjigjuDirektShkresesSekrete(
            DataGridViewRow row)
        {
            object institucioniValue =
                row.Cells["InstitucioniId"].Value;

            if (institucioniValue == null ||
                institucioniValue == DBNull.Value)
            {
                MessageBox.Show(
                    "Shkresa nuk ka institucion dergues.",
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            int viti =
                Convert.ToInt32(row.Cells["Viti"].Value);

            int numriKryesor =
                Convert.ToInt32(
                    row.Cells["NumriProtokolli"].Value);

            int institucioniId =
                Convert.ToInt32(institucioniValue);

            string klasifikimi =
                Convert.ToString(
                    row.Cells["Klasifikimi"].Value)
                ?? "Sekret";

            string permbajtja =
                $"Pergjigje per shkresen " +
                $"{numriKryesor}/1.";

            using FormShkreseDalese forma =
                new FormShkreseDalese(
                    viti,
                    numriKryesor,
                    institucioniId,
                    klasifikimi,
                    permbajtja,
                    string.Empty);

            if (forma.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(
                    "Pergjigjja sekrete u regjistrua si " +
                    $"shkrese dalese {numriKryesor}/2.",
                    "Sukses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                NgarkoShkresat();
            }
        }

        private void btnDelego_Click(
            object? sender,
            EventArgs e)
        {
            if (UserSession.PerdoruesiId <= 0 ||
                UserSession.Roli != "Menaxher")
            {
                MessageBox.Show(
                    "Vetëm Menaxheri mund të delegojë shkresa!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (dgvShkresat.CurrentRow == null)
            {
                MessageBox.Show(
                    "Zgjidhni shkresën që do të delegoni!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (cmbPunonjesi.SelectedValue == null)
            {
                MessageBox.Show(
                    "Zgjidhni punonjësin përgjegjës!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int shkresaId =
                Convert.ToInt32(
                    dgvShkresat.CurrentRow
                        .Cells["ShkresaId"].Value);

            int punonjesiId =
                Convert.ToInt32(
                    cmbPunonjesi.SelectedValue);

            string punonjesi =
                cmbPunonjesi.Text;

            string shenimi =
                txtShenim.Text.Trim();

            DateTime? afati = null;

            if (dtpAfati.Checked)
            {
                afati = dtpAfati.Value.Date;

                if (afati.Value < DateTime.Today)
                {
                    MessageBox.Show(
                        "Afati nuk mund të jetë në të kaluarën!",
                        "Kujdes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }




            }

            try
            {
                string queryKontroll = @"
                    SELECT COUNT(*)
                    FROM Delegimet
                    WHERE ShkresaId = @shkresaId
                      AND PunonjesiId = @punonjesiId
                      AND Statusi IN
                          (N'Në pritje', N'Në proces');";

                SqlParameter[] kontrollParameters =
                {
                    new SqlParameter(
                        "@shkresaId",
                        shkresaId),

                    new SqlParameter(
                        "@punonjesiId",
                        punonjesiId)
                };

                DataTable kontroll =
                    DBHelper.ExecuteQuery(
                        queryKontroll,
                        kontrollParameters);

                int ekziston =
                    Convert.ToInt32(
                        kontroll.Rows[0][0]);

                if (ekziston > 0)
                {
                    MessageBox.Show(
                        "Kjo shkresë i është deleguar më parë " +
                        "këtij punonjësi dhe është ende aktive!",
                        "Kujdes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DialogResult konfirmim =
                    MessageBox.Show(
                        $"Dëshironi t'ia delegoni shkresën " +
                        $"punonjësit {punonjesi}?",
                        "Konfirmo delegimin",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                if (konfirmim != DialogResult.Yes)
                {
                    return;
                }

                string insertQuery = @"
                    INSERT INTO Delegimet
                    (
                        ShkresaId,
                        PunonjesiId,
                        DeleguarNga,
                        DataDelegimit,
                        Afati,
                        Shenim
                    )
                    VALUES
                    (
                        @shkresaId,
                        @punonjesiId,
                        @deleguarNga,
                        SYSDATETIME(),
                        @afati,
                        @shenim
                    );";

                SqlParameter[] insertParameters =
                {
                    new SqlParameter(
                        "@shkresaId",
                        shkresaId),

                    new SqlParameter(
                        "@punonjesiId",
                        punonjesiId),

                    new SqlParameter(
                        "@deleguarNga",
                        UserSession.PerdoruesiId),

                    new SqlParameter(
                        "@afati",
                        afati.HasValue
                            ? afati.Value
                            : DBNull.Value),

                    new SqlParameter(
                        "@shenim",
                        string.IsNullOrWhiteSpace(shenimi)
                            ? DBNull.Value
                            : shenimi)
                };

                DBHelper.ExecuteNonQuery(
                    insertQuery,
                    insertParameters);

                MessageBox.Show(
                    "Shkresa u delegua me sukses!",
                    "Sukses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtShenim.Clear();
                dtpAfati.Checked = false;

                NgarkoHistorikun(shkresaId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gabim gjatë delegimit: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnHapDokumentPergjigje_Click(
            object? sender,
            EventArgs e)
        {
            if (dgvDelegimet.CurrentRow == null)
            {
                MessageBox.Show(
                    "Zgjidhni përgjigjen e punonjësit!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string path =
                Convert.ToString(
                    dgvDelegimet.CurrentRow
                        .Cells["PathDokumentPergjigje"].Value)
                ?? string.Empty;

            HapDokumentin(
                path,
                "Dokumenti i përgjigjes");
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
                    $"{pershkrimi} nuk u gjet:\n{path}",
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

        private void btnKrijoPergjigjeDalese_Click(
            object? sender,
            EventArgs e)
        {
            if (dgvDelegimet.CurrentRow == null)
            {
                MessageBox.Show(
                    "Zgjidhni përgjigjen e përfunduar!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DataGridViewRow row =
                dgvDelegimet.CurrentRow;

            string statusi =
                Convert.ToString(
                    row.Cells["Statusi"].Value)
                ?? string.Empty;

            if (statusi != "Përfunduar")
            {
                MessageBox.Show(
                    "Punonjësi nuk e ka përfunduar ende detyrën!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            object institucioniValue =
                row.Cells["InstitucioniId"].Value;

            if (institucioniValue == null ||
                institucioniValue == DBNull.Value)
            {
                MessageBox.Show(
                    "Shkresa nuk ka institucion të jashtëm!",
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            int viti =
                Convert.ToInt32(
                    row.Cells["Viti"].Value);

            int numriKryesor =
                Convert.ToInt32(
                    row.Cells["NumriProtokolli"].Value);

            int institucioniId =
                Convert.ToInt32(
                    institucioniValue);

            string klasifikimi =
                Convert.ToString(
                    row.Cells["Klasifikimi"].Value)
                ?? "Publik";

            string pergjigjja =
                Convert.ToString(
                    row.Cells["Përgjigjja"].Value)
                ?? string.Empty;

            string path =
                Convert.ToString(
                    row.Cells["PathDokumentPergjigje"].Value)
                ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(path) &&
                !File.Exists(path))
            {
                MessageBox.Show(
                    "Dokumenti i përgjigjes nuk u gjet!",
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                path = string.Empty;
            }

            string permbajtja =
                $"Përgjigje për shkresën " +
                $"{numriKryesor}/1.\r\n" +
                pergjigjja;

            using FormShkreseDalese forma =
                new FormShkreseDalese(
                    viti,
                    numriKryesor,
                    institucioniId,
                    klasifikimi,
                    permbajtja,
                    path);

            if (forma.ShowDialog() ==
                DialogResult.OK)
            {
                MessageBox.Show(
                    "Përgjigjja u regjistrua si " +
                    "shkresë dalëse.",
                    "Sukses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);


                NgarkoShkresat();
            }
        }

        private void btnHistorikuBisedes_Click(
            object? sender,
            EventArgs e)
        {
            if (dgvShkresat.CurrentRow == null)
            {
                MessageBox.Show(
                    "Zgjidhni një shkresë të brendshme!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string lloji =
                Convert.ToString(
                    dgvShkresat.CurrentRow.Cells["Lloji"].Value)
                ?? string.Empty;

            if (lloji != "e Brendshme")
            {
                MessageBox.Show(
                    "Historiku si bisedë përdoret vetëm për shkresat e brendshme.",
                    "Informacion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            int viti =
                Convert.ToInt32(
                    dgvShkresat.CurrentRow.Cells["Viti"].Value);

            int numriProtokolli =
                Convert.ToInt32(
                    dgvShkresat.CurrentRow.Cells["NumriProtokolli"].Value);

            using FormHistorikuBisedes forma =
                new FormHistorikuBisedes(
                    viti,
                    numriProtokolli);

            forma.ShowDialog();
        }

        private void dgvDelegimet_CellContentClick(
    object sender,
    DataGridViewCellEventArgs e)
        {

        }

    }


}
