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
    public partial class ListAttacksMenu : Form
    {
        private Hashtable _receivedAttacks;
                
        public ListAttacksMenu(Hashtable receivedAttacks)
        {
            _receivedAttacks = receivedAttacks;
                        
            InitializeComponent();
            
            AttacksBox.Items.Clear();
            AttacksBox.Items.Add("cenas");
        }

        private void AttackDetails_Click(object sender, EventArgs e)
        {
           


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
}
