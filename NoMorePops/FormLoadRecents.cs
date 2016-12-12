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
    public partial class FormLoadRecents : Form
    {
        public FormLoadRecents()
        {
            InitializeComponent();
        }
        public FormLoadRecents(List <string> l)
        {
            InitializeComponent();
            listBox1.Items.Clear();
            foreach (string s in l)
                if (s.Length >= 2)
                    listBox1.Items.Add(s);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //button1.Enabled = listBox1.SelectedIndex >= 0;
            button1.Text=(listBox1.SelectedIndex>=0)?"Select":"Cancel";
        }
        public string GetSelectedItem()
        {
            if (listBox1.SelectedIndex >= 0)
                return listBox1.SelectedItem.ToString();
            return "";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listBox1.SelectedIndex >= 0)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
    }
}
