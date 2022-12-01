using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;

namespace EasySave_Graphique
{
    class LangHelper
    {
        private static ResourceManager _rm = new ResourceManager("EasySave_Graphique.Languages.en", Assembly.GetExecutingAssembly());

        public static string GetString(string name)
        {
            string? value = _rm.GetString(name);
            return value == null ? "" : value;
        }
        public static void ChangeLanguage(string language)
        {
            _rm = new ResourceManager("EasySave_Graphique.Languages." + language, Assembly.GetExecutingAssembly());
        }
    }
}
