using System;

namespace FileSystemApp
{
    public class RecipientEventArgs : EventArgs
    {
        public int QuantityWeNeed { get; set; }

        public RecipientEventArgs(int quantityWeNeed)
        {
            QuantityWeNeed = QuantityWeNeed;
        }

    }
}
