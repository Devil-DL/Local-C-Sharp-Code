using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 实验四测试一
{
    public partial class Form3 : Form
    {
        public Form3(string err)
        {
            InitializeComponent();
            this.richTextBox1.Text = err;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            richTextBox1.Width = this.Width - 18;
            richTextBox1.Height = this.Height;
        }

        private void Form3_Resize(object sender, EventArgs e)
        {
            richTextBox1.Width = this.Width - 18;
            richTextBox1.Height = this.Height;
        }
    }

}
