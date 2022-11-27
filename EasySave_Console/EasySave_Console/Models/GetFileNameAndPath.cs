using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EasySave_Console.Models
{
    class GetFileNameAndPath
    {
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        internal static void listFilesInDirectory(string workingDirectory)
        {
            string[] filePaths = Directory.GetFiles(workingDirectory);

            foreach (string filePath in filePaths)
            {
                Console.WriteLine(filePath);
            }
        }

        public static void GetSubDirectoryList(string workingDirectory)
        {
            string[] directories = Directory.GetDirectories(workingDirectory);

            if (Directory.GetFiles(workingDirectory).Length == 0)
            {
                Console.WriteLine("No Sub-Directory");
            }
            else
            {
                foreach (string directory in directories)
                {
                    Console.WriteLine(directory);
                }
            }
        }
    }
}
