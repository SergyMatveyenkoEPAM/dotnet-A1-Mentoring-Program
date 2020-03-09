using System.Configuration;

namespace SystemWatcherApp.Configs
{
    public class TrackingFolderElement : ConfigurationElement
    {
        [ConfigurationProperty("address", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Address
        {
            get { return (string)this["address"]; }
            set { this["address"] = value; }
        }
    }
}