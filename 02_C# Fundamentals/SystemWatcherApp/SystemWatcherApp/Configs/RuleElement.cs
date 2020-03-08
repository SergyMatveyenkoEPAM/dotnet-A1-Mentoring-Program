using System.Configuration;
using System.Text.RegularExpressions;

namespace SystemWatcherApp.Configs
{
    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public Regex Name
        {
            get
            {
                return (Regex)base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }

        [ConfigurationProperty("address", IsRequired = true)]
        public string Address
        {
            get
            {
                return (string)base["address"];
            }
            set
            {
                base["address"] = value;
            }
        }
    }
}
