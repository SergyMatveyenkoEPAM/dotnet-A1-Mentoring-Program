using System.Collections.Generic;

namespace Seller.DAL.Interfaces
{
    interface IRepository<T>
    {
        string ConnectionString { get; set; }

        IEnumerable<T> GetAll();

        T GetById(int id);

        void Add(T item);

        void Update(T item);

        void Delete(int id);
    }
}
