using EasySave_WPF.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace EasySave_WPF.Controllers
{
    class StopProcessController
    {
        FileHelper fileHelper = new FileHelper();
        DataHelper dataHelper = new DataHelper();   
        public StopProcessController(MainWindow mainWindow)
        {
            while (true)
            {
                Thread.Sleep(500);

                string filepath_settings = fileHelper.FormatFilePath(@"%AppData%\EasySave\Settings.json");
                Settings settings = dataHelper.ReadSettingsFromJson(filepath_settings);

                Process? processus = Process.GetProcesses().FirstOrDefault(p => settings.StopProcesses.Contains(p.ProcessName));
                if (processus != null)
                {
                    MainWindow.StopProcess = true;
                    App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                    {
                        mainWindow.RunBackupworkButton.IsEnabled = false;
                    }, null);
                   
                }
                else
                {
                    MainWindow.StopProcess = false;
                    App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                    {
                        int selectionCount = mainWindow.BackupWorkRunListView.SelectedItems.Count;

                        if (selectionCount > 0)
                        {
                            var items = mainWindow.BackupWorkRunListView.SelectedItems;
                            bool runable = false;
                            foreach (BackupWork backupWork in items)
                            {
                                if (!backupWork.Running)
                                    runable = true;
                            }
                            mainWindow.RunBackupworkButton.IsEnabled = runable;
                        }
                        else
                        {
                            mainWindow.RunBackupworkButton.IsEnabled = false;
                        }
                    }, null);
                }
            }
        }
    }
}
