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

        /// <summary>
        /// Devuelve una lista de materiales. Si no hay materiales, devuelve una lista vacía.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialReadDto>>> GetAllAsync()
            => Ok(await _materialBusiness.GetAllAsync());

        /// <summary>
        /// Devuelve un material por su ID. Si el material no existe, devuelve NotFound (404).
        /// </summary>
        /// <param name="material_id"></param>
        /// <returns></returns>
        [HttpGet("{material_id:int}")]
        public async Task<ActionResult<MaterialReadDto>> GetByIdAsync([FromRoute] int material_id)
        {
            var result = await _materialBusiness.GetByIdAsync(material_id);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo material.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<DbActions>> CreateAsync([FromBody] MaterialDto dto)
            => Ok(await _materialBusiness.CreateAsync(dto));

        /// <summary>
        /// Actualiza un material existente por su ID.
        /// </summary>
        /// <param name="material_id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{material_id:int}")]
        public async Task<ActionResult<DbActions>> UpdateAsync([FromRoute] int material_id, [FromBody] MaterialDto dto)
            => Ok(await _materialBusiness.UpdateAsync(material_id, dto));
    }
}
