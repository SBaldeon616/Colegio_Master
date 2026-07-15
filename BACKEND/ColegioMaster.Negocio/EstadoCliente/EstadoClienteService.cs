using ColegioMaster.DtoModels.EstadoCliente;
using ColegioMaster.Repositorio.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMaster.Negocio.EstadoCliente
{
    public class EstadoClienteService : IEstadoClienteService
    {
        private readonly IEstadoClienteRepositorio _repository;

        public EstadoClienteService(IEstadoClienteRepositorio repository)
        {
            _repository = repository;
        }

        public async Task<EstadoClienteDto> Create(EstadoClienteCrearDto request)
        {
            EstadoClienteDto result = await _repository.Create(request);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _repository.Delete(id);
            return result;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<EstadoClienteDto>> GetAll()
        {
            List<EstadoClienteDto> result = await _repository.GetAll();
            return result;
        }

        public async Task<EstadoClienteDto> GetById(int id)
        {
            EstadoClienteDto result = await _repository.GetById(id);
            return result;
        }

        public async Task<EstadoClienteDto> Update(int id, EstadoClienteActualizarDto request)
        {
            EstadoClienteDto result = await _repository.Update(id, request);
            return result;
        }
    }
}