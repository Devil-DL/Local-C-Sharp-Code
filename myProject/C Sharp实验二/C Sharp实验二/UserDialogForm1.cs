using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Sharp实验二
{
    public partial class UserDialogForm1 : Form
    {
        public double tpSpeed;
        public UserDialogForm1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double speed;
            try {
            speed = Convert.ToDouble(textBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("键入非数值");
                textBox1.Text = "";
                return;
            }
            this.DialogResult = DialogResult.OK;
            tpSpeed = speed;
        }
    }
}
