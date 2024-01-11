using Autofac.Extensions.DependencyInjection;
using Serilog;

namespace Microsoft.Extensions.Hosting
{
    internal static class HostBuilderExtensions
    {
        internal static IHostBuilder RegisterDefaults(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                              .UseSerilog((hostContext, serviceProvider, loggingBuilder) =>
                              {
                                  loggingBuilder
                                      .Enrich.FromLogContext()
                                      .MinimumLevel.Debug()
                                      //.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Debug)
                                      .ReadFrom.Configuration(hostContext.Configuration)
                                      .WriteTo.Console();
                              });
        }
    }
}
