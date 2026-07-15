using ColegioMaster.DtoModels.EstadoCliente;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColegioMaster.Negocio.EstadoCliente
{
    public interface IEstadoClienteService : IDisposable
    {
        Task<List<EstadoClienteDto>> GetAll();
        Task<EstadoClienteDto> GetById(int id);
        Task<EstadoClienteDto> Create(EstadoClienteCrearDto request);
        Task<EstadoClienteDto> Update(int id, EstadoClienteActualizarDto request);
        Task<bool> Delete(int id, EstadoClienteDto request);
    }
}