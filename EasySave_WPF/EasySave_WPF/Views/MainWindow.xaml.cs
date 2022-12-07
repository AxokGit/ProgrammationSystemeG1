using EasySave_WPF.Controllers;
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
            //var langCode = EasySave_WPF.Properties.Settings.Default.language;

            //Properties.Settings.Default.language = "en";
            //Properties.Settings.Default.Save();

            new LanguageController();

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyLabel.Content = Convert.ToString(Convert.ToInt32(MyLabel.Content) + 1);
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
