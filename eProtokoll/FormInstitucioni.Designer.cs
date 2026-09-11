namespace eProtokoll
{
    partial class FormInstitucioni
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
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtEmri = new TextBox();
            txtAdresa = new TextBox();
            txtTelefon = new TextBox();
            txtEmail = new TextBox();
            btnRuaj = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(187, 37);
            label1.Name = "label1";
            label1.Size = new Size(47, 25);
            label1.TabIndex = 0;
            label1.Text = "Emri";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(187, 120);
            label2.Name = "label2";
            label2.Size = new Size(67, 25);
            label2.TabIndex = 1;
            label2.Text = "Adresa";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(187, 210);
            label3.Name = "label3";
            label3.Size = new Size(68, 25);
            label3.TabIndex = 2;
            label3.Text = "Telefon";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(187, 293);
            label4.Name = "label4";
            label4.Size = new Size(54, 25);
            label4.TabIndex = 3;
            label4.Text = "Email";
            // 
            // txtEmri
            // 
            txtEmri.Location = new Point(193, 70);
            txtEmri.Name = "txtEmri";
            txtEmri.Size = new Size(150, 31);
            txtEmri.TabIndex = 4;
            // 
            // txtAdresa
            // 
            txtAdresa.Location = new Point(193, 159);
            txtAdresa.Name = "txtAdresa";
            txtAdresa.Size = new Size(150, 31);
            txtAdresa.TabIndex = 5;
            // 
            // txtTelefon
            // 
            txtTelefon.Location = new Point(193, 247);
            txtTelefon.Name = "txtTelefon";
            txtTelefon.Size = new Size(150, 31);
            txtTelefon.TabIndex = 6;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(193, 336);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(150, 31);
            txtEmail.TabIndex = 7;
            // 
            // btnRuaj
            // 
            btnRuaj.Location = new Point(205, 420);
            btnRuaj.Name = "btnRuaj";
            btnRuaj.Size = new Size(112, 34);
            btnRuaj.TabIndex = 8;
            btnRuaj.Text = "Ruaj";
            btnRuaj.UseVisualStyleBackColor = true;
            btnRuaj.Click += new System.EventHandler(this.btnRuaj_Click);
            // 
            // FormInstitucioni
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(522, 519);
            Controls.Add(btnRuaj);
            Controls.Add(txtEmail);
            Controls.Add(txtTelefon);
            Controls.Add(txtAdresa);
            Controls.Add(txtEmri);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormInstitucioni";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtEmri;
        private TextBox txtAdresa;
        private TextBox txtTelefon;
        private TextBox txtEmail;
        private Button btnRuaj;
    }
}