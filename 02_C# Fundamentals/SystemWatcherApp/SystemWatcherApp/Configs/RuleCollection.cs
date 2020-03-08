using System.Configuration;

namespace SystemWatcherApp.Configs
{
    public class RuleCollection : ConfigurationElementCollection
    {      
        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleElement)element).Name;
        }
    }
}
