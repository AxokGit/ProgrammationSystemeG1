using EasySave_WPF.Models;
using EasySave_WPF.Views;
using System;
using System.Linq;
using System.Threading;

namespace EasySave_WPF.Controllers
{
    class LanguageController
    {
        DataHelper dataHelper = new DataHelper(); // Instantiation of the json helper
        FileHelper fileHelper = new FileHelper(); // Instantiation of the file helper

        public void CheckLanguageConfig() {             
            string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);
           
            // Checking if Settings.json exists
            // If yes, reading language configured
            // If not, askip to select language
            if(fileHelper.FileExists(filepath_settings))
            {
                Settings settings = dataHelper.ReadSettingsFromJson(filepath_settings);

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
            new SelectLanguage(new Settings()).ShowDialog();
        }

        public void DefineLanguage(string language)
        {
            string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);

            if (language == "en")
            {
                Settings settings = new Settings(language: "en");
                dataHelper.WriteSettingsToJson(filepath_settings, settings);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            }
            else if (language == "fr")
            {
                Settings settings = new Settings(language: "fr");
                dataHelper.WriteSettingsToJson(filepath_settings, settings);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr");
            }
            
        }
    }
}
