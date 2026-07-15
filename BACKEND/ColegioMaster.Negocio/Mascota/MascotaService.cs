using ColegioMaster.DtoModels.Mascota;
using ColegioMaster.Repositorio.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMaster.Negocio.Mascota
{
    public class MascotaService : IMascotaService
    {
        private readonly IMascotaRepositorio _repository;

        public MascotaService(IMascotaRepositorio repository)
        {
            _repository = repository;
        }

        public async Task<MascotaDto> Create(MascotaCrearDto request)
        {
            MascotaDto result = await _repository.Create(request);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _repository.Delete(id);
            return result;
        }

        public async void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<MascotaDto>> GetAll()
        {
            List<MascotaDto> result = await _repository.GetAll();
            return result;
        }

        public async Task<MascotaDto> GetById(int id)
        {
            MascotaDto result = await _repository.GetById(id);
            return result;
        }

        public async Task<MascotaDto> Update(int id, MascotaActualizarDto request)
        {
            MascotaDto result = await _repository.Update(id, request);
            return result;
        }
    }
}
