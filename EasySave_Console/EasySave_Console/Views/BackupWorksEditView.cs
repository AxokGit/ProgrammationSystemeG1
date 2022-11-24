using EasySave_Console.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Views
{
    class BackupWorksEditView
    {
        public string PromptEditBackupWorks(List<BackupWork> backupWorks)
        {
            Console.WriteLine(LangHelper.GetString("backups_work_edit_menu") + ":");

            for (int i = 0; i < backupWorks.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {(backupWorks[i].Name == null ? "NONE" : backupWorks[i].Name)}");
            }
            Console.WriteLine("\n");
            Console.Write("Options (1-5):");
            return Console.ReadLine();
        }
    }
}