namespace eProtokoll
{
    partial class FormShkreseBrendshme
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
            cmbKlasifikimi = new ComboBox();
            lblPunonjesit = new Label();
            clbPunonjesit = new CheckedListBox();
            txtPermbajtja = new TextBox();
            txtPathDokumenti = new TextBox();
            btnZgjidhDokument = new Button();
            btnRuaj = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            dtpAfati = new DateTimePicker();
            lblAfati = new Label();
            btnSkanoDokumentin = new Button();
            SuspendLayout();
            // 
            // txtViti
            // 
            txtViti.Location = new Point(261, 36);
            txtViti.Name = "txtViti";
            txtViti.ReadOnly = true;
            txtViti.Size = new Size(150, 31);
            txtViti.TabIndex = 0;
            // 
            // txtNumriProtokollit
            // 
            txtNumriProtokollit.Location = new Point(261, 107);
            txtNumriProtokollit.Name = "txtNumriProtokollit";
            txtNumriProtokollit.ReadOnly = true;
            txtNumriProtokollit.Size = new Size(150, 31);
            txtNumriProtokollit.TabIndex = 1;
            // 
            // cmbKlasifikimi
            // 
            cmbKlasifikimi.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKlasifikimi.FormattingEnabled = true;
            cmbKlasifikimi.Location = new Point(260, 182);
            cmbKlasifikimi.Name = "cmbKlasifikimi";
            cmbKlasifikimi.Size = new Size(182, 33);
            cmbKlasifikimi.TabIndex = 2;
            // 
            // lblPunonjesit
            // 
            lblPunonjesit.AutoSize = true;
            lblPunonjesit.Location = new Point(78, 267);
            lblPunonjesit.Name = "lblPunonjesit";
            lblPunonjesit.Size = new Size(157, 25);
            lblPunonjesit.TabIndex = 3;
            lblPunonjesit.Text = "Punonjësit marrës:";
            // 
            // clbPunonjesit
            // 
            clbPunonjesit.CheckOnClick = true;
            clbPunonjesit.FormattingEnabled = true;
            clbPunonjesit.Location = new Point(258, 267);
            clbPunonjesit.Name = "clbPunonjesit";
            clbPunonjesit.Size = new Size(180, 60);
            clbPunonjesit.TabIndex = 4;
            // 
            // txtPermbajtja
            // 
            txtPermbajtja.Location = new Point(261, 456);
            txtPermbajtja.Multiline = true;
            txtPermbajtja.Name = "txtPermbajtja";
            txtPermbajtja.Size = new Size(150, 46);
            txtPermbajtja.TabIndex = 5;
            // 
            // txtPathDokumenti
            // 
            txtPathDokumenti.Location = new Point(259, 528);
            txtPathDokumenti.Name = "txtPathDokumenti";
            txtPathDokumenti.ReadOnly = true;
            txtPathDokumenti.Size = new Size(150, 31);
            txtPathDokumenti.TabIndex = 6;
            // 
            // btnZgjidhDokument
            // 
            btnZgjidhDokument.Location = new Point(448, 526);
            btnZgjidhDokument.Name = "btnZgjidhDokument";
            btnZgjidhDokument.Size = new Size(159, 34);
            btnZgjidhDokument.TabIndex = 7;
            btnZgjidhDokument.Text = "Zgjidh dokument";
            btnZgjidhDokument.UseVisualStyleBackColor = true;
            // 
            // btnRuaj
            // 
            btnRuaj.Location = new Point(280, 648);
            btnRuaj.Name = "btnRuaj";
            btnRuaj.Size = new Size(131, 34);
            btnRuaj.TabIndex = 8;
            btnRuaj.Text = "Ruaj shkresën";
            btnRuaj.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(78, 34);
            label1.Name = "label1";
            label1.Size = new Size(134, 25);
            label1.TabIndex = 9;
            label1.Text = "Viti protokollar:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(78, 190);
            label2.Name = "label2";
            label2.Size = new Size(99, 25);
            label2.TabIndex = 10;
            label2.Text = "Klasifikimi: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(78, 113);
            label3.Name = "label3";
            label3.Size = new Size(162, 25);
            label3.TabIndex = 11;
            label3.Text = "Numri i protokollit:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(83, 535);
            label4.Name = "label4";
            label4.Size = new Size(104, 25);
            label4.TabIndex = 12;
            label4.Text = "Dokumenti:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(83, 459);
            label5.Name = "label5";
            label5.Size = new Size(99, 25);
            label5.TabIndex = 13;
            label5.Text = "Përmbajtja:";
            // 
            // dtpAfati
            // 
            dtpAfati.Checked = false;
            dtpAfati.Format = DateTimePickerFormat.Short;
            dtpAfati.Location = new Point(258, 374);
            dtpAfati.Name = "dtpAfati";
            dtpAfati.ShowCheckBox = true;
            dtpAfati.Size = new Size(184, 31);
            dtpAfati.TabIndex = 14;
            // 
            // lblAfati
            // 
            lblAfati.AutoSize = true;
            lblAfati.Location = new Point(78, 374);
            lblAfati.Name = "lblAfati";
            lblAfati.Size = new Size(53, 25);
            lblAfati.TabIndex = 15;
            lblAfati.Text = "Afati:";
            // 
            // btnSkanoDokumentin
            // 
            btnSkanoDokumentin.Location = new Point(448, 566);
            btnSkanoDokumentin.Name = "btnSkanoDokumentin";
            btnSkanoDokumentin.Size = new Size(159, 58);
            btnSkanoDokumentin.TabIndex = 16;
            btnSkanoDokumentin.Text = "Skano dokumentin";
            btnSkanoDokumentin.UseVisualStyleBackColor = true;
            btnSkanoDokumentin.Click += btnSkanoDokumentin_Click;
            // 
            // FormShkreseBrendshme
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(639, 694);
            Controls.Add(btnSkanoDokumentin);
            Controls.Add(lblAfati);
            Controls.Add(dtpAfati);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnRuaj);
            Controls.Add(btnZgjidhDokument);
            Controls.Add(txtPathDokumenti);
            Controls.Add(txtPermbajtja);
            Controls.Add(clbPunonjesit);
            Controls.Add(lblPunonjesit);
            Controls.Add(cmbKlasifikimi);
            Controls.Add(txtNumriProtokollit);
            Controls.Add(txtViti);
            Name = "FormShkreseBrendshme";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Regjistro shkresë të brendshme";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtViti;
        private TextBox txtNumriProtokollit;
        private ComboBox cmbKlasifikimi;
        private Label lblPunonjesit;
        private CheckedListBox clbPunonjesit;
        private TextBox txtPermbajtja;
        private TextBox txtPathDokumenti;
        private Button btnZgjidhDokument;
        private Button btnRuaj;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private DateTimePicker dtpAfati;
        private Label lblAfati;
        private Button btnSkanoDokumentin;
    }
}