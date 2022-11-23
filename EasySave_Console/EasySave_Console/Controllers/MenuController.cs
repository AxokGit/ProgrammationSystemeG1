using EasySave_Console.Views;
using System;

namespace EasySave_Console.Controllers
{
    class MenuController
    {
        private MenuView menuView;

        public MenuController()
        {
            menuView = new MenuView();
            LanguageMenuView languageMenuView = new LanguageMenuView();
            menuView.OnBootMessage();
            

            string languageOption = languageMenuView.PromptLanguageOption();
            if (languageOption == "1") { LangHelper.ChangeLanguage("fr"); }
            else if (languageOption == "2") { LangHelper.ChangeLanguage("en"); }
            else
            {
                menuView.ClearConsole();
                menuView.OnBootMessage();
                languageOption = languageMenuView.PromptLanguageOption();
            }

            menuView.ClearConsole();
            menuView.PromptMainMenu();
        }
    }
}
