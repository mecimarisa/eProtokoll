namespace eProtokoll
{
    partial class FormShkreseHyrese
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
            txtViti = new TextBox();
            txtNumriProtokollit = new TextBox();
            label2 = new Label();
            label3 = new Label();
            cmbInstitucioni = new ComboBox();
            cmbKlasifikimi = new ComboBox();
            label4 = new Label();
            label6 = new Label();
            txtPermbajtja = new TextBox();
            label7 = new Label();
            txtPathDokumenti = new TextBox();
            btnZgjidhDokument = new Button();
            btnRuaj = new Button();
            btnSkanoDokumentin = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(55, 42);
            label1.Name = "label1";
            label1.Size = new Size(134, 25);
            label1.TabIndex = 0;
            label1.Text = "Viti protokollar:";
            // 
            // txtViti
            // 
            txtViti.Location = new Point(236, 39);
            txtViti.Name = "txtViti";
            txtViti.ReadOnly = true;
            txtViti.Size = new Size(182, 31);
            txtViti.TabIndex = 1;
            // 
            // txtNumriProtokollit
            // 
            txtNumriProtokollit.Location = new Point(237, 128);
            txtNumriProtokollit.Name = "txtNumriProtokollit";
            txtNumriProtokollit.ReadOnly = true;
            txtNumriProtokollit.Size = new Size(181, 31);
            txtNumriProtokollit.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(55, 128);
            label2.Name = "label2";
            label2.Size = new Size(162, 25);
            label2.TabIndex = 3;
            label2.Text = "Numri i protokollit:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(55, 206);
            label3.Name = "label3";
            label3.Size = new Size(171, 25);
            label3.TabIndex = 4;
            label3.Text = "Institucioni dërgues:";
            // 
            // cmbInstitucioni
            // 
            cmbInstitucioni.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbInstitucioni.FormattingEnabled = true;
            cmbInstitucioni.Location = new Point(236, 203);
            cmbInstitucioni.Name = "cmbInstitucioni";
            cmbInstitucioni.Size = new Size(182, 33);
            cmbInstitucioni.TabIndex = 5;
            // 
            // cmbKlasifikimi
            // 
            cmbKlasifikimi.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKlasifikimi.FormattingEnabled = true;
            cmbKlasifikimi.Location = new Point(236, 281);
            cmbKlasifikimi.Name = "cmbKlasifikimi";
            cmbKlasifikimi.Size = new Size(182, 33);
            cmbKlasifikimi.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(55, 289);
            label4.Name = "label4";
            label4.Size = new Size(94, 25);
            label4.TabIndex = 7;
            label4.Text = "Klasifikimi:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(50, 367);
            label6.Name = "label6";
            label6.Size = new Size(99, 25);
            label6.TabIndex = 10;
            label6.Text = "Përmbajtja:";
            // 
            // txtPermbajtja
            // 
            txtPermbajtja.Location = new Point(231, 367);
            txtPermbajtja.Multiline = true;
            txtPermbajtja.Name = "txtPermbajtja";
            txtPermbajtja.Size = new Size(182, 68);
            txtPermbajtja.TabIndex = 11;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(50, 458);
            label7.Name = "label7";
            label7.Size = new Size(104, 25);
            label7.TabIndex = 12;
            label7.Text = "Dokumenti:";
            // 
            // txtPathDokumenti
            // 
            txtPathDokumenti.Location = new Point(231, 458);
            txtPathDokumenti.Name = "txtPathDokumenti";
            txtPathDokumenti.ReadOnly = true;
            txtPathDokumenti.Size = new Size(182, 31);
            txtPathDokumenti.TabIndex = 13;
            // 
            // btnZgjidhDokument
            // 
            btnZgjidhDokument.Location = new Point(431, 458);
            btnZgjidhDokument.Name = "btnZgjidhDokument";
            btnZgjidhDokument.Size = new Size(173, 31);
            btnZgjidhDokument.TabIndex = 14;
            btnZgjidhDokument.Text = "Zgjidh dokument";
            btnZgjidhDokument.UseVisualStyleBackColor = true;
            btnZgjidhDokument.Click += btnZgjidhDokument_Click;
            // 
            // btnRuaj
            // 
            btnRuaj.Location = new Point(235, 557);
            btnRuaj.Name = "btnRuaj";
            btnRuaj.Size = new Size(134, 52);
            btnRuaj.TabIndex = 15;
            btnRuaj.Text = "Ruaj shkresën";
            btnRuaj.UseVisualStyleBackColor = true;
            btnRuaj.Click += btnRuaj_Click;
            // 
            // btnSkanoDokumentin
            // 
            btnSkanoDokumentin.Location = new Point(431, 507);
            btnSkanoDokumentin.Name = "btnSkanoDokumentin";
            btnSkanoDokumentin.Size = new Size(173, 30);
            btnSkanoDokumentin.TabIndex = 16;
            btnSkanoDokumentin.Text = "Skano dokumentin";
            btnSkanoDokumentin.UseVisualStyleBackColor = true;
            // 
            // FormShkreseHyrese
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(630, 643);
            Controls.Add(btnSkanoDokumentin);
            Controls.Add(btnRuaj);
            Controls.Add(btnZgjidhDokument);
            Controls.Add(txtPathDokumenti);
            Controls.Add(label7);
            Controls.Add(txtPermbajtja);
            Controls.Add(label6);
            Controls.Add(label4);
            Controls.Add(cmbKlasifikimi);
            Controls.Add(cmbInstitucioni);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtNumriProtokollit);
            Controls.Add(txtViti);
            Controls.Add(label1);
            Name = "FormShkreseHyrese";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Regjistro shkresë hyrëse";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtViti;
        private TextBox txtNumriProtokollit;
        private Label label2;
        private Label label3;
        private ComboBox cmbInstitucioni;
        private ComboBox cmbKlasifikimi;
        private Label label4;
        private Label label6;
        private TextBox txtPermbajtja;
        private Label label7;
        private TextBox txtPathDokumenti;
        private Button btnZgjidhDokument;
        private Button btnRuaj;
        private Button btnSkanoDokumentin;
    }
}