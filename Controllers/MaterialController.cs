using Architecture.Application.Materials.Dtos;
using Architecture.Application.Materials.UseCases.MaterialBusiness;
using Architecture.Domain.Entities.Base.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectureExercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialBusiness _materialBusiness;

        public MaterialController(IMaterialBusiness materialBusiness)
        {
            _materialBusiness = materialBusiness;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialReadDto>>> GetAllAsync()
            => Ok(await _materialBusiness.GetAllAsync());

        [HttpGet("{material_id:int}")]
        public async Task<ActionResult<MaterialReadDto>> GetByIdAsync([FromRoute] int material_id)
        {
            var result = await _materialBusiness.GetByIdAsync(material_id);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DbActions>> CreateAsync([FromBody] MaterialDto dto)
            => Ok(await _materialBusiness.CreateAsync(dto));

        [HttpPut("{material_id:int}")]
        public async Task<ActionResult<DbActions>> UpdateAsync([FromRoute] int material_id, [FromBody] MaterialDto dto)
            => Ok(await _materialBusiness.UpdateAsync(material_id, dto));
    }
}
