using System.Collections.Generic;

namespace EasySave_WPF.Models
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
        public int? Progression { get; set; }
        public int? TotalFiles { get; set; }
        public int? RemainingFiles { get; set; }
        public string FileNameInCopy { get; set; }
        public List<StateLog>? Logs;

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
