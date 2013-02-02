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

        public ViewResult List(int pageIndex = 1)
        {
            ProductListViewModel productListViewModel = new ProductListViewModel
            {
                Products = _productRepository.Products.OrderBy( p => p.ProductID )
                             .Skip( ( pageIndex - 1 ) * ItemSizePerPage )
                             .Take( ItemSizePerPage ),
                PagingInfo = new PagingInfo{CurrentPageIndex = pageIndex, ItemsPerPage = ItemSizePerPage, TotalItems = _productRepository.Products.Count()}
            };
            return View( productListViewModel );
        }

    }
}
