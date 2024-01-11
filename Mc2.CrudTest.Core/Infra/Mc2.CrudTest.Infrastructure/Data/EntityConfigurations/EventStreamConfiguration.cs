using Mc2.CrudTest.Domain.Abstractions.Models;
using Mc2.CrudTest.Infrastructure.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Mc2.CrudTest.Infrastructure.Data.EntityConfigurations;

internal class EventStreamConfiguration : IEntityTypeConfiguration<EventStream>
{
    public void Configure(EntityTypeBuilder<EventStream> builder)
    {
        builder.Property(e => e.AggregateType)
               .HasColumnType("varchar(64)")
               .IsRequired();

        builder.Property(p => p.Version)
        .IsConcurrencyToken();

        var serializerOptions = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ContractResolver = PrivatePropertyResolver.Instance
        };

        Func<List<Event>, List<Event>, bool> equalityComparer = (left, right) =>
        {
            if (left.Count != right.Count)
            {
                return false;
            }
            for (int i = 0; i < left.Count; i++)
            {
                if (right[i].DomainEvent.EventId != left[i].DomainEvent.EventId)
                {
                    return false;
                }
            }
            return true;
        };

        Func<List<Event>, int> hashCodeGenerator = (events) =>
        {
            if (events == null || events.Count == 0)
            {
                return 0;
            }
            var hashcode = new HashCode();
            for (int i = 0; i < events.Count; i++)
            {
                hashcode.Add(events[i].DomainEvent.EventId);
            }
            return hashcode.ToHashCode();
        };

        var comparer = new ValueComparer<List<Event>>(
            (l, r) => equalityComparer.Invoke(l, r),
            events => hashCodeGenerator.Invoke(events),
            v => JsonConvert.DeserializeObject<List<Event>>(JsonConvert.SerializeObject(v, serializerOptions), serializerOptions)!);

        var converter = new ValueConverter<List<Event>, string>(
            v => JsonConvert.SerializeObject(v, serializerOptions),
            v => JsonConvert.DeserializeObject<List<Event>>(v, serializerOptions)!);


        var eventsProperty = builder.Property(e => e.Events)
                                    .IsUnicode(false)
                                    .HasConversion(converter)
                                    .IsRequired();

        eventsProperty.Metadata.SetValueConverter(converter);
        eventsProperty.Metadata.SetValueComparer(comparer);
    }
}
