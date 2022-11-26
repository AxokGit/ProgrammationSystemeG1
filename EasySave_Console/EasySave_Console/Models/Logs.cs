using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace EasySave_Console.Models
{
    public class Logs : BackupWork
    {
        public string? FileSource { get; set; }
        public string? FileTarget { get; set; }
        public string? DestPath { get; set; }
        public int? FileSize { get; set; }
        public int? FileTransferTime { get; set; }
        public string? Time { get; set; }
        BackupWork bw = new BackupWork();
        public string GenerateLog(object bw)
        {
            string strResultJson = JsonConvert.SerializeObject(bw);
            File.WriteAllText(@"logs.json", strResultJson);
            Console.WriteLine("Stored!");

            strResultJson = String.Empty;
            strResultJson = File.ReadAllText(@"logs.json");

            var dictionnary = JsonConvert.DeserializeObject<IDictionary>(strResultJson);
            foreach (DictionaryEntry entry in dictionnary)
            {
                Console.WriteLine(entry.Key + ": " + entry.Value);
            }            
            
            return string.Format("Logs: \n\tName: {0}," + " FileSource: {1}," + " FileTarget: {2},"
                                  + " DestPath: {3}," + " FileSize: {4}," + "FileTransferTime: {5},"
                                  + "Time: {6}"
                                  ,Name, FileSource, FileTarget, DestPath, FileSize, FileTransferTime, Time);
        }

    }
}
