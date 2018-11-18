using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzAccCheck
{
   
    public partial class Form1 : Form
    {
        private List<Account> Accounts { get; set; }
        private int Counter { get; set; }
        private Process Process { get; set; }

        public Form1()
        {
            InitializeComponent();
            FetchAccounts();
            Counter = 0;
            label2.Text = Accounts.Count.ToString();
            RefreshHandle();
        }

        private void FetchAccounts()
        {
            Accounts = new List<Account>();
            var lines = File.ReadLines("accounts.txt");
            foreach (var line in lines)
            {
                var acc = new Account();
                var data = line.Split(':');
                acc.User = data[0];
                acc.Pass = data[1];
                Accounts.Add(acc);
            }
        }

        private void RefreshHandle()
        {
            Process = Process.GetProcessesByName("rs2client").First();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            BringToFront(Process);
            Thread.Sleep(1000);
            SendKeys.Send(Accounts[Counter].User);
            Thread.Sleep(250);
            SendKeys.Send("{ENTER}");
            SendKeys.Send(Accounts[Counter].Pass);
            Thread.Sleep(250);
            SendKeys.Send("{ENTER}");
            Counter++;
            label4.Text = Counter.ToString();
        }

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        private void BringToFront(Process pTemp)
        {
            SetForegroundWindow(pTemp.MainWindowHandle);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BringToFront(Process);
            Thread.Sleep(1000);
            SendKeys.Send(Accounts[Counter-1].User);
            Thread.Sleep(250);
            SendKeys.Send("{ENTER}");
            SendKeys.Send(Accounts[Counter-1].Pass);
            Thread.Sleep(250);
            SendKeys.Send("{ENTER}");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshHandle();
        }
    }

    public class Account
    {
        public string User { get; set; }
        public string Pass { get; set; }

    }
}
