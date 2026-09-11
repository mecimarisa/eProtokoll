namespace eProtokoll
{
    partial class FormPunonjes
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
            lblPerdoruesi = new Label();
            dgvDetyrat = new DataGridView();
            btnRifresko = new Button();
            btnFilloTrajtimin = new Button();
            btnHapDokumentin = new Button();
            label2 = new Label();
            txtPergjigja = new TextBox();
            btnPerfundo = new Button();
            lblDokumentPergjigje = new Label();
            txtPathPergjigje = new TextBox();
            btnZgjidhDokumentPergjigje = new Button();
            btnShkreseBrendshme = new Button();
            btnMbyllCeshtjen = new Button();
            pnlHeader = new Panel();
            btnDil = new Button();
            grpDetyrat = new GroupBox();
            btnHistorikuBisedes = new Button();
            btnCeshtjeMbyllura = new Button();
            btnSkanoDokumentPergjigje = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDetyrat).BeginInit();
            pnlHeader.SuspendLayout();
            grpDetyrat.SuspendLayout();
            SuspendLayout();
            // 
            // lblPerdoruesi
            // 
            lblPerdoruesi.AutoSize = true;
            lblPerdoruesi.BackColor = Color.Transparent;
            lblPerdoruesi.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold);
            lblPerdoruesi.ForeColor = Color.White;
            lblPerdoruesi.Location = new Point(420, 24);
            lblPerdoruesi.Name = "lblPerdoruesi";
            lblPerdoruesi.Size = new Size(226, 48);
            lblPerdoruesi.TabIndex = 0;
            lblPerdoruesi.Text = "Mirë se vini";
            // 
            // dgvDetyrat
            // 
            dgvDetyrat.AllowUserToAddRows = false;
            dgvDetyrat.AllowUserToDeleteRows = false;
            dgvDetyrat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetyrat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetyrat.Location = new Point(6, 30);
            dgvDetyrat.MultiSelect = false;
            dgvDetyrat.Name = "dgvDetyrat";
            dgvDetyrat.ReadOnly = true;
            dgvDetyrat.RowHeadersWidth = 62;
            dgvDetyrat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetyrat.Size = new Size(1339, 251);
            dgvDetyrat.TabIndex = 1;
            // 
            // btnRifresko
            // 
            btnRifresko.Location = new Point(7, 316);
            btnRifresko.Name = "btnRifresko";
            btnRifresko.Size = new Size(112, 34);
            btnRifresko.TabIndex = 3;
            btnRifresko.Text = "Rifresko";
            btnRifresko.UseVisualStyleBackColor = true;
            // 
            // btnFilloTrajtimin
            // 
            btnFilloTrajtimin.Location = new Point(355, 316);
            btnFilloTrajtimin.Name = "btnFilloTrajtimin";
            btnFilloTrajtimin.Size = new Size(127, 34);
            btnFilloTrajtimin.TabIndex = 4;
            btnFilloTrajtimin.Text = "Fillo trajtimin";
            btnFilloTrajtimin.UseVisualStyleBackColor = true;
            // 
            // btnHapDokumentin
            // 
            btnHapDokumentin.Location = new Point(155, 316);
            btnHapDokumentin.Name = "btnHapDokumentin";
            btnHapDokumentin.Size = new Size(157, 34);
            btnHapDokumentin.TabIndex = 5;
            btnHapDokumentin.Text = "Hap dokumentin";
            btnHapDokumentin.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(26, 511);
            label2.Name = "label2";
            label2.Size = new Size(87, 25);
            label2.TabIndex = 6;
            label2.Text = "Përgjigjja:";
            // 
            // txtPergjigja
            // 
            txtPergjigja.Location = new Point(26, 539);
            txtPergjigja.Multiline = true;
            txtPergjigja.Name = "txtPergjigja";
            txtPergjigja.Size = new Size(1078, 86);
            txtPergjigja.TabIndex = 7;
            // 
            // btnPerfundo
            // 
            btnPerfundo.Location = new Point(1151, 562);
            btnPerfundo.Name = "btnPerfundo";
            btnPerfundo.Size = new Size(159, 34);
            btnPerfundo.TabIndex = 8;
            btnPerfundo.Text = "Dërgo përgjigjen";
            btnPerfundo.UseVisualStyleBackColor = true;
            // 
            // lblDokumentPergjigje
            // 
            lblDokumentPergjigje.AutoSize = true;
            lblDokumentPergjigje.Location = new Point(26, 646);
            lblDokumentPergjigje.Name = "lblDokumentPergjigje";
            lblDokumentPergjigje.Size = new Size(195, 25);
            lblDokumentPergjigje.TabIndex = 9;
            lblDokumentPergjigje.Text = "Dokumenti i përgjigjes:";
            // 
            // txtPathPergjigje
            // 
            txtPathPergjigje.Location = new Point(237, 643);
            txtPathPergjigje.Name = "txtPathPergjigje";
            txtPathPergjigje.ReadOnly = true;
            txtPathPergjigje.Size = new Size(287, 31);
            txtPathPergjigje.TabIndex = 10;
            // 
            // btnZgjidhDokumentPergjigje
            // 
            btnZgjidhDokumentPergjigje.Location = new Point(562, 643);
            btnZgjidhDokumentPergjigje.Name = "btnZgjidhDokumentPergjigje";
            btnZgjidhDokumentPergjigje.Size = new Size(159, 34);
            btnZgjidhDokumentPergjigje.TabIndex = 11;
            btnZgjidhDokumentPergjigje.Text = "Zgjidh dokument";
            btnZgjidhDokumentPergjigje.UseVisualStyleBackColor = true;
            // 
            // btnShkreseBrendshme
            // 
            btnShkreseBrendshme.Location = new Point(710, 316);
            btnShkreseBrendshme.Name = "btnShkreseBrendshme";
            btnShkreseBrendshme.Size = new Size(235, 34);
            btnShkreseBrendshme.TabIndex = 12;
            btnShkreseBrendshme.Text = "Krijo shkresë të brendshme";
            btnShkreseBrendshme.UseVisualStyleBackColor = true;
            // 
            // btnMbyllCeshtjen
            // 
            btnMbyllCeshtjen.Location = new Point(524, 316);
            btnMbyllCeshtjen.Name = "btnMbyllCeshtjen";
            btnMbyllCeshtjen.Size = new Size(146, 34);
            btnMbyllCeshtjen.TabIndex = 13;
            btnMbyllCeshtjen.Text = "Mbyll çështjen";
            btnMbyllCeshtjen.UseVisualStyleBackColor = true;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.SteelBlue;
            pnlHeader.Controls.Add(btnDil);
            pnlHeader.Controls.Add(lblPerdoruesi);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1383, 99);
            pnlHeader.TabIndex = 14;
            // 
            // btnDil
            // 
            btnDil.Location = new Point(1306, 24);
            btnDil.Name = "btnDil";
            btnDil.Size = new Size(65, 38);
            btnDil.TabIndex = 16;
            btnDil.Text = "Dil";
            btnDil.UseVisualStyleBackColor = true;
            btnDil.Click += btnDil_Click;
            // 
            // grpDetyrat
            // 
            grpDetyrat.Controls.Add(btnHistorikuBisedes);
            grpDetyrat.Controls.Add(btnCeshtjeMbyllura);
            grpDetyrat.Controls.Add(dgvDetyrat);
            grpDetyrat.Controls.Add(btnRifresko);
            grpDetyrat.Controls.Add(btnShkreseBrendshme);
            grpDetyrat.Controls.Add(btnMbyllCeshtjen);
            grpDetyrat.Controls.Add(btnHapDokumentin);
            grpDetyrat.Controls.Add(btnFilloTrajtimin);
            grpDetyrat.Location = new Point(20, 118);
            grpDetyrat.Name = "grpDetyrat";
            grpDetyrat.Size = new Size(1351, 366);
            grpDetyrat.TabIndex = 15;
            grpDetyrat.TabStop = false;
            grpDetyrat.Text = "Detyrat e mia";
            // 
            // btnHistorikuBisedes
            // 
            btnHistorikuBisedes.Location = new Point(974, 316);
            btnHistorikuBisedes.Name = "btnHistorikuBisedes";
            btnHistorikuBisedes.Size = new Size(136, 34);
            btnHistorikuBisedes.TabIndex = 15;
            btnHistorikuBisedes.Text = "Shiko bisedën";
            btnHistorikuBisedes.UseVisualStyleBackColor = true;
            btnHistorikuBisedes.Click += btnHistorikuBisedes_Click;
            // 
            // btnCeshtjeMbyllura
            // 
            btnCeshtjeMbyllura.Location = new Point(1135, 316);
            btnCeshtjeMbyllura.Name = "btnCeshtjeMbyllura";
            btnCeshtjeMbyllura.Size = new Size(210, 34);
            btnCeshtjeMbyllura.TabIndex = 14;
            btnCeshtjeMbyllura.Text = "Çështjet e mbyllura";
            btnCeshtjeMbyllura.UseVisualStyleBackColor = true;
            btnCeshtjeMbyllura.Click += btnCeshtjeMbyllura_Click;
            // 
            // btnSkanoDokumentPergjigje
            // 
            btnSkanoDokumentPergjigje.Location = new Point(739, 643);
            btnSkanoDokumentPergjigje.Name = "btnSkanoDokumentPergjigje";
            btnSkanoDokumentPergjigje.Size = new Size(179, 34);
            btnSkanoDokumentPergjigje.TabIndex = 16;
            btnSkanoDokumentPergjigje.Text = "Skano dokumentin";
            btnSkanoDokumentPergjigje.UseVisualStyleBackColor = true;
            btnSkanoDokumentPergjigje.Click += btnSkanoDokumentPergjigje_Click;
            // 
            // FormPunonjes
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1383, 710);
            Controls.Add(btnSkanoDokumentPergjigje);
            Controls.Add(grpDetyrat);
            Controls.Add(pnlHeader);
            Controls.Add(btnZgjidhDokumentPergjigje);
            Controls.Add(txtPathPergjigje);
            Controls.Add(lblDokumentPergjigje);
            Controls.Add(btnPerfundo);
            Controls.Add(txtPergjigja);
            Controls.Add(label2);
            Name = "FormPunonjes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Paneli i Punonjësit";
            ((System.ComponentModel.ISupportInitialize)dgvDetyrat).EndInit();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            grpDetyrat.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPerdoruesi;
        private DataGridView dgvDetyrat;
        private Button btnRifresko;
        private Button btnFilloTrajtimin;
        private Button btnHapDokumentin;
        private Label label2;
        private TextBox txtPergjigja;
        private Button btnPerfundo;
        private Label lblDokumentPergjigje;
        private TextBox txtPathPergjigje;
        private Button btnZgjidhDokumentPergjigje;
        private Button btnShkreseBrendshme;
        private Button btnMbyllCeshtjen;
        private Panel pnlHeader;
        private GroupBox grpDetyrat;
        private Button btnCeshtjeMbyllura;
        private Button btnHistorikuBisedes;
        private Button btnDil;
        private Button btnSkanoDokumentPergjigje;
    }
}