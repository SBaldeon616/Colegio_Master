using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Lo que el cliente envía para CREAR un juego recreativo
namespace ColegioMaster.DtoModels.JuegoRecreativo
{
    public class JuegoRecreativoCrearDto
    {
        public string Nombre { get; set; } = null!;  // Ej: "Castillo Inflable"
        public string? Tipo { get; set; }             // Ej: "Inflable"
        public string? Estado { get; set; }           // Ej: "Disponible"
        public string UsuarioCreacion { get; set; } = null!; // Ej: "admin"
    }
}