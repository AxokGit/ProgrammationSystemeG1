using EasySave_WPF.Models;
using EasySave_WPF.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

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
            return dataHelper.ReadBackupWorkFromJson(filepath_bw_config);
        }
        public List<string>? GetBackupWorksName()
        {
            string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);

            List<string> backupworksname = new List<string>();
            var backupworks = dataHelper.ReadBackupWorkFromJson(filepath_bw_config);
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
                    backupWork.Files = files;
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
                        dataHelper.WriteStateLog(filepath_statelog, stateLog);

                        // Measure time to copy
                        var watch = new Stopwatch();
                        watch.Start();
                        string relativePathFile = Path.GetRelativePath(backupWork.SrcFolder, file.FullPath);
                        try
                        {
                            // Start copy
                            var fileExt = "." + file.Name.Split('.')[^1];
                            if (!Directory.Exists(backupWork.DstFolder + @"\" + relativePathFile))
                                Directory.CreateDirectory(Path.GetDirectoryName(backupWork.DstFolder + @"\" + relativePathFile));

                            if (settings.ExtentionFileToEncrypt.Contains(fileExt)){
                                ProcessStartInfo startInfo = new ProcessStartInfo("CryptoSoft_Console.exe", "run " + file.FullPath + " " + backupWork.DstFolder + @"\" + relativePathFile + " " + settings.XorKey);
                                Process process = Process.Start(startInfo);
                                process.WaitForExit();
                            }
                            else
                            {
                                File.Copy(file.FullPath, backupWork.DstFolder + @"\" + relativePathFile, true);
                            }
                        }
                        catch { }
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
                    }
                    // Write StateLog.json
                    stateLog.Active = false;
                    dataHelper.WriteStateLog(filepath_statelog, stateLog);

                    // Show backup work finished status
                }
                catch { }
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
                    backupWork.Files = files;
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
                        dataHelper.WriteStateLog(filepath_statelog, stateLog);

                        var watch = new Stopwatch();
                        watch.Start();
                        string relativePathFile = Path.GetRelativePath(backupWork.SrcFolder, file.FullPath);
                        try
                        {
                            var fileExt = "." + file.Name.Split('.')[^1];
                            if (!Directory.Exists(backupWork.DstFolder + @"\" + subDstPath + @"\" + relativePathFile))
                                Directory.CreateDirectory(Path.GetDirectoryName(backupWork.DstFolder + @"\" + subDstPath + @"\" + relativePathFile));
                            if (settings.ExtentionFileToEncrypt.Contains(fileExt))
                            {
                                ProcessStartInfo startInfo = new ProcessStartInfo("CryptoSoft_Console.exe", "run " + file.FullPath + " " + backupWork.DstFolder + @"\" + subDstPath + @"\" + relativePathFile + " " + settings.XorKey);
                                Process process = Process.Start(startInfo);
                                process.WaitForExit();
                            }
                            else
                            {
                                File.Copy(file.FullPath, backupWork.DstFolder + @"\" + subDstPath + @"\" + relativePathFile, true);
                            }
                        }
                        catch { }
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
                    }
                    // Write StateLog.json
                    stateLog.Active = false;
                    dataHelper.WriteStateLog(filepath_statelog, stateLog);
                }
                catch { }
            }
        }
    }

}
