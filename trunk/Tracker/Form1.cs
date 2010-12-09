using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommModule.Messages;
using System.Collections;

namespace Tracker
{
    public partial class Form1 : Form
    {

        public Form1(ThreadWorker tw)
        {
            InitializeComponent(tw);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList activeNodes = tw.hashTableToArray();
            if (activeNodes == null)
            {
                return;
            }
            listBox1.Items.Clear();
            foreach (Node n in activeNodes)
            {
                listBox1.Items.Add("Last seen " + n.IPAddress + ":" + n.port + " at " + n.LastTime);
            }
        }
    }
}
