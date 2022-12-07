using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CryptoSoft_Console
{
    class XORCipher
    {
        public byte[] key;

        public void EncryptFile(string inputFile, string outputFile, string encryption_key)
        {
            key = Encoding.ASCII.GetBytes(encryption_key);

            using (var fin = new FileStream(inputFile, FileMode.Open))
            using (var fout = new FileStream(outputFile, FileMode.Create))
            {
                byte[] buffer = new byte[4096];
                while (true)
                {
                    int bytesRead = fin.Read(buffer);
                    if (bytesRead == 0)
                        break;
                    EncryptBytes(buffer, bytesRead);
                    fout.Write(buffer, 0, bytesRead);
                }
            }
        }

        void EncryptBytes(byte[] buffer, int count)
        {
            for (int i = 0; i < count; i++)
                buffer[i] = (byte)(buffer[i] ^ key[i % key.Length]);
        }
    }
}
