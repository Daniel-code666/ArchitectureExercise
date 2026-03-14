using System.ComponentModel.DataAnnotations.Schema;

namespace Architecture.Domain.Entities
{
    public class AuditTable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime ModificationDate { get; set; }
    }
}
