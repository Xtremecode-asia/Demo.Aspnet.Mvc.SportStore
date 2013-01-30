using System.Linq;
using System.Web.Mvc;

using Demo.Aspnet.Mvc.SportStore.Domain.Abstract;

namespace Demo.Aspnet.Mvc.SportStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private const int PageSize = 4;

        public ProductController( IProductRepository productRepository )
        {
            _productRepository = productRepository;
        }

        //
        // GET: /Product/List

        public ViewResult List(int page = 1)
        {
            return View( _productRepository.Products.OrderBy(p=>p.ProductID)
                                                    .Skip((page-1)*PageSize)
                                                    .Take(PageSize));
        }

    }
}
