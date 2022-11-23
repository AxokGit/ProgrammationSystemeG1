using EasySave_Console.Models;
using EasySave_Console.Views;
using System;
using System.Collections.Generic;

namespace EasySave_Console.Controllers
{
    class MenuController
    {
        MenuView menuView = new MenuView();
        LanguageMenuView languageMenuView = new LanguageMenuView();

        public MenuController()
        {
            bool languageSelected = false;
            while (!languageSelected)
            {
                menuView.ClearConsole();
                menuView.OnBootMessage();
                string languageOption = languageMenuView.PromptLanguageOption();
                if (languageOption == "1")
                {
                    LangHelper.ChangeLanguage("fr");
                    languageSelected = true;
                }
                else if (languageOption == "2")
                { 
                    LangHelper.ChangeLanguage("en");
                    languageSelected = true;
                }
            }

            bool optionSelected = false;
            while (!optionSelected)
            {
                menuView.ClearConsole();
                menuView.PromptMainMenu();
                string menuOption = menuView.PromptMainMenu();
                if (menuOption == "1")
                {
                    menuView.Print("TODO");
                }
                else if (menuOption == "2")
                {
                    BackupWorksEditView backupWorksEditView = new BackupWorksEditView();
                    JsonHelper jsonHelper = new JsonHelper();
                    List<BackupWork>? backupWorks = jsonHelper.ReadBackupWorkFromJson("C:/BackupWorks.json");
                    backupWorksEditView.PromptEditBackupWorks(backupWorks);
                }
            }
        }
    }
}
