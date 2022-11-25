using EasySave_Console.Models;
using EasySave_Console.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Controllers
{
    class BackupWorksEditController
    {
        MenuView menuView = new MenuView();
        BackupWorksEditView backupWorksEditView = new BackupWorksEditView();
        JsonHelper jsonHelper = new JsonHelper();
        string filepath_bw_config = @"%AppData%/BackupWorks.json";

        public string FormatFilePath(string path)
        {
            return Environment.ExpandEnvironmentVariables(path);
        }
        public BackupWorksEditController()
        {
            filepath_bw_config = FormatFilePath(filepath_bw_config);

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
                string menuBWOption = backupWorksEditView.PromptEditBackupWorks(backupWorks);
                if (menuBWOption == "1" || menuBWOption == "2" || menuBWOption == "3" || menuBWOption == "4" || menuBWOption == "5")
                {
                    int i = Convert.ToInt32(menuBWOption)-1;

                    string previousName = backupWorks[i].Name ?? "";
                    menuView.ClearConsole();
                    backupWorks[i].Name = backupWorksEditView.PromptEditBackupWorksName(backupWorks[i]);
                    if (backupWorks[i].Name == "r")
                    {
                        backupWorks[i].Name = null;
                        backupWorks[i].SrcFolder = null;
                        backupWorks[i].DstFolder = null;
                        backupWorks[i].Type = null;
                    } else
                    {
                        if (backupWorks[i].Name == "" && previousName != "")
                        {
                            backupWorks[i].Name = previousName;
                        }

                        string previousSrcFolder = backupWorks[i].SrcFolder ?? "";
                        menuView.ClearConsole();
                        backupWorks[i].SrcFolder = backupWorksEditView.PromptEditBackupWorksSrcFolder(backupWorks[i]);
                        if (backupWorks[i].SrcFolder == "" && previousSrcFolder != "")
                        {
                            backupWorks[i].SrcFolder = previousSrcFolder;
                        }

                        string previousDstFolder = backupWorks[i].DstFolder ?? "";
                        menuView.ClearConsole();
                        backupWorks[i].DstFolder = backupWorksEditView.PromptEditBackupWorksDstFolder(backupWorks[i]);
                        if (backupWorks[i].DstFolder == "" && previousDstFolder != "")
                        {
                            backupWorks[i].DstFolder = previousDstFolder;
                        }

                        string previousType = backupWorks[i].Type ?? "";
                        menuView.ClearConsole();
                        backupWorks[i].Type = backupWorksEditView.PromptEditBackupWorksType(backupWorks[i]);
                        if (backupWorks[i].Type == "" && previousType != "")
                        {
                            backupWorks[i].Type = previousType;
                        }

                        jsonHelper.WriteBackupWorkToJson(filepath_bw_config, backupWorks);

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
