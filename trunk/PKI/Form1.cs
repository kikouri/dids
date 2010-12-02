using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PKI
{
    public partial class Form1 : Form
    {
        private PKI _pki;
        
        public Form1()
        {
            InitializeComponent();
            _pki = new PKI();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            long reference = _pki.registerOffBand();
            
            this.textBoxRefValue.Text = System.Convert.ToString(reference);
            this.textBoxIAK.Text = _pki.getIAK(reference);
        }

        private void buttonRevocate_Click(object sender, EventArgs e)
        {
            if (textBoxSerialNumber.Text.Length == 0)
                return;

            _pki.revocateCertificate(Convert.ToInt64(textBoxSerialNumber.Text));

            listBoxRevocationList.Items.Clear();
            foreach (long i in _pki.getRevocationList())
            {
                listBoxRevocationList.Items.Add(i);
            }
        }
    }
}
