// DTO = Lo que se devuelve al cliente (Postman, frontend, etc.)
namespace ColegioMaster.DtoModels.JuegoRecreativo
{
    public class JuegoRecreativoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Tipo { get; set; }
        public string? Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; }
    }
}