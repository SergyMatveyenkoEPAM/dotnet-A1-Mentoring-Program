using System.Configuration;

namespace SystemWatcherApp.Configs
{
    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("pattern", IsKey = true, IsRequired = true)]
        public string Pattern
        {
            get { return (string)this["pattern"]; }
            set { this["pattern"] = value; }
        }

        [ConfigurationProperty("address", IsRequired = true)]
        public string Address
        {
            get { return (string)this["address"]; }
            set { this["address"] = value; }
        }

        [ConfigurationProperty("isRequiredNumeration", IsRequired = true)]
        public bool IsRequiredNumeration
        {
            get { return (bool)this["isRequiredNumeration"]; }
            set { this["isRequiredNumeration"] = value; }
        }

        [ConfigurationProperty("isRequiredMoveDate", IsRequired = true)]
        public bool IsRequiredMoveDate
        {
            get { return (bool)this["isRequiredMoveDate"]; }
            set { this["isRequiredMoveDate"] = value; }
        }


    }
}
