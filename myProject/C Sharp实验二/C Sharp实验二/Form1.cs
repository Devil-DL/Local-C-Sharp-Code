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
    public partial class Form1 : Form
    {
        int Mode = 0;
        char[] ch = new char[] {  'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X','Y', 'Z' };
        int letterNumber = 0;
        double typeSpead = 1;
        TimeSpan time0 = new TimeSpan();
        TimeSpan time = new TimeSpan();
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time = time - new TimeSpan(0, 0, 1);
            label3.Text = time.ToString();
            if (time.Ticks == 0)
            {
                richTextBox1.ReadOnly = true;
                timer1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            String s = "";
            try {
                letterNumber = Convert.ToInt32(textBox2.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("键入非数值");
                textBox2.Text = "";
                return;
            }
            Random r = new Random();
            if (Mode==0) for (int i = 0; i < letterNumber; i++) s += Convert.ToString(ch[r.Next(ch.Length)]);
            if (Mode==1) for (int i = 0; i < letterNumber; i++) s += Convert.ToString(ch[r.Next(36)]);
            if (Mode==2) for (int i = 0; i < letterNumber; i++) s += Convert.ToString(ch[r.Next(26)]);
            textBox1.Text = s;
            button3.Enabled = true;
            button1.Enabled = false;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text==richTextBox1.Text)
            {
                timer1.Enabled = false;
                string s = (time0 - time).ToString();
                MessageBox.Show(string.Format("完全匹配，您的得分为100，您的用时为{0}",s));
                button1.Enabled = true;
                button4.Enabled = false;
                label3.Text = "00:00:00";
            }
            if (richTextBox1.Text.Length > textBox1.Text.Length)
            {
                MessageBox.Show("您输入的内容过长，当然我不是在质疑您的智商");
                string s = richTextBox1.Text.Substring(0,richTextBox1.Text.Length-1);
                richTextBox1.Text = s;
                richTextBox1.Select(s.Length, 0);
                return;
            }
            int l = richTextBox1.Text.Length;
            if (l == 0) return;
            if (richTextBox1.Text[l-1]!=textBox1.Text[l-1])
            {
                richTextBox1.Select(l - 1, 1);
                richTextBox1.SelectionColor = Color.Red;
                richTextBox1.Select(l, 0);
            }
            else
            {
                richTextBox1.Select(l - 1, 1);
                richTextBox1.SelectionColor = Color.Black;
                richTextBox1.Select(l, 0);
            }
        }

    

        private void button3_Click(object sender, EventArgs e)
        {
            time = new TimeSpan(0, 0, (int)(letterNumber * typeSpead));
            time0 = time;
            timer1.Enabled = true;
            richTextBox1.ReadOnly = false;
            button4.Enabled = true;
            button3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            int myCount = 0;
            int lenth = richTextBox1.Text.Length;
            for (int i=0;i<lenth;i++)
            {
                if (richTextBox1.Text[i] == textBox1.Text[i]) myCount++;
            }
            double score =100*(double)myCount / textBox1.Text.Length;
            string s = (time0 - time).ToString();
            MessageBox.Show(String.Format("您的得分为：{0:f1} ,  用时{1}", score,s));
            button1.Enabled = true;
            button4.Enabled = false;
            richTextBox1.ReadOnly = true;
            label3.Text = "00:00:00";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 fm2 = new Form2();
            if (fm2.ShowDialog() == DialogResult.OK)
            {
                textBox1.Font = fm2.f;
                textBox1.ForeColor = fm2.c;
            }
            fm2.Close();
            fm2.Dispose();
        }

        private void 初级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            typeSpead = 2;
        }

        private void 中级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            typeSpead = 1;
        }

        private void 高级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            typeSpead = 0.5;
        }

        private void 自定义ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserDialogForm1 ufm1 = new UserDialogForm1();
            if (ufm1.ShowDialog()==DialogResult.OK)
            {
                typeSpead = ufm1.tpSpeed;
            }
            ufm1.Close();
            ufm1.Dispose();
        }

        private void 常规ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mode = 0;
        }

        private void 非大写模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mode = 1;
        }

        private void 纯小写模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mode = 2;
        }

        private void 功能简介ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("  这是一个简单的打字软件，可以测试您的打字速度与准确率，并具有以下功能：\n  （1）修改打字速度，以符合您的个人习惯\n  （2）修改测试模式，享受不同的打字体验\n  （3）对字体进行修改，使界面更加个性化\n");
        }

        private void 版本检测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("已经是最新版本");
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Design by 丁磊\n Test by 邬昊天\n 制作日期：2015年5月18日\n 如果发现任何bug，欢迎联系作者:  dinglei@whut.edu.cn\n 请支持仙剑奇侠传六，支持国产正版游戏\n\n\n ALL RIGHT RESEVED ");
        }
    } 
}
