using System;
using System.Runtime.Serialization;
using Task.DB;

namespace Task
{
    public class OrderDetailsSurrogateSelector : ISurrogateSelector
    {
        private ISurrogateSelector _sel;
        public void ChainSelector(ISurrogateSelector selector)
            => _sel=selector;

        public ISurrogateSelector GetNextSelector()
            => _sel;

        public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
        {
            selector = this;
            return type == typeof(Order_Detail)
                ? new OrderDetailsSerializationSurrogate()
                : null;
        }
    }
}
