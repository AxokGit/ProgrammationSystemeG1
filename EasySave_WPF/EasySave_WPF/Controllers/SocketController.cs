using EasySave_WPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace EasySave_WPF.Controllers
{
    class SocketController
    {
        public static bool IsConnected;
        public static Socket socketServer;
        public static Socket socketClient;
        BackupWorksRunController backupWorksRunController = new BackupWorksRunController();
        public SocketController(MainWindow mainWindow, MainController mainController)
        {
            ListenConnection();
            AcceptConnection();
            EcouterReseau(mainWindow, mainController);
        }

        private void ListenConnection()
        {
            socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketServer.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000));
            socketServer.Listen(1);
        }
        private void AcceptConnection()
        {
            socketClient = socketServer.Accept();
            IsConnected = true;
        }

        private void EcouterReseau(MainWindow mainWindow, MainController mainController)
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                try
                {
                    int bytesReceived = socketClient.Receive(buffer);
                    string msg = Encoding.ASCII.GetString(buffer, 0, bytesReceived);
                    if (msg.StartsWith("<msg>get_all_bw<msg>"))
                    {
                        List<BackupWork> backupworks = backupWorksRunController.GetBackupWorks();
                        string json = "<msg>get_all_bw_response<msg>" + JsonConvert.SerializeObject(backupworks.ToArray(), Formatting.Indented);
                        socketClient.Send(Encoding.ASCII.GetBytes(json));
                    }
                    else if (msg.StartsWith("<msg>run_backupworks<msg>"))
                    {
                        msg = msg.Replace("<msg>run_backupworks<msg>", "");
                        List<BackupWork> backupWorks = JsonConvert.DeserializeObject<List<BackupWork>>(msg);
                        Thread t = new Thread(() => new BackupWorksRunController().StartCopy(backupWorks, mainWindow, mainController));
                        t.Start();
                    }
                    else if (msg.StartsWith("<msg>pause_backupworks<msg>"))
                    {
                        MainController.Paused = true;
                    }
                    else if (msg.StartsWith("<msg>stop_backupworks<msg>"))
                    {
                        MainController.StopButton = true;
                    }
                }
                catch (Exception)
                {
                    IsConnected = false;
                    AcceptConnection();
                }
            }
        }

        public static void UpdateProgress(MainWindow mainWindow, double progress)
        {
            if (IsConnected)
            {
                string json = "<msg>progress_update<msg>" + JsonConvert.SerializeObject(progress, Formatting.Indented);
                socketClient.Send(Encoding.ASCII.GetBytes(json));
            }
        }

        public static void UpdateProgressLabel(MainWindow mainWindow, string progressLabel)
        {
            if (IsConnected)
            {
                string json = "<msg>progressLabel_update<msg>" + JsonConvert.SerializeObject(progressLabel, Formatting.Indented);
                socketClient.Send(Encoding.ASCII.GetBytes(json));
            }
        }
    }
}
