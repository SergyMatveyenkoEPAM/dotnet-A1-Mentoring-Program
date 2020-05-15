using System;
using System.Data.Entity.Infrastructure;
using System.Runtime.Serialization;
using Task.DB;

namespace Task
{
    public class OrderDetailsSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var orderDetails = obj as Order_Detail ?? throw new Exception();
            info.AddValue(nameof(orderDetails.OrderID), orderDetails.OrderID);
            info.AddValue(nameof(orderDetails.ProductID), orderDetails.ProductID);
            info.AddValue(nameof(orderDetails.Discount), orderDetails.Discount);
            info.AddValue(nameof(orderDetails.Quantity), orderDetails.Quantity);
            info.AddValue(nameof(orderDetails.UnitPrice), orderDetails.UnitPrice);

            var serializationContext = (context.Context as IObjectContextAdapter)?.ObjectContext
                                       ?? throw new Exception();

            serializationContext.LoadProperty(orderDetails, p => p.Product);
            serializationContext.LoadProperty(orderDetails, p => p.Order);

            info.AddValue(nameof(orderDetails.Order), orderDetails.Order);
            info.AddValue(nameof(orderDetails.Product), orderDetails.Product);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var orderDetails = obj as Order_Detail ?? throw new Exception();
            orderDetails.OrderID = info.GetInt32(nameof(orderDetails.OrderID));
            orderDetails.ProductID = info.GetInt32(nameof(orderDetails.ProductID));
            orderDetails.Discount = info.GetSingle(nameof(orderDetails.Discount));
            orderDetails.Quantity = info.GetInt16(nameof(orderDetails.Quantity));
            orderDetails.UnitPrice = info.GetDecimal(nameof(orderDetails.UnitPrice));
            orderDetails.Order = (Order)info.GetValue(nameof(orderDetails.Order), typeof(Order));
            orderDetails.Product = (Product)info.GetValue(nameof(orderDetails.Product), typeof(Product));
            return orderDetails;
        }
    }
}
