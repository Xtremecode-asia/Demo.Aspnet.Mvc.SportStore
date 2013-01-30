using System.Linq;

using Demo.Aspnet.Mvc.SportStore.Domain.Abstract;
using Demo.Aspnet.Mvc.SportStore.Domain.Entities;

namespace Demo.Aspnet.Mvc.SportStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private readonly EFDbContext _efDbContext = new EFDbContext();

        public IQueryable<Product> Products {
            get { return _efDbContext.Products; }
        }
    }
}
