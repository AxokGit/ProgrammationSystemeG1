using EasySave_WPF.Views;

namespace EasySave_WPF.Controllers
{
    class MainController
    {
        public MainController()
        {
            new LanguageController().CheckLanguageConfig();
        }
    }
}
