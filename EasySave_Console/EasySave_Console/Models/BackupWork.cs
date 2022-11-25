namespace EasySave_Console.Models
{
    class BackupWork
    {
        public string? Name { get; set; }
        public string? SrcFolder { get; set; }
        public string? DstFolder { get; set; }
        public string? Type { get; set; }

        public BackupWork(string? name, string? src_folder, string? dst_folder, string? type)
        {
            this.Name = name;
            this.SrcFolder = src_folder;
            this.DstFolder = dst_folder;
            this.Type = type;
        }
    }
}
