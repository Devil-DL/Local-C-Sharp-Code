using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] s = new string[] { "计算机导论", "数值分析", "可视化编程", "数据结构","英语", "洗脑课程","数字命理学","魔药学","黑魔法防御术","古典魔法史" };
            comboBox1.Items.AddRange(s);
            listBox1.SelectionMode = SelectionMode.MultiExtended;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s = comboBox1.Text;
            if (s.Length == 0) { MessageBox.Show("请输入添加课程"); return; }
            else if (!comboBox1.Items.Contains(s))
            {
                comboBox1.Items.Add(s);
                listBox1.Items.Add(s);
            }
            else if (listBox1.Items.Contains(s))
            {
                MessageBox.Show(string.Format("{0}已添加！",s));
            }
            else listBox1.Items.Add(s);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i=listBox1.SelectedItems.Count-1;i>=0;i--)
            {
                listBox1.Items.Remove(listBox1.SelectedItems[i]);
            }
        }
    }
}
