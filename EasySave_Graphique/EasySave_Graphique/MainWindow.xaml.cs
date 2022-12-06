using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Azure.Amqp.Framing;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace EasySave_Graphique
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            switch (index)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
            }
        }


        private void Execute_Button_Click(object sender, RoutedEventArgs e)
        {
            while (Main.NavigationService.RemoveBackEntry() != null) ;
            Main.Content = new Saves();
        }

        private void Logs_Button_Click(object sender, RoutedEventArgs e)
        {
            while (Main.NavigationService.RemoveBackEntry() != null) ;
            Main.Content = new Logs();
        }
    }
}
