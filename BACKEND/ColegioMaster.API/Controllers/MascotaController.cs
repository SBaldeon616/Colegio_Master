using ColegioMaster.DtoModels.Mascota;
using ColegioMaster.Negocio.Mascota;
using Microsoft.AspNetCore.Mvc;

namespace ColegioMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {
        private readonly IMascotaService _mascotaService;

        public MascotaController(IMascotaService mascotaService)
        {
            _mascotaService = mascotaService;
        }

        // GET: api/Mascota
        [HttpGet]
        public async Task<ActionResult<List<MascotaDto>>> GetAllMascota()
        {
            List<MascotaDto> mascotas = await _mascotaService.GetAll();
            return Ok(mascotas);
        }

        // GET: api/Mascota/1
        [HttpGet("{id}")]
        public async Task<ActionResult<MascotaDto>> GetMascotaById(int id)
        {
            MascotaDto mascota = await _mascotaService.GetById(id);
            if (mascota == null)
                return NotFound();
            return Ok(mascota);
        }

        // POST: api/Mascota
        [HttpPost]
        public async Task<ActionResult<MascotaDto>> CreateMascota([FromBody] MascotaCrearDto mascota)
        {
            MascotaDto nuevaMascota = await _mascotaService.Create(mascota);
            return Ok(nuevaMascota);
        }

        // PUT: api/Mascota/1
        [HttpPut("{id}")]
        public async Task<ActionResult<MascotaDto>> UpdateMascota(int id, [FromBody] MascotaActualizarDto mascota)
        {
            MascotaDto updatedMascota = await _mascotaService.Update(id, mascota);
            if (updatedMascota == null)
                return NotFound();
            return Ok(updatedMascota);
        }

        // DELETE: api/Mascota/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteMascota(int id)
        {
            bool result = await _mascotaService.Delete(id);
            if (!result)
                return NotFound();
            return Ok(result);
        }
    }
}