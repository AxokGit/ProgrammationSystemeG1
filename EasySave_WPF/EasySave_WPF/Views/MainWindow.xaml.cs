﻿using EasySave_WPF.Controllers;
using EasySave_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace EasySave_WPF
{
    public partial class MainWindow : Window
    {
        BackupWorksRunController backupWorksRunController = new BackupWorksRunController();
        BackupWorksCreateController backupWorksCreateController = new BackupWorksCreateController();
        OpenLogsController openLogsController = new OpenLogsController();
        DataHelper dataHelper = new DataHelper();
        FileHelper fileHelper = new FileHelper();
        public MainWindow()
        {
            InitializeComponent();
            new MainController();
            UpdateView(); // Updating all window
        }

        private void UpdateView()
        {
            //Run
            BackupWorkRunListView.ItemsSource = backupWorksRunController.GetBackupWorks();

            //Edit
            BackupWorksListEditComboBox.ItemsSource = backupWorksRunController.GetBackupWorksName();

            //Logs
            LogListView.ItemsSource = openLogsController.GetLogs();

            //Settings
            string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);
            var settings = dataHelper.ReadSettingsFromJson(filepath_settings);

            if (settings.Language == "en")
                LanguageSettingsComboBox.SelectedIndex = 0;
            if (settings.Language == "fr")
                LanguageSettingsComboBox.SelectedIndex = 1;

            if (settings.LogExtension == ".json")
                LogFormatSettingsComboBox.SelectedIndex = 0;
            if (settings.LogExtension == ".xml")
                LogFormatSettingsComboBox.SelectedIndex = 1;

            XorKeyTextBox.Text = settings.XorKey;
            FileExtentionEncryptListBox.ItemsSource = settings.ExtentionFileToEncrypt;
        }

        private void RunBackupworkButton_Click(object sender, RoutedEventArgs e)
        {
            var items = BackupWorkRunListView.SelectedItems;
            foreach (BackupWork backupWork in items)
            {
                backupWorksRunController.RunCopy(backupWork);
            }
        }

        private void ButtonClickCreateBackupWork(object sender, RoutedEventArgs e)
        {
            string name = NameBackupworkCreateTextBox.Text;
            string srcFolder = SrcFolderBackupworkCreateTextBox.Text;
            string dstFolder = DstFolderBackupworkCreateTextBox.Text;
            ComboBoxItem typeItem = (ComboBoxItem)TypeBackupworkCreateComboBox.SelectedItem;
            if (name != "" && srcFolder != "" && dstFolder != "" && typeItem != null)
            {
                string type = typeItem.Tag.ToString();

                if (fileHelper.DirectoryExists(srcFolder) && fileHelper.DirectoryExists(dstFolder))
                {
                    backupWorksCreateController.CreateBackupAndSave(
                        new BackupWork(name, srcFolder, dstFolder, type)
                    );
                    NameBackupworkCreateTextBox.Text = "";
                    SrcFolderBackupworkCreateTextBox.Text = "";
                    DstFolderBackupworkCreateTextBox.Text = "";
                    TypeBackupworkCreateComboBox.SelectedItem = -1;
                    MessageBox.Show((string)Application.Current.FindResource("backupwork_added"));
                    UpdateView(); // Updating all window
                }
                else
                {
                    MessageBox.Show((string)Application.Current.FindResource("check_input_folder"));
                }
            }
            else
            {
                MessageBox.Show((string)Application.Current.FindResource("complete_input_correctly"));
            }
        }
        private void SaveBackupWorkEditButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameBackupWorkEditTextBox.Text;
            string srcFolder = SrcFolderBackupWorkEditTextBox.Text;
            string dstFolder = DstFolderBackupWorkEditTextBox.Text;
            ComboBoxItem typeItem = (ComboBoxItem)TypeBackupWorkEditComboBox.SelectedItem;
            if (name != "" && srcFolder != "" && dstFolder != "" && typeItem != null)
            {
                string type = typeItem.Tag.ToString();

                if (fileHelper.DirectoryExists(srcFolder) && fileHelper.DirectoryExists(dstFolder))
                {
                    string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
                    List<BackupWork> backupworks = dataHelper.ReadBackupWorkFromJson(filepath_bw_config);

                    int index = BackupWorksListEditComboBox.SelectedIndex;

                    backupworks[index] = new BackupWork(name, srcFolder, dstFolder, type);

                    dataHelper.WriteBackupWorkToJson(filepath_bw_config, backupworks);
                    BackupWorksListEditComboBox.ItemsSource = backupWorksRunController.GetBackupWorksName();
                    BackupWorksListEditComboBox.SelectedIndex = -1;
                    MessageBox.Show((string)Application.Current.FindResource("backupwork_edited"));
                    UpdateView(); // Updating all window
                }
                else
                {
                    MessageBox.Show((string)Application.Current.FindResource("check_input_folder"));
                }
            }
            else
            {
                MessageBox.Show((string)Application.Current.FindResource("complete_input_correctly"));
            }
        }
        private void DeleteBackupWorkEditButton_Click(object sender, RoutedEventArgs e)
        {
            int index = BackupWorksListEditComboBox.SelectedIndex;

            string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
            List<BackupWork> backupworks = dataHelper.ReadBackupWorkFromJson(filepath_bw_config);

            string typeBackupwork;
            if (TypeBackupWorkEditComboBox.SelectedIndex == 0)
                typeBackupwork = "complete";
            else
                typeBackupwork = "differencial";

            backupworks.RemoveAt(index);

            dataHelper.WriteBackupWorkToJson(filepath_bw_config, backupworks);
            BackupWorksListEditComboBox.ItemsSource = backupWorksRunController.GetBackupWorksName();
            BackupWorksListEditComboBox.SelectedIndex = -1;
            MessageBox.Show((string)Application.Current.FindResource("backupwork_deleted"));
            UpdateView(); // Updating all window
        }

        private void BackupWorksListEditComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = BackupWorksListEditComboBox.SelectedIndex;

            if (index == -1)
            {
                NameBackupWorkEditTextBox.Text = "";
                SrcFolderBackupWorkEditTextBox.Text = "";
                DstFolderBackupWorkEditTextBox.Text = "";
            }
            else
            {
                string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
                List<BackupWork> backupworks = dataHelper.ReadBackupWorkFromJson(filepath_bw_config);

                NameBackupWorkEditTextBox.IsEnabled = true;
                NameBackupWorkEditTextBox.Text = backupworks[index].Name;
                SrcFolderBackupWorkEditTextBox.IsEnabled = true;
                SrcFolderBackupWorkEditTextBox.Text = backupworks[index].SrcFolder;
                DstFolderBackupWorkEditTextBox.IsEnabled = true;
                DstFolderBackupWorkEditTextBox.Text = backupworks[index].DstFolder;

                TypeBackupWorkEditComboBox.IsEnabled = true;
                if (backupworks[index].Type == "complete")
                    TypeBackupWorkEditComboBox.SelectedIndex = 0;
                else if (backupworks[index].Type == "differencial")
                    TypeBackupWorkEditComboBox.SelectedIndex = 1;


                SaveBackupWorkEditButton.IsEnabled = true;
                DeleteBackupWorkEditButton.IsEnabled = true;
            }
        }
        private void LogListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FileModel item = (FileModel)LogListView.SelectedItem;
            if (item != null)
            {
                fileHelper.OpenFile(item);
            }
        }
        private void InsertFileExtentionEncryptButton_Click(object sender, RoutedEventArgs e)
        {
            string text = FileExtentionEncryptTextBox.Text;
            if (text != "")
            {
                string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);

                Settings settings = dataHelper.ReadSettingsFromJson(filepath_settings);

                if (text.StartsWith("."))
                    settings.ExtentionFileToEncrypt.Add(text);
                else
                    settings.ExtentionFileToEncrypt.Add("." + text);
                dataHelper.WriteSettingsToJson(filepath_settings, settings);
                UpdateView(); // Updating all window
            }
        }

        private void DeleteFileExtentionEncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (FileExtentionEncryptListBox.SelectedIndex >= 0)
            {
                int index = FileExtentionEncryptListBox.SelectedIndex;

                string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);
                Settings settings = dataHelper.ReadSettingsFromJson(filepath_settings);

                settings.ExtentionFileToEncrypt.RemoveAt(index);

                dataHelper.WriteSettingsToJson(filepath_settings, settings);
                UpdateView(); // Updating all window
            }
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            //Language
            ComboBoxItem langSelection = (ComboBoxItem)LanguageSettingsComboBox.SelectedItem;
            string language = langSelection.Tag.ToString();
            new LanguageController().DefineLanguage(language);

            //File format
            ComboBoxItem formatSelection = (ComboBoxItem)LogFormatSettingsComboBox.SelectedItem;
            string format = formatSelection.Tag.ToString();

            //Xor key
            string xorkey = XorKeyTextBox.Text;

            //ExtentionFileToEncrypt
            List<string> extentionfiletoencrypt = (List<string>)FileExtentionEncryptListBox.ItemsSource;

            string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);
            var settings = dataHelper.ReadSettingsFromJson(filepath_settings);
            settings.Language = language;
            settings.LogExtension = format;
            settings.XorKey = xorkey;
            settings.ExtentionFileToEncrypt = extentionfiletoencrypt;

            dataHelper.WriteSettingsToJson(filepath_settings, settings);
            UpdateView(); // Updating all window
        }
    }
}
