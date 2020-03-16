using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIoC
{
	[ImportConstructor]
	public class CustomerBLL
	{
        public CustomerBLL(ICustomerDAL dal, Logger logger)
        {
            Console.WriteLine("CustomerBLL instance has been created");
        }
	}

	public class CustomerBLL2
	{
		[Import]
		public ICustomerDAL CustomerDAL { get; set; }
		[Import]
		public Logger logger { get; set; }
	}
}
