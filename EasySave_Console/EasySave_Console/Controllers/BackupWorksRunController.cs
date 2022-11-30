using EasySave_Console.Models;
using EasySave_Console.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave_Console.Controllers
{
    class BackupWorksRunController
    {
        MenuView menuView = new MenuView();
        BackupWorksRunView backupWorksRunView = new BackupWorksRunView();
        JsonHelper jsonHelper = new JsonHelper();
        FileHelper fileHelper = new FileHelper();

        string filepath_bw_config;
        string filepath_statelog;
        string filepath_log;

        public BackupWorksRunController()
        {
            filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
            filepath_statelog = fileHelper.FormatFilePath(fileHelper.filepath_statelog);
            filepath_log = fileHelper.FormatFilePath(fileHelper.filepath_log).Replace("{}", DateTime.Now.ToString("yyyyMMdd"));
            List<BackupWork>? backupWorks = jsonHelper.ReadBackupWorkFromJson(filepath_bw_config);
            if (backupWorks == null)
            {
                List<BackupWork> list_temp = new List<BackupWork>();

                for (int i = 0; i < 5; i++)
                {
                    list_temp.Add(new BackupWork(null, null, null, null));
                }
                jsonHelper.WriteBackupWorkToJson(filepath_bw_config, list_temp);
                backupWorks = jsonHelper.ReadBackupWorkFromJson(filepath_bw_config);
            }
            bool optionSelected = false;

            while (!optionSelected)
            {
                menuView.ClearConsole();
                string menuBWOption = backupWorksRunView.PromptRunBackupWorks(backupWorks);
                if (menuBWOption == "0")
                {
                    optionSelected = true;
                }
                else if (menuBWOption == "1" || menuBWOption == "2" || menuBWOption == "3" || menuBWOption == "4" || menuBWOption == "5")
                {
                    int i = Convert.ToInt32(menuBWOption) - 1;
                    if (backupWorks[i].IsEmpty())
                    {
                        menuView.ClearConsole();
                        backupWorksRunView.ErrorMsgEmptyBW();
                    }
                    else
                    {
                        RunCopy(backupWorks[i]);
                    }
                }
                else if (menuBWOption == "a")
                {
                    RunCopy(backupWorks[0], false);
                    RunCopy(backupWorks[1], false);
                    RunCopy(backupWorks[2], false);
                    RunCopy(backupWorks[3], false);
                    RunCopy(backupWorks[4], false);
                    backupWorksRunView.MsgAllBackupWorkFinished();
                }
            }
        }

        public void RunCopy(BackupWork backupWork, bool enterToContinue=true)
        {
            filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
            filepath_statelog = fileHelper.FormatFilePath(fileHelper.filepath_statelog);
            filepath_log = fileHelper.FormatFilePath(fileHelper.filepath_log).Replace("{}", DateTime.Now.ToString("yyyyMMdd"));
            if (backupWork.Type == "complete")
            {
                try
                {
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

                    foreach (FileModel file in files)
                    {
                        jsonHelper.WriteStateLogToJson(filepath_statelog, stateLog);

                        menuView.ClearConsole();
                        backupWorksRunView.CopyMessage(stateLog, file, enterToContinue);

                        var watch = new System.Diagnostics.Stopwatch();
                        watch.Start();
                        string relativePathFile = Path.GetRelativePath(backupWork.SrcFolder, file.FullPath);
                        try
                        {
                            if (!Directory.Exists(backupWork.DstFolder + @"\" + relativePathFile))
                                Directory.CreateDirectory(Path.GetDirectoryName(backupWork.DstFolder + @"\" + relativePathFile));
                            File.Copy(file.FullPath, backupWork.DstFolder + @"\" + relativePathFile, true);
                        }
                        catch { }
                        watch.Stop();

                        Log log = new Log(
                            backupWork.Name,
                            DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                            file.Size,
                            file.FullPath,
                            backupWork.DstFolder + @"\" + relativePathFile,
                            watch.ElapsedMilliseconds
                        );
                        jsonHelper.WriteLogFromJson(filepath_log, log);
                        stateLog.RemainingFiles--;
                        stateLog.RemainingSize -= file.Size;
                        jsonHelper.WriteStateLogToJson(filepath_statelog, stateLog);
                    }
                    stateLog.Active = false;
                    jsonHelper.WriteStateLogToJson(filepath_statelog, stateLog);
                    menuView.ClearConsole();
                    backupWorksRunView.CopyMessage(stateLog, null, enterToContinue);
                }
                catch { }
            }
            else if (backupWork.Type == "differencial")
            {
                try
                {
                    string subDstPath = "";
                    if (!fileHelper.DirectoryExists(backupWork.DstFolder + @"\complete"))
                    {
                        fileHelper.CreateDirectory(backupWork.DstFolder, @"\complete");
                        subDstPath = @"\complete";
                    }
                    else
                    {
                        subDstPath = @"\partial_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    }
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

                    foreach (FileModel file in files)
                    {
                        jsonHelper.WriteStateLogToJson(filepath_statelog, stateLog);

                        menuView.ClearConsole();
                        backupWorksRunView.CopyMessage(stateLog, file, enterToContinue);

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

                        Log log = new Log(
                            backupWork.Name,
                            DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                            file.Size,
                            file.FullPath,
                            backupWork.DstFolder + @"\" + subDstPath + @"\" + relativePathFile,
                            watch.ElapsedMilliseconds
                        );
                        jsonHelper.WriteLogFromJson(filepath_log, log);
                        stateLog.RemainingFiles--;
                        stateLog.RemainingSize -= file.Size;
                        jsonHelper.WriteStateLogToJson(filepath_statelog, stateLog);
                    }
                    stateLog.Active = false;
                    jsonHelper.WriteStateLogToJson(filepath_statelog, stateLog);
                    menuView.ClearConsole();
                    backupWorksRunView.CopyMessage(stateLog, null, enterToContinue);
                }
                catch { }
            }
        }
    }
}
