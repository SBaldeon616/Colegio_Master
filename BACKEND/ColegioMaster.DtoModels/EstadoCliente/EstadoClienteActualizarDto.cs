using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMaster.DtoModels.EstadoCliente
{
    public class EstadoClienteActualizarDto
    {
        public int Id { get; set; }

        public string Codigo { get; set; } = null!;

        public string Descripcion { get; set; } = null!;
    }
}
