namespace eProtokoll
{
    partial class FormRaporteStatistikore
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
            label1 = new Label();
            cmbViti = new ComboBox();
            btnNgarko = new Button();
            lblTotalShkresa = new Label();
            lblBrendshme = new Label();
            lblDalese = new Label();
            lblHyrese = new Label();
            label2 = new Label();
            dgvSipasInstitucionit = new DataGridView();
            label3 = new Label();
            dgvSipasMuajit = new DataGridView();
            label4 = new Label();
            dgvSipasLlojit = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvSipasInstitucionit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSipasMuajit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSipasLlojit).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 25);
            label1.Name = "label1";
            label1.Size = new Size(134, 25);
            label1.TabIndex = 0;
            label1.Text = "Viti protokollar:";
            // 
            // cmbViti
            // 
            cmbViti.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbViti.FormattingEnabled = true;
            cmbViti.Location = new Point(181, 25);
            cmbViti.Name = "cmbViti";
            cmbViti.Size = new Size(182, 33);
            cmbViti.TabIndex = 1;
            // 
            // btnNgarko
            // 
            btnNgarko.Location = new Point(392, 25);
            btnNgarko.Name = "btnNgarko";
            btnNgarko.Size = new Size(153, 34);
            btnNgarko.TabIndex = 2;
            btnNgarko.Text = "Ngarko raportin";
            btnNgarko.UseVisualStyleBackColor = true;
            btnNgarko.Click += btnNgarko_Click;
            // 
            // lblTotalShkresa
            // 
            lblTotalShkresa.AutoSize = true;
            lblTotalShkresa.Location = new Point(28, 101);
            lblTotalShkresa.Name = "lblTotalShkresa";
            lblTotalShkresa.Size = new Size(163, 25);
            lblTotalShkresa.TabIndex = 3;
            lblTotalShkresa.Text = "Totali i shkresave: 0";
            // 
            // lblBrendshme
            // 
            lblBrendshme.AutoSize = true;
            lblBrendshme.Location = new Point(805, 101);
            lblBrendshme.Name = "lblBrendshme";
            lblBrendshme.Size = new Size(142, 25);
            lblBrendshme.TabIndex = 4;
            lblBrendshme.Text = "Të brendshme: 0";
            // 
            // lblDalese
            // 
            lblDalese.AutoSize = true;
            lblDalese.Location = new Point(570, 101);
            lblDalese.Name = "lblDalese";
            lblDalese.Size = new Size(83, 25);
            lblDalese.TabIndex = 5;
            lblDalese.Text = "Dalëse: 0";
            // 
            // lblHyrese
            // 
            lblHyrese.AutoSize = true;
            lblHyrese.Location = new Point(308, 101);
            lblHyrese.Name = "lblHyrese";
            lblHyrese.Size = new Size(85, 25);
            lblHyrese.TabIndex = 6;
            lblHyrese.Text = "Hyrëse: 0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 165);
            label2.Name = "label2";
            label2.Size = new Size(220, 25);
            label2.TabIndex = 7;
            label2.Text = "Shkresat sipas institucionit";
            // 
            // dgvSipasInstitucionit
            // 
            dgvSipasInstitucionit.AllowUserToAddRows = false;
            dgvSipasInstitucionit.AllowUserToDeleteRows = false;
            dgvSipasInstitucionit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSipasInstitucionit.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSipasInstitucionit.Location = new Point(38, 193);
            dgvSipasInstitucionit.MultiSelect = false;
            dgvSipasInstitucionit.Name = "dgvSipasInstitucionit";
            dgvSipasInstitucionit.ReadOnly = true;
            dgvSipasInstitucionit.RowHeadersWidth = 62;
            dgvSipasInstitucionit.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSipasInstitucionit.Size = new Size(454, 479);
            dgvSipasInstitucionit.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(529, 165);
            label3.Name = "label3";
            label3.Size = new Size(178, 25);
            label3.TabIndex = 9;
            label3.Text = "Shkresat sipas muajit";
            // 
            // dgvSipasMuajit
            // 
            dgvSipasMuajit.AllowUserToAddRows = false;
            dgvSipasMuajit.AllowUserToDeleteRows = false;
            dgvSipasMuajit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSipasMuajit.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSipasMuajit.Location = new Point(529, 193);
            dgvSipasMuajit.MultiSelect = false;
            dgvSipasMuajit.Name = "dgvSipasMuajit";
            dgvSipasMuajit.ReadOnly = true;
            dgvSipasMuajit.RowHeadersWidth = 62;
            dgvSipasMuajit.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSipasMuajit.Size = new Size(487, 203);
            dgvSipasMuajit.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(529, 436);
            label4.Name = "label4";
            label4.Size = new Size(162, 25);
            label4.TabIndex = 11;
            label4.Text = "Shkresat sipas llojit";
            // 
            // dgvSipasLlojit
            // 
            dgvSipasLlojit.AllowUserToAddRows = false;
            dgvSipasLlojit.AllowUserToDeleteRows = false;
            dgvSipasLlojit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSipasLlojit.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSipasLlojit.Location = new Point(529, 464);
            dgvSipasLlojit.MultiSelect = false;
            dgvSipasLlojit.Name = "dgvSipasLlojit";
            dgvSipasLlojit.ReadOnly = true;
            dgvSipasLlojit.RowHeadersWidth = 62;
            dgvSipasLlojit.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSipasLlojit.Size = new Size(487, 203);
            dgvSipasLlojit.TabIndex = 12;
            // 
            // FormRaporteStatistikore
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1028, 684);
            Controls.Add(dgvSipasLlojit);
            Controls.Add(label4);
            Controls.Add(dgvSipasMuajit);
            Controls.Add(label3);
            Controls.Add(dgvSipasInstitucionit);
            Controls.Add(label2);
            Controls.Add(lblHyrese);
            Controls.Add(lblDalese);
            Controls.Add(lblBrendshme);
            Controls.Add(lblTotalShkresa);
            Controls.Add(btnNgarko);
            Controls.Add(cmbViti);
            Controls.Add(label1);
            Name = "FormRaporteStatistikore";
            Text = "Raportet statistikore";
            Load += FormRaporteStatistikore_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSipasInstitucionit).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSipasMuajit).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSipasLlojit).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cmbViti;
        private Button btnNgarko;
        private Label lblTotalShkresa;
        private Label lblBrendshme;
        private Label lblDalese;
        private Label lblHyrese;
        private Label label2;
        private DataGridView dgvSipasInstitucionit;
        private Label label3;
        private DataGridView dgvSipasMuajit;
        private Label label4;
        private DataGridView dgvSipasLlojit;
    }
}