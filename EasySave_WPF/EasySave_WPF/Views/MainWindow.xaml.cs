using EasySave_WPF.Controllers;
using System.Windows;

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
    }
}
