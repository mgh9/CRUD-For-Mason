using Autofac;
using IbanNet.DependencyInjection;
using IbanNet.DependencyInjection.Autofac;
using Mc2.CrudTest.Application.Abstractions.Repositories;
using Mc2.CrudTest.Domain.Abstractions.ExternalServices;
using Mc2.CrudTest.Infrastructure.Data;
using Mc2.CrudTest.Infrastructure.Data.Options;
using Mc2.CrudTest.Infrastructure.Data.Repositories;
using Mc2.CrudTest.Infrastructure.Data.Repositories.EventStore;
using Mc2.CrudTest.Infrastructure.ExternalServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Mc2.CrudTest.Infrastructure.DI.AutofacModules
{
    public sealed class InfrastructureModule : Module
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly IConfiguration Configuration;

        public InfrastructureModule(IConfiguration configuration) : this(CreateDbOptions(configuration), configuration)
        {

        }

        public InfrastructureModule(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        {
            Configuration = configuration;
            _options = options;
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterApplicationSettings(builder);

            RegisterPersistentServices(builder);

            //builder.RegisterType<NotificationsService>()
            //    .AsImplementedInterfaces()
            //    .SingleInstance();

            RegisterExternalServices(builder);
        }

        private void RegisterApplicationSettings(ContainerBuilder builder)
        {
            builder.RegisterInstance(Options.Create(DatabaseOptions.Create(Configuration)));
        }

        private void RegisterPersistentServices(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>()
                .AsSelf()
                .InstancePerRequest()
                .InstancePerLifetimeScope()
                .WithParameter(new NamedParameter("options", _options));

            builder.RegisterType<UnitOfWork>()
                .AsImplementedInterfaces()
                .InstancePerRequest()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>));

            builder.RegisterGeneric(typeof(EventStreamRepository<>))
                .As(typeof(IEventStreamRepository<>))
                .InstancePerRequest()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(ReadModelRepository<>))
                .As(typeof(IReadModelRepository<>));

            builder.RegisterType<EntityFrameworkEventStore>()
                .AsImplementedInterfaces();
        }

        private static void RegisterExternalServices(ContainerBuilder builder)
        {
            builder.RegisterType<GooglePhoneNumberValidator>()
                    .As<IPhoneNumberValidator>()
                    .SingleInstance();

            // for validating IBAN (International Bank account number)
            builder.RegisterIbanNet();
            builder.RegisterType<BankAccountNumberValidator>()
                    .As<IBankAccountNumberValidator>()
                    .SingleInstance();
        }

        private static DbContextOptions<ApplicationDbContext> CreateDbOptions(IConfiguration configuration)
        {
            var databaseSettings = DatabaseOptions.Create(configuration);
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(databaseSettings.SqlConnectionString);
            return builder.Options;
        }
    }
}
