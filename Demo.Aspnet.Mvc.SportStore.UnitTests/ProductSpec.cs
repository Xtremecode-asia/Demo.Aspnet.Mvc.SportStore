using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

using Demo.Aspnet.Mvc.SportStore.Domain.Abstract;
using Demo.Aspnet.Mvc.SportStore.Domain.Entities;
using Demo.Aspnet.Mvc.SportStore.WebUI.Controllers;
using Demo.Aspnet.Mvc.SportStore.WebUI.HtmlHelpers;
using Demo.Aspnet.Mvc.SportStore.WebUI.Models;
using Demo.Aspnet.Mvc.SportStore.WebUI.ViewModels;
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
            ProductListViewModel productListViewModel = (productController.List(page).Model as ProductListViewModel);
            Assert.IsNotNull(productListViewModel);
            IEnumerable<Product> productsInPage = productListViewModel.Products;

            // Assert
            Product[] actual = productsInPage.ToArray();
            Assert.AreEqual( 2, actual.Length );
            Assert.AreEqual( 5, actual[ 0 ].ProductID );
            Assert.AreEqual( "P5", actual[ 0 ].Name );
            Assert.AreEqual( 6, actual[ 1 ].ProductID );
            Assert.AreEqual( "P6", actual[ 1 ].Name );
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Arrange - Define HtmlHelper, at the moment we'd just instantiate it from current default type which takes null params on its contructor args
            HtmlHelper htmlHelper = null;

            // Arrange - Expected result
            StringBuilder expectedBuilder = new StringBuilder();
            expectedBuilder.Append( string.Format( "<a class=\"{0}\" href=\"{1}\">{2}</a>", "selected", buildPageUrl( 1 ), 1 ) );
            expectedBuilder.Append( string.Format( "<a href=\"{0}\">{1}</a>", buildPageUrl( 2 ), 2 ) );

            // Arrange - Create paging info 
            PagingInfo pagingInfo = new PagingInfo { CurrentPageIndex = 1, ItemsPerPage = 2, TotalItems = 4 };

            // Arrange - Setup the pageUrl delegate 
            Func<int, string> pageUrlDelegate = buildPageUrl;

            // Act:
            MvcHtmlString result = htmlHelper.PageLinks( pagingInfo, pageUrlDelegate );

            // Assert:
            String expected = expectedBuilder.ToString();
            String actual = result.ToString();
            Assert.AreEqual( expected, actual );
        }

        private static string buildPageUrl( int i )
        {
            return string.Format( "pageIndex={0}", i );
        }
    }
}
