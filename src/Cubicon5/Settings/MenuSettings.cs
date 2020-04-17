using System.IO;
using Config.Net;

namespace Cubicon5.Settings
{
    public static class MenuSettings
    {
        public static string SettingsFile = @"scripts/Cubicon5.json";

        public static ISettings BuildSettings()
        {
            var configbuilder = new ConfigurationBuilder<ISettings>().UseJsonFile(SettingsFile);
            return configbuilder.Build();
        }

        public static ISettings InitSettings()
        {
            if (!File.Exists(SettingsFile))
            {
                return CreateSettings();
            }
            return BuildSettings();
        }

        public static ISettings CreateSettings()
        {
            //If Settings Exist, delete and recreate it
            if (File.Exists(SettingsFile))
            {
                File.Delete(SettingsFile);
            }
            return BuildSettings();            
        }
    }
}

