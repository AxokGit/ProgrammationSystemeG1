using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace EasySave_Console.Models
{
    class File
    {
        public string? DestFileName { get; set; }
        public string? SrcFilePath { get; set; }
        public string? Text { get; set; }
        public string? DestPath { get; set; }
        public string? FilePath { get; set; }
        public void Copy(string FilePath, string DestFileName)
        {
            Text = System.IO.File.ReadAllText(FilePath);

            var jsonSettings = new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
            };

            var serializedObject = JsonConvert.SerializeObject(Text, Newtonsoft.Json.Formatting.Indented, jsonSettings);

            //Save to file
            DestPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            FilePath = Path.Combine(DestPath, DestFileName);

            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                sw.Write(serializedObject);
            }

            //Read from file
            using (StreamReader sr = new StreamReader(FilePath))
            {
                string content = sr.ReadToEnd();
            }
        }
    }
}
