﻿using EasySave_Console.Models;
using EasySave_Console.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasySave_Console.Controllers
{
    class ExtensionController
    {
        MenuView menuView = new MenuView();
        ExtensionMenuView controllerMenuView = new ExtensionMenuView();
        DataHelper dataHelper = new DataHelper();
        FileHelper fileHelper = new FileHelper();

        public ExtensionController()
        {
            string filepath_settings = fileHelper.FormatFilePath(fileHelper.filepath_settings);

            bool ExtensionSelected = false;

            // While user do not select correct choice
            while (!ExtensionSelected)
            {
                menuView.ClearConsole();
                string ExtensionOption = controllerMenuView.PromptExtensionOption();
                if (ExtensionOption == "0") // Go back
                {
                    ExtensionSelected = true;
                }
                else if (ExtensionOption == "1") // Change extension to .json
                {
                    Settings settings = new Settings(".json"); // Creating set
                    dataHelper.WriteSettingsToJson(filepath_settings, settings);
                    ExtensionSelected = true;
                }
                else if (ExtensionOption == "2") // Change extension to .xml
                {
                    Settings settings = new Settings(".xml");
                    dataHelper.WriteSettingsToJson(filepath_settings, settings);
                    ExtensionSelected = true;
                }
            }
        }
    }
}
