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

        }
    }    
}