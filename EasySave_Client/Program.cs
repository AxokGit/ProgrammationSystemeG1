using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EasySave_Client
{
    class Program
    {
        static Socket sck;
        static void Main(string[] args)
        {
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            try
            {
                sck.Connect(localEndPoint);
            }
            catch
            {
                Console.WriteLine("Unable to connect to remote point!\r\n");
                Main(args);
            }
            Console.WriteLine("What backupwork would you like to execute?");
            string text = Console.ReadLine();
            byte[] data = Encoding.ASCII.GetBytes(text);

            sck.Send(data);
            Console.WriteLine("Data Sent!\r\n");
            Console.WriteLine("Press any key to continue");
            Console.Read();
            sck.Close();
        }
    }
}
