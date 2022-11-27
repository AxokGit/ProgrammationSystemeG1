using EasySave_Console.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Views
{
    class BackupWorksRunView
    {
        TableView tableView = new TableView(110);

        public string PromptRunBackupWorks(List<BackupWork> backupWorks)
        {
            Console.WriteLine(LangHelper.GetString("backups_work_run_menu"));
            Console.WriteLine(LangHelper.GetString("choose_backup_work_to_run") + ":");

            tableView.PrintLine();
            tableView.PrintRow(
                LangHelper.GetString("number"),
                LangHelper.GetString("name"),
                LangHelper.GetString("src_folder"),
                LangHelper.GetString("dst_folder"),
                LangHelper.GetString("type")
                );
            tableView.PrintLine();
            tableView.PrintRow("", "", "", "", "");

            for (int i = 0; i < backupWorks.Count; i++)
            {
                string name = backupWorks[i].Name ?? "-";
                string src_folder = backupWorks[i].SrcFolder ?? "-";
                string dst_folder = backupWorks[i].DstFolder ?? "-";
                string type = backupWorks[i].Type ?? "-";

                tableView.PrintRow(
                    Convert.ToString(i + 1),
                    name,
                    src_folder,
                    dst_folder,
                    type
                    );
                tableView.PrintRow("", "", "", "", "");
            }
            tableView.PrintLine();
            Console.WriteLine();
            Console.WriteLine("6:" + LangHelper.GetString("exit_menu"));
            Console.WriteLine();
            Console.Write(LangHelper.GetString("number_or_all") + " (1-6): ");
            return Console.ReadLine();
        }
        public void ErrorMsgEmptyBW()
        {
            Console.WriteLine(LangHelper.GetString("err_empty_bw"));
            Console.Write(LangHelper.GetString("type_enter_to_continue"));
            Console.ReadKey();
        }

        public void CopyMessage(StateLog stateLog, string? filename)
        {
            Console.WriteLine(LangHelper.GetString("copy_monitor") + " :");
            Console.WriteLine();
            Console.WriteLine(LangHelper.GetString("backup_work_name") + " : " + stateLog.BackupWorkName);
            Console.WriteLine(LangHelper.GetString("copy_status") + " : " + (stateLog.Active == true ? LangHelper.GetString("finished") : LangHelper.GetString("running")));

            Console.WriteLine(filename + " - " + LangHelper.GetString("starting_copy"));
        }
    }
}
