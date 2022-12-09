using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        private Mutex mutex = new System.Threading.Mutex(false, "EasySave_WPF");
        protected override void OnStartup(StartupEventArgs e)
        {
            if(!mutex.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show("Une autre instance de l'app est déjà en cours", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
            else
            {
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
