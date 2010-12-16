using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommModule
{
    public partial class RefNumAndIAK : Form
    {
        private long _refNumber;

        private string _iak;

        public long ReferenceNumber
        {
            get { return _refNumber; }
        }
        public string IAK
        {
            get { return _iak; }
        }
        
        public RefNumAndIAK()
        {
            InitializeComponent();
        }

        private void buttonPaste_Click(object sender, EventArgs e)
        {
            textBoxIAK.Text = Clipboard.GetText();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            _iak = textBoxIAK.Text;
            _refNumber = long.Parse(textBoxRefNum.Text);

            this.Close();
        }
    }
}
