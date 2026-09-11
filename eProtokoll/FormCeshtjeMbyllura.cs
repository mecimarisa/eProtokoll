using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace eProtokoll
{
    public partial class FormCeshtjeMbyllura : Form
    {
        public FormCeshtjeMbyllura()
        {
            InitializeComponent();

            dgvCeshtjet.SelectionChanged +=
                dgvCeshtjet_SelectionChanged;
        }

        private void FormCeshtjeMbyllura_Load(
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

            if (UserSession.Roli != "Menaxher" &&
                UserSession.Roli != "Punonjes")
            {
                MessageBox.Show(
                    "Nuk keni të drejtë të shikoni këtë faqe!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                Close();
                return;
            }

            KonfiguroTabelen();
            NgarkoCeshtjet();
        }

        private void KonfiguroTabelen()
        {
            dgvCeshtjet.ReadOnly = true;
            dgvCeshtjet.AllowUserToAddRows = false;
            dgvCeshtjet.AllowUserToDeleteRows = false;
            dgvCeshtjet.MultiSelect = false;
            dgvCeshtjet.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvCeshtjet.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvCeshtjet.RowHeadersVisible = false;
            dgvCeshtjet.BackgroundColor =
                System.Drawing.Color.White;

            btnHapDokumentin.Enabled = false;
            btnShikoHistorikun.Enabled = false;
        }

        private void btnRifresko_Click(
            object sender,
            EventArgs e)
        {
            NgarkoCeshtjet();
        }

        private void NgarkoCeshtjet()
        {
            try
            {
                string query = @"
                    SELECT
                        s.ShkresaId,
                        s.Viti,
                        s.NumriProtokolli,
                        s.NumriKorrespondences,

                        CONCAT(
                            s.NumriProtokolli,
                            N'/',
                            s.NumriKorrespondences
                        ) AS [Nr. protokolli],

                        CONVERT(
                            NVARCHAR(10),
                            s.DataRegjistrimit,
                            104
                        ) AS [Data e regjistrimit],

                        s.LlojiShkreses AS [Lloji i shkresës],
                        s.Klasifikimi,

                        CASE
                            WHEN i.InstitucioniId IS NOT NULL
                                THEN i.Emri
                            ELSE CONCAT(
                                regjistruesi.Emri,
                                N' ',
                                regjistruesi.Mbiemri
                            )
                        END AS [Institucioni / Dërguesi],

                        ISNULL(s.Permbajtja, N'')
                            AS [Mesazhi i fundit],

                        ISNULL(
                            s.KomentMbylljeje,
                            N'Pa koment mbylljeje'
                        ) AS [Komenti i mbylljes],

                        CASE
                            WHEN s.DataMbylljes IS NULL THEN N''
                            ELSE
                                CONVERT(
                                    NVARCHAR(10),
                                    s.DataMbylljes,
                                    104
                                ) + N' ' +
                                CONVERT(
                                    NVARCHAR(5),
                                    s.DataMbylljes,
                                    108
                                )
                        END AS [Data e mbylljes],

                        CONCAT(
                            mbyllesi.Emri,
                            N' ',
                            mbyllesi.Mbiemri
                        ) AS [Mbyllur nga],

                        dokumentiFundit.PathDokumenti

                    FROM Shkresat s

                    LEFT JOIN Institucionet i
                        ON s.InstitucioniId = i.InstitucioniId

                    LEFT JOIN Perdoruesit regjistruesi
                        ON s.PerdoruesiId =
                           regjistruesi.PerdoruesiId

                    LEFT JOIN Perdoruesit mbyllesi
                        ON s.MbyllurNga =
                           mbyllesi.PerdoruesiId

                    OUTER APPLY
                    (
                        SELECT TOP 1 sd.PathDokumenti
                        FROM Shkresat sd
                        WHERE sd.Viti = s.Viti
                          AND sd.NumriProtokolli =
                              s.NumriProtokolli
                          AND sd.PathDokumenti IS NOT NULL
                          AND LTRIM(RTRIM(sd.PathDokumenti)) <> N''
                        ORDER BY
                            sd.NumriKorrespondences DESC,
                            sd.ShkresaId DESC
                    ) dokumentiFundit

                    WHERE s.Mbyllur = 1

                      AND s.NumriKorrespondences =
                      (
                          SELECT MAX(sf.NumriKorrespondences)
                          FROM Shkresat sf
                          WHERE sf.Viti = s.Viti
                            AND sf.NumriProtokolli =
                                s.NumriProtokolli
                            AND sf.Mbyllur = 1
                      )

                      AND
                      (
                          @eshteMenaxher = 1
                          OR s.Klasifikimi <> N'Sekret'
                      )

                    ORDER BY
                        s.DataMbylljes DESC,
                        s.Viti DESC,
                        s.NumriProtokolli DESC;";

                SqlParameter[] parameters =
                {
                    new SqlParameter(
                        "@eshteMenaxher",
                        SqlDbType.Bit)
                    {
                        Value = UserSession.Roli == "Menaxher"
                    }
                };

                dgvCeshtjet.DataSource =
                    DBHelper.ExecuteQuery(query, parameters);

                FshihKolonatTeknike();
                dgvCeshtjet.ClearSelection();
                dgvCeshtjet.CurrentCell = null;
                btnHapDokumentin.Enabled = false;
                btnShikoHistorikun.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gabim gjatë ngarkimit të çështjeve: " +
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
                "ShkresaId",
                "Viti",
                "NumriProtokolli",
                "NumriKorrespondences",
                "PathDokumenti"
            };

            foreach (string kolona in kolonat)
            {
                if (dgvCeshtjet.Columns.Contains(kolona))
                {
                    dgvCeshtjet.Columns[kolona].Visible = false;
                }
            }
        }

        private void dgvCeshtjet_SelectionChanged(
            object? sender,
            EventArgs e)
        {
            bool kaRresht =
                dgvCeshtjet.CurrentRow != null &&
                !dgvCeshtjet.CurrentRow.IsNewRow;

            btnHapDokumentin.Enabled = kaRresht;
            btnShikoHistorikun.Enabled = kaRresht;
        }

        private void btnHapDokumentin_Click(
            object sender,
            EventArgs e)
        {
            if (dgvCeshtjet.CurrentRow == null)
            {
                MessageBox.Show(
                    "Zgjidhni një çështje!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string pathDokumenti = Convert.ToString(
                dgvCeshtjet.CurrentRow
                    .Cells["PathDokumenti"].Value)
                ?? string.Empty;

            if (string.IsNullOrWhiteSpace(pathDokumenti))
            {
                MessageBox.Show(
                    "Kjo çështje nuk ka dokument të ngarkuar.",
                    "Informacion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (!File.Exists(pathDokumenti))
            {
                MessageBox.Show(
                    "Dokumenti nuk u gjet në vendndodhjen e ruajtur.",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Process.Start(
                    new ProcessStartInfo
                    {
                        FileName = pathDokumenti,
                        UseShellExecute = true
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Dokumenti nuk mund të hapej: " + ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnShikoHistorikun_Click(
            object sender,
            EventArgs e)
        {
            if (dgvCeshtjet.CurrentRow == null)
            {
                MessageBox.Show(
                    "Zgjidhni një çështje!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int viti = Convert.ToInt32(
                dgvCeshtjet.CurrentRow.Cells["Viti"].Value);

            int numriProtokolli = Convert.ToInt32(
                dgvCeshtjet.CurrentRow
                    .Cells["NumriProtokolli"].Value);

            using FormHistorikuBisedes forma =
                new FormHistorikuBisedes(viti, numriProtokolli);

            forma.ShowDialog(this);
        }
    }
}
