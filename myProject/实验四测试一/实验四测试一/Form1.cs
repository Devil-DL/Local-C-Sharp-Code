﻿using System;
using System.IO;
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
    public partial class Form1 : Form
    {
        int count = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fm2 = new Form2();
            fm2.Text = "未命名" + count.ToString();
            fm2.name = fm2.Text;
            count++;
            fm2.MdiParent = this;
            fm2.hasChanged = true;
            fm2.Show();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form2 i in this.MdiChildren)
            {
                if (i == this.ActiveMdiChild)
                { 
                    Clipboard.SetData(DataFormats.Text, i.ReplaceSelectionText());
                    break;
                }
            }
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form2 i in this.MdiChildren)
            {
                if (i == this.ActiveMdiChild)
                {
                    i.latestText = i.GetText();
                    i.CurPos = i.GetSelectedStart()+i.GetSeleLen();
                    Clipboard.SetData(DataFormats.Text, i.ReplaceSelectionText());
                    i.TextPos(i.RemoveSelection());
                    break;
                }
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text, true))
            {
                foreach (Form2 i in this.MdiChildren)
                {
                    if (i == this.ActiveMdiChild)
                    {
                        i.latestText = i.GetText();
                        i.CurPos = i.GetSelectedStart();
                        int start=i.RemoveSelection();
                        i.InsertText(start, iData.GetData(DataFormats.Text, true).ToString());
                        i.TextPos(start + iData.GetData(DataFormats.Text, true).ToString().Replace("\n","").Length);
                        break;
                    }
                }
            }

        }

        private void 取消ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fm2 = (Form2)this.ActiveMdiChild;
            if (fm2 == null) return;
            int tempP= fm2.GetSelectedStart();
            string tempS= fm2.GetText();
            if (fm2.latestText == null) return;
            fm2.SetText( fm2.latestText);
            fm2.TextPos(fm2.CurPos);
            fm2.latestText = tempS;
            fm2.CurPos = tempP;
        }

        private void 水平平铺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void 垂直平铺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void 层叠ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter= "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                Form2 fm2 = new Form2();
                fm2.path = ofd.FileName;
                fm2.name = Path.GetFileNameWithoutExtension(fm2.path);
                fm2.Text = fm2.name;
                StreamReader sr = new StreamReader(ofd.FileName, Encoding.Default);
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    fm2.SetText( fm2.GetText()+ line);
                    fm2.SetText(fm2.GetText() + "\n");
                }
                sr.Close();
                fm2.MdiParent = this;
                fm2.hasChanged = false;
                fm2.Show();
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fm2 = (Form2)this.ActiveMdiChild;
            if (fm2.path==null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = fm2.Text + ".txt";
                sfd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                   
                    FileStream fs = new FileStream(sfd.FileName, FileMode.OpenOrCreate);
                    fs.Close();
                    foreach (Form2 i in this.MdiChildren)
                    {
                        if (i == this.ActiveMdiChild)
                        {
                            File.WriteAllText(sfd.FileName, i.ReplaceText(),Encoding.Default);
                            i.hasChanged = false;
                        }
                    }
                }
            }
            else
            {
                File.WriteAllText(fm2.path, fm2.ReplaceText(), Encoding.Default);
                fm2.hasChanged = false;
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName =((Form2) this.ActiveMdiChild).Text + ".txt";
            sfd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(sfd.FileName, FileMode.OpenOrCreate);
                fs.Close();
                foreach (Form2 i in this.MdiChildren)
                {
                    if (i == this.ActiveMdiChild)
                    {
                        File.WriteAllText(sfd.FileName, i.ReplaceText(), Encoding.Default);
                        i.hasChanged = false;
                    }
                }
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fm2 = (Form2)this.ActiveMdiChild;
            fm2.RemoveSelection();
        }

        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void 功能简介ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("这是一款轻量级多文档记事本软件，基本实现了Windows系统自带的记事本功能，并具有以下特点：\n\n  (1)支持多文件操作，您可以同时对多个文本文件进行处理.\n  (2)支持多种视图排列方式，以满足您对多文档操作的各种需要\n  (3)您可以对文本文件进行插入、删除、撤销等系统功能，并且可以将您新建的文本文件保存至您的计算机上\n  ");
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Design by 丁磊\n 制作日期：2015年6月8日\n 如果发现任何bug，欢迎联系作者:  dinglei@whut.edu.cn\n 请支持仙剑奇侠传六，支持国产正版游戏\n\n\n ALL RIGHT RESEVED ");
        }
    }
}
