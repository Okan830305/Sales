namespace Sales.Domain.Models
{
    using System.Data.Entity;
    using Common.Models;
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }
        public DbSet<Products> Products { get; set; }
    }
}
