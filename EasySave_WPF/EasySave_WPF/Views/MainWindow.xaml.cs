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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasySave_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            new MainController();

            InitializeComponent();

            DataHelper dataHelper = new DataHelper();
            FileHelper fileHelper = new FileHelper();

            string filepath_bw_config = fileHelper.FormatFilePath(fileHelper.filepath_bw_config);

            var backupWorks = dataHelper.ReadBackupWorkFromJson(filepath_bw_config);

            BackupWorkListView.ItemsSource = backupWorks;

            //for( int i = 0; i< backupWorks.Count; i++)
            //{
            //    BackupWorkListView.Items.Add(new BackupWork { Name = backupWorks[i].Name, SrcFolder = backupWorks[i].SrcFolder, DstFolder = null, Type = null });
            //}

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
