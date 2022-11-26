using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace EasySave_Console.Models
{
    class Logs
    {
        public string? Name { get; set; }
        public string? FileSource { get; set; }
        public string? FileTarget { get; set; }
        public string? DestPath { get; set; }
        public int? FileSize { get; set; }
        public int? FileTransferTime { get; set; }
        public string? Time { get; set; }
        
        public void GenerateLog(string message)
        {
            string LogPath = ConfigurationManager.AppSettings["logPath"];

            //using (StreamWriter writer = new StreamWriter(LogPath, true)) 
        }

    }
}
