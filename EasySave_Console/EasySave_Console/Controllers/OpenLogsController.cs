using EasySave_Console.Models;
using EasySave_Console.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave_Console.Controllers
{
    class OpenLogsController
    {
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        OpenLogsView openLogsView = new OpenLogsView();
        MenuView menuView = new MenuView();
        FileHelper fileHelper = new FileHelper();
        public OpenLogsController()
        {
            string path_log = Path.GetDirectoryName(fileHelper.FormatFilePath(fileHelper.filepath_log));
            List<FileModel> fileModels = fileHelper.GetAllFile(path_log);

            menuView.ClearConsole();
            
            string choice = openLogsView.DiplayLogChoice(fileModels);

            FileHelper.OpenFile(fileModels[Convert.ToInt32(choice) - 1]);
        }
    }
}
