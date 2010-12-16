using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CommModule.Messages;

namespace IDS.Menus
{
    public partial class AttackSolutionMenu : Form
    {
        private AttackSolutionMessage _attackSolution;

        public AttackSolutionMenu(AttackSolutionMessage attackSolution)
        {
            _attackSolution = attackSolution;

            InitializeComponent();

            AttackId.Text = attackSolution.AttackId;
            AttackSolDesc.Text = attackSolution.AttackDesc;
            HealerId.Text = attackSolution.HealerId;
            HealerAddr.Text = attackSolution.HealerAddress;

            fileNameLabel.Text = attackSolution.FileName;
        }

        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Plugin File (*.Ipg)|*.Ipg";
            saveFile.ShowDialog();

            if (saveFile.FileName != "")
            {
                FileStream fs = new FileStream(saveFile.FileName, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(System.Convert.FromBase64String(_attackSolution.FileContent));
                bw.Close();
                MessageBox.Show("File Saved Succesfully", "Save Successful");
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show("You must select a file to save to.", "File Save Error");
                return;
            }
        }
    }
}