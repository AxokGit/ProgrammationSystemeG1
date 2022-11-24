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
        string filepath_backup_works_config = @"%AppData%/BackupWorks.json";




        public BackupWorksEditController()
        {
            filepath_backup_works_config = Environment.ExpandEnvironmentVariables(filepath_backup_works_config);

            List<BackupWork>? backupWorks = jsonHelper.ReadBackupWorkFromJson(filepath_backup_works_config);

            if (backupWorks == null)
            {
                List<BackupWork> list_temp = new List<BackupWork>();
                for (int i = 0; i < 5; i++)
                {
                    list_temp.Add(new BackupWork(null, null, null, null));
                }

                jsonHelper.WriteBackupWorkToJson(filepath_backup_works_config, list_temp);
                backupWorks = jsonHelper.ReadBackupWorkFromJson(filepath_backup_works_config);
            }
            menuView.ClearConsole();
            backupWorksEditView.PromptEditBackupWorks(backupWorks);
        }
    }
}
