using EasySave_WPF.Models;
using EasySave_WPF.Views;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace EasySave_WPF.Controllers
{
    class MainController
    {
        public static bool StopProcess { get; set; }
        public static bool Paused { get; set; }
        BackupWorksRunController backupWorksRunController = new BackupWorksRunController();
        BackupWorksCreateController backupWorksCreateController = new BackupWorksCreateController();
        OpenLogsController openLogsController = new OpenLogsController();
        DataHelper dataHelper = new DataHelper();
        FileHelper fileHelper = new FileHelper();

        public MainController(MainWindow mainWindow)
        {
            new LanguageController().CheckLanguageConfig();

            Thread stopProcessThread = new Thread(() => new StopProcessController(mainWindow));
            Thread socketThread = new Thread(() => new SocketController(mainWindow, this));
            stopProcessThread.Start();
            socketThread.Start();

            UpdateView(mainWindow);
        }

        public void UpdateView(MainWindow mainWindow)
        {
            //Run
            mainWindow.BackupWorkRunListView.ItemsSource = backupWorksRunController.GetBackupWorks();

            //Edit
            mainWindow.BackupWorksListEditComboBox.ItemsSource = backupWorksRunController.GetBackupWorksName();

            //Logs
            mainWindow.LogListView.ItemsSource = openLogsController.GetLogs();

            //Settings
            string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);
            var settings = dataHelper.ReadSettingsFromJson(filepath_settings);

            if (settings.Language == "en")
                mainWindow.LanguageSettingsComboBox.SelectedIndex = 0;
            if (settings.Language == "fr")
                mainWindow.LanguageSettingsComboBox.SelectedIndex = 1;

            if (settings.LogExtension == ".json")
                mainWindow.LogFormatSettingsComboBox.SelectedIndex = 0;
            if (settings.LogExtension == ".xml")
                mainWindow.LogFormatSettingsComboBox.SelectedIndex = 1;

            mainWindow.XorKeyTextBox.Text = settings.XorKey;
            mainWindow.FileExtentionEncryptListBox.ItemsSource = settings.ExtentionFileToEncrypt;
            mainWindow.StopProcessListBox.ItemsSource = settings.StopProcesses;
            mainWindow.PriorityFilesListBox.ItemsSource = settings.PriorityFiles;
        }

        public void UpdateProgression(double progression, MainWindow mainWindow)
        {
            mainWindow.BackupWorkProgressBar.Value = progression;
        }

        public void UpdateProgressionStatusLabel(string message, MainWindow mainWindow)
        {
            mainWindow. ProgressionStatusLabel.Content = (string)Application.Current.FindResource("status_backupwork") + message;
        }
    }
}
