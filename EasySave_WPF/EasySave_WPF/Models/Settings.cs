using Newtonsoft.Json;
using System.Collections.Generic;

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
        public string XorKey = "12345678";
        public List<string>? ExtentionFileToEncrypt;
        public List<string>? StopProcesses;
        public List<string>? PriorityFiles;
        /// <summary>
        /// Constructor of the Settings class
        /// </summary>
        /// <param name="language"></param>
        [JsonConstructor]
        public Settings(string? language = null, string? logExtension= null)
        {
            if (ExtentionFileToEncrypt == null)
                ExtentionFileToEncrypt = new List<string>();

            if (StopProcesses == null)
                StopProcesses = new List<string>();

            if (PriorityFiles == null)
                PriorityFiles = new List<string>();

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
