using Architecture.Application.Invoices.Dtos;
using Architecture.Application.Invoices.UseCases.InvoicesBusiness;
using Architecture.Domain.Entities.Base.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectureExercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoicesBusiness _invoicesBusiness;

        public InvoicesController(IInvoicesBusiness invoicesBusiness)
        {
            _invoicesBusiness = invoicesBusiness;
        }

        /// <summary>
        /// Devuelve una lista de facturas filtrada por los parámetros indicados en el body. Si no se indican parámetros, devuelve todas las facturas.
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpPost("filtered")]
        public async Task<ActionResult<IEnumerable<InvoiceReadDto>>> GetFiltered([FromBody] GetInvoicesByFilters filters)
            => Ok(await _invoicesBusiness.GetFilteredAsync(filters));

        /// <summary>
        /// Crea una nueva factura con los datos indicados en el body. Devuelve un enum indicando si la factura se ha creado correctamente, 
        /// si no se ha creado o si no se han encontrado los datos necesarios para crear la factura.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ActionResult<DbActions>> CreateInvoice([FromBody] InvoiceDto invoice)
            => Ok(await _invoicesBusiness.CreateAsync(invoice));

        /// <summary>
        /// Devuelve una factura por su id. Si no se encuentra la factura, devuelve 404
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<InvoiceReadDto>> GetById(int id)
        {
            var invoice = await _invoicesBusiness.GetByIdAsync(id);
            if (invoice is null)
                return NotFound();
            return Ok(invoice);
        }
    }
}
