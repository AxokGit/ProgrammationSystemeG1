using EasySave_WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace EasySave_WPF.Controllers
{
    class BackupWorksCreateController
    {
        DataHelper dataHelper = new DataHelper();
        FileHelper fileHelper = new FileHelper();
        public void CreateBackupAndSave(BackupWork backupWork)
        {
            string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
            var backupworks = dataHelper.ReadBackupWorksFromJson(filepath_bw_config);

            if (backupworks != null)
            {
                backupworks.Add(backupWork);
            }
            else
            {
                backupworks = new List<BackupWork>
                {
                    backupWork
                };
            }
            dataHelper.WriteBackupWorksToJson(filepath_bw_config, backupworks);
        }
        public void SelectSrcFolderCreateBackupWorkButton_Click(MainWindow mainWindow)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (dialog.SelectedPath != "")
                    mainWindow.SrcFolderBackupworkCreateTextBox.Text = dialog.SelectedPath;
            }
        }

        public void SelectDstFolderCreateBackupWorkButton_Click(MainWindow mainWindow)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (dialog.SelectedPath != "")
                    mainWindow.DstFolderBackupworkCreateTextBox.Text = dialog.SelectedPath;
            }
        }

        public void ButtonClickCreateBackupWork(MainWindow mainWindow, MainController mainController)
        {
            string name = mainWindow.NameBackupworkCreateTextBox.Text;
            string srcFolder = mainWindow.SrcFolderBackupworkCreateTextBox.Text;
            string dstFolder = mainWindow.DstFolderBackupworkCreateTextBox.Text;
            ComboBoxItem typeItem = (ComboBoxItem)mainWindow.TypeBackupworkCreateComboBox.SelectedItem;
            if (name != "" && srcFolder != "" && dstFolder != "" && typeItem != null)
            {
                string type = typeItem.Tag.ToString();

                if (fileHelper.DirectoryExists(srcFolder) && fileHelper.DirectoryExists(dstFolder))
                {
                    CreateBackupAndSave(
                        new BackupWork(name, srcFolder, dstFolder, type)
                    );
                    mainWindow.NameBackupworkCreateTextBox.Text = "";
                    mainWindow.SrcFolderBackupworkCreateTextBox.Text = "";
                    mainWindow.DstFolderBackupworkCreateTextBox.Text = "";
                    mainWindow.TypeBackupworkCreateComboBox.SelectedItem = -1;
                    MessageBox.Show(
                        (string)Application.Current.FindResource("backupwork_added"),
                        (string)Application.Current.FindResource("application_name"),
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                    mainController.UpdateView(mainWindow); // Updating all window
                }
                else
                {
                    MessageBox.Show(
                        (string)Application.Current.FindResource("check_input_folder"),
                        (string)Application.Current.FindResource("application_name"),
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                }
            }
            else
            {
                MessageBox.Show(
                    (string)Application.Current.FindResource("complete_input_correctly"),
                    (string)Application.Current.FindResource("application_name"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}
