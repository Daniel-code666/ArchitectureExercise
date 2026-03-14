using Architecture.Application.Common;
using Architecture.Application.Invoices.UseCases.InvoicesBusiness;
using Architecture.Application.Materials.UseCases.MaterialBusiness;
using Microsoft.Extensions.DependencyInjection;

namespace Architecture.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services.AddAutoMapper(cfg => { }, typeof(ApplicationAppProfile).Assembly)
                .AddScoped<IInvoicesBusiness, InvoiceBusiness>()
                .AddScoped<IMaterialBusiness, MaterialBusiness>();
    }
}
