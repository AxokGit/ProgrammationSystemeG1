using EasySave_WPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave_WPF
{
    class JsonHelper
    {
        // Method to read settings from Settings.json
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
        // Method to write settings in Settings.json
        public void WriteSettingsToJson(string filepath, Settings content)
        {
            string json = JsonConvert.SerializeObject(content, Formatting.Indented);

            string directoryName = Path.GetDirectoryName(filepath);

            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            File.WriteAllText($@"{filepath}", json);
        }
        // Method to write backup work to BackupWorks.json
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
        // Method to read backup works from BackupWorks.json
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
        // Method to write StateLog to StateLog.json
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
        // Read log from Logs folder
        public List<Log>? ReadLogFromJson(string filepath)
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
        // Method to append log to log's file in Logs folder
        public void WriteLogToJson(string filepath, Log content)
        {
            var logs_in_file = ReadLogFromJson(filepath) ?? new List<Log>();

            logs_in_file.Add(content);

            string json = JsonConvert.SerializeObject(logs_in_file.ToArray(), Formatting.Indented);

            string directoryName = Path.GetDirectoryName(filepath);

            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            File.WriteAllText($@"{filepath}", json);
        }
    }
}
