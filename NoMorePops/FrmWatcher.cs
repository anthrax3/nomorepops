using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NoMorePops
{
    public partial class FrmWatcher : Form
    {
        public FrmWatcher(string p="")
        {
            InitializeComponent();
            textBox1.Text = p;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            webBrowser1.Navigate(textBox1.Text);
        }
    }
}
