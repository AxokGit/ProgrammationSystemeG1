using EasySave_Console.Models;
using System;
using System.Collections.Generic;

namespace EasySave_Console.Views
{
    class OpenLogsView
    {
        TableView tableView = new TableView(110);
        public string PromptLogFiles(List<FileModel> fileModels)
        {
            Console.WriteLine($"{LangHelper.GetString("choose_log")}");
            tableView.PrintLine();
            tableView.PrintRow(LangHelper.GetString("number"), "File name");
            tableView.PrintLine();
            for (int i = 0; i < fileModels.Count; i++)
            {
                tableView.PrintRow(i + 1 +"", fileModels[i].Name);
            }
            tableView.PrintLine();
            Console.WriteLine();
            Console.WriteLine("0: " + LangHelper.GetString("exit_menu"));
            Console.WriteLine();
            Console.Write(LangHelper.GetString("number") + " (0-" + fileModels.Count + ") : ");
            return Console.ReadLine();
        }
        public void FileOpened()
        {
            Console.WriteLine();
            Console.WriteLine(LangHelper.GetString("file_opened"));
            Console.WriteLine(LangHelper.GetString("type_enter_to_continue"));
            Console.ReadKey();
        }
        public void ErrorMsgNoLogs()
        {
            Console.WriteLine(LangHelper.GetString("error_msg_no_logs"));
            Console.WriteLine(LangHelper.GetString("type_enter_to_continue"));
            Console.ReadKey();
        }
    }
}
