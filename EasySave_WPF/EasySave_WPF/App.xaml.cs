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
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Mutex mutex = new Mutex(true, "EasySave_WPF");
        protected override void OnStartup(StartupEventArgs e)
        {
            if(!mutex.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show((string)Current.FindResource("error_double_instance"), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            mutex.ReleaseMutex();
            base.OnExit(e);
        }
    }
}
