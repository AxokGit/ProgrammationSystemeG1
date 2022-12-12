using System.Collections.Generic;

namespace EasySave_WPF_client.Models
{
    /// <summary>
    /// Class meant to create our backupwork
    /// </summary>
    public class BackupWork
    {
        public string? Name { get; set; }
        public string? SrcFolder { get; set; }
        public string? DstFolder { get; set; }
        public string? Type { get; set; }
        public bool Running { get; set; } = false;
        public double? Progression { get; set; }

        /// <summary>
        /// Constructor of our class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="src_folder"></param>
        /// <param name="dst_folder"></param>
        /// <param name="type"></param>
        public BackupWork(string? name, string? src_folder, string? dst_folder, string? type)
        {
            this.Name = name;
            this.SrcFolder = src_folder;
            this.DstFolder = dst_folder;
            this.Type = type;
        }
    }
}
