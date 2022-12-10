using EasySave_WPF.Models;
using EasySave_WPF.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace EasySave_WPF.Controllers
{
    class BackupWorksRunController
    {
        DataHelper dataHelper = new DataHelper(); // Instantiation of the json helper
        FileHelper fileHelper = new FileHelper(); // Instantiation of the file helper

        // Declaration of needed variables
        string filepath_bw_config;
        string filepath_statelog;
        string filepath_log;
        string filepath_settings;

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

        // This method will take backupWork object and run the copy
        public void RunCopy(BackupWork backupWork)
        {
            //Definition of the variables
            filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
            filepath_statelog = fileHelper.FormatFilePath(fileHelper.filepath_statelog);
            filepath_log = fileHelper.FormatFilePath(fileHelper.filepath_log).Replace("{}", DateTime.Now.ToString("yyyyMMdd"));
            filepath_settings = fileHelper.FormatFilePath(@"%AppData%\EasySave\Settings.json");

            Settings settings = dataHelper.ReadSettingsFromJson(filepath_settings);

            if (backupWork.Type == "complete") // If backup work is type complete
            {
                try
                {
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

                    backupWork.TotalFiles = files.Count;
                    backupWork.RemainingFiles = files.Count;

                    backupWork.Running = true;
                    dataHelper.WriteBackupWorkToJson(filepath_bw_config, backupWork);

                    //new CopyStatus(backupWork);

                    // Each file will be copied, log will be added to the daily log and this will update monitor status
                    foreach (FileModel file in files_sorted)
                    {
                        
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
                                string cmd = "CryptoSoft_Console.exe";
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
                        backupWork.RemainingFiles = stateLog.RemainingFiles;
                        backupWork.FileNameInCopy = file.Name;
                        backupWork.Progression = (int)(((float)stateLog.TotalFiles - (float)stateLog.RemainingFiles) / (float)stateLog.TotalFiles * 100);
                        dataHelper.WriteBackupWorkToJson(filepath_bw_config, backupWork);
                    }
                    // Write StateLog.json
                    stateLog.Active = false;
                    dataHelper.WriteStateLog(filepath_statelog, stateLog);
                    backupWork.Running = false;
                    dataHelper.WriteBackupWorkToJson(filepath_bw_config, backupWork);

                    // Show backup work finished status
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

                    backupWork.Running = true;
                    dataHelper.WriteBackupWorkToJson(filepath_bw_config, backupWork);

                    // For each file edited since the last complete backup
                    foreach (FileModel file in files_sorted)
                    {
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
                        backupWork.Progression = (int)(((float)stateLog.TotalFiles - (float)stateLog.RemainingFiles) / (float)stateLog.TotalFiles * 100);
                        dataHelper.WriteBackupWorkToJson(filepath_bw_config, backupWork);
                    }
                    // Write StateLog.json
                    stateLog.Active = false;
                    dataHelper.WriteStateLog(filepath_statelog, stateLog);
                    backupWork.Running = false;
                    dataHelper.WriteBackupWorkToJson(filepath_bw_config, backupWork);
                }
                catch (Exception e) { System.Windows.MessageBox.Show("outside" + e.Message); }
            }
        }
    }

}
