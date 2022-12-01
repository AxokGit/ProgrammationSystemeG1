using EasySave_Console.Views;

namespace EasySave_Console.Controllers
{
    class MenuController
    {
        MenuView menuView = new MenuView(); // Instantiation of the main view
        public MenuController()
        {
            menuView.OnBootMessage(); // Show OnBootMessage
            new LanguageController(); // Instantiation of the language controller
            bool optionSelected = false;
            while (!optionSelected) // While user selected nothing
            {
                menuView.ClearConsole();
                string menuOption = menuView.PromptMainMenu();
                if (menuOption == "0") // Quit application
                {
                    optionSelected = true;
                }
                else if (menuOption == "1") // Run backup works
                {
                    new BackupWorksRunController();
                }
                else if (menuOption == "2") // Edit backup works
                {
                    new BackupWorksEditController();
                }
                else if (menuOption == "3") // Select log to open
                {
                    new OpenLogsController();
                }
                else if (menuOption == "4") // Change language
                {
                    menuView.ClearConsole();
                    new LanguageController().SelectLanguage();
                }
                else if (menuOption == "5") // Change extension
                {
                    menuView.ClearConsole();
                    new ExtensionController();
                }
                else if (menuOption == "6")
                {
                    new OpenSettingsDirController(); // Open Settings Directory
                }
            }
        }
    }
}
