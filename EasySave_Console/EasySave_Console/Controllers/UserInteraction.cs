using System;
using System.Collections.Generic;
using System.Text;
using EasySave_Console.Models;

namespace EasySave_Console.Controllers
{
    class UserInteraction
    {
        public Entry GetEntry()
        {
            string input = Console.ReadLine();
            return new Entry(input);
        }
    }
}
