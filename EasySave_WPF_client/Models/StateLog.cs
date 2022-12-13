using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_WPF_client.Models
{
    /// <summary>
    /// Class meant to create the state log
    /// </summary>
    public class StateLog
    {
        public string? BackupWorkName;
        public string? StartTimestamp;
        public bool? Active;
        public int? TotalFiles;
        public long? TotalSize;
        public int? RemainingFiles;
        public long? RemainingSize;
        public string? SrcFolder;
        public string? DstFolder;

        /// <summary>
        /// Constructor of the StateLog class
        /// </summary>
        /// <param name="backupWorkName"></param>
        /// <param name="startTimestamp"></param>
        /// <param name="active"></param>
        /// <param name="totalFiles"></param>
        /// <param name="fileSize"></param>
        /// <param name="remainingFiles"></param>
        /// <param name="remainingSize"></param>
        /// <param name="srcFolder"></param>
        /// <param name="dstFolder"></param>
        /// 
        public StateLog() { }
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
