using System.Configuration;

namespace SystemWatcherApp.Configs
{
    public class RuleSection : ConfigurationSection
    {
        [ConfigurationProperty("rules", IsDefaultCollection = true)]
        public RuleElementCollection Rules
        {
            get { return (RuleElementCollection)this["rules"]; }
            set { this["rules"] = value; }
        }
    }
}
