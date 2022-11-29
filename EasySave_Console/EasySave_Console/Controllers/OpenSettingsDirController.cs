using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Controllers
{
    class OpenSettingsDirController
    {
        FileHelper fileHelper = new FileHelper();
        public OpenSettingsDirController()
        {
            string settingsFilePath = fileHelper.FormatFilePath(fileHelper.filepath_settings);
            string directoryPath = fileHelper.GetDirectoryName(settingsFilePath);
            fileHelper.OpenDirectory(directoryPath);
        }
    }
}
