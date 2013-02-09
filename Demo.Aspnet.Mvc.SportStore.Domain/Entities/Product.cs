using System;
using System.Globalization;

namespace Demo.Aspnet.Mvc.SportStore.Domain.Entities
{
    public class Product : IComparable<Product>
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public int CompareTo(Product other)
        {
            return other != null ?
                    ProductID.CompareTo(other.ProductID) |
                   string.Compare(Name, other.Name, true, CultureInfo.CurrentCulture) |
                   string.Compare(Category, other.Category, true, CultureInfo.CurrentCulture) : int.MinValue;
        }
    }
}
