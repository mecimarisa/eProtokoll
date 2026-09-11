using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace eProtokoll
{
    public partial class FormHistorikuBisedes : Form
    {
        private readonly int viti;
        private readonly int numriProtokolli;

        private string pathDokumentiIZgjedhur =
            string.Empty;

        private Panel? mesazhiIZgjedhur;

        public FormHistorikuBisedes(
            int viti,
            int numriProtokolli)
        {
            InitializeComponent();

            this.viti = viti;
            this.numriProtokolli = numriProtokolli;

            Load +=
                FormHistorikuBisedes_Load;

            btnRifresko.Click +=
                btnRifresko_Click;

            btnHapDokumentin.Click +=
                btnHapDokumentin_Click;

            btnMbyll.Click +=
                btnMbyll_Click;

            pnlBiseda.Resize +=
                pnlBiseda_Resize;
        }

        private void FormHistorikuBisedes_Load(
            object? sender,
            EventArgs e)
        {

            lblProtokolli.Text =
                  $"Çështja: {numriProtokolli} — Viti {viti}";

            btnHapDokumentin.Enabled = false;

            NgarkoBiseden();
        }

        private void btnRifresko_Click(
            object? sender,
            EventArgs e)
        {
            NgarkoBiseden();
        }

        private void btnMbyll_Click(
            object? sender,
            EventArgs e)
        {
            Close();
        }

        private void pnlBiseda_Resize(
            object? sender,
            EventArgs e)
        {
            RregulloGjeresineEMesazheve();
        }

        private void NgarkoBiseden()
        {
            try
            {
                string query = @"
                    SELECT
                        s.ShkresaId,
                        s.NumriKorrespondences,

                        CONCAT(
                            s.NumriProtokolli,
                            N'/',
                            s.NumriKorrespondences
                        ) AS NumriPlote,

                        s.PerdoruesiId
                            AS DerguesiId,

                        CONCAT(
                            derguesi.Emri,
                            N' ',
                            derguesi.Mbiemri
                        ) AS Derguesi,

                       ISNULL(
                        marresit.Emrat,
                        N'Të gjithë'
                    ) AS Marresi,

                        ISNULL(
                            s.Permbajtja,
                            N''
                        ) AS Permbajtja,

                        s.PathDokumenti,

                        s.Klasifikimi,

                        s.DataRegjistrimit

                    FROM Shkresat s

                    INNER JOIN Perdoruesit derguesi
                        ON s.PerdoruesiId =
                           derguesi.PerdoruesiId

                   OUTER APPLY
                    (
                        SELECT
                            STRING_AGG(
                                CONVERT(
                                    NVARCHAR(MAX),
                                    CONCAT(
                                        p.Emri,
                                        N' ',
                                        p.Mbiemri
                                    )
                                ),
                                N', '
                            ) AS Emrat

                        FROM ShkresaPunonjesit sp

                        INNER JOIN Perdoruesit p
                            ON sp.PerdoruesiId =
                               p.PerdoruesiId

                        WHERE sp.ShkresaId =
                              s.ShkresaId
                    ) marresit

                    WHERE s.Viti = @viti
                      AND s.NumriProtokolli =
                          @numriProtokolli

                    ORDER BY
                        s.NumriKorrespondences,
                        s.DataRegjistrimit,
                        s.ShkresaId;";

                SqlParameter[] parameters =
                {
                    new SqlParameter(
                        "@viti",
                        SqlDbType.Int)
                    {
                        Value = viti
                    },

                    new SqlParameter(
                        "@numriProtokolli",
                        SqlDbType.Int)
                    {
                        Value = numriProtokolli
                    }
                };

                DataTable dt =
                    DBHelper.ExecuteQuery(
                        query,
                        parameters);

                pnlBiseda.SuspendLayout();
                pnlBiseda.Controls.Clear();

                pathDokumentiIZgjedhur =
                    string.Empty;

                mesazhiIZgjedhur = null;

                btnHapDokumentin.Enabled = false;

                if (dt.Rows.Count == 0)
                {
                    ShtoMesazhBosh();
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ShtoMesazh(row);
                    }
                }

                pnlBiseda.ResumeLayout();

                RregulloGjeresineEMesazheve();

                pnlBiseda.AutoScrollPosition =
                    new Point(
                        0,
                        pnlBiseda.DisplayRectangle.Height);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Historiku nuk mund të ngarkohej: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ShtoMesazhBosh()
        {
            Label label =
                new Label();

            label.Text =
                "Nuk u gjet asnjë mesazh për këtë çështje.";

            label.Font =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Italic);

            label.ForeColor =
                Color.DimGray;

            label.AutoSize = true;
            label.Margin =
                new Padding(20);

            pnlBiseda.Controls.Add(label);
        }

        private void ShtoMesazh(
            DataRow row)
        {
            int derguesiId =
                Convert.ToInt32(
                    row["DerguesiId"]);

            bool eshteMesazhiIm =
                derguesiId ==
                UserSession.PerdoruesiId;

            string derguesi =
                Convert.ToString(
                    row["Derguesi"])
                ?? string.Empty;

            string marresi =
                Convert.ToString(
                    row["Marresi"])
                ?? string.Empty;

            string numriPlote =
                Convert.ToString(
                    row["NumriPlote"])
                ?? string.Empty;

            string permbajtja =
                Convert.ToString(
                    row["Permbajtja"])
                ?? string.Empty;

            string klasifikimi =
                Convert.ToString(
                    row["Klasifikimi"])
                ?? string.Empty;

            string path =
                row["PathDokumenti"] == DBNull.Value
                    ? string.Empty
                    : Convert.ToString(
                        row["PathDokumenti"])
                      ?? string.Empty;

            DateTime data =
                Convert.ToDateTime(
                    row["DataRegjistrimit"]);

            Panel mbajtesi =
                new Panel();

            mbajtesi.Height = 150;
            mbajtesi.Margin =
                new Padding(8);

            mbajtesi.Tag = path;

            Panel flluska =
                new Panel();

            flluska.Width = 500;
            flluska.Height = 138;

            flluska.BackColor =
                eshteMesazhiIm
                    ? Color.FromArgb(
                        214,
                        239,
                        255)
                    : Color.White;

            flluska.BorderStyle =
                BorderStyle.FixedSingle;

            flluska.Tag = path;

            if (eshteMesazhiIm)
            {
                flluska.Left =
                    Math.Max(
                        10,
                        pnlBiseda.ClientSize.Width -
                        flluska.Width -
                        35);
            }
            else
            {
                flluska.Left = 10;
            }

            flluska.Top = 5;

            Label lblDerguesi =
                new Label();

            lblDerguesi.Text =
                eshteMesazhiIm
                 ? $"Ju → {marresi}"
              : $"{derguesi} → {marresi}";    

            lblDerguesi.Font =
                new Font(
                    "Segoe UI",
                    9,
                    FontStyle.Bold);

            lblDerguesi.ForeColor =
                eshteMesazhiIm
                    ? Color.FromArgb(
                        30,
                        105,
                        160)
                    : Color.FromArgb(
                        45,
                        45,
                        45);

            lblDerguesi.Location =
                new Point(12, 10);

            lblDerguesi.Size =
                new Size(350, 23);

            Label lblData =
                new Label();

            lblData.Text =
                data.ToString(
                    "dd.MM.yyyy HH:mm");

            lblData.Font =
                new Font(
                    "Segoe UI",
                    8);

            lblData.ForeColor =
                Color.DimGray;

            lblData.TextAlign =
                ContentAlignment.MiddleRight;

            lblData.Location =
                new Point(350, 10);

            lblData.Size =
                new Size(135, 23);

            Label lblPermbajtja =
                new Label();

            lblPermbajtja.Text =
                string.IsNullOrWhiteSpace(permbajtja)
                    ? "(Pa përmbajtje)"
                    : permbajtja;

            lblPermbajtja.Font =
                new Font(
                    "Segoe UI",
                    10);

            lblPermbajtja.Location =
                new Point(12, 38);

            lblPermbajtja.Size =
                new Size(470, 55);

            lblPermbajtja.AutoEllipsis = true;

            Label lblInformacioni =
                new Label();

            string dokumenti =
                string.IsNullOrWhiteSpace(path)
                    ? "Pa dokument"
                    : "Ka dokument";

            lblInformacioni.Text =
                $"{numriPlote}  •  " +
                $"{klasifikimi}  •  " +
                dokumenti;

            lblInformacioni.Font =
                new Font(
                    "Segoe UI",
                    8);

            lblInformacioni.ForeColor =
                Color.DimGray;

            lblInformacioni.Location =
                new Point(12, 105);

            lblInformacioni.Size =
                new Size(470, 22);

            flluska.Controls.Add(
                lblDerguesi);

            flluska.Controls.Add(
                lblData);

            flluska.Controls.Add(
                lblPermbajtja);

            flluska.Controls.Add(
                lblInformacioni);

            RegjistroKlikimin(
                mbajtesi,
                flluska,
                path);

            RegjistroKlikimin(
                flluska,
                flluska,
                path);

            RegjistroKlikimin(
                lblDerguesi,
                flluska,
                path);

            RegjistroKlikimin(
                lblData,
                flluska,
                path);

            RegjistroKlikimin(
                lblPermbajtja,
                flluska,
                path);

            RegjistroKlikimin(
                lblInformacioni,
                flluska,
                path);

            mbajtesi.Controls.Add(
                flluska);

            pnlBiseda.Controls.Add(
                mbajtesi);
        }

        private void RegjistroKlikimin(
            Control kontrolli,
            Panel flluska,
            string path)
        {
            kontrolli.Cursor =
                Cursors.Hand;

            kontrolli.Click +=
                (sender, e) =>
                {
                    ZgjidhMesazhin(
                        flluska,
                        path);
                };

            kontrolli.DoubleClick +=
                (sender, e) =>
                {
                    ZgjidhMesazhin(
                        flluska,
                        path);

                    HapDokumentinEZgjedhur();
                };
        }

        private void ZgjidhMesazhin(
            Panel flluska,
            string path)
        {
            if (mesazhiIZgjedhur != null)
            {
                mesazhiIZgjedhur.BorderStyle =
                    BorderStyle.FixedSingle;
            }

            mesazhiIZgjedhur =
                flluska;

            mesazhiIZgjedhur.BorderStyle =
                BorderStyle.Fixed3D;

            pathDokumentiIZgjedhur =
                path;

            btnHapDokumentin.Enabled =
                !string.IsNullOrWhiteSpace(path) &&
                File.Exists(path);
        }

        private void RregulloGjeresineEMesazheve()
        {
            int gjeresiaMbajtesit =
                Math.Max(
                    580,
                    pnlBiseda.ClientSize.Width - 30);

            foreach (Control kontrolli
                     in pnlBiseda.Controls)
            {
                if (kontrolli is not Panel mbajtesi)
                {
                    continue;
                }

                mbajtesi.Width =
                    gjeresiaMbajtesit;

                if (mbajtesi.Controls.Count == 0 ||
                    mbajtesi.Controls[0] is not Panel flluska)
                {
                    continue;
                }

                int gjeresiaFlluskes =
                    Math.Min(
                        500,
                        gjeresiaMbajtesit - 30);

                flluska.Width =
                    gjeresiaFlluskes;

                bool eshteMesazhiIm =
                    flluska.BackColor ==
                    Color.FromArgb(
                        214,
                        239,
                        255);

                flluska.Left =
                    eshteMesazhiIm
                        ? gjeresiaMbajtesit -
                          gjeresiaFlluskes -
                          10
                        : 10;
            }
        }

        private void btnHapDokumentin_Click(
            object? sender,
            EventArgs e)
        {
            HapDokumentinEZgjedhur();
        }

        private void HapDokumentinEZgjedhur()
        {
            if (string.IsNullOrWhiteSpace(
                    pathDokumentiIZgjedhur))
            {
                MessageBox.Show(
                    "Mesazhi i zgjedhur nuk ka dokument.",
                    "Informacion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (!File.Exists(
                    pathDokumentiIZgjedhur))
            {
                MessageBox.Show(
                    "Dokumenti nuk u gjet në adresën:\n" +
                    pathDokumentiIZgjedhur,
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
                        FileName =
                            pathDokumentiIZgjedhur,

                        UseShellExecute =
                            true
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Dokumenti nuk mund të hapej: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
