using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.Entities;
using Mc2.CrudTest.Domain.Aggregates.CustomerAggregate.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc2.CrudTest.Infrastructure.Data.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<CustomerReadModel>
    {
        public void Configure(EntityTypeBuilder<CustomerReadModel> builder)
        {
            builder.Property(p => p.FirstName)
                    .HasMaxLength(50);

            builder.Property(p => p.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.HasIndex(x =>
                                    new
                                    {
                                        x.FirstName,
                                        x.LastName,
                                        x.DateOfBirth 
                                        /* for dateOfBirth, we just need the `date` part and not the full datetime */
                                    })
                        .IsUnique();

            builder.OwnsOne(e => e.Email, childBuilder =>
            {
                childBuilder.Property(e => e.Value)
                                .HasColumnName(nameof(Customer.Email))
                                .HasColumnType("varchar(64)")
                                .IsRequired();

                childBuilder.HasIndex(e => e.Value)
                                .IsUnique();
            });

            builder.OwnsOne(e => e.PhoneNumber, childBuilder =>
            {
                childBuilder.Property(e => e.Value)
                                .HasColumnName(nameof(Customer.PhoneNumber))
                                .HasColumnType("varchar(20)")
                                .IsRequired();

                childBuilder.HasIndex(e => e.Value)
                                .IsUnique();
            });

            builder.OwnsOne(e => e.BankAccountNumber, childBuilder =>
            {
                childBuilder.Property(e => e.Value)
                    .HasColumnName(nameof(Customer.BankAccountNumber))
                    .HasColumnType("varchar(64)")
                    .IsRequired();

                childBuilder.HasIndex(e => e.Value)
                                .IsUnique();
            });

            builder.Property(c => c.DateOfBirth)
                    .IsRequired()
                    .HasColumnType("date");
        }
    }
}
