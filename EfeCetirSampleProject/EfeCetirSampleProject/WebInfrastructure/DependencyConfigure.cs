using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Infrastructure.Core.Tools;
using Infrastructure.Data;
using Infrastructure.Entity;
using Infrastructure.Service;

namespace EfeCetirSampleProject.WebInfrastructure
{
    public class DependencyConfigure
    {
        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            DependencyResolver.SetResolver(
                new Dependency(RegisterServices(builder))
                );
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(
                typeof(MvcApplication).Assembly
                ).PropertiesAutowired();

            //deal with your dependencies here
            builder.RegisterType<DataContext>().As<IDbContext>().InstancePerDependency();
            builder.RegisterGeneric(typeof(RepositoryService<>)).As(typeof(IRepository<>));
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            return builder.Build();
        }
    }
}