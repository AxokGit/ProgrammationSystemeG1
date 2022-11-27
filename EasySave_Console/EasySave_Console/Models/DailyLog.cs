using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Models
{
    class DailyLog
    {
        public string? BackupWorkName;
        public DateTime TimeAndDate;
        public int FileSize;
        public string? SrcFolder;
        public string? DestFolder;
        public long TimeTransfer;

        public DailyLog(string? BackupWorkName, DateTime TimeAndDate, int FileSize, string? SrcFolder, string? DestFolder, long TimeTransfer)
        {
            this.BackupWorkName = BackupWorkName;
            this.TimeAndDate = TimeAndDate;
            this.FileSize = FileSize;
            this.SrcFolder = SrcFolder;
            this.FileSize = FileSize;
            this.DestFolder = DestFolder;
            this.TimeTransfer = TimeTransfer;
        }
    }
}
