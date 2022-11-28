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

        public BackupWorksRunController()
        {
            string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
            string filepath_statelog = fileHelper.FormatFilePath(fileHelper.filepath_statelog);
            string filepath_log = fileHelper.FormatFilePath(fileHelper.filepath_log).Replace("{}", DateTime.Now.ToString("yyyyMMdd"));

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
                if (menuBWOption == "1" || menuBWOption == "2" || menuBWOption == "3" || menuBWOption == "4" || menuBWOption == "5")
                {
                    int i = Convert.ToInt32(menuBWOption) - 1;

                    if (backupWorks[i].IsEmpty())
                    {
                        menuView.ClearConsole();
                        backupWorksRunView.ErrorMsgEmptyBW();
                    }
                    else
                    {
                        if (backupWorks[i].Type == "complete")
                        {
                            try
                            {
                                List<FileModel> files = fileHelper.GetAllFile(backupWorks[i].SrcFolder);
                                backupWorks[i].Files = files;
                                long filesSize = new long();
                                foreach (FileModel file in files)
                                {
                                    filesSize += file.Size;
                                }
                                StateLog stateLog = new StateLog(
                                    backupWorks[i].Name, //BW name
                                    DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), // TimeStamp
                                    true, // Active
                                    files.Count, // Totalfile
                                    filesSize, // List of file size
                                    files.Count, // Remaining file
                                    filesSize, // List of remaining file size
                                    backupWorks[i].SrcFolder, // Src folder
                                    backupWorks[i].DstFolder // Dst folder
                                );
                                
                                foreach (FileModel file in files)
                                {
                                    jsonHelper.WriteStateLogToJson(filepath_statelog, stateLog);

                                    menuView.ClearConsole();
                                    backupWorksRunView.CopyMessage(stateLog, file);

                                    var watch = new System.Diagnostics.Stopwatch();
                                    watch.Start();
                                    string relativePathFile = Path.GetRelativePath(backupWorks[i].SrcFolder, file.FullPath);
                                    try
                                    {
                                        if (!Directory.Exists(backupWorks[i].DstFolder + @"\" + relativePathFile))
                                            Directory.CreateDirectory(Path.GetDirectoryName(backupWorks[i].DstFolder + @"\" + relativePathFile));
                                        File.Copy(file.FullPath, backupWorks[i].DstFolder + @"\" + relativePathFile, true);
                                    }
                                    catch (IOException iox)
                                    {
                                        Console.WriteLine(iox.Message);
                                    }
                                    watch.Stop();

                                    Log log = new Log(
                                        backupWorks[i].Name,
                                        DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                                        file.Size,
                                        file.FullPath,
                                        backupWorks[i].DstFolder + @"\" + relativePathFile,
                                        watch.ElapsedMilliseconds
                                    );

                                    jsonHelper.WriteLogToJson(filepath_log, log);
                                    stateLog.RemainingFiles--;
                                    stateLog.RemainingSize -= file.Size;
                                    jsonHelper.WriteStateLogToJson(filepath_statelog, stateLog);
                                }
                                stateLog.Active = false;
                                jsonHelper.WriteStateLogToJson(filepath_statelog, stateLog);
                                menuView.ClearConsole();
                                backupWorksRunView.CopyMessage(stateLog, null);

                            }
                            catch (Exception e) { }
                        }
                        else if (backupWorks[i].Type == "differencial")
                        {
                            try
                            {
                                string subDstPath = "";
                                if (!fileHelper.DirectoryExists(backupWorks[i].DstFolder + @"\complete"))
                                {
                                    fileHelper.CreateDirectory(backupWorks[i].DstFolder, @"\complete");
                                    subDstPath = @"\complete";
                                } else
                                {
                                    subDstPath = @"\partial_"+DateTime.Now.ToString("yyyyMMdd_HHmmss");
                                }
                                List<FileModel> files = fileHelper.GetAllEditedFile(backupWorks[i].SrcFolder, backupWorks[i].DstFolder + @"\complete");
                                backupWorks[i].Files = files;
                                long filesSize = new long();
                                foreach (FileModel file in files)
                                {
                                    filesSize += file.Size;
                                }
                                StateLog stateLog = new StateLog(
                                    backupWorks[i].Name, //BW name
                                    DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), // TimeStamp
                                    true, // Active
                                    files.Count, // Totalfile
                                    filesSize, // List of file size
                                    files.Count, // Remaining file
                                    filesSize, // List of remaining file size
                                    backupWorks[i].SrcFolder, // Src folder
                                    backupWorks[i].DstFolder + subDstPath // Dst folder
                                );

                                foreach (FileModel file in files)
                                {
                                    jsonHelper.WriteStateLogToJson(filepath_statelog, stateLog);

                                    menuView.ClearConsole();
                                    backupWorksRunView.CopyMessage(stateLog, file);

                                    var watch = new System.Diagnostics.Stopwatch();
                                    watch.Start();
                                    string relativePathFile = Path.GetRelativePath(backupWorks[i].SrcFolder, file.FullPath);
                                    try
                                    {
                                        if (!Directory.Exists(backupWorks[i].DstFolder + @"\"+ subDstPath + @"\" + relativePathFile))
                                            Directory.CreateDirectory(Path.GetDirectoryName(backupWorks[i].DstFolder + @"\" + subDstPath + @"\" + relativePathFile));
                                        File.Copy(file.FullPath, backupWorks[i].DstFolder + @"\" + subDstPath + @"\" + relativePathFile, true);
                                    }
                                    catch (IOException iox)
                                    {
                                        Console.WriteLine(iox.Message);
                                    }
                                    watch.Stop();

                                    Log log = new Log(
                                        backupWorks[i].Name,
                                        DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                                        file.Size,
                                        file.FullPath,
                                        backupWorks[i].DstFolder + @"\" + subDstPath + @"\" + relativePathFile,
                                        watch.ElapsedMilliseconds
                                    );

                                    jsonHelper.WriteLogToJson(filepath_log, log);
                                    stateLog.RemainingFiles--;
                                    stateLog.RemainingSize -= file.Size;
                                    jsonHelper.WriteStateLogToJson(filepath_statelog, stateLog);
                                }
                                stateLog.Active = false;
                                jsonHelper.WriteStateLogToJson(filepath_statelog, stateLog);
                                menuView.ClearConsole();
                                backupWorksRunView.CopyMessage(stateLog, null);
                            }
                            catch (Exception e) { }
                        }
                    }
                }
                else if (menuBWOption == "6")
                {
                    optionSelected = true;
                }
            }
        }
    }
}
