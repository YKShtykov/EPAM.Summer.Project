using System;
using System.Collections.Generic;
using Ninject;
using System.Web.Mvc;
using DependencyResolver;

namespace MvcApp.Infrastructure
{
    /// <summary>
    /// Class for dependency resolver configuration
    /// </summary>
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        /// <summary>
        /// Create dependency resolver
        /// </summary>
        /// <param name="kernel"></param>
        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            kernel.ConfigurateResolver();
        }

        /// <summary>
        /// Get service
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        /// <summary>
        /// Get services list
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}