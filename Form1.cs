using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static String msg = "";
       

        public Form1()
        {
            InitializeComponent();
            checkedListBox2.Items.AddRange(Data.filters);
            checkedListBox2.CheckOnClick = true;
            checkedListBox1.Items.AddRange(Data.lists);
            checkedListBox1.CheckOnClick = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Runnable runnable = new Runnable(checkedListBox1);
            Thread thread = new Thread(new ThreadStart(runnable.run));
            thread.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Runnable runnable = new Runnable(checkedListBox1);
            Thread thread = new Thread(new ThreadStart(runnable.clean));
            thread.Start();
        }

        public class Runnable
        {
            CheckedListBox checkedListBox1;

            [DllImport("user32.dll")]
            public static extern int FindWindow(string lpClassName, string lpWindowName);

            [DllImport("user32.dll")]
            public static extern int FindWindowEx(int hWnd1, int hWnd2, string lpsz1, string lpsz2);

            [DllImport("user32.dll")]
            public static extern int SendMessage(int hwnd, int wMsg, int wParam, string lParam);

            [DllImport("user32.dll")]
            public static extern uint PostMessage(int hwnd, int wMsg, int wParam, int lParam);

            public Runnable(CheckedListBox checkedListBox)
            {
                this.checkedListBox1 = checkedListBox;
            }

            private void SendKatalk(string title, string msg)
            {
                int hd01 = FindWindow(null, title);
                int hd03 = FindWindowEx(hd01, 0, "RichEdit20W", "");
                SendMessage(hd03, 0x000c, 0, msg);
                Thread.Sleep(20);
            }

            private void cleankakao(string title)
            {
                int hd01 = FindWindow(null, title);
                int hd03 = FindWindowEx(hd01, 0, "RichEdit20W", "");

                PostMessage(hd03, 0x0100, 0xD, 0x1C001);
                Thread.Sleep(20);
                PostMessage(hd03, 0x0100, 0xD, 0x1C001);
                Thread.Sleep(80);
            }

            public void run()
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        string str = Data.lists[i];
                        SendKatalk(str, msg);
                    }
                }
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        string str = Data.lists[i];
                        cleankakao(str);
                    }
                }
            }

            public void clean()
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        string str = Data.lists[i];
                        SendKatalk(str, msg);
                    }
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            msg = richTextBox1.Text;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void selectAllOnClick(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
                checkedListBox1.SetItemCheckState(i, CheckState.Checked);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
                checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string[] temp = Data.lists;
            Data.lists = new string[temp.Length + 1];
            for (int i = 0; i < temp.Length; i++)
            {
                Data.lists[i] = temp[i];
            }
            Data.lists[Data.lists.Length - 1] = textBox1.Text;
            checkedListBox1.Items.Add(textBox1.Text);
            checkedListBox1.Update();
            textBox1.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    string str = Data.lists[i];
                    checkedListBox1.Items.Remove(str);
                }
            }
            checkedListBox1.Update();
        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void filterApply(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    if (Data.filters[i].Equals("고기집"))
                    {
                        foreach (int t in Data.meat)
                        {
                            checkedListBox1.SetItemChecked(t-1,true);
                        }
                    }
                    else if(Data.filters[i].Equals("치킨집"))
                    {
                    }
                    else if (Data.filters[i].Equals("찌게집"))
                    {
                        foreach (int t in Data.soup)
                        {
                            checkedListBox1.SetItemChecked(t - 1, true);
                        }
                    }
                    else if (Data.filters[i].Equals("술집"))
                    {
                        foreach (int t in Data.pub)
                        {
                            checkedListBox1.SetItemChecked(t - 1, true);
                        }
                    }
                }
            }
            checkedListBox1.Update();
        }

        private void filterCancel(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    if (Data.filters[i].Equals("고기집"))
                    {
                        foreach (int t in Data.meat)
                        {
                            checkedListBox1.SetItemChecked(t - 1, false);
                        }
                    }
                    else if (Data.filters[i].Equals("치킨집"))
                    {
                    }
                    else if (Data.filters[i].Equals("찌게집"))
                    {
                        foreach (int t in Data.soup)
                        {
                            checkedListBox1.SetItemChecked(t - 1, false);
                        }
                    }
                    else if (Data.filters[i].Equals("술집"))
                    {
                        foreach (int t in Data.pub)
                        {
                            checkedListBox1.SetItemChecked(t - 1, false);
                        }
                    }
                }
            }
            checkedListBox1.Update();
        }

        private void filterSave(object sender, EventArgs e)
        {

        }
    }
}