using Mc2.CrudTest.Domain.Abstractions.Models;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    private static readonly ILoggerFactory DebugLoggerFactory = new LoggerFactory(new[]
    {
        new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
    });

    private readonly IHostEnvironment? _env;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHostEnvironment? env)
        : base(options)
    {
        _env = env;
    }

    public DbSet<CustomerReadModel> Customers { get; set; }
    public DbSet<EventStream> EventStreams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_env != null && _env.IsDevelopment())
        {
            // used to print activity when debugging
            optionsBuilder.UseLoggerFactory(DebugLoggerFactory);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        MakeAggregateTypesDontAutoGenerateIds(modelBuilder);
        MakeReadModelTypesDontAutoGenerateIds(modelBuilder);
    }

    private static void MakeAggregateTypesDontAutoGenerateIds(ModelBuilder modelBuilder)
    {
        var aggregateTypes = modelBuilder.Model
                                         .GetEntityTypes()
                                         .Select(e => e.ClrType)
                                         .Where(e => !e.IsAbstract && e.IsAssignableTo(typeof(AggregateRoot)));

        foreach (var type in aggregateTypes)
        {
            var aggregateBuilder = modelBuilder.Entity(type);

            aggregateBuilder.Ignore(nameof(AggregateRoot.DomainEvents));
            aggregateBuilder.Property(nameof(AggregateRoot.Id)).ValueGeneratedNever();
        }
    }

    private static void MakeReadModelTypesDontAutoGenerateIds(ModelBuilder modelBuilder)
    {
        var readModelTypes = modelBuilder.Model
                                                 .GetEntityTypes()
                                                 .Select(e => e.ClrType)
                                                 .Where(e => !e.IsAbstract && e.IsAssignableTo(typeof(BaseReadModel)));

        foreach (var type in readModelTypes)
        {
            var readModelBuilder = modelBuilder.Entity(type);
            readModelBuilder.Property(nameof(BaseReadModel.Id)).ValueGeneratedNever();
        }
    }
}
