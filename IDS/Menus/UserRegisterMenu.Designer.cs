namespace IDS.Menus
{
    partial class UserRegisterMenu
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
            this.SubmitButton = new System.Windows.Forms.Button();
            this.Pass = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.User = new System.Windows.Forms.Label();
            this.Username = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PasswordConf = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SubmitButton
            // 
            this.SubmitButton.Location = new System.Drawing.Point(86, 189);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(113, 29);
            this.SubmitButton.TabIndex = 9;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // Pass
            // 
            this.Pass.AutoSize = true;
            this.Pass.Location = new System.Drawing.Point(30, 81);
            this.Pass.Name = "Pass";
            this.Pass.Size = new System.Drawing.Size(53, 13);
            this.Pass.TabIndex = 8;
            this.Pass.Text = "Password";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(110, 77);
            this.Password.MaxLength = 20;
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(137, 20);
            this.Password.TabIndex = 7;
            this.Password.Tag = "";
            // 
            // User
            // 
            this.User.AutoSize = true;
            this.User.Location = new System.Drawing.Point(24, 37);
            this.User.Name = "User";
            this.User.Size = new System.Drawing.Size(55, 13);
            this.User.TabIndex = 6;
            this.User.Text = "Username";
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(109, 33);
            this.Username.MaxLength = 20;
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(138, 20);
            this.Username.TabIndex = 5;
            this.Username.Text = "Username";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Confirm Password";
            // 
            // PasswordConf
            // 
            this.PasswordConf.Location = new System.Drawing.Point(110, 149);
            this.PasswordConf.MaxLength = 20;
            this.PasswordConf.Name = "PasswordConf";
            this.PasswordConf.PasswordChar = '*';
            this.PasswordConf.Size = new System.Drawing.Size(137, 20);
            this.PasswordConf.TabIndex = 10;
            this.PasswordConf.Tag = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(94, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 39);
            this.label2.TabIndex = 12;
            this.label2.Text = "The password must be a combination\r\nof letters {a-z,A-Z}, numbers {0-9} and\r\nsymb" +
                "ols {!&/?|}.\r\n";
            // 
            // UserRegiserMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PasswordConf);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.Pass);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.User);
            this.Controls.Add(this.Username);
            this.Name = "UserRegiserMenu";
            this.Text = "UserRegiserMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.Label Pass;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label User;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PasswordConf;
        private System.Windows.Forms.Label label2;
    }
}