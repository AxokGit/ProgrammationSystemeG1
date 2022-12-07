using EasySave_WPF.Models;
using EasySave_WPF.Views;
using System;
using System.Linq;
using System.Threading;

namespace EasySave_WPF.Controllers
{
    class LanguageController
    {
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
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(settings.Language);
                }
                else
                {
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                }
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                SelectLanguage();
            }
        }

        // Method that ask to user to select language
        public void SelectLanguage()
        {
            new SelectLanguage(new Settings());
            
            /*string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);

            bool languageSelected = false;

            // While user do not select correct choice
            while (!languageSelected)
            {
                string languageOption = "1"; //languageMenuView.PromptLanguageOption();
                if (languageOption == "0") // Go back
                {
                    languageSelected = true;
                }
                else if (languageOption == "1") // Change language to french
                {
                    Settings settings = new Settings(language: "fr") ; // Creating set
                    jsonHelper.WriteSettingsToJson(filepath_settings, settings);
                    //LangHelper.ChangeLanguage(settings.Language); //TODO
                    languageSelected = true;
                }
                else if (languageOption == "2") // Change language to english
                {
                    Settings settings = new Settings(language: "en");
                    jsonHelper.WriteSettingsToJson(filepath_settings, settings);
                    //LangHelper.ChangeLanguage(settings.Language); //TODO
                    languageSelected = true;
                }
            }*/
        }
    }
}
