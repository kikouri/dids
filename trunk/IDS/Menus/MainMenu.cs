using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace IDS.Menus
{
    public partial class MainMenu : Form
    {
        private Status _status;
        private ArrayList _messagesToSend;
        private Hashtable _receivedAttacks;
        private ArrayList _receivedSolutions;

        public MainMenu(Status status, ArrayList messagesToSend, Hashtable receivedAttacks, ArrayList receivedSolutions)
        {
            _status = status;
            _messagesToSend = messagesToSend;
            _receivedAttacks = receivedAttacks;
            _receivedSolutions = receivedSolutions;
            InitializeComponent();
        }

        private void shareNewAttackButton_Click(object sender, EventArgs e)
        {
            (new ShareAttackMenu(_status, _messagesToSend)).Show();
        }

        private void sendPluginButton_Click(object sender, EventArgs e)
        {
            (new SendSolutionMenu(_status, _messagesToSend)).Show();
        }

        private void listAttacksButton_Click(object sender, EventArgs e)
        {
            (new ListAttacksMenu(_receivedAttacks)).Show();
        }

        private void listSolutionsButton_Click(object sender, EventArgs e)
        {
            (new ListSolutionsMenu(_receivedSolutions)).Show();
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            _status.IsOnline = false;
        }
    }
}
