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
        public Settings ReadSettingsFromJson(string filepath)
        {
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText($@"{filepath}");
                return JsonConvert.DeserializeObject<Settings>(json) ?? new Settings("en");
            }
            else
            {
                Settings default_settings = new Settings("en");
                WriteSettingsToJson(filepath, default_settings);
                return default_settings;
            }
        }
        public void WriteSettingsToJson(string filepath, Settings content)
        {
            string json = JsonConvert.SerializeObject(content, Formatting.Indented);

            string directoryName = Path.GetDirectoryName(filepath);
            
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            File.WriteAllText($@"{filepath}", json);
        }
        public void WriteBackupWorkToJson(string filepath, List<BackupWork> content)
        {
            string json = JsonConvert.SerializeObject(content.ToArray(), Formatting.Indented);
            string directoryName = Path.GetDirectoryName(filepath);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            File.WriteAllText($@"{filepath}", json);
        }

        public List<BackupWork>? ReadBackupWorkFromJson(string filepath)
        {
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText($@"{filepath}");
                return JsonConvert.DeserializeObject<List<BackupWork>>(json);
            }
            else
            {
                return null;
            }
        }
        public void WriteStateLogToJson(string filepath, StateLog content)
        {
            string json = JsonConvert.SerializeObject(content, Formatting.Indented);

            string directoryName = Path.GetDirectoryName(filepath);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            File.WriteAllText($@"{filepath}", json);
        }
        public List<Log>? ReadLogToJson(string filepath)
        {
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText($@"{filepath}");
                return JsonConvert.DeserializeObject<List<Log>>(json);
            }
            else
            {
                return null;
            }
        }
        public void WriteLogToJson(string filepath, Log content)
        {
            var logs_in_file = ReadLogToJson(filepath) ?? new List<Log>();

            logs_in_file.Add(content);

            string json = JsonConvert.SerializeObject(logs_in_file.ToArray(), Formatting.Indented);

            string directoryName = Path.GetDirectoryName(filepath);

            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            File.WriteAllText($@"{filepath}", json);
        }
    }
}
