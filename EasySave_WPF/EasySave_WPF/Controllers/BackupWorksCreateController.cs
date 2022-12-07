using EasySave_WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_WPF.Controllers
{
    class BackupWorksCreateController
    {
        DataHelper dataHelper = new DataHelper();
        FileHelper fileHelper = new FileHelper();
        public void CreateBackupAndSave(BackupWork backupWork)
        {
            string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
            var backupworks = dataHelper.ReadBackupWorkFromJson(filepath_bw_config);

            if (backupworks != null)
            {
                backupworks.Add(backupWork);
            }
            else
            {
                //backupworks = new List<BackupWork>().Add(backupWork);
            }

            
            dataHelper.WriteBackupWorkToJson(filepath_bw_config, backupworks);
        }
    }
}
