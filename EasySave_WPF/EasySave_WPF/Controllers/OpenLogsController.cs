﻿using EasySave_WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_WPF.Controllers
{

    class OpenLogsController
    {
        FileHelper fileHelper = new FileHelper();
        DataHelper dataHelper = new DataHelper();

        public void RefreshLogListView_ButtonClick(MainWindow mainWindow, MainController mainController)
        {
            mainController.UpdateView(mainWindow);
        }

        public void LogListView_MouseDoubleClick(MainWindow mainWindow)
        {
            FileModel item = (FileModel)mainWindow.LogListView.SelectedItem;
            if (item != null)
            {
                fileHelper.OpenFile(item);
            }
        }

        public List<FileModel> GetLogs()
        {
            string filepath_log = fileHelper.FormatFilePath(fileHelper.filepath_log);
            string dirpath_log = fileHelper.GetDirectoryName(filepath_log);
            if (!fileHelper.DirectoryExists(dirpath_log))
                fileHelper.CreateDirectory(dirpath_log, "");
            return fileHelper.GetAllFile(dirpath_log);
        }
        public List<string> GetLogName()
        {
            string filepath_log = fileHelper.FormatFilePath(fileHelper.filepath_log);

            List<string> logsname = new List<string>();
            var logs = dataHelper.ReadLog(filepath_log);
            foreach (Log log in logs)
            {
                logsname.Add(log.BackupWorkName);
            }
            return logsname;
        }
    }
}
