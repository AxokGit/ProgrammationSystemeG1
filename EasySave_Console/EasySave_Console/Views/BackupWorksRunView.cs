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
                if (type == "complete")
                {
                    type = LangHelper.GetString("type_complete");
                }
                else if (type == "differencial")
                {
                    type = LangHelper.GetString("type_differencial");
                }
                else
                {
                    type = "-";
                }
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
            Console.WriteLine("0: " + LangHelper.GetString("exit_menu"));
            Console.WriteLine();
            Console.Write(LangHelper.GetString("number_or_all") + " (0-5): ");
            return Console.ReadLine();
        }
        public void ErrorMsgEmptyBW()
        {
            Console.WriteLine(LangHelper.GetString("err_empty_bw"));
            Console.Write(LangHelper.GetString("type_enter_to_continue"));
            Console.ReadKey();
        }
        public void CopyMessage(StateLog stateLog, FileModel? file, bool enterToContinue=true)
        {
            Console.WriteLine(LangHelper.GetString("copy_monitor") + " :");
            Console.WriteLine();
            tableView.PrintLine();
            tableView.PrintRow(LangHelper.GetString("backup_work_name"), stateLog.BackupWorkName);
            tableView.PrintLine();
            tableView.PrintRow(LangHelper.GetString("copy_status"), stateLog.Active == true ? LangHelper.GetString("running") : LangHelper.GetString("finished"));
            tableView.PrintLine();
            tableView.PrintRow(LangHelper.GetString("start_time"), stateLog.StartTimestamp);
            tableView.PrintLine();
            tableView.PrintRow(LangHelper.GetString("total_file_number"), stateLog.TotalFiles + " (" + stateLog.TotalSize + " " + LangHelper.GetString("bytes") + ")");
            tableView.PrintLine();
            tableView.PrintRow(LangHelper.GetString("remaining_file_number"), stateLog.RemainingFiles + " (" + stateLog.RemainingSize + " " + LangHelper.GetString("bytes") + ")");
            tableView.PrintLine();
            if (file != null)
            {
                tableView.PrintRow(LangHelper.GetString("current_file_in_copy"), file.Name + " (" + file.Size + " " + LangHelper.GetString("bytes") + ")");
            }
            else
            {
                tableView.PrintRow(LangHelper.GetString("current_file_in_copy"), "-");
            }
            tableView.PrintLine();
            tableView.PrintRow(LangHelper.GetString("src_folder"), stateLog.SrcFolder);
            tableView.PrintLine();
            tableView.PrintRow(LangHelper.GetString("dst_folder"), stateLog.DstFolder);
            tableView.PrintLine();
            if (file == null && enterToContinue)
            {
                Console.Write(LangHelper.GetString("type_enter_to_continue"));
                Console.ReadKey();
            }
        }
        public void MsgAllBackupWorkFinished()
        {
            Console.WriteLine(LangHelper.GetString("all_wb_finished"));
            Console.WriteLine();
            Console.Write(LangHelper.GetString("type_enter_to_continue"));
            Console.ReadKey();
        }
    }
}
