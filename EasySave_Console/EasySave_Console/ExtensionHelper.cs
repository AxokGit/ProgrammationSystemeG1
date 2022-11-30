using EasySave_Console.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;

namespace EasySave_Console
{
    class ExtensionHelper
    {
        private static ResourceManager _rm = new ResourceManager("EasySave_Console.Languages.en", Assembly.GetExecutingAssembly());

        public static string GetString(string name)
        {
            string? value = _rm.GetString(name);
            return value == null ? "" : value;
        }
        public static Settings ChangeExtension(string extension)
        {
            Settings settings = new Settings(extension);
            return settings;
            //_rm = new ResourceManager("EasySave_Console.Languages." + extension, Assembly.GetExecutingAssembly());
        }
    }
}
