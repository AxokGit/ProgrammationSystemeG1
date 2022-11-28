using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Models
{
    class Log
    {
        public string BackupWorkName;
        public string DateTime;
        public long FileSize;
        public string SrcFilePath;
        public string DstFilePath;
        public long TimeTransfer;

        public Log(string BackupWorkName, string dateTime, long FileSize, string srcFilePath, string dstFilePath, long TimeTransfer)
        {
            this.BackupWorkName = BackupWorkName;
            this.DateTime = dateTime;
            this.FileSize = FileSize;
            this.SrcFilePath = srcFilePath;
            this.FileSize = FileSize;
            this.DstFilePath = dstFilePath;
            this.TimeTransfer = TimeTransfer;
        }
    }
}
