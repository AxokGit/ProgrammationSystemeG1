using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Models
{
    class StateLog
    {
        string BackupWorkName;
        DateTime StartTimestamp;
        bool State;
        int? TotalFiles;
        List<long>? FileSize;
        int RemainingFiles;
        List<long>? RemainingFileSize;
        string SrcFolder;
        string DstFolder;

        public StateLog(string backupWorkName, DateTime startTimestamp, bool state, int? totalFiles, List<long>? fileSize, int remainingFiles, List<long>? remainingFileSize, string srcFolder, string dstFolder)
        {
            this.BackupWorkName = backupWorkName;
            this.StartTimestamp = startTimestamp;
            this.State = state;
            this.TotalFiles = totalFiles;
            this.FileSize = fileSize;
            this.RemainingFiles = remainingFiles;
            this.RemainingFileSize = remainingFileSize;
            this.SrcFolder = srcFolder;
            this.DstFolder = dstFolder;
        }
    }
}
