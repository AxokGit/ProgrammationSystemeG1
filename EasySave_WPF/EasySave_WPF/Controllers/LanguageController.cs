using EasySave_WPF.Models;
using EasySave_WPF.Views;
using System;
using System.Linq;
using System.Threading;
using System.Windows;

namespace EasySave_WPF.Controllers
{
    class LanguageController
    {
        DataHelper dataHelper = new DataHelper(); // Instantiation of the json helper
        FileHelper fileHelper = new FileHelper(); // Instantiation of the file helper
        ResourceDictionary dictionary = new ResourceDictionary();

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
                    dictionary.Source = new Uri("../../Languages/"+ settings.Language + ".xaml", UriKind.Relative);
                    Application.Current.Resources.MergedDictionaries.Add(dictionary);
                }
                else
                {
                    dictionary.Source = new Uri("../../Languages/en.xaml", UriKind.Relative);
                    Application.Current.Resources.MergedDictionaries.Add(dictionary);
                }
            }
            else
            {
                dictionary.Source = new Uri("../../Languages/en.xaml", UriKind.Relative);
                Application.Current.Resources.MergedDictionaries.Add(dictionary);
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
                dictionary.Source = new Uri("../../Languages/en.xaml", UriKind.Relative);
                Application.Current.Resources.MergedDictionaries.Add(dictionary);
            }
            else if (language == "fr")
            {
                Settings settings = new Settings(language: "fr");
                dataHelper.WriteSettingsToJson(filepath_settings, settings);
                dictionary.Source = new Uri("../../Languages/fr.xaml", UriKind.Relative);
                Application.Current.Resources.MergedDictionaries.Add(dictionary);
            }
        }
    }
}
