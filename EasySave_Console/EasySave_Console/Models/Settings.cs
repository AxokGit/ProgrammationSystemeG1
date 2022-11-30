﻿using System;
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
        public string LogExtension;
        public string[] AvailableLogExtension = { ".json", ".xml" };
        /// <summary>
        /// Constructor of the Settings class
        /// </summary>
        /// <param name="language"></param>
        public Settings(string language, string logExtension)
        {
            this.Language = language;
            this.LogExtension =logExtension;
        }
    }
}
