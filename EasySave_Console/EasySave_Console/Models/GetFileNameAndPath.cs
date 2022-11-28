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
        //must declare GetfileNameAndPath[] Array = new GetfileNameAndPath[size] before using it
        public static Array listFilesInDirectory(string workingDirectory)
        {
            GetFileNameAndPath[] ObjectArray = new GetFileNameAndPath[2];
            int temoins = 0;
            string[] filePaths = Directory.GetFiles(workingDirectory);
            foreach (string filePath in filePaths)
            {
                ObjectArray[temoins] = new GetFileNameAndPath(temoins + 1, Path.GetFileName(filePath), filePath);
                temoins++;
            }
            for (int i = 0; i < ObjectArray.Length; i++)
            {
                
                Console.WriteLine("{0} - {1}", ObjectArray[i].Order, System.IO.Path.GetFileNameWithoutExtension(ObjectArray[i].FileName));
            }
            return ObjectArray;
        }

        public static void OpenFile(int Number, GetFileNameAndPath[] WitnessArray)
        {
            for (int j = 0; j < WitnessArray.Length; j++)
            {
                if (WitnessArray[j].Order == Number)
                {
                    var p = new Process();
                    p.StartInfo = new ProcessStartInfo(WitnessArray[j].FilePath)
                    {
                        UseShellExecute = true
                    };
                    p.Start();
                }
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
