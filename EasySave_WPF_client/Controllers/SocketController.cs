using EasySave_WPF_client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace EasySave_WPF_client.Controllers
{
    static class SocketController
    {
        public static bool IsConnected = false;
        public static EndPoint serverEndPoint;
        public static Socket? socketClient;

        public static void Connect(MainWindow mainWindow)
        {
            if (!IsConnected)
            {
                try
                {
                    serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);
                    socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socketClient.Connect(serverEndPoint);
                    IsConnected = true;
                    App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                    {
                        mainWindow.ConnectedStatus.Content = (string)Application.Current.FindResource("connected");
                    }, null);
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        (string)Application.Current.FindResource("impossible_connection"),
                        (string)Application.Current.FindResource("application_name"),
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                }
            } else
            {
                MessageBox.Show(
                        (string)Application.Current.FindResource("already_connected"),
                        (string)Application.Current.FindResource("application_name"),
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
            }
        }

        public static void EcouterReseau(MainWindow mainWindow)
        {
            while (true)
            {
                if (IsConnected)
                {
                    byte[] buffer = new byte[1024];
                    try
                    {
                        int bytesReceived = socketClient.Receive(buffer);
                        string msg = Encoding.ASCII.GetString(buffer, 0, bytesReceived);
                        if (msg.StartsWith("<msg>get_all_bw_response<msg>"))
                        {
                            msg = msg.Replace("<msg>get_all_bw_response<msg>", "");
                            List<BackupWork> backupWorks = JsonConvert.DeserializeObject<List<BackupWork>>(msg);
                            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                            {
                                mainWindow.BackupWorkRunListView.ItemsSource = backupWorks;
                            }, null);
                        }
                        else if (msg.StartsWith("<msg>progress_update<msg>"))
                        {
                            msg = msg.Replace("<msg>progress_update<msg>", "");
                            double progress = JsonConvert.DeserializeObject<double>(msg);
                            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                            {
                                mainWindow.BackupWorkProgressBar.Value = progress;
                            }, null);
                        }
                        else if (msg.StartsWith("<msg>progressLabel_update<msg>"))
                        {
                            msg = msg.Replace("<msg>progressLabel_update<msg>", "");
                            string progressLabel = JsonConvert.DeserializeObject<string>(msg);
                            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                            {
                                mainWindow.ProgressionStatusLabel.Content = (string)Application.Current.FindResource("status_backupwork") + progressLabel;
                            }, null);
                        }
                    }
                    catch (SocketException)
                    {
                        IsConnected = false;
                        MessageBox.Show(
                            (string)Application.Current.FindResource("impossible_connection"),
                            (string)Application.Current.FindResource("application_name"),
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                        );
                        Connect(mainWindow);
                    }
                }
            }
        }

        public static void GetAllBackupWorks()
        {
            if (IsConnected)
                socketClient.Send(Encoding.ASCII.GetBytes("<msg>get_all_bw<msg>"));
            else
                MessageBox.Show(
                    (string)Application.Current.FindResource("not_connected"),
                    (string)Application.Current.FindResource("application_name"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
        }
        public static void RunBackupworks(List<BackupWork> backupWorks)
        {
            if (IsConnected)
            {
                string json = "<msg>run_backupworks<msg>" + JsonConvert.SerializeObject(backupWorks.ToArray(), Formatting.Indented);
                socketClient.Send(Encoding.ASCII.GetBytes(json));
            }
            else
                MessageBox.Show(
                    (string)Application.Current.FindResource("not_connected"),
                    (string)Application.Current.FindResource("application_name"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
        }
        public static void PauseBackupworks()
        {
            if (IsConnected)
            {
                string json = "<msg>pause_backupworks<msg>";
                socketClient.Send(Encoding.ASCII.GetBytes(json));
            }
            else
                MessageBox.Show(
                    (string)Application.Current.FindResource("not_connected"),
                    (string)Application.Current.FindResource("application_name"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
        }
        public static void StopBackupworks()
        {
            if (IsConnected)
            {
                string json = "<msg>stop_backupworks<msg>";
                socketClient.Send(Encoding.ASCII.GetBytes(json));
            }
            else
                MessageBox.Show(
                    (string)Application.Current.FindResource("not_connected"),
                    (string)Application.Current.FindResource("application_name"),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
        }
    }
}
