using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Lo que el cliente envía para ACTUALIZAR un juego recreativo
namespace ColegioMaster.DtoModels.JuegoRecreativo
{
    public class JuegoRecreativoActualizarDto
    {
        public string Nombre { get; set; } = null!;
        public string? Tipo { get; set; }
        public string? Estado { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}