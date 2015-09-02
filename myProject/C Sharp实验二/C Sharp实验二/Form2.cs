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
    public partial class Form2 : Form
    {
        public Font f=new Font("宋体",11);
        public Color c = new Color();
        public Form2()
        {
            InitializeComponent();
            string[] items1 = {"华文中宋","楷体", "宋体","微软雅黑","新宋体","仿宋"};
            comboBox1.Items.AddRange(items1);
            string[] items2 = { "常规", "倾斜", "粗体", "粗体倾斜" };
            comboBox2.Items.AddRange(items2);
            string[] items3 = { "初号", "小初", "一号", "小一", "二号", "小二", "三号", "小三", "四号", "小四" ,"五号","小五","六号","小六","七号","八号"};
            comboBox3.Items.AddRange(items3);
            string[] items4 = { "黑色", "蓝色", "红色", "黄色" };
            comboBox4.Items.AddRange(items4);
            label5.Left = (groupBox2.Width - label5.Width) / 2;
            label5.Top = (groupBox2.Height - label5.Height) / 2;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label5.Font = new Font(comboBox1.SelectedItem.ToString(),label5.Font.Size,label5.Font.Style);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            double a=0;
            switch(comboBox3.SelectedItem.ToString())
            {
                case "初号":
                    a = 42;
                    break;
                case "小初":
                    a = 36;
                    break;
                case "一号":
                    a = 26;
                    break;
                case "小一":
                    a = 24;
                    break;
                case "二号":
                    a = 22;
                    break;
                case "小二":
                    a = 18;
                    break;
                case "三号":
                    a = 16;
                    break;
                case "小三":
                    a = 15;
                    break;
                case "四号":
                    a = 14;
                    break;
                case "小四":
                    a = 12;
                    break;
                case "五号":
                    a = 10.5;
                    break;
                case "小五":
                    a = 9;
                    break;
                case "六号":
                    a = 7.5;
                    break;
                case "小六":
                    a = 6.5;
                    break;
                case "七号":
                    a = 5.5;
                    break;
                case "八号":
                    a = 5;
                    break;
                default:
                    break;
            }
            label5.Font = new Font(label5.Font.FontFamily,(float)a,label5.Font.Style);
            label5.Left = (groupBox2.Width - label5.Width) / 2;
            label5.Top = (groupBox2.Height - label5.Height) / 2;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FontStyle fs = new FontStyle();
            if (comboBox2.SelectedItem!=null)
                switch (comboBox2.SelectedItem.ToString())
                {
                    case "常规":
                        fs = fs | FontStyle.Regular;
                        break;
                    case "粗体":
                        fs = fs | FontStyle.Bold;
                        break;
                    case "倾斜":
                        fs = fs | FontStyle.Italic;
                        break;
                    case "粗体倾斜":
                        fs = fs | FontStyle.Bold | FontStyle.Italic;
                        break;
                    default:
                        break;
                }
            if (checkBox1.Checked) fs = fs | FontStyle.Strikeout;
            if (checkBox2.Checked) fs = fs | FontStyle.Underline;
            label5.Font = new Font(label5.Font.FontFamily, label5.Font.Size, fs);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox4.SelectedItem.ToString())
            {
                case "黑色":
                    label5.ForeColor = Color.Black;
                    break;
                case "蓝色":
                    label5.ForeColor = Color.RoyalBlue;
                    break;
                case "红色":
                    label5.ForeColor = Color.Red;
                    break;
                case "黄色":
                    label5.ForeColor = Color.Goldenrod;
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f = label5.Font;
            c = label5.ForeColor;
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void comboBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            Rectangle listBound = e.Bounds;
            Font listFond = null;
            switch (e.Index)
            {
                case 0:
                    listFond = new Font(e.Font, FontStyle.Regular);
                    break;
                case 2:
                    listFond = new Font(e.Font, FontStyle.Bold);
                    break;
                case 1:
                    listFond = new Font(e.Font, FontStyle.Italic);
                    break;
                case 3:
                    listFond = new Font(e.Font, FontStyle.Bold|FontStyle.Italic);
                    break;
            }
            string str = comboBox2.Items[e.Index].ToString();
            Brush bru = new SolidBrush(Color.Black);
            Graphics g = e.Graphics;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(0xFF,0x33,0x99,0xFF)), listBound);
                g.DrawString(str, listFond, new SolidBrush(Color.White), listBound);
            }
            else
            {
                g.FillRectangle(new SolidBrush(Color.White), listBound);
                g.DrawString(str, listFond, bru, listBound);
            }
        }

        private void comboBox4_DrawItem(object sender, DrawItemEventArgs e)
        {
            Color bruLeftColor = Color.Empty;
            switch (e.Index)
            {
                case 0:
                    bruLeftColor = Color.Black;
                    break;
                case 1:
                    bruLeftColor = Color.RoyalBlue;
                    break;
                case 2:
                    bruLeftColor = Color.Red;
                    break;
                case 3:
                    bruLeftColor = Color.Goldenrod;
                    break;

            }
            Rectangle listBoundAll = e.Bounds;
            Rectangle listBoundLeft = listBoundAll;
            listBoundLeft.Width = 20;
            listBoundLeft.Height = listBoundAll.Height - 2;
            Brush bruLeft = new SolidBrush(bruLeftColor);
            Rectangle listBoundRight = listBoundAll;
            listBoundRight.Width = listBoundAll.Width - 20;
            listBoundRight.X = listBoundLeft.Right;
            Graphics g = e.Graphics;
            if (e.State == (DrawItemState.NoAccelerator | DrawItemState.NoFocusRect))
            {
                g.FillRectangle(new SolidBrush(Color.White), listBoundAll);
                g.FillRectangle(bruLeft, listBoundLeft);
                g.DrawString(comboBox4.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), listBoundRight);
            }
            else
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(0xFF, 0x33, 0x99, 0xFF)), listBoundAll);
                g.FillRectangle(bruLeft, listBoundLeft);
                g.DrawString(comboBox4.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.White), listBoundRight);
            }
        }
    }
}
