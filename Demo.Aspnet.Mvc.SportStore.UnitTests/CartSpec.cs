using System.Linq;
using Demo.Aspnet.Mvc.SportStore.Domain.Entities;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.Aspnet.Mvc.SportStore.UnitTests
{
    [TestClass]
    public class CartSpec
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            // Arrange - create some test products
            Product motherboardX79 = new Product { ProductID = 1, Name = "ASUS Sabertooth X79" };
            Product cpuLga2011 = new Product { ProductID = 2, Name = "Intel i7 3930K" };

            // Arrange - create a new cart
            Cart cart = new Cart();

            // Act 
            cart.AddItem(motherboardX79, 1);
            cart.AddItem(cpuLga2011, 1);
            CartLine[] results = cart.Lines.ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(motherboardX79, results[0].Product);
            Assert.AreEqual(cpuLga2011, results[1].Product);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange - create some test products
            Product motherboardX79 = new Product { ProductID = 1, Name = "ASUS Sabertooth X79" };
            Product cpuLga2011 = new Product { ProductID = 2, Name = "Intel i7 3930K" };

            // Arrange - create a new cart
            Cart cart = new Cart();

            // Act 
            cart.AddItem(motherboardX79, 1);
            cart.AddItem(cpuLga2011, 1);
            cart.AddItem(cpuLga2011, 10);
            CartLine[] results = cart.Lines.OrderBy(p => p.Product.ProductID).ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(1, results[0].Quantity);
            Assert.AreEqual(11, results[1].Quantity);
        }

        [TestMethod]
        public void Can_Remove_CartLine()
        {
            // Arrange - Create some Products
            Product motherboardX79 = new Product { ProductID = 1, Name = "ASUS Sabertooth X79" };
            Product cpuLga2011 = new Product { ProductID = 2, Name = "Intel i7 3930K" };

            // Arrange - Create the Cart
            Cart cart = new Cart();

            // Arrange - Add some number of Products into the cart
            cart.AddItem(motherboardX79, 5);
            cart.AddItem(cpuLga2011, 12);
            cart.AddItem(motherboardX79, 10);
            cart.AddItem(cpuLga2011, 24);

            // Act
            cart.RemoveLine(cpuLga2011);

            // Assert
            Assert.AreEqual(1, cart.Lines.Count());
            Assert.IsNull(cart.Lines.FirstOrDefault(line=>line.Product == cpuLga2011));

        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // Arrange - Create some Products
            Product motherboardX79 = new Product { ProductID = 1, Name = "ASUS Sabertooth X79", Price = 410M };
            Product cpuLga2011 = new Product { ProductID = 2, Name = "Intel i7 3930K", Price = 580M};

            // Arrange - Create the Cart
            Cart cart = new Cart();

            // Arrange - Add some number of Products into the cart
            cart.AddItem(motherboardX79, 5);
            cart.AddItem(cpuLga2011, 12);

            // Act
            decimal result = cart.ComputeTotalValue();

            // Assert
            Assert.AreEqual(9010M , result);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Arrange - Create some Products
            Product motherboardX79 = new Product { ProductID = 1, Name = "ASUS Sabertooth X79", Price = 410M };
            Product cpuLga2011 = new Product { ProductID = 2, Name = "Intel i7 3930K", Price = 580M };

            // Arrange - Create the Cart
            Cart cart = new Cart();

            // Arrange - Add some number of Products into the cart
            cart.AddItem(motherboardX79, 5);
            cart.AddItem(cpuLga2011, 12);

            // Act
            cart.Clear();

            // Assert
            Assert.AreEqual(0, cart.Lines.Count());
        }
    }
}
