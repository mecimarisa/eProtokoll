using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace eProtokoll
{
    public partial class FormMenaxher : Form
    {
        public FormMenaxher()
        {
            InitializeComponent();

            lblMiresevini.Text =
        $"Mirë se erdhët, {UserSession.EmriPlote}";
        }

        private void btnShkreseHyrese_Click(object sender, EventArgs e)
        {
            using (FormShkreseHyrese forma =
          new FormShkreseHyrese())
            {
                forma.ShowDialog();
            }
        }

        private void btnShkreseDalese_Click(object sender, EventArgs e)
        {
            using (FormShkreseDalese forma =
          new FormShkreseDalese())
            {
                forma.ShowDialog();
            }
        }

        private void btnRegjistroShkreseBrendshme_Click(object sender, EventArgs e)
        {
            using (FormShkreseBrendshme forma =
           new FormShkreseBrendshme())
            {
                forma.ShowDialog();
            }
        }

        private void btnLibriProtokollit_Click(object sender, EventArgs e)
        {
            using (FormLibriProtokollit forma =
          new FormLibriProtokollit())
            {
                forma.ShowDialog();
            }
        }

        private void btnDelegimet_Click(object sender, EventArgs e)
        {
            using (FormDelegimet forma =
           new FormDelegimet())
            {
                forma.ShowDialog();
            }
        }

        private void btnRaporteStatistikore_Click(object sender, EventArgs e)
        {
            using FormRaporteStatistikore forma =
        new FormRaporteStatistikore();

            forma.ShowDialog();
        }

        private void btnCeshtjeMbyllura_Click(object sender, EventArgs e)
        {
            using FormCeshtjeMbyllura forma =
        new FormCeshtjeMbyllura();

            forma.ShowDialog();
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
    }
}
