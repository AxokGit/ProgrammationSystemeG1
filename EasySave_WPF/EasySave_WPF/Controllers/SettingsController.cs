using EasySave_WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace EasySave_WPF.Controllers
{
    class SettingsController
    {
        DataHelper dataHelper = new DataHelper(); // Instantiation of the json helper
        FileHelper fileHelper = new FileHelper(); // Instantiation of the file helper
        public void InsertFileExtentionEncryptButton_Click(MainWindow mainWindow, MainController mainController)
        {
            string text = mainWindow.FileExtentionEncryptTextBox.Text;
            if (text != "")
            {
                string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);

                Settings settings = dataHelper.ReadSettingsFromJson(filepath_settings);

                if (text.StartsWith("."))
                    settings.ExtentionFileToEncrypt.Add(text);
                else
                    settings.ExtentionFileToEncrypt.Add("." + text);
                dataHelper.WriteSettingsToJson(filepath_settings, settings);
                mainWindow.FileExtentionEncryptTextBox.Text = "";
                mainController.UpdateView(mainWindow); // Updating all window
            }
        }

        public void DeleteFileExtentionEncryptButton_Click(MainWindow mainWindow, MainController mainController)
        {
            if (mainWindow.FileExtentionEncryptListBox.SelectedIndex >= 0)
            {
                int index = mainWindow.FileExtentionEncryptListBox.SelectedIndex;

                string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);
                Settings settings = dataHelper.ReadSettingsFromJson(filepath_settings);

                settings.ExtentionFileToEncrypt.RemoveAt(index);

                dataHelper.WriteSettingsToJson(filepath_settings, settings);
                mainController.UpdateView(mainWindow); // Updating all window
            }
        }

        public void InsertStopProcessButton_Click(MainWindow mainWindow, MainController mainController)
        {
            string text = mainWindow.StopProcessTextBox.Text;
            if (text != "")
            {
                string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);

                Settings settings = dataHelper.ReadSettingsFromJson(filepath_settings);

                settings.StopProcesses.Add(text);
                dataHelper.WriteSettingsToJson(filepath_settings, settings);
                mainWindow.StopProcessTextBox.Text = "";
                mainController.UpdateView(mainWindow); // Updating all window
            }
        }

        public void DeleteStopProcessButton_Click(MainWindow mainWindow, MainController mainController)
        {
            if (mainWindow.StopProcessListBox.SelectedIndex >= 0)
            {
                int index = mainWindow.StopProcessListBox.SelectedIndex;

                string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);
                Settings settings = dataHelper.ReadSettingsFromJson(filepath_settings);

                settings.StopProcesses.RemoveAt(index);

                dataHelper.WriteSettingsToJson(filepath_settings, settings);
                mainController.UpdateView(mainWindow); // Updating all window
            }
        }

        public void InsertPriorityFilesButton_Click(MainWindow mainWindow, MainController mainController)
        {
            string text = mainWindow.PriorityFilesTextBox.Text;
            if (text != "")
            {
                string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);

                Settings settings = dataHelper.ReadSettingsFromJson(filepath_settings);

                if (text.StartsWith("."))
                    settings.PriorityFiles.Add(text);
                else
                    settings.PriorityFiles.Add("." + text);

                dataHelper.WriteSettingsToJson(filepath_settings, settings);
                mainWindow.PriorityFilesTextBox.Text = "";
                mainController.UpdateView(mainWindow); // Updating all window
            }
        }

        public void DeletePriorityFilesButton_Click(MainWindow mainWindow, MainController mainController)
        {
            if (mainWindow.PriorityFilesListBox.SelectedIndex >= 0)
            {
                int index = mainWindow.PriorityFilesListBox.SelectedIndex;

                string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);
                Settings settings = dataHelper.ReadSettingsFromJson(filepath_settings);

                settings.PriorityFiles.RemoveAt(index);

                dataHelper.WriteSettingsToJson(filepath_settings, settings);
                mainController.UpdateView(mainWindow); // Updating all window
            }
        }

        public void SaveSettingsButton_Click(MainWindow mainWindow, MainController mainController)
        {
            //Language
            ComboBoxItem langSelection = (ComboBoxItem)mainWindow.LanguageSettingsComboBox.SelectedItem;
            string language = langSelection.Tag.ToString();
            new LanguageController().DefineLanguage(language);

            //File format
            ComboBoxItem formatSelection = (ComboBoxItem)mainWindow.LogFormatSettingsComboBox.SelectedItem;
            string format = formatSelection.Tag.ToString();

            //Xor key
            string xorkey = mainWindow.XorKeyTextBox.Text;

            //ExtentionFileToEncrypt
            List<string> extentionfiletoencrypt = (List<string>)mainWindow.FileExtentionEncryptListBox.ItemsSource;

            //StopProcesses
            List<string> stopprocesses = (List<string>)mainWindow.StopProcessListBox.ItemsSource;

            //PriorityFiles
            List<string> priorityfiles = (List<string>)mainWindow.PriorityFilesListBox.ItemsSource;

            string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);
            var settings = dataHelper.ReadSettingsFromJson(filepath_settings);
            settings.Language = language;
            settings.LogExtension = format;
            settings.XorKey = xorkey;
            settings.ExtentionFileToEncrypt = extentionfiletoencrypt;
            settings.StopProcesses = stopprocesses;
            settings.PriorityFiles = priorityfiles;

            dataHelper.WriteSettingsToJson(filepath_settings, settings);
            MessageBox.Show(
                (string)Application.Current.FindResource("settings_saved"),
                (string)Application.Current.FindResource("application_name"),
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
            mainController.UpdateView(mainWindow); // Updating all window
        }
    }
}
