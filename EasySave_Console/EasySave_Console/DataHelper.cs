using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml;
using EasySave_Console.Models;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace EasySave_Console
{
    class DataHelper 
    {
        FileHelper fileHelper = new FileHelper();

        // Method to read settings from Settings.json
        public Settings ReadSettingsFromJson(string filepath)
        {
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText($@"{filepath}");
                return JsonConvert.DeserializeObject<Settings>(json) ?? new Settings(language: "en"); ;
            }
            else
            {
                Settings default_settings = new Settings("en", ".json");
                WriteSettingsToJson(filepath, default_settings);
                return default_settings;
            }
        }
        // Method to write settings in Settings.json
        public void WriteSettingsToJson(string filepath, Settings content)
        {
            string json = JsonConvert.SerializeObject(content, Newtonsoft.Json.Formatting.Indented);

            string directoryName = Path.GetDirectoryName(filepath);
            
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            File.WriteAllText($@"{filepath}", json);
        }
        // Method to write backup work to BackupWorks.json
        public void WriteBackupWorkToJson(string filepath, List<BackupWork> content)
        {
            string json = JsonConvert.SerializeObject(content.ToArray(), Newtonsoft.Json.Formatting.Indented);
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
        public void WriteStateLog(string filepath, StateLog content)
        {
            string filepath_settings = fileHelper.FormatFilePath(@"%AppData%\EasySave\Settings.json");

            Settings settings = ReadSettingsFromJson(filepath_settings);
            
            if (settings.AvailableLogExtension.Contains(settings.LogExtension)) {
                if (settings.LogExtension == ".json")
                {
                    filepath += ".json";
                    string json = JsonConvert.SerializeObject(content, Newtonsoft.Json.Formatting.Indented);

                    string directoryName = Path.GetDirectoryName(filepath);
                    if (!Directory.Exists(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }
                    File.WriteAllText($@"{filepath}", json);
                }
                else if (settings.LogExtension == ".xml")
                {
                    filepath += ".xml";

                    XmlSerializer xs = new XmlSerializer(typeof(StateLog));

                    TextWriter txtWriter = new StreamWriter(filepath);

                    xs.Serialize(txtWriter, content);

                    txtWriter.Close();
                }
            }
        }
        // Read log from Logs folder
        public List<Log>? ReadLog(string filepath)
        {
            string filepath_settings = fileHelper.FormatFilePath(@"%AppData%\EasySave\Settings.json");

            Settings settings = ReadSettingsFromJson(filepath_settings);

            if (settings.AvailableLogExtension.Contains(settings.LogExtension))
            {
                if (settings.LogExtension == ".json")
                {
                    filepath += ".json";
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
                else if (settings.LogExtension == ".xml")
                {
                    filepath += ".xml";

                    if (File.Exists(filepath))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<Log>));
                        List<Log>? result;
                        using (FileStream fileStream = new FileStream(filepath, FileMode.Open))
                        {
                            result = (List<Log>?)serializer.Deserialize(fileStream);
                        }
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                        
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        // Method to append log to log's file in Logs folder
        public void WriteLog(string filepath, Log content)
        {
            string filepath_settings = fileHelper.FormatFilePath(@"%AppData%\EasySave\Settings.json");

            Settings settings = ReadSettingsFromJson(filepath_settings);

            var logs_in_file = ReadLog(filepath) ?? new List<Log>();
            logs_in_file.Add(content);

            string directoryName = Path.GetDirectoryName(filepath);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            if (settings.AvailableLogExtension.Contains(settings.LogExtension))
            {
                if (settings.LogExtension == ".json")
                {
                    filepath += ".json";

                    string json = JsonConvert.SerializeObject(logs_in_file.ToArray(), Newtonsoft.Json.Formatting.Indented);

                    File.WriteAllText($@"{filepath}", json);
                }
                else if (settings.LogExtension == ".xml")
                {
                    filepath += ".xml";

                    XmlSerializer xs = new XmlSerializer(typeof(List<Log>));

                    TextWriter txtWriter = new StreamWriter(filepath);

                    xs.Serialize(txtWriter, logs_in_file);

                    txtWriter.Close();
                }
            }
        }
    }
}
