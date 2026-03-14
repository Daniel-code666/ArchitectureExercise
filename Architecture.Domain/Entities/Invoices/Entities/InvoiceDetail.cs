using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Architecture.Domain.Entities
{
    [Table("InvoiceDetails")]
    public class InvoiceDetailsEntity : AuditTable
    {
        #region atributos

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceDetailId { get; set; }
        public string MaterialCode { get; set; } = string.Empty;
        public string MaterialName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }

        #endregion

        #region relaciones

        public int InvoiceId { get; set; }
        [ForeignKey(nameof(InvoiceId))]
        public InvoicesEntity Invoice { get; set; } = null!;

        public int? MaterialId { get; set; }
        [ForeignKey(nameof(MaterialId))]
        public MaterialsEntity? Material { get; set; }

        #endregion
    }

}
