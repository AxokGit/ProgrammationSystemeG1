using EasySave_Console.Models;
using EasySave_Console.Views;
using System;
using System.Linq;

namespace EasySave_Console.Controllers
{
    class LanguageController
    {
        MenuView menuView = new MenuView(); // Instantiation of the main view
        LanguageMenuView languageMenuView = new LanguageMenuView(); // Instantiation of the language view
        DataHelper jsonHelper = new DataHelper(); // Instantiation of the json helper
        FileHelper fileHelper = new FileHelper(); // Instantiation of the file helper

        public LanguageController()
        {
            
            string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);

            // Checking if Settings.json exists
            // If yes, reading language configured
            // If not, askip to select language
            if(fileHelper.FileExists(filepath_settings))
            {
                Settings settings = jsonHelper.ReadSettingsFromJson(filepath_settings);

                // Checking if language configured is existing
                // If yes, using it
                // If not, default lang = en
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

        // Method that ask to user to select language
        public void SelectLanguage()
        {
            string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);

            bool languageSelected = false;

            // While user do not select correct choice
            while (!languageSelected)
            {
                menuView.ClearConsole();
                string languageOption = languageMenuView.PromptLanguageOption();
                if (languageOption == "0") // Go back
                {
                    languageSelected = true;
                }
                else if (languageOption == "1") // Change language to french
                {
                    Settings settings = new Settings("fr"); // Creating set
                    jsonHelper.WriteSettingsToJson(filepath_settings, settings);
                    LangHelper.ChangeLanguage(settings.Language);
                    languageSelected = true;
                }
                else if (languageOption == "2") // Change language to english
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
