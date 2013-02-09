
using System.Collections.Generic;
using System.Linq;

namespace Demo.Aspnet.Mvc.SportStore.Domain.Entities
{
    public class Cart
    {
        private readonly List<CartLine> cartLines = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            // Does the Product has been allocated to an existing Cart Line previously ?
            CartLine productCartLine = Lines.Any() ? Lines.FirstOrDefault(l => l.Product.ProductID == product.ProductID) : null;
            if (productCartLine == null)
            {
                cartLines.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                productCartLine.Quantity += quantity;
            }
        }

        public IQueryable<CartLine> Lines { get { return cartLines.AsQueryable(); } }

        public void RemoveLine(Product product)
        {
            cartLines.RemoveAll(line => line.Product == product);
        }

        public decimal ComputeTotalValue()
        {
            return cartLines.Sum(line => line.Product.Price * line.Quantity);
        }

        public void Clear()
        {
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal {
            get { return Quantity*Product.Price; }
        }
    }
}
