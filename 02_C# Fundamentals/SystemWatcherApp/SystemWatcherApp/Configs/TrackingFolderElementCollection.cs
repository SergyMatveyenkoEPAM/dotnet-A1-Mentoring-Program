using System.Configuration;

namespace SystemWatcherApp.Configs
{
    [ConfigurationCollection(typeof(TrackingFolderElement), AddItemName = "trackingFolder")]
    public class TrackingFolderElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new TrackingFolderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TrackingFolderElement)element).Address;
        }
    }
}