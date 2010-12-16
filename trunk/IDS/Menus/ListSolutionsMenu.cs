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
    public partial class ListSolutionsMenu : Form
    {
        private ArrayList _receivedSolutions;

        public ListSolutionsMenu(ArrayList receivedSolutions)
        {
            _receivedSolutions = receivedSolutions;

            InitializeComponent();

            IEnumerator solutionsEnum = _receivedSolutions.GetEnumerator();
            while (solutionsEnum.MoveNext())
            {
                AttackSolutionMessage solutionMessage = (AttackSolutionMessage)solutionsEnum.Current;
                solutionsView.Items.Add(solutionMessage.AttackId + " " + solutionMessage.HealerId);
            }
        }

        private void viewAttackSolution_Click(object sender, EventArgs e)
        {
            if (solutionsView.SelectedItems.Count != 1)
            {
                MessageBox.Show("You must select one, and only one, item from the list.", "Selection Error");
                return;
            }

            String selectedSolution = solutionsView.SelectedItems[0].Text;

            IEnumerator solutionsEnum = _receivedSolutions.GetEnumerator();
            while (solutionsEnum.MoveNext())
            {
                AttackSolutionMessage solutionMessage = (AttackSolutionMessage)solutionsEnum.Current;
                String solutionMessageString = solutionMessage.AttackId+" "+solutionMessage.HealerId;
                if(solutionMessageString == selectedSolution)
                    (new AttackSolutionMenu(solutionMessage)).Show();
            }
        }
    }
}
