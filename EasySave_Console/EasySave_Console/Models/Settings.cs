using Newtonsoft.Json;

namespace EasySave_Console.Models
{
    /// <summary>
    /// Class meant to setup the settings for the language change
    /// </summary>
    class Settings
    {
        public string Language;
        public string[] AvailableLanguage = { "en", "fr" };
        public string? LogExtension;
        public string[] AvailableLogExtension = { ".json", ".xml" };
        /// <summary>
        /// Constructor of the Settings class
        /// </summary>
        /// <param name="language"></param>
        [JsonConstructor]
        public Settings(string? language = null, string? logExtension= null)
        {
            this.Language = this.Language ?? "en";
            this.LogExtension = this.LogExtension ?? ".json";
        }
    }
}