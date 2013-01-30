using System.Data.Entity;
using Demo.Aspnet.Mvc.SportStore.Domain.Entities;

namespace Demo.Aspnet.Mvc.SportStore.Domain.Concrete
{
    class EFDbContext:  DbContext
    {
        public DbSet<Product> Products { set; get; }
    }
}
