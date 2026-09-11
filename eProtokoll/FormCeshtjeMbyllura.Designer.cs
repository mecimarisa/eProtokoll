namespace eProtokoll
{
    partial class FormCeshtjeMbyllura
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblPershkrimi = new Label();
            dgvCeshtjet = new DataGridView();
            btnRifresko = new Button();
            btnHapDokumentin = new Button();
            btnShikoHistorikun = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCeshtjet).BeginInit();
            SuspendLayout();
            // 
            // lblPershkrimi
            // 
            lblPershkrimi.AutoSize = true;
            lblPershkrimi.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPershkrimi.Location = new Point(21, 25);
            lblPershkrimi.Name = "lblPershkrimi";
            lblPershkrimi.Size = new Size(276, 25);
            lblPershkrimi.TabIndex = 0;
            lblPershkrimi.Text = "Arkivi i çështjeve të mbyllura";
            // 
            // dgvCeshtjet
            // 
            dgvCeshtjet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCeshtjet.Location = new Point(21, 66);
            dgvCeshtjet.Name = "dgvCeshtjet";
            dgvCeshtjet.RowHeadersWidth = 62;
            dgvCeshtjet.Size = new Size(1096, 464);
            dgvCeshtjet.TabIndex = 1;
            // 
            // btnRifresko
            // 
            btnRifresko.Location = new Point(21, 559);
            btnRifresko.Name = "btnRifresko";
            btnRifresko.Size = new Size(112, 34);
            btnRifresko.TabIndex = 2;
            btnRifresko.Text = "Rifresko";
            btnRifresko.UseVisualStyleBackColor = true;
            btnRifresko.Click += btnRifresko_Click;
            // 
            // btnHapDokumentin
            // 
            btnHapDokumentin.Location = new Point(194, 559);
            btnHapDokumentin.Name = "btnHapDokumentin";
            btnHapDokumentin.Size = new Size(161, 34);
            btnHapDokumentin.TabIndex = 3;
            btnHapDokumentin.Text = "Hap dokumentin";
            btnHapDokumentin.UseVisualStyleBackColor = true;
            btnHapDokumentin.Click += btnHapDokumentin_Click;
            // 
            // btnShikoHistorikun
            // 
            btnShikoHistorikun.Location = new Point(400, 559);
            btnShikoHistorikun.Name = "btnShikoHistorikun";
            btnShikoHistorikun.Size = new Size(174, 34);
            btnShikoHistorikun.TabIndex = 4;
            btnShikoHistorikun.Text = "Shiko historikun";
            btnShikoHistorikun.UseVisualStyleBackColor = true;
            btnShikoHistorikun.Click += btnShikoHistorikun_Click;
            // 
            // FormCeshtjeMbyllura
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1132, 615);
            Controls.Add(btnShikoHistorikun);
            Controls.Add(btnHapDokumentin);
            Controls.Add(btnRifresko);
            Controls.Add(dgvCeshtjet);
            Controls.Add(lblPershkrimi);
            Name = "FormCeshtjeMbyllura";
            Text = "Çështjet e mbyllura";
            Load += FormCeshtjeMbyllura_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCeshtjet).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPershkrimi;
        private DataGridView dgvCeshtjet;
        private Button btnRifresko;
        private Button btnHapDokumentin;
        private Button btnShikoHistorikun;
    }
}
