using ColegioMaster.DtoModels.JuegoRecreativo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMaster.Negocio.Juegos
{
    public interface IJuegoRecreativoService : IDisposable
    {
        Task<List<JuegoRecreativoDto>> GetAll();
        Task<JuegoRecreativoDto> GetById(int id);
        Task<JuegoRecreativoDto> Create(JuegoRecreativoCrearDto request);
        Task<JuegoRecreativoDto> Update(int id, JuegoRecreativoActualizarDto request);
        Task<bool> Delete(int id);
    }
}
