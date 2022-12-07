using EasySave_WPF.Controllers;
using System.Windows;
using System.Windows.Input;

namespace EasySave_WPF
{
    public partial class MainWindow : Window
    {
        BackupWorksRunController backupWorksRunController = new BackupWorksRunController();
        public MainWindow()
        {
            new MainController();

            InitializeComponent();

            BackupWorkRunListView.ItemsSource = backupWorksRunController.GetBackupWorks();
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
        }
    }
}
