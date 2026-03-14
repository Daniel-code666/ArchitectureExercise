using Architecture.Application.Abstractions.Persistence;
using Architecture.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace Architecture.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
            => services.AddScoped<IInvoiceRepository, InvoiceRepository>()
                .AddScoped<IMaterialRepository, MaterialRepository>();
    }
}
