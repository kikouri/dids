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
        private RA _ra;
        
        public Form1()
        {
            InitializeComponent();

            _ra = new RA();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            long reference = _ra.newRegister();
            
            this.textBoxRefValue.Text = System.Convert.ToString(reference);
            this.textBoxIAK.Text = _ra.getIAK(reference);
        }
    }
}
