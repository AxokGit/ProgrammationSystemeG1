using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_WPF_client.Models
{
    /// <summary>
    /// Class meant to create the daily logs
    /// </summary>
    public class Log
    {
        public string? BackupWorkName;
        public string? DateTime;
        public long? FileSize;
        public string? SrcFilePath;
        public string? DstFilePath;
        public long? TimeTransfer;

        /// <summary>
        /// Constructor of the Log class
        /// </summary>
        /// <param name="BackupWorkName"></param>
        /// <param name="dateTime"></param>
        /// <param name="FileSize"></param>
        /// <param name="srcFilePath"></param>
        /// <param name="dstFilePath"></param>
        /// <param name="TimeTransfer"></param>
        
        public Log() { }
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
