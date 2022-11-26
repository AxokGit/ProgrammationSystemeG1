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
            menuView.ClearConsole();
            backupWorksRunView.PromptRunBackupWorks(backupWorks);
        }
    }
}
