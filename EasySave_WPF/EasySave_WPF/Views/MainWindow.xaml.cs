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
        DataHelper dataHelper = new DataHelper();
        FileHelper fileHelper = new FileHelper();
        public MainWindow()
        {
            new MainController();

            InitializeComponent();

            //Backup work run
            BackupWorkRunListView.ItemsSource = backupWorksRunController.GetBackupWorks();

            //Backup work edit
            BackupWorkEditListView.ItemsSource = backupWorksRunController.GetBackupWorks();


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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClickSaveSettings(object sender, RoutedEventArgs e)
        {
            ComboBoxItem langSelection = (ComboBoxItem)LanguageSettingsComboBox.SelectedItem;
            string language = langSelection.Tag.ToString();
            new LanguageController().DefineLanguage(language);

            ComboBoxItem formatSelection = (ComboBoxItem)LogFormatSettingsComboBox.SelectedItem;
            string format = formatSelection.Tag.ToString();
            new LanguageController().DefineLanguage(language);

            string filepath_settings = fileHelper.FormatFilePath(@"%AppData%\EasySave\Settings.json");
            Settings settings = new Settings(language, format);

            dataHelper.WriteSettingsToJson(filepath_settings, settings);
        }
    }
}
