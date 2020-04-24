namespace NorthwindApp.DAL.Models
{
    using System.Data.Entity;

    public partial class NorthwindDB : DbContext
    {
        public NorthwindDB() : base("name=NorthwindDB")
        {
        }

        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(e => e.CustomerID)
                .IsFixedLength();

            modelBuilder.Entity<Order>()
                .Property(e => e.Freight)
                .HasPrecision(19, 4);
        }
    }
}
