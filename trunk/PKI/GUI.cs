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
    public partial class GUI : Form
    {
        private PKI _pki;

        public GUI(PKI pki)
        {
            InitializeComponent();
            _pki = pki;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            if (this.textBoxSubjectName.Text.Length == 0)
                return;
            
            long reference = _pki.registerOffBand(this.textBoxSubjectName.Text);
            
            this.textBoxRefValue.Text = System.Convert.ToString(reference);
            this.textBoxIAK.Text = _pki.getIAK(reference);

            this.textBoxSubjectName.Clear();
        }

        private void buttonRevocate_Click(object sender, EventArgs e)
        {
            if (textBoxSerialNumber.Text.Length == 0)
                return;

            long serial;

            try
            {
                serial = Convert.ToInt64(textBoxSerialNumber.Text);
            }
            //If the input is not valid
            catch
            {
                return;
            }

            _pki.revocateCertificate(serial);

            listBoxRevocationList.Items.Clear();
            foreach (long i in _pki.getRevocationList())
            {
                listBoxRevocationList.Items.Add(i);
            }
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBoxIAK.Text);
        }
    }
}
