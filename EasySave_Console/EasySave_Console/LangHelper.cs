using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Globalization;

namespace EasySave_Console
{
    public static class LangHelper
    {
        private static ResourceManager _rm;

        static LangHelper()
        {
            _rm = new ResourceManager("EasySave_Console.Languages.strings", Assembly.GetExecutingAssembly());
        }

        public static string? GetString(string name)
        {
            return _rm.GetString(name);
        }
        public static void ChangeLanguage(string language)
        {
            var cultureInfo = new CultureInfo(language);

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
        }
    }
}
