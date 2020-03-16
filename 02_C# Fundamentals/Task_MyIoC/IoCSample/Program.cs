using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MyIoC;

namespace IoCSample
{
    class Program
    {
        static void Main()
        {

            string s = Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location + @"\..\..\..\..\MyIoC\bin\Debug\")).FullName;
            var files = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location + @"\..\..\..\..\MyIoC\bin\Debug\"), "MyIoC.dll",
                SearchOption.AllDirectories);
            foreach (var item in files)
            {
                Console.WriteLine("-----------------------------------------");
                var interfaces = new List<Type>();
                var assembly = Assembly.LoadFile(item);
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
                                Console.WriteLine(type + "is inherited from " + @interface);
                            }
                        }
                    }

                }
            }
            Console.WriteLine("The End");

            string s1 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var file = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location + @"\..\..\..\..\MyIoC\bin\Debug\"), "MyIoC.dll",
                SearchOption.AllDirectories).FirstOrDefault();

            var container = new Container();
            container.AddAssembly(Assembly.LoadFile(file));

            //container.AddType(typeof(CustomerBLL));
            //container.AddType(typeof(Logger));
            //container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

            var customerBLL = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
            var customerBLL2 = container.CreateInstance<CustomerBLL>();

            Console.ReadKey();
        }
    }
}
