using EasySave_Console.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave_Console
{
    class FileHelper
    {
        public string filepath_bw_config { get; set; } = @"%AppData%\EasySave\BackupWorks.json";
        public string filepath_statelog { get; set; } = @"%AppData%\EasySave\StateLog.json";
        public string FormatPath(string path)
        {
            return Path.GetFullPath(path);
        }
        public string FormatFilePath(string path)
        {
            return Environment.ExpandEnvironmentVariables(path);
        }
        public List<FileModel> GetAllFileFromFolderPath(string folderPath)
        {
            DirectoryInfo d = new DirectoryInfo(folderPath);
            FileInfo[] fileInfo = d.GetFiles("*.*", SearchOption.AllDirectories);
            List<FileModel> files = new List<FileModel>();
            
            foreach (FileInfo file in fileInfo)
            {
                files.Add(new FileModel(file.Name, file.FullName, file.Length));
            }

            return files;
        }

    }
}
