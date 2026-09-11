namespace eProtokoll
{
    partial class FormVitetProtokollare
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
            dgvVitet = new DataGridView();
            label1 = new Label();
            numViti = new NumericUpDown();
            label2 = new Label();
            numNumriFillestar = new NumericUpDown();
            btnHapVit = new Button();
            btnMbyllVit = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvVitet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numViti).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numNumriFillestar).BeginInit();
            SuspendLayout();
            // 
            // dgvVitet
            // 
            dgvVitet.AllowUserToAddRows = false;
            dgvVitet.AllowUserToDeleteRows = false;
            dgvVitet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVitet.Dock = DockStyle.Top;
            dgvVitet.Location = new Point(0, 0);
            dgvVitet.MultiSelect = false;
            dgvVitet.Name = "dgvVitet";
            dgvVitet.ReadOnly = true;
            dgvVitet.RowHeadersWidth = 62;
            dgvVitet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVitet.Size = new Size(728, 225);
            dgvVitet.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 255);
            label1.Name = "label1";
            label1.Size = new Size(41, 25);
            label1.TabIndex = 1;
            label1.Text = "Viti:";
            // 
            // numViti
            // 
            numViti.Location = new Point(63, 253);
            numViti.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            numViti.Minimum = new decimal(new int[] { 2000, 0, 0, 0 });
            numViti.Name = "numViti";
            numViti.Size = new Size(180, 31);
            numViti.TabIndex = 2;
            numViti.Value = new decimal(new int[] { 2026, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(283, 257);
            label2.Name = "label2";
            label2.Size = new Size(126, 25);
            label2.TabIndex = 3;
            label2.Text = "Numri fillestar:";
            // 
            // numNumriFillestar
            // 
            numNumriFillestar.Location = new Point(414, 257);
            numNumriFillestar.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numNumriFillestar.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numNumriFillestar.Name = "numNumriFillestar";
            numNumriFillestar.Size = new Size(180, 31);
            numNumriFillestar.TabIndex = 4;
            numNumriFillestar.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnHapVit
            // 
            btnHapVit.Location = new Point(23, 328);
            btnHapVit.Name = "btnHapVit";
            btnHapVit.Size = new Size(112, 34);
            btnHapVit.TabIndex = 5;
            btnHapVit.Text = "Hap vit të ri";
            btnHapVit.UseVisualStyleBackColor = true;
            btnHapVit.Click += btnHapVit_Click;
            // 
            // btnMbyllVit
            // 
            btnMbyllVit.Location = new Point(204, 328);
            btnMbyllVit.Name = "btnMbyllVit";
            btnMbyllVit.Size = new Size(112, 34);
            btnMbyllVit.TabIndex = 6;
            btnMbyllVit.Text = "Mbyll vitin";
            btnMbyllVit.UseVisualStyleBackColor = true;
            btnMbyllVit.Click += btnMbyllVit_Click;
            // 
            // FormVitetProtokollare
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(728, 444);
            Controls.Add(btnMbyllVit);
            Controls.Add(btnHapVit);
            Controls.Add(numNumriFillestar);
            Controls.Add(label2);
            Controls.Add(numViti);
            Controls.Add(label1);
            Controls.Add(dgvVitet);
            Name = "FormVitetProtokollare";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Vitet protokollare";
            ((System.ComponentModel.ISupportInitialize)dgvVitet).EndInit();
            ((System.ComponentModel.ISupportInitialize)numViti).EndInit();
            ((System.ComponentModel.ISupportInitialize)numNumriFillestar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvVitet;
        private Label label1;
        private NumericUpDown numViti;
        private Label label2;
        private NumericUpDown numNumriFillestar;
        private Button btnHapVit;
        private Button btnMbyllVit;
    }
}