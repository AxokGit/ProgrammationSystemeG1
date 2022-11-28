using EasySave_Console.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Controllers
{
    class LanguageController
    {
        MenuView menuView = new MenuView();
        LanguageMenuView languageMenuView = new LanguageMenuView();

        public LanguageController()
        {
            bool languageSelected = false;

            while (!languageSelected)
            {
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
        }
        
    }
}
