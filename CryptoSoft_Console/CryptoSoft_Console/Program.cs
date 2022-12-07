using System;
using System.IO;
using System.Reflection;

namespace CryptoSoft_Console
{
    class Program
    {
        
        static void Main(string[] args)
        {
            void Help()
            {
                Console.WriteLine("CryptoSoft (v" + Assembly.GetExecutingAssembly().GetName().Version + ")");
                Console.WriteLine();
                Console.WriteLine("Usage: cryptosoft [action] {[source_file] [dst_file] [key]}");
                Console.WriteLine();
                Console.WriteLine("Action:");
                Console.WriteLine("-> help");
                Console.WriteLine("-> run [source_file] [destination_file] [key]");
                Console.WriteLine();
                Console.WriteLine("Type ENTER to close");
                Console.ReadKey();
            }

            void Error(string msg)
            {
                Console.WriteLine("[ERROR] " + msg);
            }

            XORCipher cipher = new XORCipher();
            try
            {
                string action = args[0];

                if (action == "help")
                {
                    Help();
                }
                else if (action == "run")
                {
                    if (args.Length == 4)
                    {
                        string srcFile = args[1];
                        string dstFile = args[2];
                        string key = args[3];

                        if (File.Exists(srcFile))
                        {
                            if (Directory.Exists(Path.GetDirectoryName(dstFile)))
                            {
                                if (key.Length >= 8)
                                {
                                    cipher.EncryptFile(srcFile, dstFile, key);
                                }
                                else
                                {
                                    Error("The key isn't long enough (8 characters minimum)");
                                }
                            } 
                            else
                            {
                                Error("Folder of destination file doesn't exists");
                            }
                        }
                        else
                        {
                            Error("Source file doesn't exists");
                        }
                    }
                    else
                    {
                        Error("Incorrect number of arguments");
                    }
                }
                else
                {
                    Help();
                }
            }
            catch { Help(); }
        }
    }
}
