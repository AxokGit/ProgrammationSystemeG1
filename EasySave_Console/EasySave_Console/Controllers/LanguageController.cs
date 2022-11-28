using EasySave_Console.Models;
using EasySave_Console.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EasySave_Console.Controllers
{
    class LanguageController
    {
        MenuView menuView = new MenuView();
        LanguageMenuView languageMenuView = new LanguageMenuView();
        JsonHelper jsonHelper = new JsonHelper();
        FileHelper fileHelper = new FileHelper();

        public LanguageController()
        {
            string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);

            if(fileHelper.FileExists(filepath_settings))
            {
                Settings settings = jsonHelper.ReadSettingsFromJson(filepath_settings);

                if (settings.AvailableLanguage.Contains(settings.Language))
                {
                    LangHelper.ChangeLanguage(settings.Language);
                }
                else
                {
                    LangHelper.ChangeLanguage("en");
                }
            }
            else
            {
                LangHelper.ChangeLanguage("en");
                SelectLanguage();
            }
        }

        public void SelectLanguage()
        {
            string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);

            bool languageSelected = false;

            while (!languageSelected)
            {
                menuView.ClearConsole();
                string languageOption = languageMenuView.PromptLanguageOption();
                if (languageOption == "1")
                {
                    Settings settings = new Settings("fr");
                    jsonHelper.WriteSettingsToJson(filepath_settings, settings);
                    LangHelper.ChangeLanguage(settings.Language);
                    languageSelected = true;
                }
                else if (languageOption == "2")
                {
                    Settings settings = new Settings("en");
                    jsonHelper.WriteSettingsToJson(filepath_settings, settings);
                    LangHelper.ChangeLanguage(settings.Language);
                    languageSelected = true;
                }
            }
        }
        
    }
}
