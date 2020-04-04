using System;

namespace EFNorthwind.Models
{
    public class CreditCard
    {
        public int CreditCardId { get; set; }
        public int CreditCardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CardHolder { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
