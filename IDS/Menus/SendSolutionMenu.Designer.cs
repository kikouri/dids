namespace IDS.Menus
{
    partial class SendSolutionMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendSolutionMenu));
            this.AttackId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AttackSolDesc = new System.Windows.Forms.TextBox();
            this.FileTextBox = new System.Windows.Forms.TextBox();
            this.SelectFileButton = new System.Windows.Forms.Button();
            this.SubmitAttackSol = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AttackId
            // 
            this.AttackId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttackId.Location = new System.Drawing.Point(99, 20);
            this.AttackId.MaxLength = 100;
            this.AttackId.Name = "AttackId";
            this.AttackId.Size = new System.Drawing.Size(480, 23);
            this.AttackId.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Attack Identifier";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Attack Sol. Desc.";
            // 
            // AttackSolDesc
            // 
            this.AttackSolDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttackSolDesc.Location = new System.Drawing.Point(99, 57);
            this.AttackSolDesc.MaxLength = 2000;
            this.AttackSolDesc.Multiline = true;
            this.AttackSolDesc.Name = "AttackSolDesc";
            this.AttackSolDesc.Size = new System.Drawing.Size(480, 319);
            this.AttackSolDesc.TabIndex = 2;
            // 
            // FileTextBox
            // 
            this.FileTextBox.Location = new System.Drawing.Point(100, 399);
            this.FileTextBox.Name = "FileTextBox";
            this.FileTextBox.Size = new System.Drawing.Size(365, 20);
            this.FileTextBox.TabIndex = 4;
            // 
            // SelectFileButton
            // 
            this.SelectFileButton.Location = new System.Drawing.Point(471, 396);
            this.SelectFileButton.Name = "SelectFileButton";
            this.SelectFileButton.Size = new System.Drawing.Size(102, 25);
            this.SelectFileButton.TabIndex = 5;
            this.SelectFileButton.Text = "Select File";
            this.SelectFileButton.UseVisualStyleBackColor = true;
            this.SelectFileButton.Click += new System.EventHandler(this.SelectFileButton_Click);
            // 
            // SubmitAttackSol
            // 
            this.SubmitAttackSol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubmitAttackSol.Location = new System.Drawing.Point(219, 469);
            this.SubmitAttackSol.Name = "SubmitAttackSol";
            this.SubmitAttackSol.Size = new System.Drawing.Size(156, 50);
            this.SubmitAttackSol.TabIndex = 6;
            this.SubmitAttackSol.Text = "Submit Attack Solution Plugin";
            this.SubmitAttackSol.UseVisualStyleBackColor = true;
            this.SubmitAttackSol.Click += new System.EventHandler(this.SubmitAttackSol_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 381);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Select Plugin File (max. 512 Kb)";
            // 
            // SendSolutionMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SubmitAttackSol);
            this.Controls.Add(this.SelectFileButton);
            this.Controls.Add(this.FileTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AttackSolDesc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AttackId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SendSolutionMenu";
            this.Text = "Send Attack Solution";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox AttackId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox AttackSolDesc;
        private System.Windows.Forms.TextBox FileTextBox;
        private System.Windows.Forms.Button SelectFileButton;
        private System.Windows.Forms.Button SubmitAttackSol;
        private System.Windows.Forms.Label label3;

    }
}