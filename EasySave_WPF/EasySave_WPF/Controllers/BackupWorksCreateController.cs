using EasySave_WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_WPF.Controllers
{
    class BackupWorksCreateController
    {
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
        }
    }
}
