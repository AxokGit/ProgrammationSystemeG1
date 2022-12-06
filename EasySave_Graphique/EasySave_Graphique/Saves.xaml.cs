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
    /// Interaction logic for Saves.xaml
    /// </summary>
    public partial class Saves : Page
    {
        public Saves()
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
            }
            */
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Main.Content = new Saves();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Main.Content = new Saves();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Main.Content = new Saves();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Main.Content = new Saves();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Main.Content = new Saves();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Main.Content = new Saves();
        }
    }
}
