namespace IDS.Menus
{
    partial class UserLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserLogin));
            this.Username = new System.Windows.Forms.TextBox();
            this.User = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.Pass = new System.Windows.Forms.Label();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(112, 98);
            this.Username.MaxLength = 20;
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(138, 20);
            this.Username.TabIndex = 0;
            this.Username.Text = "Username";
            this.Username.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // User
            // 
            this.User.AutoSize = true;
            this.User.Location = new System.Drawing.Point(27, 100);
            this.User.Name = "User";
            this.User.Size = new System.Drawing.Size(55, 13);
            this.User.TabIndex = 1;
            this.User.Text = "Username";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(112, 140);
            this.Password.MaxLength = 20;
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(137, 20);
            this.Password.TabIndex = 2;
            this.Password.Tag = "";
            // 
            // Pass
            // 
            this.Pass.AutoSize = true;
            this.Pass.Location = new System.Drawing.Point(33, 143);
            this.Pass.Name = "Pass";
            this.Pass.Size = new System.Drawing.Size(53, 13);
            this.Pass.TabIndex = 3;
            this.Pass.Text = "Password";
            // 
            // SubmitButton
            // 
            this.SubmitButton.Location = new System.Drawing.Point(85, 194);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(113, 29);
            this.SubmitButton.TabIndex = 4;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            // 
            // UserLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.Pass);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.User);
            this.Controls.Add(this.Username);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserLogin";
            this.Text = "UserLogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Label User;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label Pass;
        private System.Windows.Forms.Button SubmitButton;
    }
}