using ColegioMaster.DtoModels.EstadoCliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMaster.Repositorio.Implement
{
    public interface IEstadoClienteRepositorio : IDisposable
    {
        public Task<List<EstadoClienteDto>> GetAll();
        public Task<EstadoClienteDto> GetById(int id);
        public Task<EstadoClienteDto> Create(EstadoClienteCrearDto request);
        public Task<EstadoClienteDto> Update(int id, EstadoClienteActualizarDto request);
        public Task<bool> Delete(int id);
    }
}


