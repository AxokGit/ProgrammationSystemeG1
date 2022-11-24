namespace EasySave_Console.Models
{
    class BackupWork
    {
        public string? Name { get; set; }
        public string? Src_folder { get; set; }
        public string? Dst_folder { get; set; }
        public string? Type { get; set; }

        public BackupWork(string? name, string? src_folder, string? dst_folder, string? type)
        {
            this.Name = name;
            this.Src_folder = src_folder;
            this.Dst_folder = dst_folder;
            this.Type = type;
        }
    }
}
