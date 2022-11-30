using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Models
{
    /// <summary>
    /// Classe permettant de définir les fichiers à sauvegarder
    /// </summary>
    class FileModel
    {
        public string Name;
        public string FullPath;
        public long Size;

        /// <summary>
        /// Constructor of the FileModel classdel
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fullpath"></param>
        /// <param name="size"></param>
        public FileModel(string name, string fullpath, long size)
        {
            this.Name = name;
            this.FullPath = fullpath;
            this.Size = size;
        }
    }
}
