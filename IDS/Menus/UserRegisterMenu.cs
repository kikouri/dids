using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace IDS.Menus
{
    public partial class UserRegisterMenu : Form
    {
        private Status _status;

        public UserRegisterMenu(Status status)
        {
            _status = status;
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (Username.Text.Length == 0 || Password.Text.Length == 0 || PasswordConf.Text.Length == 0)
            {
                MessageBox.Show("You have to fill all the fields on the register menu.", "Register Error.");
                Password.Text = "";
                PasswordConf.Text = "";
                return;
            }

            if (Password.Text != PasswordConf.Text)
            {
                MessageBox.Show("The password and password confirmation that you have input do not match.", "Password do not match");
                Password.Text = "";
                PasswordConf.Text = "";
                return;
            }

            int passwordStrength = CheckStrength(Password.Text);

            if (passwordStrength < 3)
            {
                MessageBox.Show("The password that you have defined is too weak. \n" +
                    "Your password must be a combination of letters, numbers and symbols.", "Password Strength Failed");
                Password.Text = "";
                PasswordConf.Text = "";
                return;
            }

            MessageBox.Show("Register Successfully done. Don't forget or give to anyone your password.", "Register Success!!");
     
            PasswordFile.CreatePasswordFile(Password.Text);

            _status.IdsID = Username.Text;
            _status.Password = Password.Text;
            _status.IsLoggedOn = true;

            this.Close();
            this.Dispose();            
        }

        private int CheckStrength(string password)
        {
            int score = 1;
            
            if (password.Length < 4)
                return score;

            if (password.Length >= 8)
                score++;
            if (password.Length >= 12)
                score++;
            if (Regex.Match(password, @"/\d+/", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @"/[a-z]/", RegexOptions.ECMAScript).Success &&
                Regex.Match(password, @"/[A-Z]/", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript).Success)
                score++;

            return score;
        }
    }
}
