using EasySave_Console.Models;
using EasySave_Console.Views;
using System;
using System.Collections.Generic;
using System.IO;

namespace EasySave_Console.Controllers
{
    class OpenLogsController
    {
        OpenLogsView openLogsView = new OpenLogsView();
        MenuView menuView = new MenuView();
        FileHelper fileHelper = new FileHelper();
        public OpenLogsController()
        {
            string filepath_log = fileHelper.FormatFilePath(fileHelper.filepath_log);
            string dirpath_log = fileHelper.GetDirectoryName(filepath_log);

            List<FileModel> fileModels = fileHelper.GetAllFile(dirpath_log);

            bool optionSelected = false;
            while (!optionSelected)
            {
                menuView.ClearConsole();
                string option_str = openLogsView.PromptLogFiles(fileModels);
                try
                {
                    int option = Convert.ToInt32(option_str);

                    if (option >= 1 && option <= fileModels.Count)
                    {
                        FileHelper.OpenFile(fileModels[option-1]);
                        openLogsView.FileOpened();
                    }
                    else if (option == 0)
                    {
                        optionSelected = true;
                    }
                }
                catch { }
            }
        }
    }
}
