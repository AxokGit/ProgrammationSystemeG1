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
    class BackupWorksEditController
    {
        DataHelper dataHelper = new DataHelper();
        FileHelper fileHelper = new FileHelper();

        public void BackupWorksListEditComboBox_SelectionChanged(MainWindow mainWindow)
        {
            int index = mainWindow.BackupWorksListEditComboBox.SelectedIndex;

            if (index == -1)
            {
                mainWindow.NameBackupWorkEditTextBox.Text = "";
                mainWindow.SrcFolderBackupWorkEditTextBox.Text = "";
                mainWindow.DstFolderBackupWorkEditTextBox.Text = "";
            }
            else
            {
                string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
                List<BackupWork> backupworks = dataHelper.ReadBackupWorksFromJson(filepath_bw_config);

                mainWindow.NameBackupWorkEditTextBox.IsEnabled = true;
                mainWindow.NameBackupWorkEditTextBox.Text = backupworks[index].Name;
                mainWindow.SrcFolderBackupWorkEditTextBox.IsEnabled = true;
                mainWindow.SrcFolderBackupWorkEditTextBox.Text = backupworks[index].SrcFolder;
                mainWindow.DstFolderBackupWorkEditTextBox.IsEnabled = true;
                mainWindow.DstFolderBackupWorkEditTextBox.Text = backupworks[index].DstFolder;

                mainWindow.TypeBackupWorkEditComboBox.IsEnabled = true;
                if (backupworks[index].Type == "complete")
                    mainWindow.TypeBackupWorkEditComboBox.SelectedIndex = 0;
                else if (backupworks[index].Type == "differencial")
                    mainWindow.TypeBackupWorkEditComboBox.SelectedIndex = 1;


                mainWindow.SelectSrcFolderEditBackupWorkButton.IsEnabled = true;
                mainWindow.SelectDstFolderEditBackupWorkButton.IsEnabled = true;
                mainWindow.SaveBackupWorkEditButton.IsEnabled = true;
                mainWindow.DeleteBackupWorkEditButton.IsEnabled = true;
            }
        }

        public void SelectSrcFolderEditBackupWorkButton_Click(MainWindow mainWindow)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (dialog.SelectedPath != "")
                    mainWindow.SrcFolderBackupWorkEditTextBox.Text = dialog.SelectedPath;
            }
        }

        public void SelectDstFolderEditBackupWorkButton_Click(MainWindow mainWindow)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (dialog.SelectedPath != "")
                    mainWindow.DstFolderBackupWorkEditTextBox.Text = dialog.SelectedPath;
            }
        }

        public void SaveBackupWorkEditButton_Click(MainWindow mainWindow, MainController mainController, BackupWorksRunController backupWorksRunController)
        {
            string name = mainWindow.NameBackupWorkEditTextBox.Text;
            string srcFolder = mainWindow.SrcFolderBackupWorkEditTextBox.Text;
            string dstFolder = mainWindow.DstFolderBackupWorkEditTextBox.Text;
            ComboBoxItem typeItem = (ComboBoxItem)mainWindow.TypeBackupWorkEditComboBox.SelectedItem;
            if (name != "" && srcFolder != "" && dstFolder != "" && typeItem != null)
            {
                string type = typeItem.Tag.ToString();

                if (fileHelper.DirectoryExists(srcFolder) && fileHelper.DirectoryExists(dstFolder))
                {
                    string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
                    List<BackupWork> backupworks = dataHelper.ReadBackupWorksFromJson(filepath_bw_config);

                    int index = mainWindow.BackupWorksListEditComboBox.SelectedIndex;

                    backupworks[index] = new BackupWork(name, srcFolder, dstFolder, type);

                    dataHelper.WriteBackupWorksToJson(filepath_bw_config, backupworks);
                    mainWindow.BackupWorksListEditComboBox.ItemsSource = backupWorksRunController.GetBackupWorksName();
                    mainWindow.BackupWorksListEditComboBox.SelectedIndex = -1;

                    mainWindow.NameBackupWorkEditTextBox.IsEnabled = false;
                    mainWindow.SrcFolderBackupWorkEditTextBox.IsEnabled = false;
                    mainWindow.DstFolderBackupWorkEditTextBox.IsEnabled = false;
                    mainWindow.TypeBackupWorkEditComboBox.IsEnabled = false;
                    mainWindow.SelectSrcFolderEditBackupWorkButton.IsEnabled = false;
                    mainWindow.SelectDstFolderEditBackupWorkButton.IsEnabled = false;
                    mainWindow.SaveBackupWorkEditButton.IsEnabled = false;
                    mainWindow.DeleteBackupWorkEditButton.IsEnabled = false;


                    MessageBox.Show(
                        (string)Application.Current.FindResource("backupwork_edited"),
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

        public void DeleteBackupWorkEditButton_Click(MainWindow mainWindow, MainController mainController, BackupWorksRunController backupWorksRunController)
        {
            int index = mainWindow.BackupWorksListEditComboBox.SelectedIndex;

            string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
            List<BackupWork> backupworks = dataHelper.ReadBackupWorksFromJson(filepath_bw_config);

            string typeBackupwork;
            if (mainWindow.TypeBackupWorkEditComboBox.SelectedIndex == 0)
                typeBackupwork = "complete";
            else
                typeBackupwork = "differencial";

            backupworks.RemoveAt(index);

            dataHelper.WriteBackupWorksToJson(filepath_bw_config, backupworks);
            mainWindow.BackupWorksListEditComboBox.ItemsSource = backupWorksRunController.GetBackupWorksName();
            mainWindow.BackupWorksListEditComboBox.SelectedIndex = -1;

            mainWindow.NameBackupWorkEditTextBox.IsEnabled = false;
            mainWindow.SrcFolderBackupWorkEditTextBox.IsEnabled = false;
            mainWindow.DstFolderBackupWorkEditTextBox.IsEnabled = false;
            mainWindow.TypeBackupWorkEditComboBox.IsEnabled = false;
            mainWindow.SelectSrcFolderEditBackupWorkButton.IsEnabled = false;
            mainWindow.SelectDstFolderEditBackupWorkButton.IsEnabled = false;
            mainWindow.SaveBackupWorkEditButton.IsEnabled = false;
            mainWindow.DeleteBackupWorkEditButton.IsEnabled = false;

            MessageBox.Show(
                (string)Application.Current.FindResource("backupwork_deleted"),
                (string)Application.Current.FindResource("application_name"),
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
            mainController.UpdateView(mainWindow); // Updating all window
        }
    }
}
