using Architecture.Application.Abstractions.Persistence;
using Architecture.Application.Common;
using Architecture.Application.Invoices.Dtos;
using Architecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Architecture.Infrastructure.Persistence.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ArchitectureExerciseDBContext _dbContext;

        private static readonly Dictionary<string, Expression<Func<InvoicesEntity, object>>> invoiceSort = SortUtilities.BuildSortMap<InvoicesEntity>(nameof(InvoicesEntity.Details));

        public InvoiceRepository(ArchitectureExerciseDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateInvoice(InvoicesEntity invoice)
        {
            await _dbContext.Invoices.AddAsync(invoice);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<InvoicesEntity?> GetByIdAsync(int invoice_id)
            => await _dbContext.Invoices.Include(x => x.Details).Where(x => x.InvoiceId == invoice_id).FirstOrDefaultAsync();

        public async Task<IEnumerable<InvoicesEntity>> GetFilteredAsync(GetInvoicesByFilters filters)
        {
            IQueryable<InvoicesEntity> query = _dbContext.Invoices.AsQueryable();

            bool filter_by_material = HasMaterialFilters(filters);

            query = query
                .ApplyIf(filters.IncludeDetails || filter_by_material, q => q.Include(x => x.Details))
                .ApplyIf(filters.InvoiceId.HasValue, q => q.Where(x => x.InvoiceId == filters.InvoiceId!.Value))
                .ApplyStringFilter(x => x.InvoiceCode, filters.InvoiceCode, filters.UseContains)
                .ApplyIf(filters.InvoiceStatus.HasValue, q => q.Where(x => x.InvoiceStatus == filters.InvoiceStatus!.Value))
                .ApplyStringFilter(x => x.AssignedPersonName, filters.AssignedPersonName, filters.UseContains)
                .ApplyStringFilter(x => x.Email, filters.Email, filters.UseContains)
                .ApplyStringFilter(x => x.AssignedPersonIdNumber, filters.AssignedPersonIdNumber, filters.UseContains)
                .ApplyMin(filters.TotalCostMin, x => x.TotalCost)
                .ApplyMax(filters.TotalCostMax, x => x.TotalCost)
                .ApplyMin(filters.CreationDateFrom, x => x.CreationDate)
                .ApplyMax(filters.CreationDateTo, x => x.CreationDate);

            query = ApplyMaterialFilters(query, filters);

            query = query.ApplySorting(filters.SortBy, filters.SortDesc, invoiceSort, default_key: "creation_date").ApplyPaging(filters.Page, filters.PageSize);

            return await query.ToListAsync();
        }

        #region private
        private static IQueryable<InvoicesEntity> ApplyMaterialFilters(IQueryable<InvoicesEntity> query, GetInvoicesByFilters filters)
        {
            if (filters.MaterialId.HasValue)
            {
                int material_id = filters.MaterialId.Value;
                query = query.Where(x => x.Details.Any(d => d.MaterialId == material_id));
            }

            if (!string.IsNullOrWhiteSpace(filters.MaterialCode))
            {
                string material_code = filters.MaterialCode.Trim();
                query = filters.UseContains
                    ? query.Where(x => x.Details.Any(d => d.MaterialCode != null && d.MaterialCode.Contains(material_code)))
                    : query.Where(x => x.Details.Any(d => d.MaterialCode == material_code));
            }

            if (!string.IsNullOrWhiteSpace(filters.MaterialName))
            {
                string material_name = filters.MaterialName.Trim();
                query = filters.UseContains
                    ? query.Where(x => x.Details.Any(d => d.MaterialName != null && d.MaterialName.Contains(material_name)))
                    : query.Where(x => x.Details.Any(d => d.MaterialName == material_name));
            }

            return query;
        }

        private static bool HasMaterialFilters(GetInvoicesByFilters filters)
            => filters.MaterialId.HasValue || !string.IsNullOrWhiteSpace(filters.MaterialCode) || !string.IsNullOrWhiteSpace(filters.MaterialName);

        #endregion
    }
}
