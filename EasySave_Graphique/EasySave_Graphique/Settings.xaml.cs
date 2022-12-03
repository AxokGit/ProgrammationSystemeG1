using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasySave_Graphique
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(10 + (150 * index), 0, 0, 0);
            /*
            switch (index)
            {
                case 0:
                    GridMain.Background = Brushes.Aquamarine;
                    break;
                case 1:
                    GridMain.Background = Brushes.Beige;
                    break;
                case 2:
                    GridMain.Background = Brushes.CadetBlue;
                    break;
                case 3:
                    GridMain.Background = Brushes.DarkBlue;
                    break;
                case 4:
                    GridMain.Background = Brushes.Firebrick;
                    break;
                case 5:
                    GridMain.Background = Brushes.Gainsboro;
                    break;
                case 6:
                    GridMain.Background = Brushes.HotPink;
                    break;
            }*/
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Execute_Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Saves();
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            //GridMain.Background = Brushes.Beige;
        }

        private void Logs_Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Logs();
        }

        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            //GridMain.Background = Brushes.DarkBlue;
        }
    }
}
