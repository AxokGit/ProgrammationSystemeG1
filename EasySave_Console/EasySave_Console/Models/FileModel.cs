using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Models
{
    class FileModel
    {
        public string Name;
        public string FullPath;
        public long Size;

        public FileModel(string name, string fullpath, long size)
        {
            this.Name = name;
            this.FullPath = fullpath;
            this.Size = size;
        }
    }
}
