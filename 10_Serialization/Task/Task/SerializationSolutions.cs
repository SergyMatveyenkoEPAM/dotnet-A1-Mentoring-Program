using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace Task
{
    [TestClass]
    public class SerializationSolutions
    {
        Northwind dbContext;

        [TestInitialize]
        public void Initialize()
            => dbContext = new Northwind();

        [TestMethod]
        public void SerializationCallbacks()
        {
            // Arrange
            dbContext.Configuration.ProxyCreationEnabled = false;

            var streamingContext = new StreamingContext(StreamingContextStates.Other,
                (dbContext as IObjectContextAdapter)?.ObjectContext);
            var netDataContractSerializer = new NetDataContractSerializer(streamingContext);

            var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(netDataContractSerializer, true);
            var categories = dbContext.Categories.ToList();

            // Act
            var res = tester.SerializeAndDeserialize(categories).FirstOrDefault();

            // Assert
            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Products.FirstOrDefault());
        }

        [TestMethod]
        public void ISerializable()
        {
            // Arrange
            dbContext.Configuration.ProxyCreationEnabled = false;

            var streamingContext = new StreamingContext(StreamingContextStates.Other,
                (dbContext as IObjectContextAdapter)?.ObjectContext);
            var netDataContractSerializer = new NetDataContractSerializer(streamingContext);

            var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(netDataContractSerializer, true);
            var products = dbContext.Products.ToList();

            // Act
            var res = tester.SerializeAndDeserialize(products).FirstOrDefault();

            // Assert
            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Supplier);
            Assert.IsNotNull(res.Category);
            Assert.IsNotNull(res.Order_Details);
        }


        [TestMethod]
        public void ISerializationSurrogate()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;

            var streamingContext = new StreamingContext(StreamingContextStates.Other,
                (dbContext as IObjectContextAdapter)?.ObjectContext);
            var netDataContractSerializer = new NetDataContractSerializer(streamingContext)
            {
                SurrogateSelector = new OrderDetailsSurrogateSelector()
            };
            var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(netDataContractSerializer, true);
            var orderDetails = dbContext.Order_Details.ToList();

            var res = tester.SerializeAndDeserialize(orderDetails).FirstOrDefault();

            // Assert
            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Order);
            Assert.IsNotNull(res.Product);
        }

        [TestMethod]
        public void IDataContractSurrogate()
        {
            // Arrange
            dbContext.Configuration.ProxyCreationEnabled = true;
            dbContext.Configuration.LazyLoadingEnabled = true;

            var settings = new DataContractSerializerSettings
            {
                DataContractSurrogate = new EntityDataContractSurrogate()
            };
            var serializer = new DataContractSerializer(typeof(IEnumerable<Order>), settings);
            var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(serializer, true);
            var orders = dbContext.Orders.ToList();

            //Act,Assert
            var res = tester.SerializeAndDeserialize(orders).FirstOrDefault();

            // Assert
            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Customer);
            Assert.IsNotNull(res.Employee);
            Assert.IsNotNull(res.Order_Details.FirstOrDefault());
        }
    }
}
