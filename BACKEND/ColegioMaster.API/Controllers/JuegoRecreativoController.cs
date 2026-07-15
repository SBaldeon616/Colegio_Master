using ColegioMaster.DtoModels.JuegoRecreativo;
using ColegioMaster.Negocio.Juegos;
using Microsoft.AspNetCore.Mvc;
namespace ColegioMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JuegoRecreativoController : ControllerBase
    {
        private readonly IJuegoRecreativoService _juegoRecreativoService;

        public JuegoRecreativoController(IJuegoRecreativoService juegoRecreativoService)
        {
            _juegoRecreativoService = juegoRecreativoService;
        }

        /// <summary>
        /// Obtiene todos los juegos recreativos.
        /// </summary>
        // GET: api/JuegoRecreativo
        [HttpGet]
        public async Task<ActionResult<List<JuegoRecreativoDto>>> GetAllJuegoRecreativo()
        {
            List<JuegoRecreativoDto> juegos = await _juegoRecreativoService.GetAll();
            return Ok(juegos);
        }

        /// <summary>
        /// Obtiene un juego recreativo por ID.
        /// </summary>
        // GET: api/JuegoRecreativo/1
        [HttpGet("{id}")]
        public async Task<ActionResult<JuegoRecreativoDto>> GetJuegoRecreativoById(int id)
        {
            JuegoRecreativoDto juego = await _juegoRecreativoService.GetById(id);
            if (juego == null)
                return NotFound();
            return Ok(juego);
        }

        /// <summary>
        /// Crea un nuevo juego recreativo.
        /// </summary>
        // POST: api/JuegoRecreativo
        [HttpPost]
        public async Task<ActionResult<JuegoRecreativoDto>> CreateJuegoRecreativo([FromBody] JuegoRecreativoCrearDto juego)
        {
            JuegoRecreativoDto nuevoJuego = await _juegoRecreativoService.Create(juego);
            return Ok(nuevoJuego);
        }

        /// <summary>
        /// Actualiza un juego recreativo por ID.
        /// </summary>
        // PUT: api/JuegoRecreativo/1
        [HttpPut("{id}")]
        public async Task<ActionResult<JuegoRecreativoDto>> UpdateJuegoRecreativo(int id, [FromBody] JuegoRecreativoActualizarDto juego)
        {
            JuegoRecreativoDto updatedJuego = await _juegoRecreativoService.Update(id, juego);
            if (updatedJuego == null)
                return NotFound();
            return Ok(updatedJuego);
        }

        /// <summary>
        /// Elimina un juego recreativo por ID.
        /// </summary>
        // DELETE: api/JuegoRecreativo/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteJuegoRecreativo(int id)
        {
            bool result = await _juegoRecreativoService.Delete(id);
            if (!result)
                return NotFound();
            return Ok(result);
        }
    }
}