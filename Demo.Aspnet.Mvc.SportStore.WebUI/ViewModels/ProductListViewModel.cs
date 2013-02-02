using System.Linq;

using Demo.Aspnet.Mvc.SportStore.Domain.Entities;
using Demo.Aspnet.Mvc.SportStore.WebUI.Models;

namespace Demo.Aspnet.Mvc.SportStore.WebUI.ViewModels
{
    public class ProductListViewModel
    {
        public IQueryable<Product> Products { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}