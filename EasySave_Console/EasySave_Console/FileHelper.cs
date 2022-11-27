﻿using EasySave_Console.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
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
                dstFileDict.Add(GetMD5Hash(file), file.Name);
            }
            foreach (FileInfo file in srcFileInfo)
            {
                var md5 = GetMD5Hash(file);
                if (!dstFileDict.ContainsKey(md5))
                {
                    files.Add(new FileModel(file.Name, file.FullName, file.Length));
                }
            }
            return files;
        }

        public string GetMD5Hash(FileInfo file)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(file.FullName))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
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

    }
}
