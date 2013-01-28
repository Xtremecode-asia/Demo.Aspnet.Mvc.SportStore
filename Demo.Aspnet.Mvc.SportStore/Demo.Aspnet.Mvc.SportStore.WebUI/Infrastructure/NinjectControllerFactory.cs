using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Demo.Aspnet.Mvc.SportStore.Domain.Abstract;
using Demo.Aspnet.Mvc.SportStore.Domain.Entities;
using Moq;
using Ninject;

namespace Demo.Aspnet.Mvc.SportStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory: DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            // TODO: Add bindings here
            Mock<IProductRepository> mockedProductRepository = new Mock<IProductRepository>();
            mockedProductRepository.Setup(m => m.Products).Returns(new List<Product>
                {
                    new Product{Name = "Football", Price =25},
                    new Product{Name = "Surf Board", Price =179},
                    new Product{Name = "Running Shoed", Price =95}
                }.AsQueryable());


        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType != null ? (IController) ninjectKernel.Get(controllerType) : null;
        }
    }
}