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
            return PartialView(_productRepository.FindAllProductCategories());
        }

    }
}
