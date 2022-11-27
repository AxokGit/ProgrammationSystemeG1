using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace EasySave_Console.Models
{
    public class BackupWorkLog
    {
        public string? DestPath { get; set; }
        public int? FileSize { get; set; }
        public int? FileTransferTime { get; set; }
        public string? Time { get; set; }
        //public static string GenerateLog(BackupWork BO)
        //{
        //    string strResultJson = JsonConvert.SerializeObject(BO);
        //    File.WriteAllText(@"logs.json", strResultJson);

        //    return string.Format("Logs information:\n\tName: {0}, Source foler: {1}, Destination folder: {2}, Type : {3}, Size: {4}, Transfer Time: {5}, Time: {6}",
        //                           BO.Name, BO.SrcFolder, BO.DstFolder, BO.Type, BO.FileSize, BO.FileTransferTime, BO.Time);
        //}

    }
}
