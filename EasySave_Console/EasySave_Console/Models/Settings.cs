using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Models
{
    /// <summary>
    /// Class meant to setup the settings for the language change
    /// </summary>
    class Settings
    {
        public string Language;
        public string[] AvailableLanguage = { "en", "fr" };
        /// <summary>
        /// Constructor of the Settings class
        /// </summary>
        /// <param name="language"></param>
        public Settings(string language)
        {
            this.Language = language;
        }
    }
}
