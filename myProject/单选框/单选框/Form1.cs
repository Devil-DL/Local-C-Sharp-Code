using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 单选框
{
    public partial class Form1 : Form
    {
        String GameNote = "";
        public Form1()
        {
            InitializeComponent();
        }
        private void NoteChange(GroupBox g)
        {
            foreach (Control C in g.Controls)
            {
                if (C.GetType()==typeof(RadioButton))
                {
                    RadioButton r = C as RadioButton;
                    if (r.Checked) GameNote += r.Text;
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)

        {
            GameNote = "";
            NoteChange(groupBox1);
            GameNote += " will fight with ";
            NoteChange(groupBox2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GameNote);
        }
    }
}
