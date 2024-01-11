using MediatR;
using Autofac;
using AutoMapper;
using System.Reflection;

namespace Mc2.CrudTest.Application.DI.AutofacModules
{
    public sealed class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterMediatorTypes(builder);

            RegisterNotificationHandlers(builder);

            RegisterRequestHandlers(builder);

            RegisterAutoMapperWithProfiles(builder);
        }

        private void RegisterAutoMapperWithProfiles(ContainerBuilder builder)
        {
            // Register Automapper profiles
            var config = new MapperConfiguration(cfg => { cfg.AddMaps(ThisAssembly); });
            config.AssertConfigurationIsValid();

            builder.Register(c => config)
                .AsSelf()
                .SingleInstance();

            builder.Register(c =>
            {
                var ctx = c.Resolve<IComponentContext>();
                var mapperConfig = c.Resolve<MapperConfiguration>();
                return mapperConfig.CreateMapper(ctx.Resolve);
            }).As<IMapper>()
              .SingleInstance();
        }

        private void RegisterRequestHandlers(ContainerBuilder builder)
        {
            // Register the Command and Query handler classes (they implement IRequestHandler<TRequest> or IRequestHandler<TRequest,TResponse>)
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IRequestHandler<>));
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));
        }

        private void RegisterNotificationHandlers(ContainerBuilder builder)
        {
            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));
        }

        private static void RegisterMediatorTypes(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();
        }
    }
}
