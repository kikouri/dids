namespace IDS.Menus
{
    partial class ListAttacksMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListAttacksMenu));
            this.AttacksBox = new System.Windows.Forms.ListBox();
            this.AttackDetails = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AttacksBox
            // 
            this.AttacksBox.FormattingEnabled = true;
            this.AttacksBox.Location = new System.Drawing.Point(12, 28);
            this.AttacksBox.Name = "AttacksBox";
            this.AttacksBox.Size = new System.Drawing.Size(550, 459);
            this.AttacksBox.TabIndex = 0;
            // 
            // AttackDetails
            // 
            this.AttackDetails.Location = new System.Drawing.Point(207, 511);
            this.AttackDetails.Name = "AttackDetails";
            this.AttackDetails.Size = new System.Drawing.Size(170, 35);
            this.AttackDetails.TabIndex = 1;
            this.AttackDetails.Text = "View Attack Details";
            this.AttackDetails.UseVisualStyleBackColor = true;
            this.AttackDetails.Click += new System.EventHandler(this.AttackDetails_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "List of the attacks detected on the network";
            // 
            // ListAttacksMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AttackDetails);
            this.Controls.Add(this.AttacksBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ListAttacksMenu";
            this.Text = "List Discovered Attacks";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox AttacksBox;
        private System.Windows.Forms.Button AttackDetails;
        private System.Windows.Forms.Label label1;


    }
}