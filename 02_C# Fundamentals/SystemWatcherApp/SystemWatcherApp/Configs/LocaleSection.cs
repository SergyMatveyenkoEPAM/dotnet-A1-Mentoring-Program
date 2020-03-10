using System.Configuration;

namespace SystemWatcherApp.Configs
{
    public class LocaleSection : ConfigurationSection
    {
        [ConfigurationProperty("localecode", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string LocaleCode
        {
            get { return (string)this["localecode"]; }
            set { this["localecode"] = value; }
        }
    }
}
