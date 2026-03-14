namespace Architecture.Application.Invoices.Dtos
{
    public class InvoiceDetailDto
    {
        public string MaterialCode { get; set; } = string.Empty;
        public string MaterialName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public int? MaterialId { get; set; }
    }

    public class InvoiceDetailReadDto : InvoiceDetailDto
    {
        public int InvoiceDetailId { get; set; }
        public int InvoiceId { get; set; }
    }
}
