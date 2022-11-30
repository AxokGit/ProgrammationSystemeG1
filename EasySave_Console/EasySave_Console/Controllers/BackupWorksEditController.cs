using EasySave_Console.Models;
using EasySave_Console.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave_Console.Controllers
{
    class BackupWorksEditController
    {
        MenuView menuView = new MenuView(); // Instantiation of the main view
        BackupWorksEditView backupWorksEditView = new BackupWorksEditView(); // Instantiation of the Backup works edit view
        JsonHelper jsonHelper = new JsonHelper(); // Instantiation of the json helper
        FileHelper fileHelper = new FileHelper(); // Instantiation of the file helper

        public BackupWorksEditController()
        {
            // Reading backup works in the BackupWorks.json file
            string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
            List<BackupWork>? backupWorks = jsonHelper.ReadBackupWorkFromJson(filepath_bw_config);

            // If null, creating backup works configuration with null
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

            // While user haven't selected valid option
            bool optionSelected = false;
            while (!optionSelected)
            {
                // Prompt backup works to edit and wait for choice
                menuView.ClearConsole();
                string menuBWOption = backupWorksEditView.PromptEditBackupWorks(backupWorks);
                if (menuBWOption == "0") // Go back
                {
                    optionSelected = true;
                }
                // If backup work selected
                else if (menuBWOption == "1" || menuBWOption == "2" || menuBWOption == "3" || menuBWOption == "4" || menuBWOption == "5")
                {
                    int i = Convert.ToInt32(menuBWOption)-1;
                    bool validName = false;
                    bool validSrcFolder = false;
                    bool validDstFolder = false;
                    bool validType = false;

                    // While not valid name entered
                    while (!validName)
                    {
                        string previousName = backupWorks[i].Name ?? "";
                        // Prompt menu to enter name
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
                            // If nothing, check if previous value isn't null
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

                    // While no valid source folder entered
                    while (!validSrcFolder)
                    {
                        string previousSrcFolder = backupWorks[i].SrcFolder ?? "";

                        // Prompt menu to enter source folder
                        menuView.ClearConsole();
                        backupWorks[i].SrcFolder = backupWorksEditView.PromptEditBackupWorksSrcFolder(backupWorks[i]);
                        
                        // If nothing, check if previous value isn't null
                        if (backupWorks[i].SrcFolder == "" && previousSrcFolder != "")
                        {
                            backupWorks[i].SrcFolder = previousSrcFolder;
                            validSrcFolder = true;
                        }
                        else if (backupWorks[i].SrcFolder != "")
                        {
                            backupWorks[i].SrcFolder = fileHelper.FormatPath(backupWorks[i].SrcFolder);
                            validSrcFolder = true;
                        }
                        else
                        {
                            if (fileHelper.DirectoryExists(backupWorks[i].SrcFolder))
                            {
                                validSrcFolder = true;
                            }
                        }
                    }

                    // While no valid destination folder entered 
                    while (!validDstFolder)
                    {
                        string previousDstFolder = backupWorks[i].DstFolder ?? "";
                        // Prompt menu to enter destination folder
                        menuView.ClearConsole();
                        backupWorks[i].DstFolder = backupWorksEditView.PromptEditBackupWorksDstFolder(backupWorks[i]);
                        
                        // If nothing, check if previous value isn't null
                        if (backupWorks[i].DstFolder == "" && previousDstFolder != "")
                        {
                            backupWorks[i].DstFolder = previousDstFolder;
                            validDstFolder = true;
                        }
                        else if (backupWorks[i].DstFolder != "")
                        {
                            backupWorks[i].DstFolder = fileHelper.FormatPath(backupWorks[i].DstFolder);
                            validDstFolder = true;
                        }
                        else
                        {
                            if (fileHelper.DirectoryExists(backupWorks[i].DstFolder))
                            {
                                validDstFolder = true;
                            }
                        }
                    }

                    // While no valid type entered
                    while (!validType)
                    {
                        string previousType = backupWorks[i].Type ?? "";
                        // Prompt menu to enter type
                        menuView.ClearConsole();
                        backupWorks[i].Type = backupWorksEditView.PromptEditBackupWorksType(backupWorks[i]);
                        
                        // If nothing, check if previous value isn't null
                        if (backupWorks[i].Type == "" && (previousType == "complete" || previousType == "differencial"))
                        {
                            backupWorks[i].Type = previousType;
                            validType = true;
                        }
                        else if (backupWorks[i].Type == "1")
                        {
                            backupWorks[i].Type = "complete";
                            validType = true;
                        }
                        else if (backupWorks[i].Type == "2")
                        {
                            backupWorks[i].Type = "differencial";
                            validType = true;
                        }
                        else
                        {
                            backupWorks[i].Type = "";
                        }
                    }

                    // Write all backup work edit to json file
                    jsonHelper.WriteBackupWorkToJson(filepath_bw_config, backupWorks);
                }
            }
        }
    }
}
