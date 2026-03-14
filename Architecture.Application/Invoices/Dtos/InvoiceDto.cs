using Architecture.Domain.Entities.Invoices.Enums;

namespace Architecture.Application.Invoices.Dtos
{
    public class InvoiceDto
    {
        public InvoiceStatus InvoiceStatus { get; set; } = InvoiceStatus.Created;
        public string InvoiceCode { get; set; } = string.Empty;
        public string AssignedPersonName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AssignedPersonIdNumber { get; set; } = string.Empty;
        public decimal TotalCost { get; set; }
        public List<InvoiceDetailDto> Details { get; set; } = [];
    }

    public class InvoiceReadDto : InvoiceDto
    {
        public int InvoiceId { get; set; }
    }
}
