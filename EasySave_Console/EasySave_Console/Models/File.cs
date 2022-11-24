using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace EasySave_Console.Models
{
    class File
    {
        public string? filepath { get; set; }
        public string? text { get; set; }
        public string? dest_path { get; set; }

        public void Copy(string? filepath, string dest_path)
        {
            text = System.IO.File.ReadAllText(filepath);

            var jsonSettings = new Newtonsoft.Json.JsonSerializerSettings();
            jsonSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;

            var serializedObject = JsonConvert.SerializeObject(text, Newtonsoft.Json.Formatting.Indented, jsonSettings);

            //Save to file
            dest_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string FilePath = Path.Combine(dest_path, "saveFile.json");

            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                sw.Write(serializedObject);
            }

            //Read from file
            string content = null;

            using (StreamReader sr = new StreamReader(FilePath))
            {
                content = sr.ReadToEnd();
            }
        }
    }
}
