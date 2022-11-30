using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Views
{
    class TableView
    {
        int TableWidth;
        public TableView(int tableWidth)
        {
            this.TableWidth = tableWidth;
        }
        /// <summary>
        /// Method to print a line
        /// </summary>
        public void PrintLine()
        {
            Console.WriteLine(new string('-', TableWidth+1));
        }
        /// <summary>
        /// Method to print a row on a specific column
        /// </summary>
        /// <param name="columns"></param>
        public void PrintRow(params string[] columns)
        {
            int width = (TableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }
            Console.WriteLine(row);
        }
        /// <summary>
        /// Method to align a text in the center
        /// </summary>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;
            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

    }
}
