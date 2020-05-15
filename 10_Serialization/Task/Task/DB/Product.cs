using System.Data.Entity.Infrastructure;
using System.Runtime.Serialization;

namespace Task.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Serializable]
    public partial class Product : ISerializable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Order_Details = new HashSet<Order_Detail>();
        }

        public int ProductID { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        public int? SupplierID { get; set; }

        public int? CategoryID { get; set; }

        [StringLength(20)]
        public string QuantityPerUnit { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Detail> Order_Details { get; set; }

        public virtual Supplier Supplier { get; set; }

        private Product(SerializationInfo info, StreamingContext context)
        {
            ProductID = info.GetInt32(nameof(ProductID));
            ProductName = info.GetString(nameof(ProductName));
            SupplierID = info.GetInt32(nameof(SupplierID));
            CategoryID = info.GetInt32(nameof(CategoryID));
            QuantityPerUnit = info.GetString(nameof(QuantityPerUnit));
            UnitPrice = info.GetDecimal(nameof(UnitPrice));
            UnitsInStock = info.GetInt16(nameof(UnitsInStock));
            UnitsOnOrder = info.GetInt16(nameof(UnitsOnOrder));
            ReorderLevel = info.GetInt16(nameof(ReorderLevel));
            Discontinued = info.GetBoolean(nameof(Discontinued));
            Category = (Category)info.GetValue(nameof(Category), typeof(Category));
            Order_Details = (ICollection<Order_Detail>)info.GetValue(nameof(Order_Details), typeof(ICollection<Order_Detail>));
            Supplier = (Supplier)info.GetValue(nameof(Supplier), typeof(Supplier));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(ProductID), ProductID);
            info.AddValue(nameof(ProductName), ProductName);
            info.AddValue(nameof(SupplierID), SupplierID);
            info.AddValue(nameof(CategoryID), CategoryID);
            info.AddValue(nameof(QuantityPerUnit), QuantityPerUnit);
            info.AddValue(nameof(UnitPrice), UnitPrice);
            info.AddValue(nameof(UnitsInStock), UnitsInStock);
            info.AddValue(nameof(UnitsOnOrder), UnitsOnOrder);
            info.AddValue(nameof(ReorderLevel), ReorderLevel);
            info.AddValue(nameof(Discontinued), Discontinued);

            var serializationContext = (context.Context as IObjectContextAdapter)?.ObjectContext 
                                       ?? throw new Exception();

            serializationContext.LoadProperty(this, p => p.Supplier);
            serializationContext.LoadProperty(this, p => p.Category);
            serializationContext.LoadProperty(this, p => p.Order_Details);

            info.AddValue(nameof(Category), Category, typeof(Category));
            info.AddValue(nameof(Order_Details), Order_Details, typeof(ICollection<Order_Detail>));
            info.AddValue(nameof(Supplier), Supplier, typeof(Supplier));
        }
    }
}
