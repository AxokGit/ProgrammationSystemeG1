using EasySave_WPF.Controllers;
using System.Windows;
using System.Windows.Input;

namespace EasySave_WPF
{
    public partial class MainWindow : Window
    {
        BackupWorksRunController backupWorksRunController = new BackupWorksRunController();
<<<<<<< Updated upstream
=======
        BackupWorksCreateController backupWorksCreateController = new BackupWorksCreateController();
        DataHelper dataHelper = new DataHelper();
        FileHelper fileHelper = new FileHelper();
>>>>>>> Stashed changes
        public MainWindow()
        {
            new MainController();

            InitializeComponent();

            BackupWorkRunListView.ItemsSource = backupWorksRunController.GetBackupWorks();
<<<<<<< Updated upstream
            BackupWorkEditListView.ItemsSource = backupWorksRunController.GetBackupWorks();

            this.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            string name = Name.Text;
            string sourceFolder = SourceFolder.Text;
            string destinationFolder = DestinationFolder.Text;
            string type = Type.Text;
            new BackupWorksCreateController(name, sourceFolder, destinationFolder, type);
=======

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
>>>>>>> Stashed changes
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
