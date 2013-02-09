using System.Web.Mvc;

using Demo.Aspnet.Mvc.SportStore.Domain.Abstract;
using Demo.Aspnet.Mvc.SportStore.Domain.Entities;
using Demo.Aspnet.Mvc.SportStore.WebUI.ViewModels;

namespace Demo.Aspnet.Mvc.SportStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _productRepository;

        public CartController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = GetCart(), ReturnUrl = returnUrl });
        }

        //
        // GET: /Cart/AddToChart

        public RedirectToRouteResult AddToChart(int productId, string returnUrl)
        {
            // Lookup to a product by id
            Product product = _productRepository.Find(productId);

            if (product != null)
            {
                GetCart().AddItem(product, 1);
            }
            //else
            //{

            //}
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromChart(int productId, string returnUrl)
        {
            // Lookup to a product by id
            Product product = _productRepository.Find(productId);

            if (product != null)
            {
                GetCart().RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

    }
}
