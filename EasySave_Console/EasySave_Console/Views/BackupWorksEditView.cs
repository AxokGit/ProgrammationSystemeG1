﻿using EasySave_Console.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Views
{
    class BackupWorksEditView
    {
        TableView tableView = new TableView(110);

        public string PromptEditBackupWorks(List<BackupWork> backupWorks)
        {
            Console.WriteLine(LangHelper.GetString("backups_work_edit_menu"));
            Console.WriteLine(LangHelper.GetString("choose_backup_work_to_edit") + ":");

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
                string name = backupWorks[i].Name ?? "NONE";
                string src_folder = backupWorks[i].Src_folder ?? "NONE";
                string dst_folder = backupWorks[i].Dst_folder ?? "NONE";
                string type = backupWorks[i].Type ?? "NONE";

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
            Console.Write(LangHelper.GetString("number") +" (1-6): ");
            return Console.ReadLine();
        }

        public string PromptEditBackupWorksName(BackupWork backupWork)
        {
            Console.WriteLine(LangHelper.GetString("actual_name_backup_work"));
            Console.Write(LangHelper.GetString("name") + ": ");
            return Console.ReadLine();
        }
        public string PromptEditBackupWorksSrcFolder(BackupWork backupWork)
        {
            Console.WriteLine(LangHelper.GetString("actual_src_folder_backup_work"));
            Console.Write(LangHelper.GetString("folder") + ": ");
            return Console.ReadLine();
        }
        public string PromptEditBackupWorksDstFolder(BackupWork backupWork)
        {
            Console.WriteLine(LangHelper.GetString("actual_dst_folder_backup_work"));
            Console.Write(LangHelper.GetString("folder") + ": ");
            return Console.ReadLine();
        }
        public string PromptEditBackupWorksType(BackupWork backupWork)
        {
            Console.WriteLine(LangHelper.GetString("actual_type_backup_work"));
            Console.Write(LangHelper.GetString("type") + ": ");
            return Console.ReadLine();
        }
    }
}