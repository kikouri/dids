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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void shareNewAttackButton_Click(object sender, EventArgs e)
        {
            (new ShareAttackMenu()).Show();
        }

        private void sendPluginButton_Click(object sender, EventArgs e)
        {
            (new SendSolutionMenu()).Show();
        }

        private void listAttacksButton_Click(object sender, EventArgs e)
        {
            (new ListAttacksMenu()).Show();
        }
    }
}
