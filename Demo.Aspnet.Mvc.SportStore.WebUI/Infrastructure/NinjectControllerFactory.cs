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
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _ninjectKernel;

        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance( RequestContext requestContext, Type controllerType )
        {
            return controllerType != null ? (IController)_ninjectKernel.Get( controllerType ) : null;
        }

        private void AddBindings()
        {
            // TODO: Add bindings here
            BindProductRepositoryMock();
        }

        private void BindProductRepositoryMock()
        {
            Mock<IProductRepository> mockedProductRepository = new Mock<IProductRepository>();
            mockedProductRepository.Setup( m => m.Products ).Returns( new List<Product>
                {
                    new Product{Name = "Football", Price =25},
                    new Product{Name = "Surf Board", Price =179},
                    new Product{Name = "Running Shoed", Price =95}
                }.AsQueryable() );

            _ninjectKernel.Bind<IProductRepository>().ToConstant( mockedProductRepository.Object );
        }
    }
}