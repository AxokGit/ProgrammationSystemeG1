using EasySave_WPF.Models;
using EasySave_WPF.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

        public BackupWorksRunController()
        {
            /*
            // Definition of variables
            filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
            filepath_statelog = fileHelper.FormatFilePath(fileHelper.filepath_statelog);
            filepath_log = fileHelper.FormatFilePath(fileHelper.filepath_log).Replace("{}", DateTime.Now.ToString("yyyyMMdd"));
            
            // Getting all backup works configured
            List<BackupWork>? backupWorks = dataHelper.ReadBackupWorkFromJson(filepath_bw_config);
            
            // If null, creating backup works configuration with null
            if (backupWorks == null)
            {
                List<BackupWork> list_temp = new List<BackupWork>();

                for (int i = 0; i < 5; i++)
                {
                    list_temp.Add(new BackupWork(null, null, null, null));
                }
                dataHelper.WriteBackupWorkToJson(filepath_bw_config, list_temp);
                backupWorks = dataHelper.ReadBackupWorkFromJson(filepath_bw_config);
            }

            bool optionSelected = false;
            while (!optionSelected) // While user selected no valid choice
            {
                string menuBWOption = "oui";
                if (menuBWOption == "0") // Go back
                {
                    optionSelected = true;
                }
                else if (menuBWOption == "1" || menuBWOption == "2" || menuBWOption == "3" || menuBWOption == "4" || menuBWOption == "5") // If specific backup work selected
                {
                    int i = Convert.ToInt32(menuBWOption) - 1;
                    if (backupWorks[i].IsEmpty())
                    {
                    }
                    else
                    {
                        RunCopy(backupWorks[i]);
                    }
                }
                else if (menuBWOption == "a") // If all backup works selected
                {
                    RunCopy(backupWorks[0], false);
                    RunCopy(backupWorks[1], false);
                    RunCopy(backupWorks[2], false);
                    RunCopy(backupWorks[3], false);
                    RunCopy(backupWorks[4], false);
                }
            }
            */
        }

        public List<BackupWork> GetBackupWorks()
        {
            string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
            return dataHelper.ReadBackupWorkFromJson(filepath_bw_config);
        }

        // This method will take backupWork object and run the copy
        public void RunCopy(BackupWork backupWork, bool enterToContinue=true)
        {
            //Definition of the variables
            filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
            filepath_statelog = fileHelper.FormatFilePath(fileHelper.filepath_statelog);
            filepath_log = fileHelper.FormatFilePath(fileHelper.filepath_log).Replace("{}", DateTime.Now.ToString("yyyyMMdd"));
            if (backupWork.Type == "complete") // If backup work is type complete
            {
                try
                {
                    // Getting all files from source folder
                    List<FileModel> files = fileHelper.GetAllFile(backupWork.SrcFolder);
                    backupWork.Files = files;
                    long filesSize = new long();
                    foreach (FileModel file in files)
                    {
                        filesSize += file.Size;
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
                    foreach (FileModel file in files)
                    {
                        dataHelper.WriteStateLog(filepath_statelog, stateLog);

                        //  Show status of backup work

                        // Measure time to copy
                        var watch = new System.Diagnostics.Stopwatch();
                        watch.Start();
                        string relativePathFile = Path.GetRelativePath(backupWork.SrcFolder, file.FullPath);
                        try
                        {
                            // Start copy
                            if (!Directory.Exists(backupWork.DstFolder + @"\" + relativePathFile))
                                Directory.CreateDirectory(Path.GetDirectoryName(backupWork.DstFolder + @"\" + relativePathFile));
                            File.Copy(file.FullPath, backupWork.DstFolder + @"\" + relativePathFile, true);
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

                    foreach (FileModel file in files)
                    {
                        filesSize += file.Size;
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
                    foreach (FileModel file in files)
                    {
                        dataHelper.WriteStateLog(filepath_statelog, stateLog);


                        var watch = new System.Diagnostics.Stopwatch();
                        watch.Start();
                        string relativePathFile = Path.GetRelativePath(backupWork.SrcFolder, file.FullPath);
                        try
                        {
                            if (!Directory.Exists(backupWork.DstFolder + @"\" + subDstPath + @"\" + relativePathFile))
                                Directory.CreateDirectory(Path.GetDirectoryName(backupWork.DstFolder + @"\" + subDstPath + @"\" + relativePathFile));
                            File.Copy(file.FullPath, backupWork.DstFolder + @"\" + subDstPath + @"\" + relativePathFile, true);
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
