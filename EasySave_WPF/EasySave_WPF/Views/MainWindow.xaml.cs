using EasySave_WPF.Controllers;
using EasySave_WPF.Models;
using EasySave_WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public interface IDynamicGridViewModel
        {
            ObservableCollection<ObservableCollection<ICellViewModel>> Cells { get; }

            int GridHeight { get; }
        }

        public interface ICellViewModel
        {
            ICell Cell { get; set; }
            ICommand ChangeCellStateCommand { get; }
        }

        public interface ICell
        {
            /// <summary>
            /// State of the cell.
            /// </summary>
            bool State { get; set; }
        }
    }
}
