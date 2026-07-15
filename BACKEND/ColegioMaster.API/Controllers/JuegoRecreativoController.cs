using ColegioMaster.DtoModels.Comp;
using ColegioMaster.DtoModels.JuegoRecreativo;
using ColegioMaster.Negocio.Juegos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMaster.API.Controllers
{
    /// <summary>
    /// Administrar juegos recreativos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JuegoRecreativoController : ControllerBase
    {
        private readonly IJuegoRecreativoService _juegoRecreativoService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="juegoRecreativoService"></param>
        public JuegoRecreativoController(IJuegoRecreativoService juegoRecreativoService)
        {
            _juegoRecreativoService = juegoRecreativoService;
        }

        /// <summary>
        /// Obtiene todos los juegos recreativos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<GeneralResponse<List<JuegoRecreativoDto>>>> GetAllJuegoRecreativo()
        {
            List<JuegoRecreativoDto> juegos = await _juegoRecreativoService.GetAll();
            GeneralResponse<List<JuegoRecreativoDto>> response = new GeneralResponse<List<JuegoRecreativoDto>>
            {
                Content = juegos,
                Success = true,
                Message = "Juegos recreativos obtenidos correctamente"
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un juego recreativo por ID.
        /// </summary>
        /// <param name="id">ID es el identificador (hace referencia al primary key) del juego recreativo</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<JuegoRecreativoDto>>> GetJuegoRecreativoById(int id)
        {
            JuegoRecreativoDto juego = await _juegoRecreativoService.GetById(id);
            if (juego == null)
            {
                return NotFound();
            }
            GeneralResponse<JuegoRecreativoDto> response = new GeneralResponse<JuegoRecreativoDto>
            {
                Content = juego,
                Success = true,
                Message = "Juego recreativo obtenido correctamente"
            };
            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo juego recreativo.
        /// </summary>
        /// <param name="juego">Objeto que contiene la información del nuevo juego recreativo</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<JuegoRecreativoDto>>> CreateJuegoRecreativo([FromBody] JuegoRecreativoCrearDto juego)
        {
            JuegoRecreativoDto nuevoJuego = await _juegoRecreativoService.Create(juego);
            GeneralResponse<JuegoRecreativoDto> response = new GeneralResponse<JuegoRecreativoDto>
            {
                Content = nuevoJuego,
                Success = true,
                Message = "Juego recreativo creado correctamente"
            };
            return Ok(response);
        }

        /// <summary>
        /// Actualiza un juego recreativo por ID.
        /// </summary>
        /// <param name="id">ID es el identificador (hace referencia al primary key) del juego recreativo</param>
        /// <param name="juego">Objeto que contiene la información actualizada del juego recreativo</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<JuegoRecreativoDto>>> UpdateJuegoRecreativo(int id, [FromBody] JuegoRecreativoActualizarDto juego)
        {
            JuegoRecreativoDto updatedJuego = await _juegoRecreativoService.Update(id, juego);
            if (updatedJuego == null)
            {
                return NotFound();
            }
            GeneralResponse<JuegoRecreativoDto> response = new GeneralResponse<JuegoRecreativoDto>
            {
                Content = updatedJuego,
                Success = true,
                Message = "Juego recreativo actualizado correctamente"
            };
            return Ok(response);
        }

        /// <summary>
        /// Elimina un juego recreativo por ID.
        /// </summary>
        /// <param name="id">ID es el identificador (hace referencia al primary key) del juego recreativo</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> DeleteJuegoRecreativo(int id)
        {
            bool result = await _juegoRecreativoService.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            GeneralResponse<bool> response = new GeneralResponse<bool>
            {
                Content = result,
                Success = true,
                Message = "Juego recreativo eliminado correctamente"
            };
            return Ok(response);
        }
    }
}