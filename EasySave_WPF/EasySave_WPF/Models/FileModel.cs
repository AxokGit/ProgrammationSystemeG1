using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_WPF.Models
{
    /// <summary>
    /// Classe permettant de définir les fichiers à sauvegarder
    /// </summary>
    public class FileModel
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public long Size { get; set; }

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
