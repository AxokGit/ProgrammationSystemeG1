using EasySave_WPF.Controllers;
using System.Windows;
using System.Windows.Controls;

namespace EasySave_WPF
{
    public partial class MainWindow : Window
    {
        MainController mainController;
        BackupWorksCreateController backupWorksCreateController;
        BackupWorksRunController backupWorksRunController;
        BackupWorksEditController backupWorksEditController;
        OpenLogsController openLogsController;
        SettingsController settingsController;
        public MainWindow()
        {
            InitializeComponent();
            mainController = new MainController(this);
            backupWorksCreateController = new BackupWorksCreateController();
            backupWorksRunController = new BackupWorksRunController();
            backupWorksEditController = new BackupWorksEditController();
            openLogsController = new OpenLogsController();
            settingsController = new SettingsController();
        }

        private void BackupWorkListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            backupWorksRunController.BackupWorkListView_SelectionChanged(this);
        }

        private void RunBackupworkButton_Click(object sender, RoutedEventArgs e)
        {
            backupWorksRunController.RunBackupworkButton_Click(this, mainController);
        }

        private void PauseBackupworkButton_Click(object sender, RoutedEventArgs e)
        {
            backupWorksRunController.PauseBackupworkButton_Click(this);
        }
        private void StopBackupworkButton_Click(object sender, RoutedEventArgs e)
        {
            backupWorksRunController.StopBackupworkButton_Click(this);
        }

        private void SelectSrcFolderCreateBackupWorkButton_Click(object sender, RoutedEventArgs e)
        {
            backupWorksCreateController.SelectSrcFolderCreateBackupWorkButton_Click(this);
        }

        private void SelectDstFolderCreateBackupWorkButton_Click(object sender, RoutedEventArgs e)
        {
            backupWorksCreateController.SelectDstFolderCreateBackupWorkButton_Click(this);
        }

        private void ButtonClickCreateBackupWork(object sender, RoutedEventArgs e)
        {
            backupWorksCreateController.ButtonClickCreateBackupWork(this, mainController);
        }

        private void BackupWorksListEditComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            backupWorksEditController.BackupWorksListEditComboBox_SelectionChanged(this);
        }

        private void SelectSrcFolderEditBackupWorkButton_Click(object sender, RoutedEventArgs e)
        {
            backupWorksEditController.SelectSrcFolderEditBackupWorkButton_Click(this);
        }

        private void SelectDstFolderEditBackupWorkButton_Click(object sender, RoutedEventArgs e)
        {
            backupWorksEditController.SelectDstFolderEditBackupWorkButton_Click(this);
        }

        private void SaveBackupWorkEditButton_Click(object sender, RoutedEventArgs e)
        {
            backupWorksEditController.SaveBackupWorkEditButton_Click(this, mainController, backupWorksRunController);
        }

        private void DeleteBackupWorkEditButton_Click(object sender, RoutedEventArgs e)
        {
            backupWorksEditController.DeleteBackupWorkEditButton_Click(this, mainController, backupWorksRunController);
        }

        private void LogListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            openLogsController.LogListView_MouseDoubleClick(this);
        }
        private void InsertFileExtentionEncryptButton_Click(object sender, RoutedEventArgs e)
        {
            settingsController.InsertFileExtentionEncryptButton_Click(this, mainController);
        }

        private void DeleteFileExtentionEncryptButton_Click(object sender, RoutedEventArgs e)
        {
            settingsController.DeleteFileExtentionEncryptButton_Click(this, mainController);
        }

        private void InsertStopProcessButton_Click(object sender, RoutedEventArgs e)
        {
            settingsController.InsertStopProcessButton_Click(this, mainController);
        }

        private void DeleteStopProcessButton_Click(object sender, RoutedEventArgs e)
        {
            settingsController.DeleteStopProcessButton_Click(this, mainController);
        }

        private void InsertPriorityFilesButton_Click(object sender, RoutedEventArgs e)
        {
            settingsController.InsertPriorityFilesButton_Click(this, mainController);
        }

        private void DeletePriorityFilesButton_Click(object sender, RoutedEventArgs e)
        {
            settingsController.DeletePriorityFilesButton_Click(this, mainController);
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            settingsController.SaveSettingsButton_Click(this, mainController);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            base.OnClosing(e);
        }
    }
}
