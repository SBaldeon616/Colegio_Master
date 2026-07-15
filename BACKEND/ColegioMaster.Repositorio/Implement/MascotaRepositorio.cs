using ColegioMaster.DtoModels.Mascota;
using ColegioMaster.Repositorio.BDColegioMaster;

namespace ColegioMaster.Repositorio.Implement
{
    // REPOSITORIO = Habla directamente con la base de datos
    // Aquí se hacen las operaciones reales: INSERT, SELECT, UPDATE, DELETE
    public class MascotaRepositorio : IMascotaRepositorio
    {
        // Conexión a la base de datos
        _dbContextColegio _Db = new _dbContextColegio();

        // CREATE: Inserta una nueva mascota en la BD
        public Task<MascotaDto> Create(MascotaCrearDto request)
        {
            Mascota nuevoRegistro = new Mascota
            {
                CategoriaMascota = request.CategoriaMascota,
                Raza = request.Raza,
                Edad = request.Edad,
                Nombre = request.Nombre,
                FechaCreacion = DateTime.Now,
                UsuarioCreacion = request.UsuarioCreacion
            };
            _Db.Mascota.Add(nuevoRegistro);
            _Db.SaveChanges();
            return Task.FromResult(new MascotaDto
            {
                Id = nuevoRegistro.Id,
                CategoriaMascota = nuevoRegistro.CategoriaMascota,
                Raza = nuevoRegistro.Raza,
                Edad = nuevoRegistro.Edad,
                Nombre = nuevoRegistro.Nombre,
                FechaCreacion = nuevoRegistro.FechaCreacion,
                UsuarioCreacion = nuevoRegistro.UsuarioCreacion
            });
        }

        // DELETE: Elimina una mascota por su ID
        public Task<bool> Delete(int id)
        {
            var entidad = _Db.Mascota.Find(id);
            if (entidad == null)
                return Task.FromResult(false);
            _Db.Mascota.Remove(entidad);
            _Db.SaveChanges();
            return Task.FromResult(true);
        }

        // Libera los recursos de la conexión a la BD
        public void Dispose()
        {
            _Db?.Dispose();
            GC.SuppressFinalize(this);
        }

        // GET ALL: Obtiene todas las mascotas de la tabla
        public Task<List<MascotaDto>> GetAll()
        {
            var lista = _Db.Mascota
                .Select(m => new MascotaDto
                {
                    Id = m.Id,
                    CategoriaMascota = m.CategoriaMascota,
                    Raza = m.Raza,
                    Edad = m.Edad,
                    Nombre = m.Nombre,
                    FechaCreacion = m.FechaCreacion,
                    FechaModificacion = m.FechaModificacion,
                    UsuarioCreacion = m.UsuarioCreacion,
                    UsuarioModificacion = m.UsuarioModificacion
                }).ToList();
            return Task.FromResult(lista);
        }

        // GET BY ID: Busca una mascota por su ID
        public Task<MascotaDto> GetById(int id)
        {
            var m = _Db.Mascota.Find(id);
            if (m == null)
                return Task.FromResult<MascotaDto>(null!);
            return Task.FromResult(new MascotaDto
            {
                Id = m.Id,
                CategoriaMascota = m.CategoriaMascota,
                Raza = m.Raza,
                Edad = m.Edad,
                Nombre = m.Nombre,
                FechaCreacion = m.FechaCreacion,
                FechaModificacion = m.FechaModificacion,
                UsuarioCreacion = m.UsuarioCreacion,
                UsuarioModificacion = m.UsuarioModificacion
            });
        }

        // UPDATE: Actualiza una mascota existente
        public Task<MascotaDto> Update(int id, MascotaActualizarDto request)
        {
            var entidad = _Db.Mascota.Find(id);
            if (entidad == null)
                return Task.FromResult<MascotaDto>(null!);
            entidad.CategoriaMascota = request.CategoriaMascota;
            entidad.Raza = request.Raza;
            entidad.Edad = request.Edad;
            entidad.Nombre = request.Nombre;
            entidad.FechaModificacion = DateTime.Now;
            entidad.UsuarioModificacion = request.UsuarioModificacion;
            _Db.Mascota.Update(entidad);
            _Db.SaveChanges();
            return Task.FromResult(new MascotaDto
            {
                Id = entidad.Id,
                CategoriaMascota = entidad.CategoriaMascota,
                Raza = entidad.Raza,
                Edad = entidad.Edad,
                Nombre = entidad.Nombre,
                FechaCreacion = entidad.FechaCreacion,
                FechaModificacion = entidad.FechaModificacion,
                UsuarioCreacion = entidad.UsuarioCreacion,
                UsuarioModificacion = entidad.UsuarioModificacion
            });
        }
    }
}