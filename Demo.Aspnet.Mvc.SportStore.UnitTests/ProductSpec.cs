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
            mockedProductRepository.Setup(m => m.Products).Returns(new[]
                {
                    new Product{ProductID = 1, Name = "P1"}, 
                    new Product{ProductID = 2, Name = "P2"}, 
                    new Product{ProductID = 3, Name = "P3"}, 
                    new Product{ProductID = 4, Name = "P4"}, 
                    new Product{ProductID = 5, Name = "P5"}, 
                    new Product{ProductID = 6, Name = "P6"}
                }.AsQueryable());

            // Instantiate Product Controller and pass the mocked Product Repository into the Product Controller's constructor argument
            var productController = getProductController(mockedProductRepository);

            // Act: Calls the Product Controller's List method 
            const int page = 2;
            ProductListViewModel productListViewModel = (productController.List(null, page).Model as ProductListViewModel);
            Assert.IsNotNull(productListViewModel);
            IEnumerable<Product> productsInPage = productListViewModel.Products;

            // Assert
            Product[] actual = productsInPage.ToArray();
            Assert.AreEqual(2, actual.Length);
            Assert.AreEqual(5, actual[0].ProductID);
            Assert.AreEqual("P5", actual[0].Name);
            Assert.AreEqual(6, actual[1].ProductID);
            Assert.AreEqual("P6", actual[1].Name);
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Arrange - Define HtmlHelper, at the moment we'd just instantiate it from current default type which takes null params on its contructor args
            HtmlHelper htmlHelper = null;

            // Arrange - Expected result
            StringBuilder expectedBuilder = new StringBuilder();
            expectedBuilder.Append(string.Format("<a class=\"{0}\" href=\"{1}\">{2}</a>", "selected", buildPageUrl(1), 1));
            expectedBuilder.Append(string.Format("<a href=\"{0}\">{1}</a>", buildPageUrl(2), 2));

            // Arrange - Create paging info 
            PagingInfo pagingInfo = new PagingInfo { CurrentPageIndex = 1, ItemsPerPage = 2, TotalItems = 4 };

            // Arrange - Setup the pageUrl delegate 
            Func<int, string> pageUrlDelegate = buildPageUrl;

            // Act:
            MvcHtmlString result = htmlHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Assert:
            String expected = expectedBuilder.ToString();
            String actual = result.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            // Arrange - Mock the products repository
            Mock<IProductRepository> mockedProductRepository = new Mock<IProductRepository>();
            mockedProductRepository.Setup(m => m.Products).Returns(new[]
                {
                    new Product{ProductID = 1, Category = "C1", Name = "P1"}, 
                    new Product{ProductID = 2, Category = "C2", Name = "P2"}, 
                    new Product{ProductID = 3, Category = "C1", Name = "P3"}, 
                    new Product{ProductID = 4, Category = "C2", Name = "P4"}, 
                    new Product{ProductID = 5, Category = "C3", Name = "P5"}, 
                    new Product{ProductID = 6, Category = "C2", Name = "P6"}
                }.AsQueryable());

            // Instantiate Product Controller and pass the mocked Product Repository into the Product Controller's constructor argument
            var productController = getProductController(mockedProductRepository);

            // Act - Calls the Product Controller's List method to return P2 products 
            const int page = 1;
            const string categoryP1 = "C2";
            var productsInPage = getProductsList(productController, categoryP1, page);

            // Assert - P1 products
            Assert.IsFalse(productsInPage.Any(p => p.Category == "C1" && (p.Name == "P1" || p.Name == "P3")));
            Assert.IsTrue(productsInPage.Any(p => p.Category == "C2" && (p.Name == "P2" || p.Name == "P4")));
            Assert.IsFalse(productsInPage.Any(p => p.Category == "C3" && p.Name == "P5"));
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            // Arrange - create mocked Product repository
            Mock<IProductRepository> mockedProductRepository = new Mock<IProductRepository>();
            mockedProductRepository.Setup(m => m.Products).Returns(new[]
                {
                    new Product{ProductID = 1, Name = "P1", Category = "C1"},
                    new Product{ProductID = 1, Name = "P2", Category = "C1"},
                    new Product{ProductID = 1, Name = "P3", Category = "C2"},
                    new Product{ProductID = 1, Name = "P4", Category = "C3"}
                }.AsQueryable());

            // Arrange - Create the controller
            NavController navController = new NavController(mockedProductRepository.Object);

            // Act - get the set of categories
            IEnumerable<string> products = navController.Menu().Model as IEnumerable<string>;

            // Assert
            Assert.IsNotNull(products);
            CollectionAssert.AreEqual(new[] { "C1", "C2", "C3" }, products.ToArray());
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Arrange - create ProductRepository Mock
            Mock<IProductRepository> mockedProductRepository = new Mock<IProductRepository>();
            mockedProductRepository.Setup(m => m.Products).Returns(new[]
                {
                    new Product{ProductID = 1, Name = "P1", Category = "C1"},
                    new Product{ProductID = 2, Name = "P2", Category = "C2"}
                }.AsQueryable());

            // Arrange - Create controller
            NavController navController = new NavController(mockedProductRepository.Object);

            // Act - Calls Controller's Menu action
            const string selectedCategory = "C2";
            string actualSelectedCategory = navController.Menu(selectedCategory).ViewBag.SelectedCategory;

            // Assert
            Assert.AreEqual(selectedCategory, actualSelectedCategory);
        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            // Arrange - Create a Mocked Product repository
            Mock<IProductRepository> mockedProductRepository = new Mock<IProductRepository>();
            mockedProductRepository.Setup(m => m.Products).Returns(new[]
                {
                    new Product{ProductID = 1, Name = "P1", Category = "C1"},
                    new Product{ProductID = 1, Name = "P2", Category = "C2"},
                    new Product{ProductID = 1, Name = "P3", Category = "C1"},
                    new Product{ProductID = 1, Name = "P4", Category = "C2"},
                    new Product{ProductID = 1, Name = "P5", Category = "C1"},
                    new Product{ProductID = 1, Name = "P6", Category = "C3"}
                }.AsQueryable());

            // Arrange - Create controller
            ProductController controller = new ProductController(mockedProductRepository.Object);

            // Act
            int c1TotalItems = getTotalItemsByCategoryName(controller, "C1");
            int c2TotalItems = getTotalItemsByCategoryName(controller, "C2");
            int c3TotalItems = getTotalItemsByCategoryName(controller, "C3");

            // Assert
            Assert.AreEqual(3, c1TotalItems);
            Assert.AreEqual(2, c2TotalItems);
            Assert.AreEqual(1, c3TotalItems);
        }

        #region Helpers
        private static string buildPageUrl(int i)
        {
            return string.Format("pageIndex={0}", i);
        }

        private static ProductController getProductController(Mock<IProductRepository> mockedProductRepository)
        {
            return new ProductController(mockedProductRepository.Object);
        }

        private static IEnumerable<Product> getProductsList(ProductController productController, string category, int page)
        {
            ProductListViewModel productListViewModel = (productController.List(category, page).Model as ProductListViewModel);
            Assert.IsNotNull(productListViewModel);
            return productListViewModel.Products;
        }

        private static int getTotalItemsByCategoryName(ProductController controller, string categoryName)
        {
            var productListViewModel = controller.List(categoryName).Model as ProductListViewModel;
            return productListViewModel != null ? productListViewModel.PagingInfo.TotalItems : 0;
        }

        #endregion

    }
}
