using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EasySave_Console.Views
{
    class MenuView
    {
        public MenuView() { }
        public void ClearConsole()
        {
            Console.Clear();
        }
        public void OnBootMessage()
        {
            Console.WriteLine("|-------------------------------|");
            Console.WriteLine("|---  EasySave Console Mode  ---|");
            Console.WriteLine("|-------------------------------|");
            Console.WriteLine("Version: " + Assembly.GetExecutingAssembly().GetName().Version);
            Console.WriteLine();
        }
        public string PromptMainMenu()
        {
            Console.WriteLine(LangHelper.GetString("welcome_message"));
            Console.WriteLine();
            Console.WriteLine($"1: {LangHelper.GetString("run_work")}");
            Console.WriteLine($"2: {LangHelper.GetString("define_work")}");
            Console.WriteLine($"3: {LangHelper.GetString("open_log_folder")}");
            Console.WriteLine($"4: {LangHelper.GetString("change_language")}");
            Console.WriteLine($"5: {LangHelper.GetString("exit_program")}");
            Console.WriteLine();
            Console.Write("Options (1-5): ");
            return Console.ReadLine();
        }

        public void Print(string message)
        {
            Console.WriteLine(message);
        }

        public void Wait()
        {
            Console.ReadKey();
        }
    }
}
