using NorthwindLibrary;
using System.Collections.Generic;

namespace CachingSolutionsSamples.EmployeesCache
{
    public interface IEmployeesCache
    {
        IEnumerable<Employee> Get(string forUser);
        void Set(string forUser, IEnumerable<Employee> employees);
    }
}
