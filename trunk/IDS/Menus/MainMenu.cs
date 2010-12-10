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

        public MainMenu(Status status, ArrayList messagesToSend, Hashtable receivedAttacks)
        {
            _status = status;
            _messagesToSend = messagesToSend;
            _receivedAttacks = receivedAttacks;
            InitializeComponent();
        }

        private void shareNewAttackButton_Click(object sender, EventArgs e)
        {
            (new ShareAttackMenu(_status, ArrayList.Synchronized(_messagesToSend))).Show();
        }

        private void sendPluginButton_Click(object sender, EventArgs e)
        {
            (new SendSolutionMenu(_status, ArrayList.Synchronized(_messagesToSend))).Show();
        }

        private void listAttacksButton_Click(object sender, EventArgs e)
        {
            (new ListAttacksMenu(_receivedAttacks)).Show();
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
