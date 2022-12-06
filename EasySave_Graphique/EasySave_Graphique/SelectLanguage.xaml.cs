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
using System.Windows.Shapes;

namespace EasySave_Graphique
{
    /// <summary>
    /// Interaction logic for SelectLanguage.xaml
    /// </summary>
    public partial class SelectLanguage : Window
    {
        public SelectLanguage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().ShowDialog();
        }

        private void cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmb.SelectedIndex == 0)
            {
                Properties.Settings.Default.languageCode = "en-US";
            }
            else
            {
                Properties.Settings.Default.languageCode = "fr-FR";
            }
            Properties.Settings.Default.Save();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
