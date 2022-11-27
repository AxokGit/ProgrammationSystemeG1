using System.Collections.Generic;

namespace EasySave_Console.Models
{
    class BackupWork
    {
        public string? Name;
        public string? SrcFolder;
        public string? DstFolder;
        public string? Type;
        public List<FileModel>? Files; 

        public BackupWork(string? name, string? src_folder, string? dst_folder, string? type)
        {
            this.Name = name;
            this.SrcFolder = src_folder;
            this.DstFolder = dst_folder;
            this.Type = type;
        }

        public bool IsEmpty()
        {
            if (this.Name == null || this.SrcFolder == null || this.DstFolder == null || this.Type == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
