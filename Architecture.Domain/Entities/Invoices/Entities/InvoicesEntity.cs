using Architecture.Domain.Entities.Invoices.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Architecture.Domain.Entities
{
    [Table("Invoices")]
    public class InvoicesEntity : AuditTable
    {
        #region Atributos

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }

        public InvoiceStatus InvoiceStatus { get; set; } = InvoiceStatus.Created;

        public string InvoiceCode { get; set; } = string.Empty;

        public string AssignedPersonName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string AssignedPersonIdNumber { get; set; } = string.Empty;

        public decimal TotalCost { get; set; }

        #endregion

        #region relaciones

        public virtual ICollection<InvoiceDetailsEntity> Details { get; set; } = [];

        #endregion
    }
}
