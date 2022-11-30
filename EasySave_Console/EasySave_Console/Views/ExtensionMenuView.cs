using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Views
{
    class ExtensionMenuView
    {
        /// <summary>
        /// Method which displays the language option menu
        /// </summary>
        /// <returns></returns>
        public string PromptExtensionOption()
        {
            Console.WriteLine(LangHelper.GetString("select_extension") + " : ");
            Console.WriteLine("1: .json");
            Console.WriteLine("2: .xml");
            Console.WriteLine();
            Console.WriteLine("0: " + LangHelper.GetString("exit_menu"));
            Console.WriteLine();
            Console.Write("Options (0-2): ");
            return Console.ReadLine();
        }
    }
}
