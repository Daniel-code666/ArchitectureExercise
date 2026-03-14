using Architecture.Application.Abstractions.Persistence;
using Architecture.Application.Invoices.Dtos;
using Architecture.Domain.Entities;
using Architecture.Domain.Entities.Base.Enums;
using AutoMapper;

namespace Architecture.Application.Invoices.UseCases.InvoicesBusiness
{
    public class InvoiceBusiness : IInvoicesBusiness
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public InvoiceBusiness(IMapper mapper, IInvoiceRepository invoiceRepository)
        {
            _mapper = mapper;
            _invoiceRepository = invoiceRepository;
        }

        public async Task<IEnumerable<InvoiceReadDto>> GetFilteredAsync(GetInvoicesByFilters filters)
        => _mapper.Map<IEnumerable<InvoiceReadDto>>(await _invoiceRepository.GetFilteredAsync(filters));

        public async Task<InvoiceReadDto?> GetByIdAsync(int invoiceId)
            => _mapper.Map<InvoiceReadDto?>(await _invoiceRepository.GetByIdAsync(invoiceId));

        public async Task<DbActions> CreateAsync(InvoiceDto invoice)
        {
            var entity = _mapper.Map<InvoicesEntity>(invoice);
            return await _invoiceRepository.CreateInvoice(entity) ? DbActions.Created : DbActions.NotCreated;
        }
    }
}
