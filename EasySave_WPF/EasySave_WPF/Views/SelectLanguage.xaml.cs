using EasySave_WPF.Controllers;
using EasySave_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour SelectLanguage.xaml
    /// </summary>
    public partial class SelectLanguage : Window
    {
        bool quit_program = true;
        public SelectLanguage(Settings settings)
        {
            InitializeComponent();

            var cbItems = new ObservableCollection<ComboBoxItem>();
            foreach (string language in settings.AvailableLanguage)
            {
                if (language == "fr")
                    cbItems.Add(new ComboBoxItem { Content = "Français", Tag = language });
                if (language == "en")
                    cbItems.Add(new ComboBoxItem { Content = "English", Tag = language });
            }
                
            LanguageComboBox.SelectedIndex = 0;
            LanguageComboBox.ItemsSource = cbItems;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (quit_program)
            {
                Environment.Exit(0);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)LanguageComboBox.SelectedItem;
            string language = typeItem.Tag.ToString();
            new LanguageController().DefineLanguage(language);

            quit_program = false;
            Close();
        }
    }
}
