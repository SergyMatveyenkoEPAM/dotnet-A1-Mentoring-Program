using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyIoC
{
    public class Container
    {
        private readonly IDictionary<Type, Type> _typesDictionary;

        public Container()
        {
            _typesDictionary = new Dictionary<Type, Type>();

        }

        public void AddAssembly(Assembly assembly)
        { }

        public void AddType(Type type)
        {
            _typesDictionary.Add(type, type);
        }

        public void AddType(Type type, Type baseType)
        {
            _typesDictionary.Add(baseType, type);
        }

        public object CreateInstance(Type type)
        {
            return null;
        }

        public T CreateInstance<T>()
        {
            return default(T);
        }


        public void Sample()
        {
            var container = new Container();
            container.AddAssembly(Assembly.GetExecutingAssembly());
            
            container.AddType(typeof(CustomerBLL));
            container.AddType(typeof(Logger));
            container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

            var customerBLL = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
            var customerBLL2 = container.CreateInstance<CustomerBLL>();
            
        }
    }
}
