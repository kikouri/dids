using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IDS.Menus
{
    public partial class UserLogin : Form
    {
        private Status _status;

        public UserLogin(Status status)
        {
            _status = status;
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (Username.Text.Length == 0 || Password.Text.Length == 0)
            {
                MessageBox.Show("You have to fill both Username and Password Fields.", "Input Fail!!");
                return;
            }

            bool isPasswordValid= PasswordFile.ValidatePassword(Password.Text);

            if (!isPasswordValid)
            {
                MessageBox.Show("The password that you have inserted is not valid!!","Password Validation Error!!");
                return;
            }

            MessageBox.Show("Login Succesfully complete!!", "Login Success!!");

            _status.IsLoggedOn = true;
            _status.IdsID = Username.Text;

            this.Close();
            this.Dispose();
        }
    }    
}