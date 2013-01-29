using System.Web.Mvc;

using Demo.Aspnet.Mvc.SportStore.Domain.Abstract;

namespace Demo.Aspnet.Mvc.SportStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController( IProductRepository productRepository )
        {
            _productRepository = productRepository;
        }

        //
        // GET: /Product/List

        public ViewResult List()
        {
            return View( _productRepository.Products );
        }

    }
}
