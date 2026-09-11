namespace eProtokoll
{
    partial class FormDelegimet
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
            dgvShkresat = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            cmbPunonjesi = new ComboBox();
            label3 = new Label();
            txtShenim = new TextBox();
            label4 = new Label();
            dtpAfati = new DateTimePicker();
            btnDelego = new Button();
            dgvDelegimet = new DataGridView();
            label5 = new Label();
            btnHapDokumentPergjigje = new Button();
            btnKrijoPergjigjeDalese = new Button();
            btnMbyllCeshtjen = new Button();
            btnPergjigjuBrendshme = new Button();
            btnHapDokumentin = new Button();
            btnHistorikuBisedes = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvShkresat).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDelegimet).BeginInit();
            SuspendLayout();
            // 
            // dgvShkresat
            // 
            dgvShkresat.AllowUserToAddRows = false;
            dgvShkresat.AllowUserToDeleteRows = false;
            dgvShkresat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvShkresat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvShkresat.Location = new Point(61, 82);
            dgvShkresat.MultiSelect = false;
            dgvShkresat.Name = "dgvShkresat";
            dgvShkresat.ReadOnly = true;
            dgvShkresat.RowHeadersWidth = 62;
            dgvShkresat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvShkresat.Size = new Size(1185, 179);
            dgvShkresat.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(61, 39);
            label1.Name = "label1";
            label1.Size = new Size(139, 25);
            label1.TabIndex = 1;
            label1.Text = "Shkresat hyrëse:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(61, 321);
            label2.Name = "label2";
            label2.Size = new Size(179, 25);
            label2.TabIndex = 2;
            label2.Text = "Punonjësi përgjegjës:";
            // 
            // cmbPunonjesi
            // 
            cmbPunonjesi.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPunonjesi.FormattingEnabled = true;
            cmbPunonjesi.Location = new Point(246, 318);
            cmbPunonjesi.Name = "cmbPunonjesi";
            cmbPunonjesi.Size = new Size(182, 33);
            cmbPunonjesi.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(61, 388);
            label3.Name = "label3";
            label3.Size = new Size(213, 25);
            label3.TabIndex = 4;
            label3.Text = "Udhëzimi për punonjësin:";
            // 
            // txtShenim
            // 
            txtShenim.Location = new Point(278, 385);
            txtShenim.Multiline = true;
            txtShenim.Name = "txtShenim";
            txtShenim.Size = new Size(150, 46);
            txtShenim.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(61, 467);
            label4.Name = "label4";
            label4.Size = new Size(53, 25);
            label4.TabIndex = 6;
            label4.Text = "Afati:";
            // 
            // dtpAfati
            // 
            dtpAfati.Checked = false;
            dtpAfati.Format = DateTimePickerFormat.Short;
            dtpAfati.Location = new Point(128, 467);
            dtpAfati.Name = "dtpAfati";
            dtpAfati.ShowCheckBox = true;
            dtpAfati.Size = new Size(300, 31);
            dtpAfati.TabIndex = 7;
            // 
            // btnDelego
            // 
            btnDelego.Location = new Point(61, 565);
            btnDelego.Name = "btnDelego";
            btnDelego.Size = new Size(158, 34);
            btnDelego.TabIndex = 8;
            btnDelego.Text = "Delego shkresën";
            btnDelego.TextAlign = ContentAlignment.TopCenter;
            btnDelego.UseVisualStyleBackColor = true;
            // 
            // dgvDelegimet
            // 
            dgvDelegimet.AllowUserToAddRows = false;
            dgvDelegimet.AllowUserToDeleteRows = false;
            dgvDelegimet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDelegimet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDelegimet.Location = new Point(558, 321);
            dgvDelegimet.MultiSelect = false;
            dgvDelegimet.Name = "dgvDelegimet";
            dgvDelegimet.ReadOnly = true;
            dgvDelegimet.RowHeadersWidth = 62;
            dgvDelegimet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDelegimet.Size = new Size(878, 171);
            dgvDelegimet.TabIndex = 9;
            dgvDelegimet.CellContentClick += dgvDelegimet_CellContentClick;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(558, 283);
            label5.Name = "label5";
            label5.Size = new Size(192, 25);
            label5.TabIndex = 10;
            label5.Text = "Historiku i delegimeve:";
            // 
            // btnHapDokumentPergjigje
            // 
            btnHapDokumentPergjigje.Location = new Point(558, 539);
            btnHapDokumentPergjigje.Name = "btnHapDokumentPergjigje";
            btnHapDokumentPergjigje.Size = new Size(158, 60);
            btnHapDokumentPergjigje.TabIndex = 11;
            btnHapDokumentPergjigje.Text = "Hap dokumentin e përgjigjes";
            btnHapDokumentPergjigje.TextAlign = ContentAlignment.TopCenter;
            btnHapDokumentPergjigje.UseVisualStyleBackColor = true;
            // 
            // btnKrijoPergjigjeDalese
            // 
            btnKrijoPergjigjeDalese.Location = new Point(936, 539);
            btnKrijoPergjigjeDalese.Name = "btnKrijoPergjigjeDalese";
            btnKrijoPergjigjeDalese.Size = new Size(158, 60);
            btnKrijoPergjigjeDalese.TabIndex = 12;
            btnKrijoPergjigjeDalese.Text = "Krijo shkresë dalëse";
            btnKrijoPergjigjeDalese.TextAlign = ContentAlignment.TopCenter;
            btnKrijoPergjigjeDalese.UseVisualStyleBackColor = true;
            // 
            // btnMbyllCeshtjen
            // 
            btnMbyllCeshtjen.Location = new Point(1267, 223);
            btnMbyllCeshtjen.Name = "btnMbyllCeshtjen";
            btnMbyllCeshtjen.Size = new Size(169, 38);
            btnMbyllCeshtjen.TabIndex = 13;
            btnMbyllCeshtjen.Text = "Mbyll çështjen";
            btnMbyllCeshtjen.TextAlign = ContentAlignment.TopCenter;
            btnMbyllCeshtjen.UseVisualStyleBackColor = true;
            // 
            // btnPergjigjuBrendshme
            // 
            btnPergjigjuBrendshme.Location = new Point(1267, 151);
            btnPergjigjuBrendshme.Name = "btnPergjigjuBrendshme";
            btnPergjigjuBrendshme.Size = new Size(169, 39);
            btnPergjigjuBrendshme.TabIndex = 14;
            btnPergjigjuBrendshme.Text = "Përgjigju dërguesit";
            btnPergjigjuBrendshme.TextAlign = ContentAlignment.TopCenter;
            btnPergjigjuBrendshme.UseVisualStyleBackColor = true;
            // 
            // btnHapDokumentin
            // 
            btnHapDokumentin.Location = new Point(1267, 82);
            btnHapDokumentin.Name = "btnHapDokumentin";
            btnHapDokumentin.Size = new Size(169, 42);
            btnHapDokumentin.TabIndex = 15;
            btnHapDokumentin.Text = "Hap dokumentin";
            btnHapDokumentin.TextAlign = ContentAlignment.TopCenter;
            btnHapDokumentin.UseVisualStyleBackColor = true;
            // 
            // btnHistorikuBisedes
            // 
            btnHistorikuBisedes.Location = new Point(1278, 539);
            btnHistorikuBisedes.Name = "btnHistorikuBisedes";
            btnHistorikuBisedes.Size = new Size(158, 60);
            btnHistorikuBisedes.TabIndex = 16;
            btnHistorikuBisedes.Text = "Shiko bisedën";
            btnHistorikuBisedes.TextAlign = ContentAlignment.TopCenter;
            btnHistorikuBisedes.UseVisualStyleBackColor = true;
            // 
            // FormDelegimet
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1448, 681);
            Controls.Add(btnHistorikuBisedes);
            Controls.Add(btnHapDokumentin);
            Controls.Add(btnPergjigjuBrendshme);
            Controls.Add(btnMbyllCeshtjen);
            Controls.Add(btnKrijoPergjigjeDalese);
            Controls.Add(btnHapDokumentPergjigje);
            Controls.Add(label5);
            Controls.Add(dgvDelegimet);
            Controls.Add(btnDelego);
            Controls.Add(dtpAfati);
            Controls.Add(label4);
            Controls.Add(txtShenim);
            Controls.Add(label3);
            Controls.Add(cmbPunonjesi);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvShkresat);
            Name = "FormDelegimet";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Delegimi dhe gjurmimi i shkresave";
            ((System.ComponentModel.ISupportInitialize)dgvShkresat).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDelegimet).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvShkresat;
        private Label label1;
        private Label label2;
        private ComboBox cmbPunonjesi;
        private Label label3;
        private TextBox txtShenim;
        private Label label4;
        private DateTimePicker dtpAfati;
        private Button btnDelego;
        private DataGridView dgvDelegimet;
        private Label label5;
        private Button btnHapDokumentPergjigje;
        private Button btnKrijoPergjigjeDalese;
        private Button btnMbyllCeshtjen;
        private Button btnPergjigjuBrendshme;
        private Button btnHapDokumentin;
        private Button btnHistorikuBisedes;
    }
}