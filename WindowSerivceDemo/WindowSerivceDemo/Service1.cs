using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace WindowSerivceDemo
{
    public partial class Service1 : ServiceBase
    {
        // Sử dụng Timer để viết thời gian hiện tại ra file log
        Timer timer = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is start at " + DateTime.Now);
            timer.Enabled = true;
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000;
        }

        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service is recall at " + DateTime.Now);
        }

        // hàm để viết thời gian hiện tại ra file text
        public void WriteToFile(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\logs";
            if (Directory.Exists(path)) Directory.CreateDirectory(path);
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace("/", "_") + ".txt";
            if (!File.Exists(filePath))
                using (StreamWriter stream = File.CreateText(filePath)) stream.WriteLine(message);
            else
                using (StreamWriter writer = File.AppendText(filePath)) writer.WriteLine(message);
        }
    }
}
