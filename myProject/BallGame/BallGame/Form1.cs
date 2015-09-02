using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BallGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool All = true;
            bool NotAll = true;
            foreach (Control C in groupBox1.Controls)
            {
                CheckBox ch = C as CheckBox;
                if (ch.Checked) NotAll = false;
                if (!ch.Checked) All = false;
            }
            if (All) AllSelected.CheckState = CheckState.Checked;
            else if (NotAll) AllSelected.CheckState = CheckState.Unchecked;
            else AllSelected.CheckState = CheckState.Indeterminate;
        }

        private void AllSelected_CheckedChanged(object sender, EventArgs e)
        {
            if (AllSelected.CheckState == CheckState.Checked)
            {
                foreach (Control C in groupBox1.Controls)
                {
                    CheckBox ch = C as CheckBox;
                    ch.Checked= true;
                }
            }
            else if(AllSelected.CheckState==CheckState.Unchecked)
            {
                foreach (Control C in groupBox1.Controls)
                {
                    CheckBox ch = C as CheckBox;
                    ch.Checked= false;
                }
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked) label1.Text = "true";
            else label1.Text = "false";
        }
    }
}
