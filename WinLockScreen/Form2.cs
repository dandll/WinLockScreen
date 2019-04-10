using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace WinLockScreen
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnCloseTaskmgr_Click(object sender, EventArgs e)
        {
            Process[] p = Process.GetProcessesByName("taskmgr");
            for (int h = 0; h < p.Length; h++)
            {
                p[h].Kill();
            }
        }
    }
}
