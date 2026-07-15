using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColegioMaster.DtoModels.JuegoRecreativo;

namespace ColegioMaster.Repositorio.Implement
{
    public interface IJuegoRecreativoRepositorio : IDisposable
    {
        Task<List<JuegoRecreativoDto>> GetAll();
        Task<JuegoRecreativoDto> GetById(int id);
        Task<JuegoRecreativoDto> Create(JuegoRecreativoCrearDto request);
        Task<JuegoRecreativoDto> Update(int id, JuegoRecreativoActualizarDto request);
        Task<bool> Delete(int id);
    }
}
