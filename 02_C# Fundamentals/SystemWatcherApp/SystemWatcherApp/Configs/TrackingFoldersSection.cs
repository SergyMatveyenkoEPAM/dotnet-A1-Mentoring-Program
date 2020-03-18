using System.Configuration;

namespace SystemWatcherApp.Configs
{
    public class TrackingFoldersSection : ConfigurationSection
    {
        [ConfigurationProperty("trackingFolders", IsDefaultCollection = true)]
        public TrackingFolderElementCollection TrackingFolders
        {
            get { return (TrackingFolderElementCollection)this["trackingFolders"]; }
            set { this["trackingFolders"] = value; }
        }
    }
}
