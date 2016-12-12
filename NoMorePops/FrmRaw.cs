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
    public partial class FrmRaw : Form
    {
        public FrmRaw()
        {
            InitializeComponent();
        }

        public FrmRaw(string raw)
        {
            InitializeComponent();
            this.richTextBox1.Text = raw;
        }
        public FrmRaw(string title,string raw)
        {
            InitializeComponent();
            this.richTextBox1.Text = raw;
            this.Text = title;
        }
        private void FrmRaw_Load(object sender, EventArgs e)
        {

        }
    }
}
