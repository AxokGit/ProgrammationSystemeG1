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
        FileHelper fileHelper = new FileHelper();
        

        public BackupWorksEditController()
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
                string menuBWOption = backupWorksEditView.PromptEditBackupWorks(backupWorks);
                if (menuBWOption == "1" || menuBWOption == "2" || menuBWOption == "3" || menuBWOption == "4" || menuBWOption == "5")
                {
                    int i = Convert.ToInt32(menuBWOption)-1;

                    bool validName = false;
                    bool validSrcFolder = false;
                    bool validDstFolder = false;
                    bool validType = false;

                    while (!validName)
                    {
                        string previousName = backupWorks[i].Name ?? "";
                        menuView.ClearConsole();
                        backupWorks[i].Name = backupWorksEditView.PromptEditBackupWorksName(backupWorks[i]);
                        if (backupWorks[i].Name == "r")
                        {
                            backupWorks[i].Name = null;
                            backupWorks[i].SrcFolder = null;
                            backupWorks[i].DstFolder = null;
                            backupWorks[i].Type = null;
                            validName = true;
                            validSrcFolder = true;
                            validDstFolder = true;
                            validType = true;
                        }
                        else
                        {
                            if (backupWorks[i].Name == "" && previousName != "")
                            {
                                backupWorks[i].Name = previousName;
                                validName = true;
                            }
                            else if (backupWorks[i].Name != "")
                            {
                                validName = true;
                            }
                        }
                    }

                    while (!validSrcFolder)
                    {
                        string previousSrcFolder = backupWorks[i].SrcFolder ?? "";
                        menuView.ClearConsole();
                        backupWorks[i].SrcFolder = backupWorksEditView.PromptEditBackupWorksSrcFolder(backupWorks[i]);
                        if (backupWorks[i].SrcFolder == "" && previousSrcFolder != "")
                        {
                            backupWorks[i].SrcFolder = previousSrcFolder;
                            validSrcFolder = true;
                        }
                        else if (backupWorks[i].SrcFolder != "")
                        {
                            validSrcFolder = true;
                        }
                    }

                    while (!validDstFolder)
                    {
                        string previousDstFolder = backupWorks[i].DstFolder ?? "";
                        menuView.ClearConsole();
                        backupWorks[i].DstFolder = backupWorksEditView.PromptEditBackupWorksDstFolder(backupWorks[i]);
                        if (backupWorks[i].DstFolder == "" && previousDstFolder != "")
                        {
                            backupWorks[i].DstFolder = previousDstFolder;
                            validDstFolder = true;
                        }
                        else if (backupWorks[i].DstFolder != "")
                        {
                            validDstFolder = true;
                        }
                    }

                    while (!validType)
                    {
                        string previousType = backupWorks[i].Type ?? "";
                        menuView.ClearConsole();
                        backupWorks[i].Type = backupWorksEditView.PromptEditBackupWorksType(backupWorks[i]);
                        if (backupWorks[i].Type == "" && previousType != "")
                        {
                            backupWorks[i].Type = previousType;
                            validType = true;
                        }
                        else if (backupWorks[i].Type != "")
                        {
                            validType = true;
                        }
                    }

                    jsonHelper.WriteBackupWorkToJson(filepath_bw_config, backupWorks);
                    
                }
                else if (menuBWOption == "6")
                {
                    optionSelected = true;
                }
            }
        }
    }
}
