using Autofac;
using MyStore.Core.Domain.Repositories;
using MyStore.Infrastructure.EF.Repositories;

namespace MyStore.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(InfrastructureModule).Assembly)
                .AsImplementedInterfaces();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
        }
    }
}