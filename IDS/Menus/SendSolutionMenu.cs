using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using CommModule.Messages;

namespace IDS.Menus
{
    public partial class SendSolutionMenu : Form
    {
        private Status _status;
        private ArrayList _messagesToSend;

        public SendSolutionMenu(Status status, ArrayList messagesToSend)
        {
            _status = status;
            _messagesToSend = messagesToSend;
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

        private void SubmitAttackSol_Click(object sender, EventArgs e)
        {
            if (AttackId.Text.Length == 0 || AttackSolDesc.Text.Length == 0)
            {
                MessageBox.Show("You have to imput some text either in the attack identifier " +
                    "and in the attack description to who you are giving solution.", "Input Error!!");
            }
            else
            {
                AttackSolutionMessage solMessage = new AttackSolutionMessage();
                solMessage.AttackId = AttackId.Text;
                solMessage.AttackDesc = AttackSolDesc.Text;
                if (FileTextBox.Text.Length > 0)
                {
                    solMessage.FileName = FileTextBox.Text;

                    byte[] buff = null;
                    FileStream fs = new FileStream(FileTextBox.Text, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    long numBytes = new FileInfo(FileTextBox.Text).Length;
                    buff = br.ReadBytes((int)numBytes);
                    solMessage.FileContent = System.Convert.ToBase64String(buff);
                }

                lock (_messagesToSend.SyncRoot)
                {
                    _messagesToSend.Add(solMessage);
                }
                this.Close();
                this.Dispose();
            }
            
        }     
    }
}
