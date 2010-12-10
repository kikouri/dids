namespace IDS.Menus
{
    partial class ShareAttackMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShareAttackMenu));
            this.AttackName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AttackDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SubmitAttack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AttackName
            // 
            this.AttackName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttackName.Location = new System.Drawing.Point(91, 45);
            this.AttackName.Name = "AttackName";
            this.AttackName.Size = new System.Drawing.Size(478, 23);
            this.AttackName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Attack Name";
            // 
            // AttackDescription
            // 
            this.AttackDescription.Location = new System.Drawing.Point(92, 87);
            this.AttackDescription.Multiline = true;
            this.AttackDescription.Name = "AttackDescription";
            this.AttackDescription.Size = new System.Drawing.Size(476, 372);
            this.AttackDescription.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-1, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Attack Description";
            // 
            // SubmitAttack
            // 
            this.SubmitAttack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubmitAttack.Location = new System.Drawing.Point(212, 479);
            this.SubmitAttack.Name = "SubmitAttack";
            this.SubmitAttack.Size = new System.Drawing.Size(167, 44);
            this.SubmitAttack.TabIndex = 4;
            this.SubmitAttack.Text = "&Submit Attack";
            this.SubmitAttack.UseVisualStyleBackColor = true;
            this.SubmitAttack.Click += new System.EventHandler(this.SubmitAttack_Click);
            // 
            // ShareAttackMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.SubmitAttack);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AttackDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AttackName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShareAttackMenu";
            this.Text = "Share New Attack Discovered";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox AttackName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox AttackDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SubmitAttack;
    }
}