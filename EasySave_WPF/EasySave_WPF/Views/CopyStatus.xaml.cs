using EasySave_WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EasySave_WPF.Views
{
    /// <summary>
    /// Logique d'interaction pour CopyStatus.xaml
    /// </summary>
    public partial class CopyStatus : Window
    {
        DataHelper dataHelper = new DataHelper();
        FileHelper fileHelper = new FileHelper();
        public CopyStatus(BackupWork backupWork)
        {
            InitializeComponent();
            while (backupWork.Running)
            {
                string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);
                backupWork = dataHelper.ReadBackupWorkFromJson(filepath_bw_config, backupWork);
                NameBackupwork.Content = backupWork.Name;
                SrcBackupwork.Content = backupWork.SrcFolder;
                DstBackupwork.Content = backupWork.DstFolder;
                RemainingFilesBackupwork.Content = backupWork.RemainingFiles + "/" + backupWork.TotalFiles;
                FileInCopyBackupwork.Content = backupWork.FileNameInCopy;
                ProgressBarBackupwork.Value = (double)backupWork.Progression;
                Thread.Sleep(200);
            }
            OkButton.IsEnabled = true;
            this.ShowDialog();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
