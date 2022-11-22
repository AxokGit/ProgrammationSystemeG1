using EasySave_Console.Models;
using EasySave_Console.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_Console.Controllers
{
    class MenuController
    {
        public MenuController()
        {
            new MenuView().onBootMessage();
            
            LanguageMenuView languageMenuView = new LanguageMenuView();
            languageMenuView.promptLanguageOption();
        }

    }
}
