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
            if (textBoxIAK.Text.Length == 0 || textBoxRefNum.Text.Length == 0)
            {
                MessageBox.Show("You have to fill both IAK and Reference Number fields.", "Input Error!!");
            }
            try
            {
                _refNumber = long.Parse(textBoxRefNum.Text);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("The number introduced as reference number is not valid!!", "Reference Number error!!");
                return;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("The number introduced as reference number is not valid!!", "Reference Number error!!");
                return;
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("The number introduced as reference number is not valid!!", "Reference Number error!!");
                return;
            }

            _iak = textBoxIAK.Text;

            this.Close();
        }
    }
}
