using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EasySave_Console.Models;
using Newtonsoft.Json;

namespace EasySave_Console
{
    class JsonHelper
    {
        public void WriteBackupWorkToJson(string filepath, List<BackupWork> content)
        {
            string json = JsonConvert.SerializeObject(content.ToArray(), Formatting.Indented);
            File.WriteAllText($@"{filepath}", json);
        }
        public List<BackupWork>? ReadBackupWorkFromJson(string filepath)
        {
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText($@"{filepath}");
                List<BackupWork>? backupWorks = JsonConvert.DeserializeObject<List<BackupWork>>(json);
                return backupWorks;
            } 
            else
            {
                return null;
            }
            
            
        }
    }
}
