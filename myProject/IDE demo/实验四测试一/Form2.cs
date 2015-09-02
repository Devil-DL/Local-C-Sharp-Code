using System;
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
    public partial class Form2 : Form
    {
        private int cusorPos;
        private string lastText = null;
        private string TextFileName=null;
        private Boolean FileChanged = false;
        private string TextFilePath=null;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            richTextBox1.Width = this.Width-18;
            richTextBox1.Height = this.Height;
            richTextBox1.Font = new Font("Comic Sans MS", richTextBox1.Font.Size);
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            richTextBox1.Width = this.Width - 18;
            richTextBox1.Height = this.Height;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            FileChanged =true;
            richTextBox1.Font = new Font("Comic Sans MS", richTextBox1.Font.Size);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.FileChanged==true)
            {
                DialogResult result = MessageBox.Show(string.Format("是否将更改保存到 " + this.TextFileName), "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (this.TextFilePath == null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.FileName = this.Text + ".txt";
                        sfd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {

                            FileStream fs = new FileStream(sfd.FileName, FileMode.OpenOrCreate);
                            fs.Close();
                            File.WriteAllText(sfd.FileName, this.richTextBox1.Text.Replace("\n", "\r\n"), Encoding.Default);
                        }
                        else e.Cancel = true;
                    }
                    else File.WriteAllText(this.TextFilePath, this.richTextBox1.Text.Replace("\n", "\r\n"), Encoding.Default);
                }
                else if (result == DialogResult.Cancel) e.Cancel = true;
            }
        }
        public string GetText()
        {
            return this.richTextBox1.Text;
        }
        public void SetText(string s)
        {
            richTextBox1.Text = s;
        }
        public int RemoveSelection()
        {
            int temp = this.richTextBox1.SelectionStart;
            this.richTextBox1.Text=this.richTextBox1.Text.Remove(this.richTextBox1.SelectionStart, this.richTextBox1.SelectedText.Length);
            return temp;
        }
        public void InsertText(int start ,string s)
        {
            this.richTextBox1.Text=this.richTextBox1.Text.Insert(start, s);
        }
        public string ReplaceSelectionText()
        {
            return this.richTextBox1.SelectedText.Replace("\n", "\r\n");
        }
        public string ReplaceText()
        {
            return this.richTextBox1.Text.Replace("\n", "\r\n");
        }
        public void TextPos(int pos)
        {
            this.richTextBox1.Select(pos, 0);
        }
        public int GetSelectedStart()
        {
            return this.richTextBox1.SelectionStart;
        }
        public int GetSeleLen()
        {
            return this.richTextBox1.SelectedText.Length;
        }
        public string name
        {
            get
            {
                return this.TextFileName;
            }
            set
            {
                this.TextFileName = value;
            }
        }
        public Boolean hasChanged
        {
            get
            {
                return FileChanged;
            }
            set
            {
                this.FileChanged = value;
            }
        }

        public string path
        {
            get
            {
                return TextFilePath;
            }
            set
            {
                TextFilePath = value;
            }
        }
        public string latestText
        {
            get
            {
                return lastText;
            }
            set
            {
                lastText = value;
            }
        }
        public int CurPos
        {
            get
            {
                return cusorPos;
            }
            set
            {
                cusorPos = value;
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                this.cusorPos = this.richTextBox1.SelectionStart;
                this.lastText = this.richTextBox1.Text;
            }
            else if (e.KeyCode == Keys.Back)
            {
                this.cusorPos = this.richTextBox1.SelectionStart+this.richTextBox1.SelectedText.Length;
                this.lastText = this.richTextBox1.Text;

            }
        }

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tempP = GetSelectedStart();
            string tempS = GetText();
            if (latestText == null) return;
            SetText(latestText);
            TextPos(CurPos);
            latestText = tempS;
            CurPos = tempP;
           
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            latestText = GetText();
            CurPos = GetSelectedStart() + GetSeleLen();
            Clipboard.SetData(DataFormats.Text, ReplaceSelectionText());
            TextPos(RemoveSelection());

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, ReplaceSelectionText());

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text, true))
            {
                latestText = GetText();
                CurPos = GetSelectedStart();
                int start = RemoveSelection();
                InsertText(start, iData.GetData(DataFormats.Text, true).ToString());
                TextPos(start + iData.GetData(DataFormats.Text, true).ToString().Replace("\n", "").Length);

            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            latestText = GetText();
            CurPos = GetSelectedStart() + GetSeleLen();
            TextPos(RemoveSelection());
        }
    }
}
