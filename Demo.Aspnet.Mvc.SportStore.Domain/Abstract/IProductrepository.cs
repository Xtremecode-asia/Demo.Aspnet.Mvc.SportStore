using System.Collections.Generic;
using System.Linq;

using Demo.Aspnet.Mvc.SportStore.Domain.Entities;

namespace Demo.Aspnet.Mvc.SportStore.Domain.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        Product Find(int productId);
        IEnumerable<string> FindAllProductCategories();
    }
}
