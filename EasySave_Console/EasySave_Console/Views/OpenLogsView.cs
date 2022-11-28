using EasySave_Console.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Views
{
    class OpenLogsView
    {
        public string DiplayLogChoice(List<FileModel> fileModels)
        {
            Console.WriteLine($"{LangHelper.GetString("choose_log")}");
            for (int i = 0; i < fileModels.Count; i++)
            {
                Console.WriteLine(i+1 + " - " + fileModels[i].Name);
            }
            Console.WriteLine(LangHelper.GetString("number"));
            return Console.ReadLine();
        }
    }
}
