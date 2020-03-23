namespace Seller.DAL.Interfaces
{
    interface IRepository<T>
    {
        public string ConnectionString { get; set; }

        T Get(int id);

        void Add(T item);

        void Update(T item);

        void Delete(int id);
    }
}
