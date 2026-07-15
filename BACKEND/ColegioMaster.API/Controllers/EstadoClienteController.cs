using ColegioMaster.DtoModels.Comp;
using ColegioMaster.DtoModels.EstadoCliente;
using ColegioMaster.Negocio.EstadoCliente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMaster.API.Controllers
{
    /// <summary>
    /// Administrar estado cliente
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] /* => si no tienes token valido / no ingresas*/
    public class EstadoClienteController : ControllerBase
    {
        private readonly IEstadoClienteService _estadoClienteService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="estadoClienteService"></param>
        public EstadoClienteController(IEstadoClienteService estadoClienteService)
        {
            _estadoClienteService = estadoClienteService;
        }

        /// <summary>
        /// Obtiene todos los estados de cliente.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<GeneralResponse<List<EstadoClienteDto>>>> GetAllEstadoCliente()
        {
            List<EstadoClienteDto> estados = await _estadoClienteService.GetAll();
            GeneralResponse<List<EstadoClienteDto>> response = new GeneralResponse<List<EstadoClienteDto>>
            {
                Content = estados,
                Success = true,
                Message = "Estados de cliente obtenidos correctamente"
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un estado de cliente por ID.
        /// </summary>
        /// <param name="id">ID es el identificador (hace referencia al primary key) del estado de cliente</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<EstadoClienteDto>>> GetEstadoClienteById(int id)
        {
            EstadoClienteDto estado = await _estadoClienteService.GetById(id);
            if (estado == null)
            {
                return NotFound();
            }
            GeneralResponse<EstadoClienteDto> response = new GeneralResponse<EstadoClienteDto>
            {
                Content = estado,
                Success = true,
                Message = "Estado de cliente obtenido correctamente"
            };
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo estado de cliente.
        /// </summary>
        /// <param name="estadoCliente">Objeto que contiene la información del nuevo estado de cliente</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<EstadoClienteDto>>> CreateEstadoCliente([FromBody] EstadoClienteCrearDto estadoCliente)
        {
            EstadoClienteDto createdEstado = await _estadoClienteService.Create(estadoCliente);
            GeneralResponse<EstadoClienteDto> response = new GeneralResponse<EstadoClienteDto>
            {
                Content = createdEstado,
                Success = true,
                Message = "Estado de cliente creado correctamente"
            };
            return Ok(response);
        }

        /// <summary>
        /// Actualiza un estado de cliente por ID.
        /// </summary>
        /// <param name="id">ID es el identificador (hace referencia al primary key) del estado de cliente</param>
        /// <param name="estadoCliente">Objeto que contiene la información actualizada del estado de cliente</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<EstadoClienteDto>>> UpdateEstadoCliente(int id, [FromBody] EstadoClienteActualizarDto estadoCliente)
        {
            EstadoClienteDto updatedEstado = await _estadoClienteService.Update(id, estadoCliente);
            if (updatedEstado == null)
            {
                return NotFound();
            }
            GeneralResponse<EstadoClienteDto> response = new GeneralResponse<EstadoClienteDto>
            {
                Content = updatedEstado,
                Success = true,
                Message = "Estado de cliente actualizado correctamente"
            };
            return Ok(response);
        }

        /// <summary>
        /// Elimina un estado de cliente por ID.
        /// </summary>
        /// <param name="id">ID es el identificador (hace referencia al primary key) del estado de cliente</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> DeleteEstadoCliente(int id)
        {
            bool result = await _estadoClienteService.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            GeneralResponse<bool> response = new GeneralResponse<bool>
            {
                Content = result,
                Success = true,
                Message = "Estado de cliente eliminado correctamente"
            };
            return Ok(response);
        }
    }
}