using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Models
{
    class Settings
    {
        public string Language;
        public string[] AvailableLanguage = { "en", "fr" };

        public Settings(string language)
        {
            this.Language = language;
        }
    }
}
