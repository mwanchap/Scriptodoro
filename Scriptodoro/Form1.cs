using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scriptodoro
{
    public partial class Form1 : Form
    {
        public static int TimePerWord = Int32.Parse(ConfigurationManager.AppSettings["TimePerWord"]);
        public static DateTime ShowUntil = DateTime.MinValue;

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            timer1_Tick(null, null);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(TipTime());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var scripture = ScriptureController.GetScripture(textBox1.Text);
            notifyIcon1.BalloonTipText = scripture;
            notifyIcon1.BalloonTipTitle = textBox1.Text;
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(TipTime());
            timer1.Stop(); timer1.Start();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(TipTime());
        }

        public int TipTime()
        {
            var tip = notifyIcon1.BalloonTipText;
            var words = tip.Count<char>(x => x == ' ') + 1;
            var time = TimePerWord * words;
            ShowUntil = DateTime.Now.AddMilliseconds(time);
            return time + 1000;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.Focus();
        }

        private void notifyIcon1_BalloonTipClosed(object sender, EventArgs e)
        {
            Thread.Sleep(1);
            
            if(ShowUntil > DateTime.Now)
            {
                notifyIcon1.ShowBalloonTip((ShowUntil - DateTime.Now).Milliseconds);
            }
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            ShowUntil = DateTime.MinValue;
        }
    }
}
