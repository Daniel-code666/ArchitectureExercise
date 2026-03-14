using Architecture.Application.Abstractions.Persistence;
using Architecture.Application.Materials.Dtos;
using Architecture.Domain.Entities;
using Architecture.Domain.Entities.Base.Enums;
using AutoMapper;

namespace Architecture.Application.Materials.UseCases.MaterialBusiness
{
    public class MaterialBusiness : IMaterialBusiness
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;
        public MaterialBusiness(IMapper mapper, IMaterialRepository materialRepository)
        {
            _mapper = mapper;
            _materialRepository = materialRepository;
        }

        public async Task<DbActions> CreateAsync(MaterialDto dto)
        {
            if (dto is null)
                return DbActions.NotCreated;

            var code = (dto.MaterialCode ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(code))
                return DbActions.NotCreated;

            if (await _materialRepository.CheckMaterialExistByCode(code))
                return DbActions.NotCreated;

            MaterialsEntity entity = _mapper.Map<MaterialsEntity>(dto);

            return await _materialRepository.CreateMaterial(entity) ? DbActions.Created : DbActions.NotCreated;
        }

        public async Task<DbActions> UpdateAsync(int material_id, MaterialDto dto)
        {
            if (dto is null || material_id <= 0)
                return DbActions.NotUpdated;

            var existing = await _materialRepository.GetByIdAsync(material_id);
            if (existing is null)
                return DbActions.NotUpdated;

            var new_code = (dto.MaterialCode ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(new_code))
                return DbActions.NotUpdated;

            if (!string.Equals(existing.MaterialCode, new_code, StringComparison.OrdinalIgnoreCase) && await _materialRepository.CheckMaterialExistByCode(new_code))
                return DbActions.NotUpdated;

            MaterialsEntity entity = _mapper.Map<MaterialsEntity>(dto);
            entity.MaterialId = material_id;

            return await _materialRepository.UpdateMaterial(existing) ? DbActions.Updated : DbActions.NotUpdated;
        }

        public async Task<MaterialReadDto?> GetByIdAsync(int material_id)
            => _mapper.Map<MaterialReadDto?>(await _materialRepository.GetByIdAsync(material_id));

        public async Task<IEnumerable<MaterialReadDto>> GetAllAsync()
            => _mapper.Map<IEnumerable<MaterialReadDto>>(await _materialRepository.GetAllAsync());
    }
}
