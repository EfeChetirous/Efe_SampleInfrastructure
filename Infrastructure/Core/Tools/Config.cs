using System.Configuration;

namespace Infrastructure.Core.Tools
{
    public class Config
    {
        static public void SetAppSetting(string key, string value)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var AppSection = configFile.GetSection("appSettings") as AppSettingsSection;

            if (AppSection.Settings[key] == null)
                AppSection.Settings.Add(new KeyValueConfigurationElement(key, value));
            else
                AppSection.Settings[key].Value = value;

            configFile.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        static public string GetAppSetting(string key)
        {
            string result = string.Empty;

            if (ConfigurationManager.ConnectionStrings[key] != null)
                return ConfigurationManager.ConnectionStrings[key].ToString();
            else if (ConfigurationManager.AppSettings[key] != null)
                return ConfigurationManager.AppSettings[key].ToString();

            return result;
        }
    }
}