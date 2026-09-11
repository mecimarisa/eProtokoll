namespace eProtokoll
{
    partial class FormLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            label1 = new Label();
            label2 = new Label();
            pnlMajtas = new Panel();
            pnlDjathtas = new Panel();
            pnlLogin = new Panel();
            label3 = new Label();
            label4 = new Label();
            picDokumentet = new PictureBox();
            pnlMajtas.SuspendLayout();
            pnlDjathtas.SuspendLayout();
            pnlLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picDokumentet).BeginInit();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(61, 175);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(303, 31);
            txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(61, 274);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(303, 31);
            txtPassword.TabIndex = 1;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.SteelBlue;
            btnLogin.Location = new Point(161, 337);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(108, 46);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Hyr";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(61, 138);
            label1.Name = "label1";
            label1.Size = new Size(102, 25);
            label1.TabIndex = 3;
            label1.Text = "Përdoruesi";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(61, 246);
            label2.Name = "label2";
            label2.Size = new Size(102, 25);
            label2.TabIndex = 4;
            label2.Text = "Fjalëkalimi";
            // 
            // pnlMajtas
            // 
            pnlMajtas.BackColor = Color.SteelBlue;
            pnlMajtas.Controls.Add(picDokumentet);
            pnlMajtas.Dock = DockStyle.Left;
            pnlMajtas.Location = new Point(0, 0);
            pnlMajtas.Name = "pnlMajtas";
            pnlMajtas.Size = new Size(458, 601);
            pnlMajtas.TabIndex = 5;
            // 
            // pnlDjathtas
            // 
            pnlDjathtas.BackColor = SystemColors.GradientActiveCaption;
            pnlDjathtas.Controls.Add(pnlLogin);
            pnlDjathtas.Dock = DockStyle.Fill;
            pnlDjathtas.Location = new Point(458, 0);
            pnlDjathtas.Name = "pnlDjathtas";
            pnlDjathtas.Size = new Size(584, 601);
            pnlDjathtas.TabIndex = 6;
            // 
            // pnlLogin
            // 
            pnlLogin.BackColor = SystemColors.InactiveBorder;
            pnlLogin.Controls.Add(label4);
            pnlLogin.Controls.Add(label3);
            pnlLogin.Controls.Add(btnLogin);
            pnlLogin.Controls.Add(txtUsername);
            pnlLogin.Controls.Add(label1);
            pnlLogin.Controls.Add(label2);
            pnlLogin.Controls.Add(txtPassword);
            pnlLogin.Location = new Point(81, 76);
            pnlLogin.Name = "pnlLogin";
            pnlLogin.Size = new Size(425, 429);
            pnlLogin.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI Black", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.SteelBlue;
            label3.Location = new Point(82, 47);
            label3.Name = "label3";
            label3.Size = new Size(249, 54);
            label3.TabIndex = 5;
            label3.Text = "Mirë se vini";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Light", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(92, 101);
            label4.Name = "label4";
            label4.Size = new Size(239, 25);
            label4.TabIndex = 6;
            label4.Text = "Identifikohuni për të vazhduar";
            // 
            // picDokumentet
            // 
            picDokumentet.BackColor = Color.Transparent;
            picDokumentet.Image = Properties.Resources.logo_e_protokoll;
            picDokumentet.Location = new Point(12, 123);
            picDokumentet.Name = "picDokumentet";
            picDokumentet.Size = new Size(426, 352);
            picDokumentet.SizeMode = PictureBoxSizeMode.Zoom;
            picDokumentet.TabIndex = 0;
            picDokumentet.TabStop = false;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1042, 601);
            Controls.Add(pnlDjathtas);
            Controls.Add(pnlMajtas);
            Name = "FormLogin";
            Text = "eProtokoll";
            pnlMajtas.ResumeLayout(false);
            pnlDjathtas.ResumeLayout(false);
            pnlLogin.ResumeLayout(false);
            pnlLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picDokumentet).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label label1;
        private Label label2;
        private Panel pnlMajtas;
        private Panel pnlDjathtas;
        private Panel pnlLogin;
        private Label label3;
        private PictureBox picDokumentet;
        private Label label4;
    }
}
