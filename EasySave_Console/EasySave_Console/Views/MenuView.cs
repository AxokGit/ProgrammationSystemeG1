using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EasySave_Console.Views
{
    class MenuView
    {
        public MenuView() { }
        public void onBootMessage()
        {
            Console.WriteLine("|-------------------------------|");
            Console.WriteLine("|---  EasySave Console Mode  ---|");
            Console.WriteLine("|-------------------------------|");
            Console.WriteLine("Version: " + Assembly.GetExecutingAssembly().GetName().Version);
            Console.WriteLine();
        }

        public void clearConsole()
        {
            Console.Clear();
        }
    }
}
