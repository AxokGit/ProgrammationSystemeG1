using System.Resources;
using System.Reflection;

namespace EasySave_Console
{
    public static class LangHelper
    {
        private static ResourceManager _rm;

        public static string? GetString(string name)
        {
            return _rm.GetString(name);
        }

        public static void ChangeLanguage(string language)
        {
            _rm = new ResourceManager("EasySave_Console.Languages."+language, Assembly.GetExecutingAssembly());
        }
    }
}
