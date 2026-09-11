namespace eProtokoll
{
    partial class FormShkreseDalese
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
            txtViti = new TextBox();
            txtNumriProtokollit = new TextBox();
            cmbInstitucioni = new ComboBox();
            cmbKlasifikimi = new ComboBox();
            txtPermbajtja = new TextBox();
            txtPathDokumenti = new TextBox();
            btnZgjidhDokument = new Button();
            btnRuaj = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            btnSkanoDokumentin = new Button();
            SuspendLayout();
            // 
            // txtViti
            // 
            txtViti.Location = new Point(359, 20);
            txtViti.Name = "txtViti";
            txtViti.ReadOnly = true;
            txtViti.Size = new Size(182, 31);
            txtViti.TabIndex = 0;
            // 
            // txtNumriProtokollit
            // 
            txtNumriProtokollit.Location = new Point(359, 98);
            txtNumriProtokollit.Multiline = true;
            txtNumriProtokollit.Name = "txtNumriProtokollit";
            txtNumriProtokollit.ReadOnly = true;
            txtNumriProtokollit.Size = new Size(180, 33);
            txtNumriProtokollit.TabIndex = 1;
            // 
            // cmbInstitucioni
            // 
            cmbInstitucioni.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbInstitucioni.FormattingEnabled = true;
            cmbInstitucioni.Location = new Point(359, 168);
            cmbInstitucioni.Name = "cmbInstitucioni";
            cmbInstitucioni.Size = new Size(182, 33);
            cmbInstitucioni.TabIndex = 2;
            // 
            // cmbKlasifikimi
            // 
            cmbKlasifikimi.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKlasifikimi.FormattingEnabled = true;
            cmbKlasifikimi.Location = new Point(359, 249);
            cmbKlasifikimi.Name = "cmbKlasifikimi";
            cmbKlasifikimi.Size = new Size(182, 33);
            cmbKlasifikimi.TabIndex = 3;
            // 
            // txtPermbajtja
            // 
            txtPermbajtja.Location = new Point(359, 337);
            txtPermbajtja.Multiline = true;
            txtPermbajtja.Name = "txtPermbajtja";
            txtPermbajtja.Size = new Size(180, 46);
            txtPermbajtja.TabIndex = 6;
            // 
            // txtPathDokumenti
            // 
            txtPathDokumenti.Location = new Point(359, 426);
            txtPathDokumenti.Name = "txtPathDokumenti";
            txtPathDokumenti.ReadOnly = true;
            txtPathDokumenti.Size = new Size(180, 31);
            txtPathDokumenti.TabIndex = 7;
            // 
            // btnZgjidhDokument
            // 
            btnZgjidhDokument.Location = new Point(558, 427);
            btnZgjidhDokument.Name = "btnZgjidhDokument";
            btnZgjidhDokument.Size = new Size(168, 30);
            btnZgjidhDokument.TabIndex = 8;
            btnZgjidhDokument.Text = "Zgjidh dokument";
            btnZgjidhDokument.UseVisualStyleBackColor = true;
            // 
            // btnRuaj
            // 
            btnRuaj.Location = new Point(311, 529);
            btnRuaj.Name = "btnRuaj";
            btnRuaj.Size = new Size(137, 34);
            btnRuaj.TabIndex = 9;
            btnRuaj.Text = "Ruaj shkresën";
            btnRuaj.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(133, 23);
            label1.Name = "label1";
            label1.Size = new Size(134, 25);
            label1.TabIndex = 10;
            label1.Text = "Viti protokollar:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(133, 176);
            label2.Name = "label2";
            label2.Size = new Size(161, 25);
            label2.TabIndex = 11;
            label2.Text = "Institucioni marrës:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(133, 101);
            label3.Name = "label3";
            label3.Size = new Size(162, 25);
            label3.TabIndex = 12;
            label3.Text = "Numri i protokollit:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(133, 252);
            label4.Name = "label4";
            label4.Size = new Size(104, 25);
            label4.TabIndex = 13;
            label4.Text = "Klasifikimi:  ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(133, 340);
            label5.Name = "label5";
            label5.Size = new Size(114, 25);
            label5.TabIndex = 14;
            label5.Text = "Përmbajtja:   ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(133, 435);
            label6.Name = "label6";
            label6.Size = new Size(104, 25);
            label6.TabIndex = 15;
            label6.Text = "Dokumenti:";
            // 
            // btnSkanoDokumentin
            // 
            btnSkanoDokumentin.Location = new Point(558, 463);
            btnSkanoDokumentin.Name = "btnSkanoDokumentin";
            btnSkanoDokumentin.Size = new Size(168, 59);
            btnSkanoDokumentin.TabIndex = 16;
            btnSkanoDokumentin.Text = "Skano dokumentin";
            btnSkanoDokumentin.UseVisualStyleBackColor = true;
            btnSkanoDokumentin.Click += btnSkanoDokumentin_Click;
            // 
            // FormShkreseDalese
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(745, 584);
            Controls.Add(btnSkanoDokumentin);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnRuaj);
            Controls.Add(btnZgjidhDokument);
            Controls.Add(txtPathDokumenti);
            Controls.Add(txtPermbajtja);
            Controls.Add(cmbKlasifikimi);
            Controls.Add(cmbInstitucioni);
            Controls.Add(txtNumriProtokollit);
            Controls.Add(txtViti);
            Name = "FormShkreseDalese";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Regjistro shkresë dalëse";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtViti;
        private TextBox txtNumriProtokollit;
        private ComboBox cmbInstitucioni;
        private ComboBox cmbKlasifikimi;
        private TextBox txtPermbajtja;
        private TextBox txtPathDokumenti;
        private Button btnZgjidhDokument;
        private Button btnRuaj;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button btnSkanoDokumentin;
    }
}