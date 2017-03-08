using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Moq;
using SportStore.Domain.Entities;
using SportStore.Domain.Abstract;

namespace SportStoreWebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBinding();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }


        /// <summary>
        /// Binding dependency
        /// </summary>
        private void AddBinding()
        {
            Mock<IProductRepository> mockObject = new Mock<IProductRepository>();
            mockObject.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product {Name="Football", Price=19.95M },
                new Product {Name="Surf board", Price=179 },
                new Product {Name="Running shoes", Price=95 }
            });

            kernel.Bind<IProductRepository>().ToConstant(mockObject.Object);
        }
    }
}