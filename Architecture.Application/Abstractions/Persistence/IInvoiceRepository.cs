using Architecture.Application.Invoices.Dtos;
using Architecture.Domain.Entities;

namespace Architecture.Application.Abstractions.Persistence
{
    public interface IInvoiceRepository
    {
        /// <summary>
        /// Método para crear una nueva factura
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        Task<bool> CreateInvoice(InvoicesEntity invoice);
        /// <summary>
        /// Obtiene una factura por id para operaciones de negocio (ej: expedir).
        /// Incluye el detalle si el agregado lo requiere.
        /// </summary>
        Task<InvoicesEntity?> GetByIdAsync(int invoice_id);
        /// <summary>
        /// Devuelve una lista de facturas filtradas por los criterios especificados en el objeto GetInvoicesByFilters.
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<IEnumerable<InvoicesEntity>> GetFilteredAsync(GetInvoicesByFilters filters);
    }
}
