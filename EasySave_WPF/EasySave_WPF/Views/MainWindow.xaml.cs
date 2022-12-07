using EasySave_WPF.Controllers;
using EasySave_WPF.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace EasySave_WPF
{
    public partial class MainWindow : Window
    {

        BackupWorksRunController backupWorksRunController = new BackupWorksRunController();
        BackupWorksCreateController backupWorksCreateController = new BackupWorksCreateController();
        DataHelper dataHelper = new DataHelper();
        FileHelper fileHelper = new FileHelper();
        public MainWindow()
        {
            new MainController();

            InitializeComponent();

            //Backup work run
            BackupWorkRunListView.ItemsSource = backupWorksRunController.GetBackupWorks();

            //Backup work create
            var typeBackupwork = new ObservableCollection<ComboBoxItem>();
            typeBackupwork.Add(new ComboBoxItem { Content = "Complete", Tag = "complete" });
            typeBackupwork.Add(new ComboBoxItem { Content = "Differencial", Tag = "differencial" });
            TypeBackupworkCreateComboBox.SelectedIndex = 0;
            TypeBackupworkCreateComboBox.ItemsSource = typeBackupwork;

            // Backup work edit
            BackupWorksListEditComboBox.ItemsSource = backupWorksRunController.GetBackupWorksName();

            //Settings
            string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);
            var settings = dataHelper.ReadSettingsFromJson(filepath_settings);

            var langs = new ObservableCollection<ComboBoxItem>();
            foreach (string language in settings.AvailableLanguage)
            {
                if (language == "fr")
                    langs.Add(new ComboBoxItem { Content = "Français", Tag = language });
                if (language == "en")
                    langs.Add(new ComboBoxItem { Content = "English", Tag = language });
            }

            if (settings.Language == "en")
                LanguageSettingsComboBox.SelectedIndex = 0;
            if (settings.Language == "fr")
                LanguageSettingsComboBox.SelectedIndex = 1;
            LanguageSettingsComboBox.ItemsSource = langs;

            var formats = new ObservableCollection<ComboBoxItem>();
            formats.Add(new ComboBoxItem { Content = "JSON", Tag = ".json" });
            formats.Add(new ComboBoxItem { Content = "XML", Tag = ".xml" });

            if (settings.LogExtension == ".json")
                LogFormatSettingsComboBox.SelectedIndex = 0;
            if (settings.LogExtension == ".xml")
                LogFormatSettingsComboBox.SelectedIndex = 1;
            LogFormatSettingsComboBox.ItemsSource = formats;

            this.ShowDialog();
        }

        private void ButtonClickSaveSettings(object sender, RoutedEventArgs e)
        {
            ComboBoxItem langSelection = (ComboBoxItem)LanguageSettingsComboBox.SelectedItem;
            string language = langSelection.Tag.ToString();
            new LanguageController().DefineLanguage(language);

            ComboBoxItem formatSelection = (ComboBoxItem)LogFormatSettingsComboBox.SelectedItem;
            string format = formatSelection.Tag.ToString();
            new LanguageController().DefineLanguage(language);

            string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);
            Settings settings = new Settings(language, format);

            dataHelper.WriteSettingsToJson(filepath_settings, settings);
        }

        private void ButtonClickCreateBackupWork(object sender, RoutedEventArgs e)
        {
            string name = NameBackupworkCreateTextBox.Text;
            string srcFolder = SrcFolderBackupworkCreateTextBox.Text;
            string dstFolder = DstFolderBackupworkCreateTextBox.Text;
            ComboBoxItem typeItem = (ComboBoxItem)TypeBackupworkCreateComboBox.SelectedItem;
            string type = typeItem.Tag.ToString();

            backupWorksCreateController.CreateBackupAndSave(
                new BackupWork(name, srcFolder, dstFolder, type)
            );
        }

        private void BackupWorksListEditComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
