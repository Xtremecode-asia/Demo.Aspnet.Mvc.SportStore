using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

using Demo.Aspnet.Mvc.SportStore.Domain.Abstract;
using Demo.Aspnet.Mvc.SportStore.Domain.Concrete;
using Demo.Aspnet.Mvc.SportStore.Domain.Entities;
using Moq;
using Ninject;

namespace Demo.Aspnet.Mvc.SportStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _ninjectKernel = new StandardKernel();

        public NinjectControllerFactory()
        {
            AddBindings();
        }

        protected override IController GetControllerInstance( RequestContext requestContext, Type controllerType )
        {
            return controllerType != null ? (IController)_ninjectKernel.Get( controllerType ) : null;
        }

        private void AddBindings()
        {
            // -- Add bindings here
            //BindProductRepositoryMock();
            _ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
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