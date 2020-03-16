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
        {
            var interfaces = new List<Type>();
            foreach (var type in assembly.GetTypes().Where(x => x.IsInterface))
            {
                interfaces.Add(type);
            }

            foreach (var type in assembly.GetTypes().Where(x => x.IsClass))
            {
                if (interfaces.Any(i => i.IsAssignableFrom(type)))
                {
                    foreach (var @interface in interfaces)
                    {
                        if (@interface.IsAssignableFrom(type))
                        {
                            AddType(type, @interface);
                        }
                    }
                }
                // Console.WriteLine(type.FullName + " has attributes: " + type.Attributes);
                else
                {
                    if (type == typeof(CustomerBLL) || type == typeof(Logger))
                    {
                        _typesDictionary.Add(type, type);
                    }
                }
            }



        }

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
            if (!_typesDictionary.ContainsKey(type))
            {
                return null;
            }
            var concreteType = _typesDictionary[type];
            var defaultCtor = concreteType.GetConstructors()[0];
            var defaultParams = defaultCtor.GetParameters();
            var parameters = defaultParams.Select(param => CreateInstance(param.ParameterType)).ToArray();

            return defaultCtor.Invoke(parameters);
        }

        public T CreateInstance<T>()
        {
            return (T)CreateInstance(typeof(T));
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
