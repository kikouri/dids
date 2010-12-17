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

        private string _pkiAddress;

        public long ReferenceNumber
        {
            get { return _refNumber; }
        }
        public string IAK
        {
            get { return _iak; }
        }

        public string PKIAddress
        {
            get { return _pkiAddress; }
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
            if (textBoxIAK.Text.Length == 0 || textBoxRefNum.Text.Length == 0 || textBoxPKIAddress.Text.Length == 0)
            {
                MessageBox.Show("You have to fill in all fields.", "Input Error!!");
            }
            try
            {
                _refNumber = long.Parse(textBoxRefNum.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("The number introduced as reference number is not valid!!", "Reference Number error!!");
                return;
            }

            System.Net.IPAddress ip;
            if (! System.Net.IPAddress.TryParse(textBoxPKIAddress.Text, out ip))
            {
                MessageBox.Show("The IP introduced as the address of the PKI is not valid!!", "PKI Address error!!");
                return;
            }
            _pkiAddress = textBoxPKIAddress.Text;
            _iak = textBoxIAK.Text;

            this.Close();
        }
    }
}
