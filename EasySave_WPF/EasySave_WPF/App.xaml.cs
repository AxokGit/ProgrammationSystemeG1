using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace EasySave_WPF
{
    public partial class App : Application
    {
        private Mutex mutex = new Mutex(true, "EasySave_WPF");
        ResourceDictionary dictionary = new ResourceDictionary();
        protected override void OnStartup(StartupEventArgs e)
        {
            if(!mutex.WaitOne(TimeSpan.Zero, true))
            {
                dictionary.Source = new Uri("../../Languages/en.xaml", UriKind.Relative);
                Application.Current.Resources.MergedDictionaries.Add(dictionary);
                MessageBox.Show(
                    (string)Current.FindResource("error_double_instance"),
                    (string)Current.FindResource("application_name"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                Shutdown();
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                base.OnStartup(e);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                mutex.ReleaseMutex();
            }
            catch { }
            base.OnExit(e);
        }
    }
}
