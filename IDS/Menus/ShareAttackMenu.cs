using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using CommModule.Messages;

namespace IDS.Menus
{
    public partial class ShareAttackMenu : Form
    {
        private Status _status;
        private ArrayList _messagesToSend;

        public ShareAttackMenu(Status status, ArrayList messagesToSend)
        {
            _status = status;
            _messagesToSend = messagesToSend;
            InitializeComponent();
        }

        private void SubmitAttack_Click(object sender, EventArgs e)
        {
            if (AttackName.Text.Length == 0 || AttackDescription.Text.Length == 0)
            {
                MessageBox.Show("You have to set some content either in the attack name and in the attack " +
                "description.","Input Error!!");
            }
            else
            {
                NewAttackMessage newAttack = new NewAttackMessage();
                newAttack.AttackName = AttackName.Text;
                newAttack.AttackDescription = AttackDescription.Text;
                _status.PublishedAttackMaxId++;
                newAttack.AttackId = _status.IdsID + _status.PublishedAttackMaxId.ToString();
                newAttack.SourceIdsAddress = _status.Node.IPAddress;
                newAttack.SourceIdsPort = _status.Node.port;
                newAttack.SourceIdsId = _status.IdsID;

                lock (_messagesToSend.SyncRoot)
                {
                    _messagesToSend.Add(newAttack);
                }

                this.Close();
                MessageBox.Show("Attack Detection Sent Successfully.", "Send successful!!");
                this.Dispose();
            }
        }
    }
}
