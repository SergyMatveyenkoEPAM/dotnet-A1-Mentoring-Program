using System.Configuration;

namespace SystemWatcherApp.Configs
{
    public class WatcherSectionGroup : ConfigurationSectionGroup
    {
        [ConfigurationProperty("ruleSection", IsRequired = false)]
        public RuleSection GeneralSettings
        {
            get { return (RuleSection)base.Sections["ruleSection"]; }
        }

        [ConfigurationProperty("localeSection", IsRequired = true)]
        public LocaleSection ContextSettings
        {
            get { return (LocaleSection)base.Sections["localeSection"]; }
        }

        [ConfigurationProperty("trackingFoldersSection", IsRequired = true)]
        public TrackingFoldersSection TrackingFoldersSettings
        {
            get { return (TrackingFoldersSection)base.Sections["trackingFoldersSection"]; }
        }
    }
}
