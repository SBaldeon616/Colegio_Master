using ColegioMaster.DtoModels.Comp;
using ColegioMaster.DtoModels.Mascota;
using ColegioMaster.Negocio.Mascota;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMaster.API.Controllers
{
    /// <summary>
    /// Administrar mascotas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MascotaController : ControllerBase
    {
        private readonly IMascotaService _mascotaService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mascotaService"></param>
        public MascotaController(IMascotaService mascotaService)
        {
            _mascotaService = mascotaService;
        }

        /// <summary>
        /// Obtiene todas las mascotas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<GeneralResponse<List<MascotaDto>>>> GetAllMascota()
        {
            List<MascotaDto> mascotas = await _mascotaService.GetAll();
            GeneralResponse<List<MascotaDto>> response = new GeneralResponse<List<MascotaDto>>
            {
                Content = mascotas,
                Success = true,
                Message = "Mascotas obtenidas correctamente"
            };
            return Ok(response);
        }

        /// <summary>
        /// Obtiene una mascota por ID.
        /// </summary>
        /// <param name="id">ID es el identificador (hace referencia al primary key) de la mascota</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<MascotaDto>>> GetMascotaById(int id)
        {
            MascotaDto mascota = await _mascotaService.GetById(id);
            if (mascota == null)
            {
                return NotFound();
            }
            GeneralResponse<MascotaDto> response = new GeneralResponse<MascotaDto>
            {
                Content = mascota,
                Success = true,
                Message = "Mascota obtenida correctamente"
            };
            return Ok(response);
        }

        /// <summary>
        /// Crea una nueva mascota.
        /// </summary>
        /// <param name="mascota">Objeto que contiene la información de la nueva mascota</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<MascotaDto>>> CreateMascota([FromBody] MascotaCrearDto mascota)
        {
            MascotaDto nuevaMascota = await _mascotaService.Create(mascota);
            GeneralResponse<MascotaDto> response = new GeneralResponse<MascotaDto>
            {
                Content = nuevaMascota,
                Success = true,
                Message = "Mascota creada correctamente"
            };
            return Ok(response);
        }

        /// <summary>
        /// Actualiza una mascota por ID.
        /// </summary>
        /// <param name="id">ID es el identificador (hace referencia al primary key) de la mascota</param>
        /// <param name="mascota">Objeto que contiene la información actualizada de la mascota</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<MascotaDto>>> UpdateMascota(int id, [FromBody] MascotaActualizarDto mascota)
        {
            MascotaDto updatedMascota = await _mascotaService.Update(id, mascota);
            if (updatedMascota == null)
            {
                return NotFound();
            }
            GeneralResponse<MascotaDto> response = new GeneralResponse<MascotaDto>
            {
                Content = updatedMascota,
                Success = true,
                Message = "Mascota actualizada correctamente"
            };
            return Ok(response);
        }

        /// <summary>
        /// Elimina una mascota por ID.
        /// </summary>
        /// <param name="id">ID es el identificador (hace referencia al primary key) de la mascota</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> DeleteMascota(int id)
        {
            bool result = await _mascotaService.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            GeneralResponse<bool> response = new GeneralResponse<bool>
            {
                Content = result,
                Success = true,
                Message = "Mascota eliminada correctamente"
            };
            return Ok(response);
        }
    }
}