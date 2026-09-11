namespace eProtokoll
{
    partial class FormHistorikuBisedes
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
            lblProtokolli = new Label();
            pnlBiseda = new FlowLayoutPanel();
            btnHapDokumentin = new Button();
            btnRifresko = new Button();
            btnMbyll = new Button();
            SuspendLayout();
            // 
            // lblProtokolli
            // 
            lblProtokolli.AutoSize = true;
            lblProtokolli.Location = new Point(25, 41);
            lblProtokolli.Name = "lblProtokolli";
            lblProtokolli.Size = new Size(73, 25);
            lblProtokolli.TabIndex = 0;
            lblProtokolli.Text = "Çështja:";
            // 
            // pnlBiseda
            // 
            pnlBiseda.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlBiseda.AutoScroll = true;
            pnlBiseda.BackColor = Color.WhiteSmoke;
            pnlBiseda.BorderStyle = BorderStyle.FixedSingle;
            pnlBiseda.FlowDirection = FlowDirection.TopDown;
            pnlBiseda.Location = new Point(25, 90);
            pnlBiseda.Name = "pnlBiseda";
            pnlBiseda.Size = new Size(798, 389);
            pnlBiseda.TabIndex = 1;
            pnlBiseda.WrapContents = false;
            // 
            // btnHapDokumentin
            // 
            btnHapDokumentin.Location = new Point(25, 520);
            btnHapDokumentin.Name = "btnHapDokumentin";
            btnHapDokumentin.Size = new Size(262, 34);
            btnHapDokumentin.TabIndex = 2;
            btnHapDokumentin.Text = "Hap dokumentin e mesazhit";
            btnHapDokumentin.UseVisualStyleBackColor = true;
            // 
            // btnRifresko
            // 
            btnRifresko.Location = new Point(321, 520);
            btnRifresko.Name = "btnRifresko";
            btnRifresko.Size = new Size(161, 34);
            btnRifresko.TabIndex = 3;
            btnRifresko.Text = "Rifresko bisedën";
            btnRifresko.UseVisualStyleBackColor = true;
            // 
            // btnMbyll
            // 
            btnMbyll.Location = new Point(500, 520);
            btnMbyll.Name = "btnMbyll";
            btnMbyll.Size = new Size(161, 34);
            btnMbyll.TabIndex = 4;
            btnMbyll.Text = "Mbyll";
            btnMbyll.UseVisualStyleBackColor = true;
            // 
            // FormHistorikuBisedes
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(835, 593);
            Controls.Add(btnMbyll);
            Controls.Add(btnRifresko);
            Controls.Add(btnHapDokumentin);
            Controls.Add(pnlBiseda);
            Controls.Add(lblProtokolli);
            Name = "FormHistorikuBisedes";
            Text = "Historiku i bisedës";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblProtokolli;
        private FlowLayoutPanel pnlBiseda;
        private Button btnHapDokumentin;
        private Button btnRifresko;
        private Button btnMbyll;
    }
}