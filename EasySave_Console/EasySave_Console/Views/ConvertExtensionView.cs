using System;
using EasySave_Console.Models;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Views
{
    class ConvertExtensionView
    {
        TableView tableView = new TableView(110);
        public void PromptConversion()
        {
            Console.WriteLine($"{LangHelper.GetString("choose_log")}");
        }
    }
}
