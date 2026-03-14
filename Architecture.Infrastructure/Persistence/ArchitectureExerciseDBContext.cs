using Architecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Infrastructure.Persistence
{
    public sealed partial class ArchitectureExerciseDBContext : DbContext
    {
        public ArchitectureExerciseDBContext(DbContextOptions<ArchitectureExerciseDBContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("data");

            ConfigureInvoices(modelBuilder);
            ConfigureInvoiceDetails(modelBuilder);
            ConfigureMaterials(modelBuilder);
        }

        private static void ConfigureInvoices(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoicesEntity>(entity =>
            {
                entity.ToTable("Invoices");

                entity.HasKey(e => e.InvoiceId);

                entity.Property(e => e.InvoiceId).ValueGeneratedOnAdd();

                entity.Property(e => e.InvoiceStatus).IsRequired();

                entity.Property(e => e.InvoiceCode).IsRequired().HasMaxLength(50);

                entity.Property(e => e.AssignedPersonName).IsRequired().HasMaxLength(200);

                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);

                entity.Property(e => e.AssignedPersonIdNumber).IsRequired().HasMaxLength(50);

                entity.Property(e => e.TotalCost).HasColumnType("decimal(18,2)").IsRequired();

                entity.HasMany(e => e.Details).WithOne(d => d.Invoice).HasForeignKey(d => d.InvoiceId).OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.InvoiceCode).IsUnique();
            });
        }

        private static void ConfigureInvoiceDetails(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceDetailsEntity>(entity =>
            {
                entity.ToTable("InvoiceDetails");

                entity.HasKey(e => e.InvoiceDetailId);

                entity.Property(e => e.InvoiceDetailId).ValueGeneratedOnAdd();

                entity.Property(e => e.InvoiceId).IsRequired();

                entity.Property(e => e.MaterialId).IsRequired(false);

                entity.Property(e => e.MaterialCode).IsRequired().HasMaxLength(50);

                entity.Property(e => e.MaterialName).IsRequired().HasMaxLength(200);

                entity.Property(e => e.Quantity).HasColumnType("decimal(18,2)").IsRequired();

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();

                entity.Property(e => e.LineTotal).HasColumnType("decimal(18,2)").IsRequired();

                entity.HasOne(e => e.Material).WithMany().HasForeignKey(e => e.MaterialId).OnDelete(DeleteBehavior.SetNull);

                entity.HasIndex(e => e.InvoiceId);
            });
        }

        private static void ConfigureMaterials(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MaterialsEntity>(entity =>
            {
                entity.ToTable("Materials");

                entity.HasKey(e => e.MaterialId);

                entity.Property(e => e.MaterialId).ValueGeneratedOnAdd();

                entity.Property(e => e.MaterialCode).IsRequired().HasMaxLength(50);

                entity.Property(e => e.MaterialName).IsRequired().HasMaxLength(200);

                entity.Property(e => e.MaterialCost).HasColumnType("decimal(18,2)").IsRequired();

                entity.HasIndex(e => e.MaterialCode).IsUnique();
            });
        }
    }
}
