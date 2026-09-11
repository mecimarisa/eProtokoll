using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace eProtokoll
{
    public partial class FormLibriProtokollit : Form
    {
        private readonly PrintDocument printDocument;
        private int rreshtiAktualPerPrintim;

        public FormLibriProtokollit()
        {
            InitializeComponent();

            printDocument = new PrintDocument();

            printDocument.DefaultPageSettings.Landscape = true;

            printDocument.PrintPage +=
                PrintDocument_PrintPage;

            Load +=
                FormLibriProtokollit_Load;

            Activated +=
                FormLibriProtokollit_Activated;

            cmbViti.SelectedIndexChanged +=
                cmbViti_SelectedIndexChanged;

            btnNgarko.Click +=
                btnNgarko_Click;

            btnPrinto.Click +=
                btnPrinto_Click;
        }

        private void FormLibriProtokollit_Load(
            object? sender,
            EventArgs e)
        {
            KonfiguroTabelen();
            NgarkoVitet();
        }

        private void FormLibriProtokollit_Activated(
            object? sender,
            EventArgs e)
        {
           
            if (cmbViti.SelectedIndex >= 0)
            {
                NgarkoLibrin();
            }
        }

        private void KonfiguroTabelen()
        {
            dgvProtokolli.ReadOnly = true;
            dgvProtokolli.AllowUserToAddRows = false;
            dgvProtokolli.AllowUserToDeleteRows = false;
            dgvProtokolli.MultiSelect = false;

            dgvProtokolli.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvProtokolli.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvProtokolli.RowHeadersVisible = false;
        }

        private void NgarkoVitet()
        {
            try
            {
                int? vitiIZgjedhur = null;

                if (cmbViti.SelectedValue != null &&
                    cmbViti.SelectedValue is not DataRowView)
                {
                    if (int.TryParse(
                            cmbViti.SelectedValue.ToString(),
                            out int viti))
                    {
                        vitiIZgjedhur = viti;
                    }
                }

                string query = @"
                    SELECT DISTINCT Viti
                    FROM
                    (
                        SELECT Viti
                        FROM VitetProtokollare

                        UNION

                        SELECT Viti
                        FROM Shkresat
                    ) AS Vitet

                    ORDER BY Viti DESC;";

                DataTable dt =
                    DBHelper.ExecuteQuery(query);

                cmbViti.DataSource = null;
                cmbViti.DisplayMember = "Viti";
                cmbViti.ValueMember = "Viti";
                cmbViti.DataSource = dt;

                if (cmbViti.Items.Count == 0)
                {
                    dgvProtokolli.DataSource = null;
                    return;
                }

                if (vitiIZgjedhur.HasValue)
                {
                    cmbViti.SelectedValue =
                        vitiIZgjedhur.Value;
                }
                else
                {
                    cmbViti.SelectedIndex = 0;
                }

                NgarkoLibrin();
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

        //Ndryshimi i vitit
        private void cmbViti_SelectedIndexChanged(
            object? sender,
            EventArgs e)
        {
            if (cmbViti.SelectedValue == null ||
                cmbViti.SelectedValue is DataRowView)
            {
                return;
            }

            NgarkoLibrin();
        }

        private void btnNgarko_Click(
            object? sender,
            EventArgs e)
        {
            
            NgarkoVitet();
        }


        private void NgarkoLibrin()
        {
            if (cmbViti.SelectedValue == null ||
                cmbViti.SelectedValue is DataRowView)
            {
                return;
            }

            if (!int.TryParse(
                    cmbViti.SelectedValue.ToString(),
                    out int viti))
            {
                MessageBox.Show(
                    "Viti protokollar nuk është i vlefshëm!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                string query = @"
                    SELECT
                        CONCAT(
                            s.NumriProtokolli,
                            N'/',
                            s.NumriKorrespondences
                        ) AS [Nr. Protokolli],

                        CONVERT(
                            NVARCHAR(10),
                            s.DataRegjistrimit,
                            104
                        ) AS Data,

                        CASE s.LlojiShkreses
                            WHEN N'Hyrese'
                                THEN N'Hyrëse'

                            WHEN N'Dalese'
                                THEN N'Dalëse'

                            WHEN N'e Brendshme'
                                THEN N'E brendshme'

                            ELSE s.LlojiShkreses
                        END AS Lloji,

                        CASE
                            WHEN i.InstitucioniId IS NOT NULL
                                THEN i.Emri

                            WHEN s.LlojiShkreses =
                                 N'e Brendshme'
                                THEN CONCAT(
                                    ISNULL(p.Emri, N''),
                                    N' ',
                                    ISNULL(p.Mbiemri, N''),
                                    N' - Institucioni ynë'
                                )

                            ELSE N'Institucioni ynë'
                        END AS Institucioni,

                        s.Klasifikimi,

                        ISNULL(
                            s.Permbajtja,
                            N''
                        ) AS [Përmbajtja],

                        CONCAT(
                            ISNULL(p.Emri, N''),
                            N' ',
                            ISNULL(p.Mbiemri, N'')
                        ) AS [Regjistruar nga]

                    FROM dbo.Shkresat s

                    LEFT JOIN dbo.Institucionet i
                        ON s.InstitucioniId =
                           i.InstitucioniId

                    LEFT JOIN dbo.Perdoruesit p
                        ON s.PerdoruesiId =
                           p.PerdoruesiId

                    WHERE s.Viti = @viti

                    ORDER BY
                        s.NumriProtokolli,
                        s.NumriKorrespondences,
                        s.DataRegjistrimit;";

                SqlParameter[] parameters =
                {
                    new SqlParameter(
                        "@viti",
                        SqlDbType.Int)
                    {
                        Value = viti
                    }
                };

                DataTable dt =
                    DBHelper.ExecuteQuery(
                        query,
                        parameters);

                
                dgvProtokolli.DataSource = null;

                
                dgvProtokolli.DataSource = dt;

                dgvProtokolli.ClearSelection();
                dgvProtokolli.CurrentCell = null;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        $"Nuk ka shkresa për vitin {viti}.",
                        "Informacion",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gabim gjatë ngarkimit të Librit të Protokollit: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnPrinto_Click(
            object? sender,
            EventArgs e)
        {
            if (dgvProtokolli.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Nuk ka të dhëna për printim!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            rreshtiAktualPerPrintim = 0;

            using PrintPreviewDialog preview =
                new PrintPreviewDialog();

            preview.Document = printDocument;
            preview.Width = 1200;
            preview.Height = 800;

            preview.StartPosition =
                FormStartPosition.CenterScreen;

            preview.ShowDialog();
        }

        private void PrintDocument_PrintPage(
            object? sender,
            PrintPageEventArgs e)
        {
            if (e.Graphics == null)
            {
                return;
            }

            Graphics graphics = e.Graphics;

            using Font fontTitulli =
                new Font(
                    "Arial",
                    16,
                    FontStyle.Bold);

            using Font fontNormal =
                new Font(
                    "Arial",
                    8,
                    FontStyle.Regular);

            using Font fontHeader =
                new Font(
                    "Arial",
                    8,
                    FontStyle.Bold);

            using Pen pen =
                new Pen(Color.Black);

            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;

            string titulli =
                $"LIBRI I PROTOKOLLIT — {cmbViti.Text}";

            SizeF madhesiaTitullit =
                graphics.MeasureString(
                    titulli,
                    fontTitulli);

            float titulliX =
                e.MarginBounds.Left +
                (
                    e.MarginBounds.Width -
                    madhesiaTitullit.Width
                ) / 2;

            graphics.DrawString(
                titulli,
                fontTitulli,
                Brushes.Black,
                titulliX,
                y);

            y += 45;

            string[] kolonat =
            {
                "Nr. Protokolli",
                "Data",
                "Lloji",
                "Institucioni",
                "Klasifikimi",
                "Përmbajtja",
                "Regjistruar nga"
            };

            int[] gjeresiteBaze =
            {
                90,
                75,
                80,
                135,
                90,
                220,
                130
            };

            int shumaGjeresive = 0;

            foreach (int gjeresia in gjeresiteBaze)
            {
                shumaGjeresive += gjeresia;
            }

            float koeficienti =
                (float)e.MarginBounds.Width /
                shumaGjeresive;

            int[] gjeresite =
                new int[gjeresiteBaze.Length];

            for (int i = 0;
                 i < gjeresiteBaze.Length;
                 i++)
            {
                gjeresite[i] =
                    (int)(
                        gjeresiteBaze[i] *
                        koeficienti
                    );
            }

            int lartesiaHeader = 35;
            int kolonaX = x;

            for (int i = 0;
                 i < kolonat.Length;
                 i++)
            {
                Rectangle rect =
                    new Rectangle(
                        kolonaX,
                        y,
                        gjeresite[i],
                        lartesiaHeader);

                graphics.FillRectangle(
                    Brushes.LightGray,
                    rect);

                graphics.DrawRectangle(
                    pen,
                    rect);

                graphics.DrawString(
                    kolonat[i],
                    fontHeader,
                    Brushes.Black,
                    new RectangleF(
                        rect.X + 3,
                        rect.Y + 3,
                        rect.Width - 6,
                        rect.Height - 6));

                kolonaX += gjeresite[i];
            }

            y += lartesiaHeader;

            int lartesiaRreshtit = 45;

            while (rreshtiAktualPerPrintim <
                   dgvProtokolli.Rows.Count)
            {
                if (y + lartesiaRreshtit >
                    e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                DataGridViewRow row =
                    dgvProtokolli.Rows[
                        rreshtiAktualPerPrintim];

                if (row.IsNewRow)
                {
                    rreshtiAktualPerPrintim++;
                    continue;
                }

                kolonaX = x;

                for (int i = 0;
                     i < kolonat.Length;
                     i++)
                {
                    string vlere =
                        Convert.ToString(
                            row.Cells[i].Value)
                        ?? string.Empty;

                    Rectangle rect =
                        new Rectangle(
                            kolonaX,
                            y,
                            gjeresite[i],
                            lartesiaRreshtit);

                    graphics.DrawRectangle(
                        pen,
                        rect);

                    graphics.DrawString(
                        vlere,
                        fontNormal,
                        Brushes.Black,
                        new RectangleF(
                            rect.X + 3,
                            rect.Y + 3,
                            rect.Width - 6,
                            rect.Height - 6));

                    kolonaX += gjeresite[i];
                }

                y += lartesiaRreshtit;
                rreshtiAktualPerPrintim++;
            }

            e.HasMorePages = false;

           
            rreshtiAktualPerPrintim = 0;
        }
    }
}