using System.Resources;
using System.Reflection;

namespace EasySave_Console
{
    public static class LangHelper
    {
        private static ResourceManager _rm = new ResourceManager("EasySave_Console.Languages.en", Assembly.GetExecutingAssembly());
        
        public static string GetString(string name)
        {
            string? value = _rm.GetString(name);
            return value == null ? "" : value;
        }
        public static void ChangeLanguage(string language)
        {
            _rm = new ResourceManager("EasySave_Console.Languages."+language, Assembly.GetExecutingAssembly());
        }
    }
}
