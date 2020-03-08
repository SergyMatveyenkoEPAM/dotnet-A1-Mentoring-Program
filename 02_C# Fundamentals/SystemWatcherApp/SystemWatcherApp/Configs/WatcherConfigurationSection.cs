using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace SystemWatcherApp.Configs
{
    public class WatcherSectionGroup : ConfigurationSectionGroup
    {
        [ConfigurationProperty("ruleSection", IsRequired = false)]
        public RuleSection ContextSettings
        {
            get { return (RuleSection)base.Sections["ruleSection"]; }
        }
    }
}
