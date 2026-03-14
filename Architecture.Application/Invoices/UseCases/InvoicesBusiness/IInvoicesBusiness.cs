using Architecture.Application.Invoices.Dtos;
using Architecture.Domain.Entities.Base.Enums;

namespace Architecture.Application.Invoices.UseCases.InvoicesBusiness
{
    public interface IInvoicesBusiness
    {
        Task<IEnumerable<InvoiceReadDto>> GetFilteredAsync(GetInvoicesByFilters filters);
        Task<InvoiceReadDto?> GetByIdAsync(int invoiceId);
        Task<DbActions> CreateAsync(InvoiceDto invoice);
    }
}
