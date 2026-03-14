using Architecture.Domain.Entities.Invoices.Enums;

namespace Architecture.Application.Invoices.Dtos
{
    public class GetInvoicesByFilters
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int? InvoiceId { get; set; }
        public string? InvoiceCode { get; set; }
        public InvoiceStatus? InvoiceStatus { get; set; }
        public string? AssignedPersonName { get; set; }
        public string? Email { get; set; }
        public string? AssignedPersonIdNumber { get; set; }
        public decimal? TotalCostMin { get; set; }
        public decimal? TotalCostMax { get; set; }
        public DateTime? CreationDateFrom { get; set; }
        public DateTime? CreationDateTo { get; set; }
        public string? MaterialCode { get; set; }
        public string? MaterialName { get; set; }
        public int? MaterialId { get; set; }
        public string? SortBy { get; set; }
        public bool SortDesc { get; set; } = true;
        public bool UseContains { get; set; } = true;
        public bool IncludeDetails { get; set; } = false;
    }
}
