namespace IDS.Menus
{
    partial class AttackDetailsMenu
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
            this.label1 = new System.Windows.Forms.Label();
            this.IdsIdentifier = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.AttackedIdsIp = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.AttackedIdsPort = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.AttackId = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.AttackDescription = new System.Windows.Forms.TextBox();
            this.AttackName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Attacked Ids Identifier";
            // 
            // IdsIdentifier
            // 
            this.IdsIdentifier.AutoSize = true;
            this.IdsIdentifier.Location = new System.Drawing.Point(72, 33);
            this.IdsIdentifier.Name = "IdsIdentifier";
            this.IdsIdentifier.Size = new System.Drawing.Size(0, 13);
            this.IdsIdentifier.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(208, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Attacked Ids Source Ip";
            // 
            // AttackedIdsIp
            // 
            this.AttackedIdsIp.AutoSize = true;
            this.AttackedIdsIp.Location = new System.Drawing.Point(265, 33);
            this.AttackedIdsIp.Name = "AttackedIdsIp";
            this.AttackedIdsIp.Size = new System.Drawing.Size(0, 13);
            this.AttackedIdsIp.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(408, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(163, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Attacked Ids Source Port";
            // 
            // AttackedIdsPort
            // 
            this.AttackedIdsPort.AutoSize = true;
            this.AttackedIdsPort.Location = new System.Drawing.Point(476, 33);
            this.AttackedIdsPort.Name = "AttackedIdsPort";
            this.AttackedIdsPort.Size = new System.Drawing.Size(0, 13);
            this.AttackedIdsPort.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "Attack Identifier";
            // 
            // AttackId
            // 
            this.AttackId.AutoSize = true;
            this.AttackId.Location = new System.Drawing.Point(134, 68);
            this.AttackId.Name = "AttackId";
            this.AttackId.Size = new System.Drawing.Size(0, 13);
            this.AttackId.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 15);
            this.label9.TabIndex = 8;
            this.label9.Text = "Attack Name";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(12, 129);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(122, 15);
            this.label11.TabIndex = 10;
            this.label11.Text = "Attack Description";
            // 
            // AttackDescription
            // 
            this.AttackDescription.Location = new System.Drawing.Point(12, 156);
            this.AttackDescription.Multiline = true;
            this.AttackDescription.Name = "AttackDescription";
            this.AttackDescription.ReadOnly = true;
            this.AttackDescription.Size = new System.Drawing.Size(559, 394);
            this.AttackDescription.TabIndex = 12;
            // 
            // AttackName
            // 
            this.AttackName.Location = new System.Drawing.Point(105, 99);
            this.AttackName.Name = "AttackName";
            this.AttackName.ReadOnly = true;
            this.AttackName.Size = new System.Drawing.Size(466, 20);
            this.AttackName.TabIndex = 13;
            // 
            // AttackDetailsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.AttackName);
            this.Controls.Add(this.AttackDescription);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.AttackId);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.AttackedIdsPort);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.AttackedIdsIp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IdsIdentifier);
            this.Controls.Add(this.label1);
            this.Name = "AttackDetailsMenu";
            this.Text = "AttackDetailsMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label IdsIdentifier;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label AttackedIdsIp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label AttackedIdsPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label AttackId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox AttackDescription;
        private System.Windows.Forms.TextBox AttackName;
    }
}