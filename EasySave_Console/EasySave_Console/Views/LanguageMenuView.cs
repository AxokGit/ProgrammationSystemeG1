using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Views
{
    class LanguageMenuView
    {
        public string PromptLanguageOption()
        {
            Console.WriteLine(LangHelper.GetString("select_language") + " : ");
            Console.WriteLine("1: Français");
            Console.WriteLine("2: English");
            Console.WriteLine();
            Console.WriteLine("0: " + LangHelper.GetString("exit_menu"));
            Console.WriteLine();
            Console.Write("Options (0-2): ");
            return Console.ReadLine();
        }
    }
}
