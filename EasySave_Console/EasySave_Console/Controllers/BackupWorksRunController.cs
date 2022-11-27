﻿using EasySave_Console.Models;
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
                                List<FileModel> files = fileHelper.GetAllFileFromFolderPath(backupWorks[i].SrcFolder);
                                backupWorks[i].Files = files;
                                long filesSize = new long();
                                foreach (FileModel file in files)
                                {
                                    filesSize += file.Size;
                                }
                                StateLog stateLog = new StateLog(
                                    backupWorks[i].Name, //BW name
                                    new DateTime(), // TimeStamp
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
                                    var watch = new System.Diagnostics.Stopwatch();

                                    watch.Start();

                                    try
                                    {
                                        var relativePathFile = Path.GetRelativePath(backupWorks[i].SrcFolder, file.FullPath);
                                        if (!Directory.Exists(backupWorks[i].DstFolder + @"\" + relativePathFile))
                                            Directory.CreateDirectory(Path.GetDirectoryName(backupWorks[i].DstFolder + @"\" + relativePathFile));
                                        File.Copy(file.FullPath, backupWorks[i].DstFolder + @"\" + relativePathFile, true);
                                    }
                                    catch (IOException iox)
                                    {
                                        Console.WriteLine(iox.Message);
                                    }

                                    watch.Stop();

                                    Console.WriteLine(file.Name);
                                    stateLog.RemainingFiles--;
                                    stateLog.RemainingSize -= file.Size;

                                    Console.WriteLine(stateLog.RemainingFiles);
                                    Console.WriteLine(stateLog.RemainingSize);
                                    Console.ReadKey();
                                }
                                stateLog.Active = false;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            Console.ReadKey();
                        }
                        else if (backupWorks[i].Type == "differencial")
                        {
                            Console.WriteLine("Differencial backup TODO");
                            Console.ReadKey();
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
