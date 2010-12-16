namespace IDS.Menus
{
    partial class AttackSolutionMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttackSolutionMenu));
            this.label3 = new System.Windows.Forms.Label();
            this.SaveFileButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.AttackSolDesc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AttackId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.HealerId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.HealerAddr = new System.Windows.Forms.TextBox();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 530);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Save Plugin File";
            // 
            // SaveFileButton
            // 
            this.SaveFileButton.Location = new System.Drawing.Point(471, 525);
            this.SaveFileButton.Name = "SaveFileButton";
            this.SaveFileButton.Size = new System.Drawing.Size(102, 25);
            this.SaveFileButton.TabIndex = 13;
            this.SaveFileButton.Text = "Save File";
            this.SaveFileButton.UseVisualStyleBackColor = true;
            this.SaveFileButton.Click += new System.EventHandler(this.SaveFileButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Attack Sol. Desc.";
            // 
            // AttackSolDesc
            // 
            this.AttackSolDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttackSolDesc.Location = new System.Drawing.Point(96, 118);
            this.AttackSolDesc.MaxLength = 2000;
            this.AttackSolDesc.Multiline = true;
            this.AttackSolDesc.Name = "AttackSolDesc";
            this.AttackSolDesc.Size = new System.Drawing.Size(480, 389);
            this.AttackSolDesc.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Attack Identifier";
            // 
            // AttackId
            // 
            this.AttackId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttackId.Location = new System.Drawing.Point(98, 10);
            this.AttackId.MaxLength = 100;
            this.AttackId.Name = "AttackId";
            this.AttackId.Size = new System.Drawing.Size(480, 23);
            this.AttackId.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Healer Identifier";
            // 
            // HealerId
            // 
            this.HealerId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HealerId.Location = new System.Drawing.Point(97, 43);
            this.HealerId.MaxLength = 100;
            this.HealerId.Name = "HealerId";
            this.HealerId.Size = new System.Drawing.Size(480, 23);
            this.HealerId.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Healer Address";
            // 
            // HealerAddr
            // 
            this.HealerAddr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HealerAddr.Location = new System.Drawing.Point(96, 79);
            this.HealerAddr.MaxLength = 100;
            this.HealerAddr.Name = "HealerAddr";
            this.HealerAddr.Size = new System.Drawing.Size(480, 23);
            this.HealerAddr.TabIndex = 17;
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Location = new System.Drawing.Point(95, 530);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(35, 13);
            this.fileNameLabel.TabIndex = 19;
            this.fileNameLabel.Text = "label6";
            // 
            // AttackSolutionMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.HealerAddr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.HealerId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SaveFileButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AttackSolDesc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AttackId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AttackSolutionMenu";
            this.Text = "AttackSolutionMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SaveFileButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox AttackSolDesc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox AttackId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox HealerId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox HealerAddr;
        private System.Windows.Forms.Label fileNameLabel;
    }
}