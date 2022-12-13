using EasySave_WPF_client.Controllers;
using EasySave_WPF_client.Models;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace EasySave_WPF_client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Thread t = new Thread(() => SocketController.EcouterReseau(this));
            t.Start();
        }

        private void ConnectToServerButton_Click(object sender, RoutedEventArgs e)
        {
            SocketController.Connect(this);
        }

        private void GetBackupworkButton_Click(object sender, RoutedEventArgs e)
        {
            SocketController.GetAllBackupWorks();
        }

        private void RunBackupworkButton_Click(object sender, RoutedEventArgs e)
        {
            List<BackupWork> backupWorks = new List<BackupWork>();
            var backupworksSelected = BackupWorkRunListView.SelectedItems;
            foreach (BackupWork backupwork in backupworksSelected)
            {
                backupWorks.Add(backupwork);
            }
            SocketController.RunBackupworks(backupWorks);
        }

        private void PauseBackupworkButton_Click(object sender, RoutedEventArgs e)
        {
            SocketController.PauseBackupworks();
        }

        private void StopBackupworkButton_Click(object sender, RoutedEventArgs e)
        {
            SocketController.StopBackupworks();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            base.OnClosing(e);
        }
    }
}
