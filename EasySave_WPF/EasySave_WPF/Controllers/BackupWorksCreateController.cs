using EasySave_WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_WPF.Controllers
{
    class BackupWorksCreateController
    {
<<<<<<< Updated upstream
        JsonHelper jsonHelper = new JsonHelper(); // Instantiation of the json helper
        FileHelper fileHelper = new FileHelper(); // Instantiation of the file helper

        public BackupWorksCreateController(string Name, string SourceFolder, string DestinationFolder, string Type)
        {
            string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
            List<BackupWork> backupWorks = jsonHelper.ReadBackupWorkFromJson(filepath_bw_config);
            if (backupWorks != null)
            {
                backupWorks.Add(new BackupWork(Name, SourceFolder, DestinationFolder, Type));
                jsonHelper.WriteBackupWorkToJson(filepath_bw_config, backupWorks);
                jsonHelper.ReadBackupWorkFromJson(filepath_bw_config);
            }
=======
        DataHelper dataHelper = new DataHelper();
        FileHelper fileHelper = new FileHelper();
        public void CreateBackupAndSave(BackupWork backupWork)
        {
            string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
            var backupworks = dataHelper.ReadBackupWorkFromJson(filepath_bw_config);

            backupworks.Add(backupWork);

            dataHelper.WriteBackupWorkToJson(filepath_bw_config, backupworks);
>>>>>>> Stashed changes
        }
    }
}
