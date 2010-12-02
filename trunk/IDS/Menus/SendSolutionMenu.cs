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
    public partial class SendSolutionMenu : Form
    {
        public SendSolutionMenu()
        {
            InitializeComponent();
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "IDS Plugin File (*.Ipg)|*.Ipg";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                FileTextBox.Text = openFile.FileName;
            }
        }

        

        
    }
}
