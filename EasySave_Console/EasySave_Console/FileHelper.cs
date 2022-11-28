using EasySave_Console.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EasySave_Console
{
    class FileHelper
    {
        public string filepath_settings = @"%AppData%\EasySave\Settings.json";
        public string filepath_bw_config = @"%AppData%\EasySave\BackupWorks.json";
        public string filepath_statelog = @"%AppData%\EasySave\StateLog.json";
        public string filepath_log = @"%AppData%\EasySave\Logs\EasySave_Log_{}.json";

        public string FormatPath(string path)
        {
            return Path.GetFullPath(path);
        }
        public string FormatFilePath(string path)
        {
            return Environment.ExpandEnvironmentVariables(path);
        }
        public List<FileModel> GetAllFile(string folderPath)
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
        public List<FileModel> GetAllEditedFile(string srcFolderPath, string dstFolderPath)
        {
            DirectoryInfo srcDir = new DirectoryInfo(srcFolderPath);
            DirectoryInfo dstDir = new DirectoryInfo(dstFolderPath);

            FileInfo[] srcFileInfo = srcDir.GetFiles("*.*", SearchOption.AllDirectories);
            FileInfo[] dstFileInfo = dstDir.GetFiles("*.*", SearchOption.AllDirectories);
            List<FileModel> files = new List<FileModel>();

            Dictionary<string, string> srcFileDict = new Dictionary<string, string>();
            Dictionary<string, string> dstFileDict = new Dictionary<string, string>();

            foreach (FileInfo file in dstFileInfo)
            {
                dstFileDict.Add(GetFileNameAndMD5Hash(file), file.Name);
            }

            foreach (FileInfo file in srcFileInfo)
            {
                var md5 = GetFileNameAndMD5Hash(file);
                if (!dstFileDict.ContainsKey(md5))
                {
                    files.Add(new FileModel(file.Name, file.FullName, file.Length));
                }
            }
            return files;
        }

        public string GetFileNameAndMD5Hash(FileInfo file)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(file.FullName))
                {
                    var hash = md5.ComputeHash(stream);
                    return (file.Name+BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant());
                }
            }
        }

        public void CreateDirectory(string path, string foldername)
        {
            path += foldername;
            Directory.CreateDirectory(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public bool FileExists(string filepath)
        {
            return File.Exists(filepath);
        }
         
        
        public static void OpenFile(FileModel fileModel)
        {
           var p = new Process();
           p.StartInfo = new ProcessStartInfo(fileModel.FullPath)
              {
                UseShellExecute = true
              };
           p.Start();   
        }

    }
}
