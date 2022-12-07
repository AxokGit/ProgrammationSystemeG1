using Newtonsoft.Json;

namespace EasySave_WPF.Models
{
    /// <summary>
    /// Class meant to setup the settings for the language change
    /// </summary>
    public class Settings
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
            if (language != null && logExtension == null)
            {
                this.Language = language;
                this.LogExtension = this.LogExtension ?? ".json";
            }
            else if (language == null && logExtension != null)
            {
                this.Language = this.Language ?? "en";
                this.LogExtension = logExtension;
            }
            else
            {
                this.Language = language;
                this.LogExtension = logExtension;
            }
        }
    }
}