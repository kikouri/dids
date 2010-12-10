using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommModule.Messages;

namespace IDS.Menus
{
    public partial class AttackDetailsMenu : Form
    {
        private NewAttackMessage _attackToShow;

        public AttackDetailsMenu(NewAttackMessage attackToShow)
        {
            _attackToShow = attackToShow;
            InitializeComponent();
            AttackedIdsIp.Text = _attackToShow.SourceIdsAddress;
            AttackedIdsPort.Text = attackToShow.SourceIdsPort.ToString();
            AttackId.Text = attackToShow.AttackId;
            IdsIdentifier.Text = attackToShow.SourceIdsId;
            AttackName.Text = attackToShow.AttackName;
            AttackDescription.Text = attackToShow.AttackDescription;
        }
    }
}
