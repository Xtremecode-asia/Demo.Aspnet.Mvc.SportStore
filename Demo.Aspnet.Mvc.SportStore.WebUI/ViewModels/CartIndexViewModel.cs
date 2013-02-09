using Demo.Aspnet.Mvc.SportStore.Domain.Entities;

namespace Demo.Aspnet.Mvc.SportStore.WebUI.ViewModels
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }

        public string ReturnUrl { get; set; }
    }
}