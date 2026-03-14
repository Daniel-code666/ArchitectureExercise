using Architecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Infrastructure.Persistence
{
    public sealed partial class ArchitectureExerciseDBContext
    {
        public DbSet<InvoicesEntity> Invoices { get; set; }
        public DbSet<MaterialsEntity> Materials { get; set; }
    }
}
