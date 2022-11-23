using EasySave_Console.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Views
{
    class LanguageMenuView
    {
        public LanguageMenuView() { }
        public string PromptLanguageOption()
        {
            Console.WriteLine("Select language:");
            Console.WriteLine("1: Français");
            Console.WriteLine("2: English");
            Console.WriteLine();
            Console.Write("Choose: ");
            return Console.ReadLine();
        }
    }
}
