using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace EasySave_Console.Models
{
    class FolderModel
    {
        public bool witness { get; set; }
        public string? sourceDir { get; set; }
        public string? backupDir { get; set; }
        public string[]? piclist { get; set; }
        public string[]? srclist { get; set; }
        public string[]? deslist { get; set; }
        List<int>? Haslist1 { get; set; }
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
        public void CheckHashCode(string sourceDir, string backupDir)
        {
            bool witness = true;
            List<int> Haslist1 = new List<int>();
            List<int> Haslist2 = new List<int>();
            int Hash1 = 0;
            string[] srclist = Directory.GetFiles(sourceDir, "*.*");

            foreach (string f in srclist)
            {
                string fName = f.Substring(sourceDir.Length + 1);
                Hash1 = fName.GetHashCode();
                Haslist1.Add(Hash1);
            }
            string[] deslist = Directory.GetFiles(backupDir, "*.*");

            foreach (string f in deslist)
            {
                string fName = f.Substring(backupDir.Length + 1);
                Hash1 = fName.GetHashCode();
                Haslist2.Add(Hash1);
            }

            for (int i = 0; i < Haslist1.Count; i++)
            {
                if (Haslist1[i] != Haslist2[i])
                {
                    witness = false;
                }
            }
            if (witness == true)
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("weird");
            }
        }
    }
}
