using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows;

namespace EasySave_WPF.Controllers
{
    class ThreadController
    {
        public void CheckIfThreadIsRunning(string ThreadName)
        {
            Thread mainThread = Thread.CurrentThread;
            mainThread.Name = "Main Thread";

            bool witness = true;
            Thread thread = new Thread(Compute);
            thread.Start();
            while (witness)
            {
                Process[] pname = Process.GetProcessesByName(ThreadName);
                if (pname.Length == 0)
                {
                    MessageBox.Show("no");
                    Thread.Sleep(1000);
                }
                else
                {
                    MessageBox.Show("oui");
                    Thread.Sleep(1000);
                }
            }
        }

        private void Compute()
        {
            
        }

        public void StopProcess(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            foreach (Process process in processes)
            {
                process.Kill();
                process.WaitForExit();
                process.Dispose();
            }
        }
    }
}
