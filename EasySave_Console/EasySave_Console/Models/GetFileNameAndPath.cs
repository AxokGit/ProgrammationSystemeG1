using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace EasySave_Console.Models
{
    class GetFileNameAndPath
    {
        public int? Order { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        
        public GetFileNameAndPath(int Order, string FileName, string FilePath)
        {
            this.Order = Order;
            this.FileName = FileName;
            this.FilePath = FilePath;


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
