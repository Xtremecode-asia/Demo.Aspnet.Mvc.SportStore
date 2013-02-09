using System.Linq;
using System.Web.Mvc;

using Demo.Aspnet.Mvc.SportStore.Domain.Abstract;
using Demo.Aspnet.Mvc.SportStore.WebUI.Models;
using Demo.Aspnet.Mvc.SportStore.WebUI.ViewModels;

namespace Demo.Aspnet.Mvc.SportStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private const int ItemSizePerPage = 4;

        public ProductController( IProductRepository productRepository )
        {
            _productRepository = productRepository;
        }

        //
        // GET: /Product/List

        public ViewResult List(string category, int pageIndex = 1)
        {
            ProductListViewModel productListViewModel = new ProductListViewModel
            {
                Products = _productRepository.Products
                             .OrderBy( p => p.ProductID )
                             .Where(p=> category == null || p.Category == category )
                             .Skip( ( pageIndex - 1 ) * ItemSizePerPage )
                             .Take( ItemSizePerPage ),
                PagingInfo = new PagingInfo{CurrentPageIndex = pageIndex, ItemsPerPage = ItemSizePerPage, 
                                            TotalItems = string.IsNullOrWhiteSpace(category) ? _productRepository.Products.Count() : _productRepository.Products.Count(p => p.Category == category) },
                CurrentCategory = category
            };
            return View( productListViewModel );
        }

    }
}
