using System.Linq;
using System.Collections.Generic;

using Demo.Aspnet.Mvc.SportStore.Domain.Abstract;
using Demo.Aspnet.Mvc.SportStore.Domain.Entities;
using Demo.Aspnet.Mvc.SportStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Demo.Aspnet.Mvc.SportStore.UnitTests
{
    /// <summary>
    /// Summary description for ProductSpec
    /// </summary>
    [TestClass]
    public class ProductSpec
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Arrange: 
            // Mock the Product Repository so that it would return more than 4 items when its Products property is accessed
            Mock<IProductRepository> mockedProductRepository = new Mock<IProductRepository>();
            mockedProductRepository.Setup( m => m.Products ).Returns( new[]
                {
                    new Product{ProductID = 1, Name = "P1"}, 
                    new Product{ProductID = 2, Name = "P2"}, 
                    new Product{ProductID = 3, Name = "P3"}, 
                    new Product{ProductID = 4, Name = "P4"}, 
                    new Product{ProductID = 5, Name = "P5"}, 
                    new Product{ProductID = 6, Name = "P6"}
                }.AsQueryable() );

            // Instantiate Product Controller and pass the mocked Product Repository into the Product Controller's constructor argument
            ProductController productController = new ProductController( mockedProductRepository.Object );

            // Act: Calls the Product Controller's List method 
            const int page = 2;
            IEnumerable<Product> productsInPage = (IEnumerable<Product>)productController.List( page ).Model;

            // Assert
            Product[] actual = productsInPage.ToArray();
            Assert.AreEqual(2, actual.Length );
            Assert.AreEqual(5, actual[ 0 ].ProductID );
            Assert.AreEqual( "P5", actual[ 0 ].Name );
            Assert.AreEqual( 6, actual[ 1 ].ProductID );
            Assert.AreEqual( "P6", actual[ 1 ].Name );
        }
    }
}
