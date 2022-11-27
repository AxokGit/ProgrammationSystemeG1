using EasySave_Console.Models;
using EasySave_Console.Views;
using System;
using System.Collections.Generic;
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
                        try
                        {
                            List<FileModel> files = fileHelper.GetAllFileFromFolderPath(backupWorks[i].SrcFolder);
                            backupWorks[i].Files = files;

                            foreach (FileModel file in files)
                            {
                                Console.WriteLine(file.Name);
                            }
                            
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Console.ReadKey();
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
