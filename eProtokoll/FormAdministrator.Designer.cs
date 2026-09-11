namespace eProtokoll
{
    partial class FormAdministrator
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
            dgvInstitucionet = new DataGridView();
            btnShtoInstitucion = new Button();
            btnModifikoInstitucion = new Button();
            btnFshiInstitucion = new Button();
            btnVitetProtokollare = new Button();
            btnPerdoruesit = new Button();
            lblMiresevini = new Label();
            pnlHeader = new Panel();
            grpAdministrimi = new GroupBox();
            grpInstitucionet = new GroupBox();
            btnDil = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvInstitucionet).BeginInit();
            pnlHeader.SuspendLayout();
            grpAdministrimi.SuspendLayout();
            grpInstitucionet.SuspendLayout();
            SuspendLayout();
            // 
            // dgvInstitucionet
            // 
            dgvInstitucionet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInstitucionet.Location = new Point(6, 41);
            dgvInstitucionet.Name = "dgvInstitucionet";
            dgvInstitucionet.RowHeadersWidth = 62;
            dgvInstitucionet.Size = new Size(785, 302);
            dgvInstitucionet.TabIndex = 1;
            // 
            // btnShtoInstitucion
            // 
            btnShtoInstitucion.Location = new Point(19, 367);
            btnShtoInstitucion.Name = "btnShtoInstitucion";
            btnShtoInstitucion.Size = new Size(146, 49);
            btnShtoInstitucion.TabIndex = 2;
            btnShtoInstitucion.Text = "Shto ";
            btnShtoInstitucion.UseVisualStyleBackColor = true;
            btnShtoInstitucion.Click += btnShtoInstitucion_Click;
            // 
            // btnModifikoInstitucion
            // 
            btnModifikoInstitucion.Location = new Point(315, 367);
            btnModifikoInstitucion.Name = "btnModifikoInstitucion";
            btnModifikoInstitucion.Size = new Size(159, 49);
            btnModifikoInstitucion.TabIndex = 3;
            btnModifikoInstitucion.Text = "Modifiko ";
            btnModifikoInstitucion.UseVisualStyleBackColor = true;
            btnModifikoInstitucion.Click += btnModifikoInstitucion_Click;
            // 
            // btnFshiInstitucion
            // 
            btnFshiInstitucion.Location = new Point(642, 367);
            btnFshiInstitucion.Name = "btnFshiInstitucion";
            btnFshiInstitucion.Size = new Size(133, 49);
            btnFshiInstitucion.TabIndex = 4;
            btnFshiInstitucion.Text = "Fshi";
            btnFshiInstitucion.UseVisualStyleBackColor = true;
            btnFshiInstitucion.Click += btnFshiInstitucion_Click;
            // 
            // btnVitetProtokollare
            // 
            btnVitetProtokollare.Location = new Point(41, 210);
            btnVitetProtokollare.Name = "btnVitetProtokollare";
            btnVitetProtokollare.Size = new Size(238, 57);
            btnVitetProtokollare.TabIndex = 6;
            btnVitetProtokollare.Text = "Vitet protokollare";
            btnVitetProtokollare.UseVisualStyleBackColor = true;
            btnVitetProtokollare.Click += btnVitetProtokollare_Click;
            // 
            // btnPerdoruesit
            // 
            btnPerdoruesit.Location = new Point(41, 84);
            btnPerdoruesit.Name = "btnPerdoruesit";
            btnPerdoruesit.Size = new Size(238, 62);
            btnPerdoruesit.TabIndex = 7;
            btnPerdoruesit.Text = "Menaxho përdoruesit";
            btnPerdoruesit.UseVisualStyleBackColor = true;
            btnPerdoruesit.Click += btnPerdoruesit_Click;
            // 
            // lblMiresevini
            // 
            lblMiresevini.AutoSize = true;
            lblMiresevini.BackColor = Color.Transparent;
            lblMiresevini.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMiresevini.ForeColor = Color.White;
            lblMiresevini.Location = new Point(352, 27);
            lblMiresevini.Name = "lblMiresevini";
            lblMiresevini.Size = new Size(219, 38);
            lblMiresevini.TabIndex = 8;
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
            pnlHeader.Size = new Size(1151, 96);
            pnlHeader.TabIndex = 9;
            // 
            // grpAdministrimi
            // 
            grpAdministrimi.Controls.Add(btnPerdoruesit);
            grpAdministrimi.Controls.Add(btnVitetProtokollare);
            grpAdministrimi.Location = new Point(839, 112);
            grpAdministrimi.Name = "grpAdministrimi";
            grpAdministrimi.Size = new Size(300, 324);
            grpAdministrimi.TabIndex = 10;
            grpAdministrimi.TabStop = false;
            grpAdministrimi.Text = "Administrimi i sistemit";
            // 
            // grpInstitucionet
            // 
            grpInstitucionet.Controls.Add(dgvInstitucionet);
            grpInstitucionet.Controls.Add(btnShtoInstitucion);
            grpInstitucionet.Controls.Add(btnModifikoInstitucion);
            grpInstitucionet.Controls.Add(btnFshiInstitucion);
            grpInstitucionet.Location = new Point(12, 112);
            grpInstitucionet.Name = "grpInstitucionet";
            grpInstitucionet.Size = new Size(797, 447);
            grpInstitucionet.TabIndex = 11;
            grpInstitucionet.TabStop = false;
            grpInstitucionet.Text = "Menaxhimi i institucioneve";
            // 
            // btnDil
            // 
            btnDil.Location = new Point(1074, 27);
            btnDil.Name = "btnDil";
            btnDil.Size = new Size(65, 38);
            btnDil.TabIndex = 5;
            btnDil.Text = "Dil";
            btnDil.UseVisualStyleBackColor = true;
            btnDil.Click += btnDil_Click;
            // 
            // FormAdministrator
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1151, 571);
            Controls.Add(grpInstitucionet);
            Controls.Add(grpAdministrimi);
            Controls.Add(pnlHeader);
            Name = "FormAdministrator";
            Text = "Paneli administratorit";
            ((System.ComponentModel.ISupportInitialize)dgvInstitucionet).EndInit();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            grpAdministrimi.ResumeLayout(false);
            grpInstitucionet.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dgvInstitucionet;
        private Button btnShtoInstitucion;
        private Button btnModifikoInstitucion;
        private Button btnFshiInstitucion;
        private Button btnVitetProtokollare;
        private Button btnPerdoruesit;
        private Label lblMiresevini;
        private Panel pnlHeader;
        private GroupBox grpAdministrimi;
        private GroupBox grpInstitucionet;
        private Button btnDil;
    }
}