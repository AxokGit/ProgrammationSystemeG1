using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace EasySave_Console.Models
{
    class FolderModel
    {
        public string? sourceDir { get; set; }
        public string? backupDir { get; set; }
        public string[]? piclist { get; set; }
        public void Copy(string sourceDir, string backupDir)
        {
            try
            {
                string[] picList = Directory.GetFiles(sourceDir, "*.*");

                foreach (string f in picList)
                {
                    string fName = f.Substring(sourceDir.Length + 1);

                    File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName), true);
                }
            }

            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);
            }
        }
    }
}
