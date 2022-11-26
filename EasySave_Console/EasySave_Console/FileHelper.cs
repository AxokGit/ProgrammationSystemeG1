using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console
{
    class FileHelper
    {
        public string filepath_bw_config { get; set; } = @"%AppData%/BackupWorks.json";
        public string FormatFilePath(string path)
        {
            return Environment.ExpandEnvironmentVariables(path);
        }

    }
}
