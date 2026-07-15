using ColegioMaster.DtoModels.EstadoCliente;
using ColegioMaster.Repositorio.BDColegioMaster;

namespace ColegioMaster.Repositorio.Implement
{
    public class EstadoClienteRepositorio : IEstadoClienteRepositorio
    {
        _dbContextColegio _Db = new _dbContextColegio();

        public Task<EstadoClienteDto> Create(EstadoClienteCrearDto request)
        {
            EstadoCliente nuevoRegistro = new EstadoCliente
            {
                Codigo = request.Codigo,
                Descripcion = request.Descripcion,
            };
            _Db.EstadoCliente.Add(nuevoRegistro);
            _Db.SaveChanges();
            return Task.FromResult(new EstadoClienteDto
            {
                Id = nuevoRegistro.Id,
                Codigo = nuevoRegistro.Codigo,
                Descripcion = nuevoRegistro.Descripcion
            });
        }

        public Task<bool> Delete(int id)
        {
            var entidad = _Db.EstadoCliente.Find(id);
            if (entidad == null)
                return Task.FromResult(false);
            _Db.EstadoCliente.Remove(entidad);
            _Db.SaveChanges();
            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _Db?.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task<List<EstadoClienteDto>> GetAll()
        {
            var lista = _Db.EstadoCliente
                .Select(e => new EstadoClienteDto
                {
                    Id = e.Id,
                    Codigo = e.Codigo,
                    Descripcion = e.Descripcion
                })
                .ToList();
            return Task.FromResult(lista);
        }

        public Task<EstadoClienteDto> GetById(int id)
        {
            var e = _Db.EstadoCliente.Find(id);
            if (e == null)
                return Task.FromResult<EstadoClienteDto>(null!);
            return Task.FromResult(new EstadoClienteDto
            {
                Id = e.Id,
                Codigo = e.Codigo,
                Descripcion = e.Descripcion
            });
        }

        public Task<EstadoClienteDto> Update(int id, EstadoClienteActualizarDto request)
        {
            var entidad = _Db.EstadoCliente.Find(id);
            if (entidad == null)
                return Task.FromResult<EstadoClienteDto>(null!);
            entidad.Codigo = request.Codigo;
            entidad.Descripcion = request.Descripcion;
            _Db.EstadoCliente.Update(entidad);
            _Db.SaveChanges();
            return Task.FromResult(new EstadoClienteDto
            {
                Id = entidad.Id,
                Codigo = entidad.Codigo,
                Descripcion = entidad.Descripcion
            });
        }
    }
}