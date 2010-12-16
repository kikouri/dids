namespace IDS.Menus
{
    partial class ListSolutionsMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListSolutionsMenu));
            this.solutionsView = new System.Windows.Forms.ListView();
            this.viewAttackSolution = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // solutionsView
            // 
            this.solutionsView.Location = new System.Drawing.Point(12, 12);
            this.solutionsView.Name = "solutionsView";
            this.solutionsView.Size = new System.Drawing.Size(560, 493);
            this.solutionsView.TabIndex = 0;
            this.solutionsView.UseCompatibleStateImageBehavior = false;
            // 
            // viewAttackSolution
            // 
            this.viewAttackSolution.Location = new System.Drawing.Point(210, 516);
            this.viewAttackSolution.Name = "viewAttackSolution";
            this.viewAttackSolution.Size = new System.Drawing.Size(164, 34);
            this.viewAttackSolution.TabIndex = 1;
            this.viewAttackSolution.Text = "View Attack Solution";
            this.viewAttackSolution.UseVisualStyleBackColor = true;
            this.viewAttackSolution.Click += new System.EventHandler(this.viewAttackSolution_Click);
            // 
            // ListSolutionsMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.viewAttackSolution);
            this.Controls.Add(this.solutionsView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ListSolutionsMenu";
            this.Text = "ListSolutionsMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView solutionsView;
        private System.Windows.Forms.Button viewAttackSolution;
    }
}