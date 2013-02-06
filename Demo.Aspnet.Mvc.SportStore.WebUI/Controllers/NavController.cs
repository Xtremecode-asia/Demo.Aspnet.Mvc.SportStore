using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Demo.Aspnet.Mvc.SportStore.Domain.Abstract;

namespace Demo.Aspnet.Mvc.SportStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository _productRepository;

        public NavController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        //
        // GET: /Nav/Menu
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = _productRepository.Products.Select(p => p.Category).Distinct().OrderBy(p => p);
            return PartialView(categories);
        }

    }
}
