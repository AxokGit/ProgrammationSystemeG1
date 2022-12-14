using EasySave_WPF.Models;
using EasySave_WPF.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace EasySave_WPF.Controllers
{
    class BackupWorksRunController
    {
        DataHelper dataHelper = new DataHelper(); // Instantiation of the json helper
        FileHelper fileHelper = new FileHelper(); // Instantiation of the file helper
        string cmd = "CryptoSoft_Console.exe";

        // Declaration of needed variables
        string filepath_statelog;
        string filepath_log;
        string filepath_settings;

        public void BackupWorkListView_SelectionChanged(MainWindow mainWindow)
        {
            if (MainController.StopProcess)
            {
                mainWindow.RunBackupworkButton.IsEnabled = false;
            }
            else
            {
                int selectionCount = mainWindow.BackupWorkRunListView.SelectedItems.Count;

                if (selectionCount > 0)
                {
                    var items = mainWindow.BackupWorkRunListView.SelectedItems;
                    bool run = false;
                    bool pause_or_stop = true;
                    foreach (BackupWork backupWork in items)
                    {
                        if (!backupWork.Running)
                            run = true;
                        else
                        {
                            pause_or_stop = true;
                        }
                    }
                    mainWindow.RunBackupworkButton.IsEnabled = run;
                    mainWindow.PauseBackupworkButton.IsEnabled = pause_or_stop;
                    mainWindow.StopBackupworkButton.IsEnabled = pause_or_stop;
                }
                else
                {
                    mainWindow.RunBackupworkButton.IsEnabled = false;
                    mainWindow.PauseBackupworkButton.IsEnabled = false;
                    mainWindow.StopBackupworkButton.IsEnabled = false;
                }
            }
        }

        public void RunBackupworkButton_Click(MainWindow mainWindow, MainController mainController)
        {
            if (MainController.Paused)
            {
                MainController.Paused = false;
            }
            else
            {
                List<BackupWork> backupWorks = new List<BackupWork>();
                var backupworksSelected = mainWindow.BackupWorkRunListView.SelectedItems;
                foreach (BackupWork backupwork in backupworksSelected)
                {
                    backupWorks.Add(backupwork);
                }
                Thread t = new Thread(() => new BackupWorksRunController().StartCopy(backupWorks, mainWindow, mainController));
                t.Start();
            }
        }

        public void PauseBackupworkButton_Click(MainWindow mainWindow)
        {
            MainController.Paused = true;
        }
        
        public void StopBackupworkButton_Click(MainWindow mainWindow)
        {
            MainController.StopButton = true;
        }

        public List<BackupWork>? GetBackupWorks()
        {
            string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
            return dataHelper.ReadBackupWorksFromJson(filepath_bw_config);
        }
        public List<string>? GetBackupWorksName()
        {
            string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);

            List<string> backupworksname = new List<string>();
            var backupworks = dataHelper.ReadBackupWorksFromJson(filepath_bw_config);
            if (backupworks != null)
            {
                foreach (BackupWork backupwork in backupworks)
                {
                    backupworksname.Add(backupwork.Name);
                }
                return backupworksname;
            } else
            {
                return null;
            }
        }

        public void StartCopy(List<BackupWork> backupworks, MainWindow mainWindow, MainController mainController)
        {
            foreach (BackupWork backupWork in backupworks)
            {
                RunCopy(backupWork, mainWindow, mainController);
            }
        }

        // This method will take backupWork object and run the copy
        public void RunCopy(BackupWork backupWork, MainWindow mainWindow, MainController mainController)
        {
            //Definition of the variables
            filepath_statelog = fileHelper.FormatFilePath(fileHelper.filepath_statelog);
            filepath_log = fileHelper.FormatFilePath(fileHelper.filepath_log).Replace("{}", DateTime.Now.ToString("yyyyMMdd"));
            filepath_settings = fileHelper.FormatFilePath(@"%AppData%\EasySave\Settings.json");

            Settings settings = dataHelper.ReadSettingsFromJson(filepath_settings);

            if (settings.ExtentionFileToEncrypt.Count > 0)
            {
                if (!fileHelper.FileExists(cmd))
                {
                    MessageBox.Show(
                        (string)Application.Current.FindResource("cryptosoft_missing"),
                        (string)Application.Current.FindResource("application_name"),
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                    return;
                }
            }

            if (backupWork.Type == "complete") // If backup work is type complete
            {
                try
                {
                    if (!fileHelper.DirectoryExists(backupWork.SrcFolder))
                    {
                        MessageBox.Show(
                            (string)Application.Current.FindResource("check_input_folder"),
                            (string)Application.Current.FindResource("application_name"),
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                        );
                        return;
                    }
                    if (!fileHelper.DirectoryExists(backupWork.DstFolder))
                    {
                        MessageBox.Show(
                            (string)Application.Current.FindResource("check_input_folder"),
                            (string)Application.Current.FindResource("application_name"),
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                        );
                        return;
                    }


                    // Getting all files from source folder
                    List<FileModel> files = fileHelper.GetAllFile(backupWork.SrcFolder);
                    long filesSize = new long();
                    List<FileModel> files_sorted = new List<FileModel>();

                    foreach (FileModel file in files)
                    {
                        filesSize += file.Size;

                        string file_extension = "." + file.Name.Split('.')[^1];
                        if (settings.PriorityFiles.Contains(file_extension))
                        {
                            files_sorted.Insert(0, file);
                        } else
                        {
                            files_sorted.Add(file);
                        }
                    }
                    StateLog stateLog = new StateLog(
                        backupWork.Name, //BW name
                        DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), // TimeStamp
                        true, // Active
                        files.Count, // Totalfile
                        filesSize, // List of file size
                        files.Count, // Remaining file
                        filesSize, // List of remaining file size
                        backupWork.SrcFolder, // Src folder
                        backupWork.DstFolder // Dst folder
                    );

                    // Each file will be copied, log will be added to the daily log and this will update monitor status
                    foreach (FileModel file in files_sorted)
                    {
                        while (MainController.Paused)
                        {
                            if (MainController.StopButton || MainController.StopProcess)
                            {
                                App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                                {
                                    string status = $" ({backupWork.Name}) : {(string)Application.Current.FindResource("stopped")}";
                                    mainController.UpdateProgression(0.0, mainWindow);
                                    SocketController.UpdateProgress(mainWindow, 0.0);
                                    mainController.UpdateProgressionStatusLabel(status, mainWindow);
                                    SocketController.UpdateProgressLabel(mainWindow, status);
                                }, null);
                                MainController.StopButton = false;
                                return;
                            }
                            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                            {
                                string status = $" ({backupWork.Name}) : {(string)Application.Current.FindResource("paused")}";
                                SocketController.UpdateProgressLabel(mainWindow, status);
                                mainController.UpdateProgressionStatusLabel(status, mainWindow);
                            }, null);
                            Thread.Sleep(500);
                        }

                        if (MainController.StopButton || MainController.StopProcess)
                        {
                            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                            {
                                string status = $" ({backupWork.Name}) : {(string)Application.Current.FindResource("stopped")}";
                                mainController.UpdateProgression(0.0, mainWindow);
                                SocketController.UpdateProgress(mainWindow, 0.0);
                                mainController.UpdateProgressionStatusLabel(status, mainWindow);
                                SocketController.UpdateProgressLabel(mainWindow, status);
                            }, null);
                            MainController.StopButton = false;
                            return;
                        }

                        dataHelper.WriteStateLog(filepath_statelog, stateLog);
                        // Measure time to copy
                        var watch = new Stopwatch();
                        string relativePathFile = Path.GetRelativePath(backupWork.SrcFolder, file.FullPath);
                        watch.Start();
                        try
                        {
                            // Start copy
                            var fileExt = "." + file.Name.Split('.')[^1];
                            if (!Directory.Exists(backupWork.DstFolder + @"\" + relativePathFile))
                                Directory.CreateDirectory(Path.GetDirectoryName(backupWork.DstFolder + @"\" + relativePathFile));

                            if (settings.ExtentionFileToEncrypt.Contains(fileExt)){
                                string args = "run \"" + file.FullPath + "\" \"" + backupWork.DstFolder + @"\" + relativePathFile + "\" " + settings.XorKey;
                                ProcessStartInfo startInfo = new ProcessStartInfo(cmd, args);
                                Process process = Process.Start(startInfo);
                                process.WaitForExit();
                            }
                            else
                            {
                                File.Copy(file.FullPath, backupWork.DstFolder + @"\" + relativePathFile, true);
                            }
                        }
                        catch (Exception e) { System.Windows.MessageBox.Show(e.Message); }
                        watch.Stop();

                        // Write log to daily log
                        Log log = new Log(
                            backupWork.Name,
                            DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                            file.Size,
                            file.FullPath,
                            backupWork.DstFolder + @"\" + relativePathFile,
                            watch.ElapsedMilliseconds
                        );
                        dataHelper.WriteLog(filepath_log, log);
                        stateLog.RemainingFiles--;
                        stateLog.RemainingSize -= file.Size;
                        dataHelper.WriteStateLog(filepath_statelog, stateLog);
                        double? Progression = ((float)stateLog.TotalFiles - (float)stateLog.RemainingFiles) / (float)stateLog.TotalFiles * 100;

                        App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                        {
                            mainController.UpdateProgression(Progression ?? 0.0, mainWindow);
                            SocketController.UpdateProgress(mainWindow, Progression ?? 0.0);
                            string status = $" ({backupWork.Name}) : {file.Name} ({stateLog.TotalFiles - stateLog.RemainingFiles}/{stateLog.TotalFiles})";
                            mainController.UpdateProgressionStatusLabel(status, mainWindow);
                            SocketController.UpdateProgressLabel(mainWindow, status);
                        }, null);
                    }
                    // Write StateLog.json
                    stateLog.Active = false;
                    dataHelper.WriteStateLog(filepath_statelog, stateLog);

                    App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                    {
                        mainController.UpdateProgression(0.0, mainWindow);
                        SocketController.UpdateProgress(mainWindow, 0.0);
                        mainController.UpdateProgressionStatusLabel("", mainWindow);
                        SocketController.UpdateProgressLabel(mainWindow, "");


                    }, null);
                }
                catch (Exception e) { System.Windows.MessageBox.Show(e.Message); }
            }
            else if (backupWork.Type == "differencial") // If backup work is type differencial
            {
                try
                {
                    string subDstPath = "";
                    // If complete backup haven't be made
                    if (!fileHelper.DirectoryExists(backupWork.DstFolder + @"\complete"))
                    {
                        fileHelper.CreateDirectory(backupWork.DstFolder, @"\complete");
                        subDstPath = @"\complete";
                    }
                    // If complete backup already made
                    else
                    {
                        subDstPath = @"\partial_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    }
                    

                    App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                    {
                        string status = $" ({backupWork.Name}) : {(string)Application.Current.FindResource("getting_different_files")}";
                        mainController.UpdateProgressionStatusLabel(status, mainWindow);
                        SocketController.UpdateProgressLabel(mainWindow, status);
                    }, null);
                    // Get all edited file between source folder and complete backup folder
                    List<FileModel> files = fileHelper.GetAllEditedFile(backupWork.SrcFolder, backupWork.DstFolder + @"\complete");

                    long filesSize = new long();
                    List<FileModel> files_sorted = new List<FileModel>();
                    foreach (FileModel file in files)
                    {
                        filesSize += file.Size;
                        string file_extension = "." + file.Name.Split('.')[^1];
                        if (settings.PriorityFiles.Contains(file_extension))
                        {
                            files_sorted.Insert(0, file);
                        }
                        else
                        {
                            files_sorted.Add(file);
                        }
                    }

                    StateLog stateLog = new StateLog(
                        backupWork.Name, //BW name
                        DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), // TimeStamp
                        true, // Active
                        files.Count, // Totalfile
                        filesSize, // List of file size
                        files.Count, // Remaining file
                        filesSize, // List of remaining file size
                        backupWork.SrcFolder, // Src folder
                        backupWork.DstFolder + subDstPath // Dst folder
                    );

                    // For each file edited since the last complete backup
                    foreach (FileModel file in files_sorted)
                    {
                        while (MainController.Paused)
                        {
                            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                            {
                                string status = $" ({backupWork.Name}) : {(string)Application.Current.FindResource("paused")}";
                                mainController.UpdateProgressionStatusLabel(status, mainWindow);
                                SocketController.UpdateProgressLabel(mainWindow, status);
                            }, null);
                            Thread.Sleep(1000);
                        }
                        dataHelper.WriteStateLog(filepath_statelog, stateLog);

                        var watch = new Stopwatch();
                        string relativePathFile = Path.GetRelativePath(backupWork.SrcFolder, file.FullPath);
                        watch.Start();
                        try
                        {
                            var fileExt = "." + file.Name.Split('.')[^1];
                            if (!Directory.Exists(backupWork.DstFolder + @"\" + subDstPath + @"\" + relativePathFile))
                                Directory.CreateDirectory(Path.GetDirectoryName(backupWork.DstFolder + @"\" + subDstPath + @"\" + relativePathFile));
                            if (settings.ExtentionFileToEncrypt.Contains(fileExt))
                            {
                                string cmd = "CryptoSoft_Console.exe";
                                string args = "run \"" + file.FullPath + "\" \"" + backupWork.DstFolder + @"\" + subDstPath + @"\" + relativePathFile + "\" " + settings.XorKey;
                                ProcessStartInfo startInfo = new ProcessStartInfo(cmd, args);
                                Process process = Process.Start(startInfo);
                                process.WaitForExit();
                            }
                            else
                            {
                                File.Copy(file.FullPath, backupWork.DstFolder + @"\" + subDstPath + @"\" + relativePathFile, true);
                            }
                        }
                        catch (Exception e) { System.Windows.MessageBox.Show("inside" + e.Message); }
                        watch.Stop();

                        // Write log to daily log
                        Log log = new Log(
                            backupWork.Name,
                            DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                            file.Size,
                            file.FullPath,
                            backupWork.DstFolder + @"\" + subDstPath + @"\" + relativePathFile,
                            watch.ElapsedMilliseconds
                        );
                        dataHelper.WriteLog(filepath_log, log);
                        stateLog.RemainingFiles--;
                        stateLog.RemainingSize -= file.Size;
                        dataHelper.WriteStateLog(filepath_statelog, stateLog);
                        double? Progression = ((float)stateLog.TotalFiles - (float)stateLog.RemainingFiles) / (float)stateLog.TotalFiles * 100;

                        App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                        {
                            mainController.UpdateProgression(Progression ?? 0.0, mainWindow);
                            SocketController.UpdateProgress(mainWindow, Progression ?? 0.0);
                            string status = $" ({backupWork.Name}) : {file.Name} ({stateLog.TotalFiles - stateLog.RemainingFiles}/{stateLog.TotalFiles})";
                            SocketController.UpdateProgressLabel(mainWindow, status);
                            mainController.UpdateProgressionStatusLabel(status, mainWindow);
                        }, null);
                    }
                    // Write StateLog.json
                    stateLog.Active = false;
                    dataHelper.WriteStateLog(filepath_statelog, stateLog);
                    App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                    {
                        mainController.UpdateProgression(0.0, mainWindow);
                        SocketController.UpdateProgress(mainWindow, 0.0);
                        SocketController.UpdateProgressLabel(mainWindow, "");
                        mainController.UpdateProgressionStatusLabel("", mainWindow);
                    }, null);
                }
                catch (Exception e) { System.Windows.MessageBox.Show("outside" + e.Message); }
            }
        }
    }

}
