using EasySave_WPF.Controllers;
using EasySave_WPF.Models;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Threading;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace EasySave_WPF
{
    public partial class MainWindow : Window
    {
        public static bool StopProcess { get; set; }
        public static bool Paused { get; set; }
        BackupWorksRunController backupWorksRunController = new BackupWorksRunController();
        BackupWorksCreateController backupWorksCreateController = new BackupWorksCreateController();
        OpenLogsController openLogsController = new OpenLogsController();
        DataHelper dataHelper = new DataHelper();
        FileHelper fileHelper = new FileHelper();
        public MainWindow()
        {
            InitializeComponent();
            new MainController();

            Thread t = new Thread(() => new StopProcessController(this));
            t.Start();

            UpdateView();
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
            StopProcessListBox.ItemsSource = settings.StopProcesses;
            PriorityFilesListBox.ItemsSource = settings.PriorityFiles;
        }

        public void UpdateProgression(double progression)
        {
            BackupWorkProgressBar.Value = progression;
        }
        public void UpdateProgressionStatusLabel(string message)
        {
            ProgressionStatusLabel.Content = (string)Application.Current.FindResource("status_backupwork") + message;
        }

        private void BackupWorkRunListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StopProcess)
            {
                RunBackupworkButton.IsEnabled = false;
            }
            else 
            { 
                int selectionCount = BackupWorkRunListView.SelectedItems.Count;

                if (selectionCount > 0)
                {
                    var items = BackupWorkRunListView.SelectedItems;
                    bool run = false;
                    bool pause = true;
                    foreach (BackupWork backupWork in items)
                    {
                        if (!backupWork.Running)
                            run = true;
                        else
                        {
                            pause = true;
                        }
                    }
                    RunBackupworkButton.IsEnabled = run;
                    PauseBackupworkButton.IsEnabled = pause;
                }
                else
                {
                    RunBackupworkButton.IsEnabled = false;
                    PauseBackupworkButton.IsEnabled = false;
                }
            }
            
        }

        private void RunBackupworkButton_Click(object sender, RoutedEventArgs e)
        {
            if (Paused)
            {
                Paused = false;
            } else
            {
                List<BackupWork> backupWorks = new List<BackupWork>();
                var backupworksSelected = BackupWorkRunListView.SelectedItems;
                foreach (BackupWork backupwork in backupworksSelected)
                {
                    backupWorks.Add(backupwork);
                }
                Thread t = new Thread(() => new BackupWorksRunController().StartCopy(backupWorks, this));
                t.Start();
                UpdateView();
            }
        }

        private void PauseBackupworkButton_Click(object sender, RoutedEventArgs e)
        {
            Paused = true;
        }

        private void SelectSrcFolderCreateBackupWorkButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (dialog.SelectedPath != "")
                    SrcFolderBackupworkCreateTextBox.Text = dialog.SelectedPath;
            }
        }

        private void SelectDstFolderCreateBackupWorkButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (dialog.SelectedPath != "")
                    DstFolderBackupworkCreateTextBox.Text = dialog.SelectedPath;
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
                    MessageBox.Show(
                        (string)Application.Current.FindResource("backupwork_added"),
                        (string)Application.Current.FindResource("application_name"),
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                    UpdateView(); // Updating all window
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
                List<BackupWork> backupworks = dataHelper.ReadBackupWorksFromJson(filepath_bw_config);

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


                SelectSrcFolderEditBackupWorkButton.IsEnabled = true;
                SelectDstFolderEditBackupWorkButton.IsEnabled = true;
                SaveBackupWorkEditButton.IsEnabled = true;
                DeleteBackupWorkEditButton.IsEnabled = true;
            }
        }


        private void SelectSrcFolderEditBackupWorkButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (dialog.SelectedPath != "")
                    SrcFolderBackupWorkEditTextBox.Text = dialog.SelectedPath;
            }
        }

        private void SelectDstFolderEditBackupWorkButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (dialog.SelectedPath != "")
                    DstFolderBackupWorkEditTextBox.Text = dialog.SelectedPath;
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
                    List<BackupWork> backupworks = dataHelper.ReadBackupWorksFromJson(filepath_bw_config);

                    int index = BackupWorksListEditComboBox.SelectedIndex;

                    backupworks[index] = new BackupWork(name, srcFolder, dstFolder, type);

                    dataHelper.WriteBackupWorksToJson(filepath_bw_config, backupworks);
                    BackupWorksListEditComboBox.ItemsSource = backupWorksRunController.GetBackupWorksName();
                    BackupWorksListEditComboBox.SelectedIndex = -1;

                    NameBackupWorkEditTextBox.IsEnabled = false;
                    SrcFolderBackupWorkEditTextBox.IsEnabled = false;
                    DstFolderBackupWorkEditTextBox.IsEnabled = false;
                    TypeBackupWorkEditComboBox.IsEnabled = false;
                    SelectSrcFolderEditBackupWorkButton.IsEnabled = false;
                    SelectDstFolderEditBackupWorkButton.IsEnabled = false;
                    SaveBackupWorkEditButton.IsEnabled = false;
                    DeleteBackupWorkEditButton.IsEnabled = false;


                    MessageBox.Show(
                        (string)Application.Current.FindResource("backupwork_edited"),
                        (string)Application.Current.FindResource("application_name"),
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                    UpdateView(); // Updating all window
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
        private void DeleteBackupWorkEditButton_Click(object sender, RoutedEventArgs e)
        {
            int index = BackupWorksListEditComboBox.SelectedIndex;

            string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
            List<BackupWork> backupworks = dataHelper.ReadBackupWorksFromJson(filepath_bw_config);

            string typeBackupwork;
            if (TypeBackupWorkEditComboBox.SelectedIndex == 0)
                typeBackupwork = "complete";
            else
                typeBackupwork = "differencial";

            backupworks.RemoveAt(index);

            dataHelper.WriteBackupWorksToJson(filepath_bw_config, backupworks);
            BackupWorksListEditComboBox.ItemsSource = backupWorksRunController.GetBackupWorksName();
            BackupWorksListEditComboBox.SelectedIndex = -1;

            NameBackupWorkEditTextBox.IsEnabled = false;
            SrcFolderBackupWorkEditTextBox.IsEnabled = false;
            DstFolderBackupWorkEditTextBox.IsEnabled = false;
            TypeBackupWorkEditComboBox.IsEnabled = false;
            SelectSrcFolderEditBackupWorkButton.IsEnabled = false;
            SelectDstFolderEditBackupWorkButton.IsEnabled = false;
            SaveBackupWorkEditButton.IsEnabled = false;
            DeleteBackupWorkEditButton.IsEnabled = false;

            MessageBox.Show(
                (string)Application.Current.FindResource("backupwork_deleted"),
                (string)Application.Current.FindResource("application_name"),
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
            UpdateView(); // Updating all window
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
                FileExtentionEncryptTextBox.Text = "";
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

        private void InsertStopProcessButton_Click(object sender, RoutedEventArgs e)
        {
            string text = StopProcessTextBox.Text;
            if (text != "")
            {
                string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);

                Settings settings = dataHelper.ReadSettingsFromJson(filepath_settings);

                settings.StopProcesses.Add(text);
                dataHelper.WriteSettingsToJson(filepath_settings, settings);
                StopProcessTextBox.Text = "";
                UpdateView(); // Updating all window
            }
        }

        private void DeleteStopProcessButton_Click(object sender, RoutedEventArgs e)
        {
            if (StopProcessListBox.SelectedIndex >= 0)
            {
                int index = StopProcessListBox.SelectedIndex;

                string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);
                Settings settings = dataHelper.ReadSettingsFromJson(filepath_settings);

                settings.StopProcesses.RemoveAt(index);

                dataHelper.WriteSettingsToJson(filepath_settings, settings);
                UpdateView(); // Updating all window
            }
        }

        private void InsertPriorityFilesButton_Click(object sender, RoutedEventArgs e)
        {
            string text = PriorityFilesTextBox.Text;
            if (text != "")
            {
                string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);

                Settings settings = dataHelper.ReadSettingsFromJson(filepath_settings);

                if (text.StartsWith("."))
                    settings.PriorityFiles.Add(text);
                else
                    settings.PriorityFiles.Add("." + text);
                
                dataHelper.WriteSettingsToJson(filepath_settings, settings);
                PriorityFilesTextBox.Text = "";
                UpdateView(); // Updating all window
            }
        }

        private void DeletePriorityFilesButton_Click(object sender, RoutedEventArgs e)
        {
            if (PriorityFilesListBox.SelectedIndex >= 0)
            {
                int index = PriorityFilesListBox.SelectedIndex;

                string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);
                Settings settings = dataHelper.ReadSettingsFromJson(filepath_settings);

                settings.PriorityFiles.RemoveAt(index);

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

            //StopProcesses
            List<string> stopprocesses = (List<string>)StopProcessListBox.ItemsSource;

            //PriorityFiles
            List<string> priorityfiles = (List<string>)PriorityFilesListBox.ItemsSource;

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
            UpdateView(); // Updating all window
        }
    }
}
