using System.Collections.Generic;
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

        public Product Find(int productId)
        {
            return Products.FirstOrDefault(p=>p.ProductID == productId);
        }

        public IEnumerable<string> FindAllProductCategories()
        {
            return Products.Select(p => p.Category).Distinct().OrderBy(p => p);
        }
    }
}
