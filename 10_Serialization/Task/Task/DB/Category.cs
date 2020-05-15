using System.Data.Entity.Infrastructure;
using System.Runtime.Serialization;

namespace Task.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [DataContract]
    public class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [DataMember]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(15)]
        [DataMember]
        public string CategoryName { get; set; }

        [Column(TypeName = "ntext")]
        [DataMember]
        public string Description { get; set; }

        [Column(TypeName = "image")]
        [DataMember]
        public byte[] Picture { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [DataMember]
        public virtual ICollection<Product> Products { get; set; }

        [OnSerializing]
        public void OnSerializing(StreamingContext context)
        {
            var serializationContext = (context.Context as IObjectContextAdapter)?.ObjectContext
                                       ?? throw new Exception();

            serializationContext.LoadProperty(this, p => p.Products);
        }
    }
}
