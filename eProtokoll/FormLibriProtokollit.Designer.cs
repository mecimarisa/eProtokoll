namespace eProtokoll
{
    partial class FormLibriProtokollit
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
            cmbViti = new ComboBox();
            btnNgarko = new Button();
            dgvProtokolli = new DataGridView();
            btnPrinto = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProtokolli).BeginInit();
            SuspendLayout();
            // 
            // cmbViti
            // 
            cmbViti.FormattingEnabled = true;
            cmbViti.Location = new Point(276, 32);
            cmbViti.Name = "cmbViti";
            cmbViti.Size = new Size(182, 33);
            cmbViti.TabIndex = 0;
            // 
            // btnNgarko
            // 
            btnNgarko.Location = new Point(474, 96);
            btnNgarko.Name = "btnNgarko";
            btnNgarko.Size = new Size(170, 34);
            btnNgarko.TabIndex = 1;
            btnNgarko.Text = "Ngarko librin";
            btnNgarko.UseVisualStyleBackColor = true;
            // 
            // dgvProtokolli
            // 
            dgvProtokolli.AllowUserToAddRows = false;
            dgvProtokolli.AllowUserToDeleteRows = false;
            dgvProtokolli.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProtokolli.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProtokolli.Location = new Point(81, 96);
            dgvProtokolli.Name = "dgvProtokolli";
            dgvProtokolli.ReadOnly = true;
            dgvProtokolli.RowHeadersWidth = 62;
            dgvProtokolli.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProtokolli.Size = new Size(360, 225);
            dgvProtokolli.TabIndex = 2;
            // 
            // btnPrinto
            // 
            btnPrinto.Location = new Point(474, 287);
            btnPrinto.Name = "btnPrinto";
            btnPrinto.Size = new Size(170, 34);
            btnPrinto.TabIndex = 3;
            btnPrinto.Text = "Printo librin";
            btnPrinto.UseVisualStyleBackColor = true;
            // 
            // FormLibriProtokollit
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnPrinto);
            Controls.Add(dgvProtokolli);
            Controls.Add(btnNgarko);
            Controls.Add(cmbViti);
            Name = "FormLibriProtokollit";
            Text = "Gjenerimi dhe printimi i Librit të Protokollit";
            ((System.ComponentModel.ISupportInitialize)dgvProtokolli).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmbViti;
        private Button btnNgarko;
        private DataGridView dgvProtokolli;
        private Button btnPrinto;
    }
}