using MyIoC.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
            var types = assembly.ExportedTypes;
            foreach (var type in types)
            {
                var constructorImportAttribute = type.GetCustomAttribute<ImportConstructorAttribute>();
                if (constructorImportAttribute != null || HasImportProperties(type))
                {
                    _typesDictionary.Add(type, type);
                }

                var exportAttributes = type.GetCustomAttributes<ExportAttribute>();
                foreach (var exportAttribute in exportAttributes)
                {
                    _typesDictionary.Add(exportAttribute.Contract ?? type, type);
                }
            }
        }

        private bool HasImportProperties(Type type)
        {
            var propertiesInfo = GetPropertiesRequiedImport(type);
            return propertiesInfo.Any();
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
                throw new IoCException($"IoC container does not have a dependency for {type.FullName}");
            }

            Type concreteType = _typesDictionary[type];
            ConstructorInfo constructorInfo = GetConstructor(concreteType);
            object instance = CreateFromConstructor(concreteType, constructorInfo);

            if (concreteType.GetCustomAttribute<ImportConstructorAttribute>() != null)
            {
                return instance;
            }

            ResolveProperties(concreteType, instance);
            return instance;
        }

        private ConstructorInfo GetConstructor(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();

            if (constructors.Length == 0)
            {
                throw new IoCException($"There are no public constructors for type {type.FullName}");
            }

            return constructors.First();
        }

        private object CreateFromConstructor(Type type, ConstructorInfo constructorInfo)
        {
            ParameterInfo[] parameters = constructorInfo.GetParameters();

            object[] parametersInstances = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                parametersInstances[i] = CreateInstance(parameters[i].ParameterType);
            }

            object instance = Activator.CreateInstance(type, parametersInstances);

            return instance;
        }

        private void ResolveProperties(Type type, object instance)
        {
            var propertiesInfo = GetPropertiesRequiedImport(type);
            foreach (var property in propertiesInfo)
            {
                var resolvedProperty = CreateInstance(property.PropertyType);
                property.SetValue(instance, resolvedProperty);
            }
        }

        private IEnumerable<PropertyInfo> GetPropertiesRequiedImport(Type type)
        {
            return type.GetProperties().Where(p => p.GetCustomAttribute<ImportAttribute>() != null);
        }

        public T CreateInstance<T>()
        {
            return (T)CreateInstance(typeof(T));
        }


        public void AssemblySample()
        {
            var container = new Container();

            container.AddAssembly(Assembly.GetExecutingAssembly());

            var customerBLL = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
            var customerBLL2 = container.CreateInstance<CustomerBLL2>();
            Console.WriteLine($"customerBLL2.CustomerDAL is not null - {customerBLL2.CustomerDAL != null}");
            Console.WriteLine($"customerBLL2.logger is not null - {customerBLL2.logger != null}");
        }

        public void ExplicitSample()
        {
            var container = new Container();

            container.AddType(typeof(CustomerBLL));
            container.AddType(typeof(CustomerBLL2));
            container.AddType(typeof(Logger));
            container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

            var customerBLL = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
            var customerBLL2 = container.CreateInstance<CustomerBLL2>();
            Console.WriteLine($"customerBLL2.CustomerDAL is not null - {customerBLL2.CustomerDAL != null}");
            Console.WriteLine($"customerBLL2.logger is not null - {customerBLL2.logger != null}");
        }
    }
}
