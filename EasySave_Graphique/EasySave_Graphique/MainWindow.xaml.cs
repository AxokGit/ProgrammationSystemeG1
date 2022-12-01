using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Azure.Amqp.Framing;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

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
            MyLabel.Content = $" {LangHelper.GetString("run_work")}";
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmb.SelectedIndex == 0)
            {
               
            }
        }

        private void French_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FrenchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EnglishButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
