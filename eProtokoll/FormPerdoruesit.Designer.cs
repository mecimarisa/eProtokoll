namespace eProtokoll
{
    partial class FormPerdoruesit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvPerdoruesit = new DataGridView();
            lblEmri = new Label();
            txtEmri = new TextBox();
            lblMbiemri = new Label();
            txtMbiemri = new TextBox();
            txtUsername = new TextBox();
            lblUsername = new Label();
            txtPassword = new TextBox();
            lblPassword = new Label();
            lblRoli = new Label();
            cmbRoli = new ComboBox();
            chkAktiv = new CheckBox();
            btnShto = new Button();
            btnAktivizoCaktivizo = new Button();
            btnModifiko = new Button();
            btnPastro = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPerdoruesit).BeginInit();
            SuspendLayout();
            // 
            // dgvPerdoruesit
            // 
            dgvPerdoruesit.AllowUserToAddRows = false;
            dgvPerdoruesit.AllowUserToDeleteRows = false;
            dgvPerdoruesit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPerdoruesit.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPerdoruesit.Location = new Point(12, 45);
            dgvPerdoruesit.MultiSelect = false;
            dgvPerdoruesit.Name = "dgvPerdoruesit";
            dgvPerdoruesit.ReadOnly = true;
            dgvPerdoruesit.RowHeadersWidth = 62;
            dgvPerdoruesit.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPerdoruesit.Size = new Size(904, 115);
            dgvPerdoruesit.TabIndex = 0;
            // 
            // lblEmri
            // 
            lblEmri.AutoSize = true;
            lblEmri.Location = new Point(19, 199);
            lblEmri.Name = "lblEmri";
            lblEmri.Size = new Size(51, 25);
            lblEmri.TabIndex = 1;
            lblEmri.Text = "Emri:";
            // 
            // txtEmri
            // 
            txtEmri.Location = new Point(81, 202);
            txtEmri.Name = "txtEmri";
            txtEmri.Size = new Size(150, 31);
            txtEmri.TabIndex = 2;
            // 
            // lblMbiemri
            // 
            lblMbiemri.AutoSize = true;
            lblMbiemri.Location = new Point(317, 205);
            lblMbiemri.Name = "lblMbiemri";
            lblMbiemri.Size = new Size(82, 25);
            lblMbiemri.TabIndex = 3;
            lblMbiemri.Text = "Mbiemri:";
            // 
            // txtMbiemri
            // 
            txtMbiemri.Location = new Point(405, 205);
            txtMbiemri.Name = "txtMbiemri";
            txtMbiemri.Size = new Size(150, 31);
            txtMbiemri.TabIndex = 4;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(749, 205);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(150, 31);
            txtUsername.TabIndex = 6;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(648, 208);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(95, 25);
            lblUsername.TabIndex = 5;
            lblUsername.Text = "Username:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(114, 282);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(150, 31);
            txtPassword.TabIndex = 8;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(13, 285);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(97, 25);
            lblPassword.TabIndex = 7;
            lblPassword.Text = "Fjalëkalimi:";
            // 
            // lblRoli
            // 
            lblRoli.AutoSize = true;
            lblRoli.Location = new Point(317, 288);
            lblRoli.Name = "lblRoli";
            lblRoli.Size = new Size(45, 25);
            lblRoli.TabIndex = 9;
            lblRoli.Text = "Roli:";
            // 
            // cmbRoli
            // 
            cmbRoli.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRoli.FormattingEnabled = true;
            cmbRoli.Location = new Point(402, 289);
            cmbRoli.Name = "cmbRoli";
            cmbRoli.Size = new Size(182, 33);
            cmbRoli.TabIndex = 10;
            // 
            // chkAktiv
            // 
            chkAktiv.AutoSize = true;
            chkAktiv.Checked = true;
            chkAktiv.CheckState = CheckState.Checked;
            chkAktiv.Location = new Point(648, 291);
            chkAktiv.Name = "chkAktiv";
            chkAktiv.Size = new Size(159, 29);
            chkAktiv.TabIndex = 11;
            chkAktiv.Text = "Përdorues aktiv";
            chkAktiv.UseVisualStyleBackColor = true;
            // 
            // btnShto
            // 
            btnShto.Location = new Point(70, 423);
            btnShto.Name = "btnShto";
            btnShto.Size = new Size(149, 34);
            btnShto.TabIndex = 12;
            btnShto.Text = "Shto përdorues";
            btnShto.UseVisualStyleBackColor = true;
            // 
            // btnAktivizoCaktivizo
            // 
            btnAktivizoCaktivizo.Location = new Point(494, 423);
            btnAktivizoCaktivizo.Name = "btnAktivizoCaktivizo";
            btnAktivizoCaktivizo.Size = new Size(175, 34);
            btnAktivizoCaktivizo.TabIndex = 13;
            btnAktivizoCaktivizo.Text = "Aktivizo/Çaktivizo";
            btnAktivizoCaktivizo.UseVisualStyleBackColor = true;
            // 
            // btnModifiko
            // 
            btnModifiko.Location = new Point(285, 423);
            btnModifiko.Name = "btnModifiko";
            btnModifiko.Size = new Size(149, 34);
            btnModifiko.TabIndex = 14;
            btnModifiko.Text = "Modifiko";
            btnModifiko.UseVisualStyleBackColor = true;
            // 
            // btnPastro
            // 
            btnPastro.Location = new Point(707, 423);
            btnPastro.Name = "btnPastro";
            btnPastro.Size = new Size(175, 34);
            btnPastro.TabIndex = 15;
            btnPastro.Text = "Pastro fushat";
            btnPastro.UseVisualStyleBackColor = true;
            // 
            // FormPerdoruesit
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(928, 544);
            Controls.Add(btnPastro);
            Controls.Add(btnModifiko);
            Controls.Add(btnAktivizoCaktivizo);
            Controls.Add(btnShto);
            Controls.Add(chkAktiv);
            Controls.Add(cmbRoli);
            Controls.Add(lblRoli);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            Controls.Add(txtMbiemri);
            Controls.Add(lblMbiemri);
            Controls.Add(txtEmri);
            Controls.Add(lblEmri);
            Controls.Add(dgvPerdoruesit);
            Name = "FormPerdoruesit";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Menaxhimi i përdoruesve";
            ((System.ComponentModel.ISupportInitialize)dgvPerdoruesit).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPerdoruesit;
        private Label lblEmri;
        private TextBox txtEmri;
        private Label lblMbiemri;
        private TextBox txtMbiemri;
        private TextBox txtUsername;
        private Label lblUsername;
        private TextBox txtPassword;
        private Label lblPassword;
        private Label lblRoli;
        private ComboBox cmbRoli;
        private CheckBox chkAktiv;
        private Button btnShto;
        private Button btnAktivizoCaktivizo;
        private Button btnModifiko;
        private Button btnPastro;
    }
}