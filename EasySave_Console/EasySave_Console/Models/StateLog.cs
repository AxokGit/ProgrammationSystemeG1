using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Models
{
    class StateLog
    {
        public string BackupWorkName;
        public string StartTimestamp;
        public bool Active;
        public int TotalFiles;
        public long TotalSize;
        public int RemainingFiles;
        public long RemainingSize;
        public string SrcFolder;
        public string DstFolder; 

        public StateLog(string backupWorkName, string startTimestamp, bool active, int totalFiles, long fileSize, int remainingFiles, long remainingSize, string srcFolder, string dstFolder)
        {
            this.BackupWorkName = backupWorkName;
            this.StartTimestamp = startTimestamp;
            this.Active = active;
            this.TotalFiles = totalFiles;
            this.TotalSize = fileSize;
            this.RemainingFiles = remainingFiles;
            this.RemainingSize = remainingSize;
            this.SrcFolder = srcFolder;
            this.DstFolder = dstFolder;
        }
    }
}
