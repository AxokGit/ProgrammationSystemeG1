using EasySave_WPF.Models;
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

namespace EasySave_WPF.Views
{
    /// <summary>
    /// Logique d'interaction pour SelectLanguage.xaml
    /// </summary>
    public partial class SelectLanguage : Window
    {
        public SelectLanguage(Settings settings)
        {
            InitializeComponent();



            LanguageComboBox.ItemsSource = 
            this.ShowDialog();
        }
    }
}
