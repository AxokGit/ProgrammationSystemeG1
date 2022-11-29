﻿using EasySave_Console.Views;

namespace EasySave_Console.Controllers
{
    class MenuController
    {
        MenuView menuView = new MenuView();
        FileHelper fileHelper = new FileHelper();
        public MenuController()
        {
            menuView.OnBootMessage();
            new LanguageController();
            bool optionSelected = false;
            while (!optionSelected)
            {
                menuView.ClearConsole();
                string menuOption = menuView.PromptMainMenu();
                if (menuOption == "0")
                {
                    optionSelected = true;
                }
                else if (menuOption == "1")
                {
                    new BackupWorksRunController();
                }
                else if (menuOption == "2")
                {
                    new BackupWorksEditController();
                }
                else if (menuOption == "3")
                {
                    new OpenLogsController();
                }
                else if (menuOption == "4")
                {
                    menuView.ClearConsole();
                    new LanguageController().SelectLanguage();
                }
                else if (menuOption == "5")
                {
                    new OpenSettingsDirController();
                }
            }
        }
    }
}
