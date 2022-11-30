﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EasySave_Console.Views
{
    class MenuView
    {
        /// <summary>
        /// Method which erase all the previous messages on the console
        /// </summary>
        public void ClearConsole()
        {
            Console.Clear();
        }
        /// <summary>
        /// Method to display the first message that appeared at the start of the application
        /// </summary>
        public void OnBootMessage()
        {
            //Console.WriteLine("|-------------------------------|");
            //Console.WriteLine("|---  EasySave Console Mode  ---|");
            //Console.WriteLine("|-------------------------------|");
            //Console.WriteLine("Version: " + Assembly.GetExecutingAssembly().GetName().Version);
            //Console.WriteLine();

            //Console.WriteLine("███████  █████  ███████ ██    ██ ███████  █████  ██    ██ ███████ ");
            //Console.WriteLine("██      ██   ██ ██       ██  ██  ██      ██   ██ ██    ██ ██      ");
            //Console.WriteLine("█████   ███████ ███████   ████   ███████ ███████ ██    ██ █████   ");
            //Console.WriteLine("██      ██   ██      ██    ██         ██ ██   ██  ██  ██  ██      ");
            //Console.WriteLine("███████ ██   ██ ███████    ██    ███████ ██   ██   ████   ███████ ");
            //Console.WriteLine();

            Console.WriteLine(@"  ______                 _____                 ");
            Console.WriteLine(@" |  ____|               / ____|                ");
            Console.WriteLine(@" | |__   __ _ ___ _   _| (___   __ ___   _____ ");
            Console.WriteLine(@" |  __| / _` / __| | | |\___ \ / _` \ \ / / _ \");
            Console.WriteLine(@" | |___| (_| \__ \ |_| |____) | (_| |\ V /  __/");
            Console.WriteLine(@" |______\__,_|___/\__, |_____/ \__,_| \_/ \___|");
            Console.WriteLine(@"                   __/ |                       ");
            Console.WriteLine(@"                  |___/                        ");
            Console.WriteLine();
            Console.WriteLine("Version: " + Assembly.GetExecutingAssembly().GetName().Version);
            Console.WriteLine();
        }
        /// <summary>
        /// Method whichs display the options the user can choose
        /// </summary>
        /// <returns></returns>
        public string PromptMainMenu()
        {
            Console.WriteLine(LangHelper.GetString("welcome_message"));
            Console.WriteLine();
            Console.WriteLine($"1: {LangHelper.GetString("run_work")}");
            Console.WriteLine($"2: {LangHelper.GetString("define_work")}");
            Console.WriteLine($"3: {LangHelper.GetString("show_log_in_logs_folder")}");
            Console.WriteLine($"4: {LangHelper.GetString("change_language")}");
            Console.WriteLine($"5: {LangHelper.GetString("change_log_extension")}");
            Console.WriteLine($"6: {LangHelper.GetString("open_settings_dir")}");
            Console.WriteLine();
            Console.WriteLine($"0: {LangHelper.GetString("exit_program")}");
            Console.WriteLine();
            Console.Write("Options (0-5): ");
            return Console.ReadLine();
        }
    }
}
