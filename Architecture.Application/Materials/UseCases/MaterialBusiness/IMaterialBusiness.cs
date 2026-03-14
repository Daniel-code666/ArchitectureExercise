using Architecture.Application.Materials.Dtos;
using Architecture.Domain.Entities.Base.Enums;

namespace Architecture.Application.Materials.UseCases.MaterialBusiness
{
    public interface IMaterialBusiness
    {
        Task<DbActions> CreateAsync(MaterialDto dto);
        Task<DbActions> UpdateAsync(int material_id, MaterialDto dto);
        Task<MaterialReadDto?> GetByIdAsync(int material_id);
        Task<IEnumerable<MaterialReadDto>> GetAllAsync();
    }
}
