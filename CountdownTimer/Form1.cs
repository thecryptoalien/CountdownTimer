using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountdownTimer
{
    public partial class Form1 : Form
    {
        public DateTime? toDate { get; set; }
        public bool runTimer { get; set; }
        public bool timerRunning { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // do something on click
            if (runTimer)
            {
                runTimer = false;
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (e.End > e.Start)
            {
                // check shiz
                if (toDate != null)
                {
                    // change it
                    toDate = e.End;
                    runTimer = true;
                }
                else
                {
                    // create it 
                    toDate = e.End;
                    runTimer = true;
                }
                DateTime newTime = new DateTime(e.End.Year, e.End.Month, e.End.Day, dateTimePicker1.Value.Hour, dateTimePicker1.Value.Minute, dateTimePicker1.Value.Second);                
                dateTimePicker1.Value = newTime;
                if (!timerRunning)
                {
                    Task.Factory.StartNew(doLoop);
                }
            }
        }

        private void doLoop()
        {
            // start timer loop
            while (runTimer)
            {
                timerRunning = true;
                // do loop yo
                if (toDate != null)
                {
                    // calc time left and update label
                    TimeSpan timeleft = (DateTime)toDate - DateTime.Now;
                    this.Invoke(new Action(() => label1.Text = timeleft.ToString("dd\\:hh\\:mm\\:ss")));
                    this.Invoke(new Action(() => label1.Refresh()));
                }
                Thread.Sleep(250);
            }
            timerRunning = false;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // do stuff maybe
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            monthCalendar1.SelectionEnd = dateTimePicker1.Value;
        }
    }
}
