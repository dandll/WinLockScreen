using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace WinLockScreen
{
    public partial class Form1 : Form
    {
        #region 变量
        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(
        int hWnd, // handle to destination window
        int Msg, // message
        int wParam, // first message parameter
        int lParam); // second message parameter

        Random Random1 = new Random();
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();

            //OpenTaskMgrForm();
            btnClose.Hide();
            btnCloseTaskmgr.Hide();
            lblInfo1.Text = "Width：" + this.Width.ToString();
            lblInfo2.Text = "Height：" + this.Height.ToString();
            lblInfo3.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //lblInfo1.Text = this.Width.ToString();
            SetTopMost();
            btnClose.Location = new Point(this.Width - 150, this.Height - 50);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseTaskMgrForm();
            e.Cancel = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        //bool closeTaskMgrForm = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Minute % 30 != 0)
            //if (DateTime.Now.Second % 20 < 10)
            {
                this.Hide();
                //if (closeTaskMgrForm)
                //{
                //    CloseTaskMgrForm();
                //    closeTaskMgrForm = false;
                //}
            }
            else
            {
                //OpenTaskMgrForm();
                this.Show();
                SetTopMost();
                lblInfo3.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SetLblTipMessageChange();
                //closeTaskMgrForm = true;
            }
        }
        /// <summary>
        /// 设置窗体为最前显示
        /// </summary>
        void SetTopMost()
        {
            this.TopMost = false;
            this.TopMost = true;
            //if (!this.TopMost)
            //{
            //    //this.TopMost = false;
            //    this.TopMost = true;
            //}
        }
        /// <summary>
        /// 改变提示信息位置
        /// </summary>
        void SetLblTipMessageChange()
        {
            int width = this.Width - Random1.Next(this.Width - lblTipMessage.Width);
            int height = this.Height - Random1.Next(this.Height - lblTipMessage.Height);
            lblTipMessage.Location = new Point(width, height);
        }
        void OpenTaskMgrForm()
        {
            CloseTaskMgrForm();
            Process p = new Process();
            p.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.System);
            p.StartInfo.FileName = "taskmgr.exe";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
        }
        void CloseTaskMgrForm()
        {
            const int WM_CLOSE = 0x0010;
            int taskManager = FindWindow("#32770", "Windows Task Manager");
            SendMessage(taskManager, WM_CLOSE, 0, 0); 
            Process[] p = Process.GetProcessesByName("taskmgr");
            for (int h = 0; h < p.Length; h++)
            {
                p[h].Kill();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Alt && e.KeyCode == Keys.F4)
            {
                e.Handled = true;
            }
        }

        private void btnCloseTaskmgr_Click(object sender, EventArgs e)
        {
            CloseTaskMgrForm();
        }
    }
}
