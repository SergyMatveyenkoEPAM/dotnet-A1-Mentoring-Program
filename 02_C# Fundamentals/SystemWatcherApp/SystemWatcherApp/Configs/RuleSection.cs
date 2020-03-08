using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SystemWatcherApp.Configs
{
    public class RuleSection : ConfigurationSection
    {
        [ConfigurationProperty("rules", IsDefaultCollection = true)]
        // Specify the type of elements found in the collection
        public RuleCollection Rules
        {
            get
            {
                // Get the collection and parse it
                return (RuleCollection)this["rules"];
            }
        }
    }
}
