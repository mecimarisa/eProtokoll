using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace eProtokoll
{
    public partial class FormPerdoruesit : Form
    {
        private int perdoruesiIdIZgjedhur = 0;

        public FormPerdoruesit()
        {
            InitializeComponent();

            Load += FormPerdoruesit_Load;
            dgvPerdoruesit.SelectionChanged +=
                dgvPerdoruesit_SelectionChanged;

            btnShto.Click += btnShto_Click;
            btnModifiko.Click += btnModifiko_Click;
            btnAktivizoCaktivizo.Click +=
                btnAktivizoCaktivizo_Click;
            btnPastro.Click += btnPastro_Click;
        }

        private void FormPerdoruesit_Load(
            object? sender,
            EventArgs e)
        {
            KonfiguroGrid();
            NgarkoRolet();
            NgarkoPerdoruesit();
            PastroFushat();
        }

        private void KonfiguroGrid()
        {
            dgvPerdoruesit.ReadOnly = true;
            dgvPerdoruesit.AllowUserToAddRows = false;
            dgvPerdoruesit.AllowUserToDeleteRows = false;
            dgvPerdoruesit.MultiSelect = false;

            dgvPerdoruesit.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvPerdoruesit.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void NgarkoRolet()
        {
            try
            {
                string query = @"
                    SELECT
                        RolId,
                        EmriRolit
                    FROM Rolet
                    ORDER BY RolId;";

                DataTable dt =
                    DBHelper.ExecuteQuery(query);

                cmbRoli.DataSource = dt;
                cmbRoli.DisplayMember = "EmriRolit";
                cmbRoli.ValueMember = "RolId";
                cmbRoli.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Rolet nuk u ngarkuan: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void NgarkoPerdoruesit()
        {
            try
            {
                string query = @"
                    SELECT
                        p.PerdoruesiId,
                        p.Emri,
                        p.Mbiemri,
                        p.Username,
                        p.RolId,
                        r.EmriRolit AS Roli,

                        CASE
                            WHEN p.Aktiv = 1
                                THEN N'Aktiv'
                            ELSE N'Jo aktiv'
                        END AS Statusi,

                        p.Aktiv

                    FROM Perdoruesit p

                    INNER JOIN Rolet r
                        ON p.RolId = r.RolId

                    ORDER BY
                        p.Emri,
                        p.Mbiemri;";

                DataTable dt =
                    DBHelper.ExecuteQuery(query);

                dgvPerdoruesit.DataSource = dt;

                if (dgvPerdoruesit.Columns.Contains(
                        "PerdoruesiId"))
                {
                    dgvPerdoruesit
                        .Columns["PerdoruesiId"]
                        .Visible = false;
                }

                if (dgvPerdoruesit.Columns.Contains("RolId"))
                {
                    dgvPerdoruesit
                        .Columns["RolId"]
                        .Visible = false;
                }

                if (dgvPerdoruesit.Columns.Contains("Aktiv"))
                {
                    dgvPerdoruesit
                        .Columns["Aktiv"]
                        .Visible = false;
                }

                dgvPerdoruesit.ClearSelection();
                dgvPerdoruesit.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Përdoruesit nuk u ngarkuan: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dgvPerdoruesit_SelectionChanged(
            object? sender,
            EventArgs e)
        {
            if (dgvPerdoruesit.CurrentRow == null)
            {
                return;
            }

            DataGridViewRow row =
                dgvPerdoruesit.CurrentRow;

            if (row.Cells["PerdoruesiId"].Value == null ||
                row.Cells["PerdoruesiId"].Value == DBNull.Value)
            {
                return;
            }

            perdoruesiIdIZgjedhur =
                Convert.ToInt32(
                    row.Cells["PerdoruesiId"].Value);

            txtEmri.Text =
                Convert.ToString(
                    row.Cells["Emri"].Value)
                ?? string.Empty;

            txtMbiemri.Text =
                Convert.ToString(
                    row.Cells["Mbiemri"].Value)
                ?? string.Empty;

            txtUsername.Text =
                Convert.ToString(
                    row.Cells["Username"].Value)
                ?? string.Empty;

            int rolId =
                Convert.ToInt32(
                    row.Cells["RolId"].Value);

            cmbRoli.SelectedValue = rolId;

            chkAktiv.Checked =
                Convert.ToBoolean(
                    row.Cells["Aktiv"].Value);

            // Fjalekalimi ekzistues nuk shfaqet,
            // Ne modifikim,mund te lihet bosh nese nuk ndryshohet.
            txtPassword.Clear();
        }

        private void btnShto_Click(
            object? sender,
            EventArgs e)
        {
            if (!KontrolloFushat(perShtim: true))
            {
                return;
            }

            string emri = txtEmri.Text.Trim();
            string mbiemri = txtMbiemri.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            int rolId = Convert.ToInt32(cmbRoli.SelectedValue);
            bool aktiv = chkAktiv.Checked;

            if (EkzistonUsername(username, 0))
            {
                MessageBox.Show(
                    "Ekziston një përdorues me këtë username!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtUsername.Focus();
                return;
            }

            try
            {
                string query = @"
                    INSERT INTO Perdoruesit
                    (
                        Emri,
                        Mbiemri,
                        Username,
                        Password,
                        RolId,
                        Aktiv,
                        DataKrijimit
                    )
                    VALUES
                    (
                        @emri,
                        @mbiemri,
                        @username,
                        @password,
                        @rolId,
                        @aktiv,
                        GETDATE()
                    );";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@emri", emri),
                    new SqlParameter("@mbiemri", mbiemri),
                    new SqlParameter("@username", username),
                    new SqlParameter("@password", password),
                    new SqlParameter("@rolId", rolId),
                    new SqlParameter("@aktiv", aktiv)
                };

                DBHelper.ExecuteNonQuery(
                    query,
                    parameters);

                MessageBox.Show(
                    "Përdoruesi u regjistrua me sukses!",
                    "Sukses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                NgarkoPerdoruesit();
                PastroFushat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Përdoruesi nuk u regjistrua: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnModifiko_Click(
            object? sender,
            EventArgs e)
        {
            if (perdoruesiIdIZgjedhur <= 0)
            {
                MessageBox.Show(
                    "Zgjidhni përdoruesin që dëshironi të modifikoni!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!KontrolloFushat(perShtim: false))
            {
                return;
            }

            string emri = txtEmri.Text.Trim();
            string mbiemri = txtMbiemri.Text.Trim();
            string username = txtUsername.Text.Trim();
            string passwordIRi = txtPassword.Text;
            int rolId = Convert.ToInt32(cmbRoli.SelectedValue);
            bool aktiv = chkAktiv.Checked;

            if (EkzistonUsername(
                    username,
                    perdoruesiIdIZgjedhur))
            {
                MessageBox.Show(
                    "Ekziston një përdorues tjetër me këtë username!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtUsername.Focus();
                return;
            }

            try
            {
                string query;

                SqlParameter[] parameters;

                if (string.IsNullOrWhiteSpace(passwordIRi))
                {
                    // Nese fjalekalimi lihet bosh,
                    // ruhet fjalekalimi i meparshem.
                    query = @"
                        UPDATE Perdoruesit
                        SET
                            Emri = @emri,
                            Mbiemri = @mbiemri,
                            Username = @username,
                            RolId = @rolId,
                            Aktiv = @aktiv
                        WHERE PerdoruesiId = @perdoruesiId;";

                    parameters =
                    [
                        new SqlParameter("@emri", emri),
                        new SqlParameter("@mbiemri", mbiemri),
                        new SqlParameter("@username", username),
                        new SqlParameter("@rolId", rolId),
                        new SqlParameter("@aktiv", aktiv),
                        new SqlParameter(
                            "@perdoruesiId",
                            perdoruesiIdIZgjedhur)
                    ];
                }
                else
                {
                    
                    query = @"
                        UPDATE Perdoruesit
                        SET
                            Emri = @emri,
                            Mbiemri = @mbiemri,
                            Username = @username,
                            Password = @password,
                            RolId = @rolId,
                            Aktiv = @aktiv
                        WHERE PerdoruesiId = @perdoruesiId;";

                    parameters =
                    [
                        new SqlParameter("@emri", emri),
                        new SqlParameter("@mbiemri", mbiemri),
                        new SqlParameter("@username", username),
                        new SqlParameter(
                            "@password",
                            passwordIRi),
                        new SqlParameter("@rolId", rolId),
                        new SqlParameter("@aktiv", aktiv),
                        new SqlParameter(
                            "@perdoruesiId",
                            perdoruesiIdIZgjedhur)
                    ];
                }

                int ndryshuar =
                    DBHelper.ExecuteNonQuery(
                        query,
                        parameters);

                if (ndryshuar == 0)
                {
                    MessageBox.Show(
                        "Përdoruesi nuk u gjet.",
                        "Kujdes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                MessageBox.Show(
                    "Përdoruesi u modifikua me sukses!",
                    "Sukses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                NgarkoPerdoruesit();
                PastroFushat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Përdoruesi nuk u modifikua: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnAktivizoCaktivizo_Click(
            object? sender,
            EventArgs e)
        {
            if (perdoruesiIdIZgjedhur <= 0)
            {
                MessageBox.Show(
                    "Zgjidhni një përdorues!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (perdoruesiIdIZgjedhur ==
                UserSession.PerdoruesiId)
            {
                MessageBox.Show(
                    "Nuk mund të çaktivizoni llogarinë " +
                    "me të cilën jeni futur në sistem!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            bool aktualishtAktiv =
                chkAktiv.Checked;

            bool statusiIRi =
                !aktualishtAktiv;

            string veprimi =
                statusiIRi
                    ? "aktivizoni"
                    : "çaktivizoni";

            DialogResult konfirmim =
                MessageBox.Show(
                    $"Dëshironi ta {veprimi} këtë përdorues?",
                    "Konfirmo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (konfirmim != DialogResult.Yes)
            {
                return;
            }

            try
            {
                string query = @"
                    UPDATE Perdoruesit
                    SET Aktiv = @aktiv
                    WHERE PerdoruesiId = @perdoruesiId;";

                SqlParameter[] parameters =
                {
                    new SqlParameter(
                        "@aktiv",
                        statusiIRi),

                    new SqlParameter(
                        "@perdoruesiId",
                        perdoruesiIdIZgjedhur)
                };

                DBHelper.ExecuteNonQuery(
                    query,
                    parameters);

                MessageBox.Show(
                    statusiIRi
                        ? "Përdoruesi u aktivizua."
                        : "Përdoruesi u çaktivizua.",
                    "Sukses",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                NgarkoPerdoruesit();
                PastroFushat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Statusi nuk u ndryshua: " +
                    ex.Message,
                    "Gabim",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnPastro_Click(
            object? sender,
            EventArgs e)
        {
            PastroFushat();
        }

        private bool KontrolloFushat(bool perShtim)
        {
            if (string.IsNullOrWhiteSpace(txtEmri.Text))
            {
                MessageBox.Show(
                    "Shkruani emrin!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtEmri.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMbiemri.Text))
            {
                MessageBox.Show(
                    "Shkruani mbiemrin!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtMbiemri.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show(
                    "Shkruani username-in!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtUsername.Focus();
                return false;
            }

            if (perShtim &&
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(
                    "Shkruani fjalëkalimin!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPassword.Focus();
                return false;
            }

            if (cmbRoli.SelectedValue == null)
            {
                MessageBox.Show(
                    "Zgjidhni rolin!",
                    "Kujdes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbRoli.Focus();
                return false;
            }

            return true;
        }

        private bool EkzistonUsername(
            string username,
            int perdoruesiIdPerTuPerjashtuar)
        {
            string query = @"
                SELECT PerdoruesiId
                FROM Perdoruesit
                WHERE Username = @username
                  AND PerdoruesiId <> @perdoruesiId;";

            SqlParameter[] parameters =
            {
                new SqlParameter(
                    "@username",
                    username),

                new SqlParameter(
                    "@perdoruesiId",
                    perdoruesiIdPerTuPerjashtuar)
            };

            DataTable dt =
                DBHelper.ExecuteQuery(
                    query,
                    parameters);

            return dt.Rows.Count > 0;
        }

        private void PastroFushat()
        {
            perdoruesiIdIZgjedhur = 0;

            txtEmri.Clear();
            txtMbiemri.Clear();
            txtUsername.Clear();
            txtPassword.Clear();

            cmbRoli.SelectedIndex = -1;
            chkAktiv.Checked = true;

            dgvPerdoruesit.ClearSelection();
            dgvPerdoruesit.CurrentCell = null;

            txtEmri.Focus();
        }
    }
}