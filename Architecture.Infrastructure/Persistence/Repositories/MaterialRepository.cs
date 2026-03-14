using Architecture.Application.Abstractions.Persistence;
using Architecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Infrastructure.Persistence.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ArchitectureExerciseDBContext _dbContext;
        public MaterialRepository(ArchitectureExerciseDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CheckMaterialExistByCode(string materialCode)
            => await _dbContext.Materials.AnyAsync(x => x.MaterialCode == materialCode);

        public async Task<bool> CreateMaterial(MaterialsEntity material)
        {
            await _dbContext.Materials.AddAsync(material);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<MaterialsEntity>> GetAllAsync()
            => await _dbContext.Materials.ToListAsync();

        public async Task<MaterialsEntity?> GetByIdAsync(int material_id)
            => await _dbContext.Materials.FirstOrDefaultAsync(x => x.MaterialId == material_id);

        public async Task<bool> UpdateMaterial(MaterialsEntity material)
        {
            if (material is null || material.MaterialId <= 0)
                return false;

            var existing = await _dbContext.Set<MaterialsEntity>()
                .FirstOrDefaultAsync(x => x.MaterialId == material.MaterialId);

            if (existing is null)
                return false;

            existing.MaterialCode = material.MaterialCode?.Trim() ?? string.Empty;
            existing.MaterialName = material.MaterialName?.Trim() ?? string.Empty;
            existing.MaterialCost = material.MaterialCost;
            existing.ModificationDate = DateTime.UtcNow;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
