using ColegioMaster.DtoModels.Mascota;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMaster.Negocio.Mascota
{
    public interface IMascotaService:IDisposable
    {
        Task<List<MascotaDto>> GetAll();
        Task<MascotaDto> GetById(int id);
        Task<MascotaDto> Create(MascotaCrearDto request);
        Task<MascotaDto> Update(int id, MascotaActualizarDto request);
        Task<bool> Delete(int id);
    }
}
