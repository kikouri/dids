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
    public partial class ListAttacksMenu : Form
    {
        private Hashtable _receivedAttacks;
                
        public ListAttacksMenu(Hashtable receivedAttacks)
        {
            _receivedAttacks = receivedAttacks;
                        
            InitializeComponent();
            
            AttacksBox.Items.Clear();
            lock (_receivedAttacks.SyncRoot)
            {
                IDictionaryEnumerator attacksEnum = _receivedAttacks.GetEnumerator();

                while (attacksEnum.MoveNext())
                {
                    NewAttackMessage newAttack = (NewAttackMessage)attacksEnum.Value;
                    AttacksBox.Items.Add(newAttack.AttackId);
                }
            }            
        }

        private void AttackDetails_Click(object sender, EventArgs e)
        {
            String attackId = (String)AttacksBox.SelectedItem;

            if (attackId == null)
                return;

            NewAttackMessage newAttack;

            lock (_receivedAttacks.SyncRoot)
            {
                if (_receivedAttacks.ContainsKey(attackId))
                    newAttack = (NewAttackMessage)_receivedAttacks[attackId];
                else
                {
                    MessageBox.Show("The selected attack no more exists.", "Select Error!!");
                    return;
                }
            }

            (new AttackDetailsMenu(newAttack)).Show();
        }       
    }
}
