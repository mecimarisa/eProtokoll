namespace eProtokoll
{
    partial class FormMenaxher
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
            btnShkreseHyrese = new Button();
            btnShkreseDalese = new Button();
            btnRegjistroShkreseBrendshme = new Button();
            btnLibriProtokollit = new Button();
            btnDelegimet = new Button();
            btnRaporteStatistikore = new Button();
            lblMiresevini = new Label();
            pnlHeader = new Panel();
            btnDil = new Button();
            grpShkresat = new GroupBox();
            grpKontrolli = new GroupBox();
            btnCeshtjeMbyllura = new Button();
            pnlHeader.SuspendLayout();
            grpShkresat.SuspendLayout();
            grpKontrolli.SuspendLayout();
            SuspendLayout();
            // 
            // btnShkreseHyrese
            // 
            btnShkreseHyrese.Location = new Point(29, 49);
            btnShkreseHyrese.Name = "btnShkreseHyrese";
            btnShkreseHyrese.Size = new Size(270, 34);
            btnShkreseHyrese.TabIndex = 0;
            btnShkreseHyrese.Text = "Regjistro shkresë hyrëse";
            btnShkreseHyrese.UseVisualStyleBackColor = true;
            btnShkreseHyrese.Click += btnShkreseHyrese_Click;
            // 
            // btnShkreseDalese
            // 
            btnShkreseDalese.Location = new Point(29, 129);
            btnShkreseDalese.Name = "btnShkreseDalese";
            btnShkreseDalese.Size = new Size(270, 34);
            btnShkreseDalese.TabIndex = 1;
            btnShkreseDalese.Text = "Regjistro shkresë dalëse";
            btnShkreseDalese.UseVisualStyleBackColor = true;
            btnShkreseDalese.Click += btnShkreseDalese_Click;
            // 
            // btnRegjistroShkreseBrendshme
            // 
            btnRegjistroShkreseBrendshme.Location = new Point(29, 207);
            btnRegjistroShkreseBrendshme.Name = "btnRegjistroShkreseBrendshme";
            btnRegjistroShkreseBrendshme.Size = new Size(270, 34);
            btnRegjistroShkreseBrendshme.TabIndex = 2;
            btnRegjistroShkreseBrendshme.Text = "Regjistro shkresë të brendshme";
            btnRegjistroShkreseBrendshme.UseVisualStyleBackColor = true;
            btnRegjistroShkreseBrendshme.Click += btnRegjistroShkreseBrendshme_Click;
            // 
            // btnLibriProtokollit
            // 
            btnLibriProtokollit.Location = new Point(30, 49);
            btnLibriProtokollit.Name = "btnLibriProtokollit";
            btnLibriProtokollit.Size = new Size(270, 34);
            btnLibriProtokollit.TabIndex = 3;
            btnLibriProtokollit.Text = "Libri i Protokollit";
            btnLibriProtokollit.UseVisualStyleBackColor = true;
            btnLibriProtokollit.Click += btnLibriProtokollit_Click;
            // 
            // btnDelegimet
            // 
            btnDelegimet.Location = new Point(30, 129);
            btnDelegimet.Name = "btnDelegimet";
            btnDelegimet.Size = new Size(270, 34);
            btnDelegimet.TabIndex = 4;
            btnDelegimet.Text = "Delegimi i shkresave";
            btnDelegimet.UseVisualStyleBackColor = true;
            btnDelegimet.Click += btnDelegimet_Click;
            // 
            // btnRaporteStatistikore
            // 
            btnRaporteStatistikore.Location = new Point(30, 197);
            btnRaporteStatistikore.Name = "btnRaporteStatistikore";
            btnRaporteStatistikore.Size = new Size(270, 34);
            btnRaporteStatistikore.TabIndex = 5;
            btnRaporteStatistikore.Text = "Raporte statistikore";
            btnRaporteStatistikore.UseVisualStyleBackColor = true;
            btnRaporteStatistikore.Click += btnRaporteStatistikore_Click;
            // 
            // lblMiresevini
            // 
            lblMiresevini.AutoSize = true;
            lblMiresevini.BackColor = Color.Transparent;
            lblMiresevini.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMiresevini.ForeColor = Color.White;
            lblMiresevini.Location = new Point(205, 19);
            lblMiresevini.Name = "lblMiresevini";
            lblMiresevini.Size = new Size(219, 38);
            lblMiresevini.TabIndex = 6;
            lblMiresevini.Text = "Mirë se erdhët";
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.SteelBlue;
            pnlHeader.Controls.Add(btnDil);
            pnlHeader.Controls.Add(lblMiresevini);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(851, 77);
            pnlHeader.TabIndex = 7;
            // 
            // btnDil
            // 
            btnDil.Location = new Point(783, 19);
            btnDil.Name = "btnDil";
            btnDil.Size = new Size(65, 38);
            btnDil.TabIndex = 10;
            btnDil.Text = "Dil";
            btnDil.UseVisualStyleBackColor = true;
            btnDil.Click += btnDil_Click;
            // 
            // grpShkresat
            // 
            grpShkresat.Controls.Add(btnShkreseHyrese);
            grpShkresat.Controls.Add(btnShkreseDalese);
            grpShkresat.Controls.Add(btnRegjistroShkreseBrendshme);
            grpShkresat.Location = new Point(30, 144);
            grpShkresat.Name = "grpShkresat";
            grpShkresat.Size = new Size(360, 267);
            grpShkresat.TabIndex = 8;
            grpShkresat.TabStop = false;
            grpShkresat.Text = " Menaxhimi i shkresave";
            // 
            // grpKontrolli
            // 
            grpKontrolli.Controls.Add(btnCeshtjeMbyllura);
            grpKontrolli.Controls.Add(btnLibriProtokollit);
            grpKontrolli.Controls.Add(btnDelegimet);
            grpKontrolli.Controls.Add(btnRaporteStatistikore);
            grpKontrolli.Location = new Point(477, 144);
            grpKontrolli.Name = "grpKontrolli";
            grpKontrolli.Size = new Size(347, 305);
            grpKontrolli.TabIndex = 9;
            grpKontrolli.TabStop = false;
            grpKontrolli.Text = "Kontrolli dhe raportimi";
            // 
            // btnCeshtjeMbyllura
            // 
            btnCeshtjeMbyllura.Location = new Point(30, 265);
            btnCeshtjeMbyllura.Name = "btnCeshtjeMbyllura";
            btnCeshtjeMbyllura.Size = new Size(270, 34);
            btnCeshtjeMbyllura.TabIndex = 6;
            btnCeshtjeMbyllura.Text = "Çështjet e mbyllura";
            btnCeshtjeMbyllura.UseVisualStyleBackColor = true;
            btnCeshtjeMbyllura.Click += btnCeshtjeMbyllura_Click;
            // 
            // FormMenaxher
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(851, 494);
            Controls.Add(grpKontrolli);
            Controls.Add(grpShkresat);
            Controls.Add(pnlHeader);
            Name = "FormMenaxher";
            Text = "Paneli menaxherit";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            grpShkresat.ResumeLayout(false);
            grpKontrolli.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnShkreseHyrese;
        private Button btnShkreseDalese;
        private Button btnRegjistroShkreseBrendshme;
        private Button btnLibriProtokollit;
        private Button btnDelegimet;
        private Button btnRaporteStatistikore;
        private Label lblMiresevini;
        private Panel pnlHeader;
        private GroupBox grpShkresat;
        private GroupBox grpKontrolli;
        private Button btnCeshtjeMbyllura;
        private Button btnDil;
    }
}