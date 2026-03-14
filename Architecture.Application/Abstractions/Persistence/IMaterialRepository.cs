using Architecture.Domain.Entities;

namespace Architecture.Application.Abstractions.Persistence
{
    public interface IMaterialRepository
    {
        Task<bool> CreateMaterial(MaterialsEntity material);
        Task<bool> UpdateMaterial(MaterialsEntity material);
        Task<MaterialsEntity?> GetByIdAsync(int materialId);
        Task<bool> CheckMaterialExistByCode(string materialCode);
        Task<IEnumerable<MaterialsEntity>> GetAllAsync();
    }
}
