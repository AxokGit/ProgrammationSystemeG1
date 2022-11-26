using EasySave_Console.Models;
using EasySave_Console.Views;
using System;

namespace EasySave_Console.Controllers
{
    class MenuController
    {
        MenuView menuView = new MenuView();
        public MenuController()
        {
            new LanguageController();

            bool optionSelected = false;
            while (!optionSelected)
            {
                menuView.ClearConsole();
                string menuOption = menuView.PromptMainMenu();
                if (menuOption == "1")
                {
                    new BackupWorksRunController();
                }
                else if (menuOption == "2")
                {
                    new BackupWorksEditController();
                }
                else if (menuOption == "3")
                {
                    menuView.Print("TODO");
                    menuView.Wait();
                }
                else if (menuOption == "4")
                {
                    new LanguageController();
                }
                else if (menuOption == "5")
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
