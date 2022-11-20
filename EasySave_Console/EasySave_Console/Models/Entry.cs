using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Models
{
    internal class Entry
    {
        public string Text { get; set; }

        public Entry(string text)
        {
            this.Text = text;
        }
    }
}
