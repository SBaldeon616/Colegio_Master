using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColegioMaster.DtoModels.JuegoRecreativo;
using ColegioMaster.Repositorio.BDColegioMaster;

namespace ColegioMaster.Repositorio.Implement
{

        public class JuegoRecreativoRepositorio : IJuegoRecreativoRepositorio
        {
            // Conexión a la base de datos
            _dbContextColegio _Db = new _dbContextColegio();

            // GET ALL: Obtiene todos los juegos recreativos
            public Task<List<JuegoRecreativoDto>> GetAll()
            {
                var lista = _Db.JuegoRecreativo
                    .Select(j => new JuegoRecreativoDto
                    {
                        Id = j.Id,
                        Nombre = j.Nombre,
                        Tipo = j.Tipo,
                        Estado = j.Estado,
                        FechaCreacion = j.FechaCreacion,
                        FechaModificacion = j.FechaModificacion,
                        UsuarioCreacion = j.UsuarioCreacion,
                        UsuarioModificacion = j.UsuarioModificacion
                    }).ToList();
                return Task.FromResult(lista);
            }

            // GET BY ID: Busca un juego por su ID
            public Task<JuegoRecreativoDto> GetById(int id)
            {
                var j = _Db.JuegoRecreativo.Find(id);
                if (j == null)
                    return Task.FromResult<JuegoRecreativoDto>(null!);
                return Task.FromResult(new JuegoRecreativoDto
                {
                    Id = j.Id,
                    Nombre = j.Nombre,
                    Tipo = j.Tipo,
                    Estado = j.Estado,
                    FechaCreacion = j.FechaCreacion,
                    FechaModificacion = j.FechaModificacion,
                    UsuarioCreacion = j.UsuarioCreacion,
                    UsuarioModificacion = j.UsuarioModificacion
                });
            }

            // CREATE: Inserta un nuevo juego en la BD
            public Task<JuegoRecreativoDto> Create(JuegoRecreativoCrearDto request)
            {
                var nuevo = new JuegoRecreativo
                {
                    Nombre = request.Nombre,
                    Tipo = request.Tipo,
                    Estado = request.Estado,
                    FechaCreacion = DateTime.Now,       // Se genera automáticamente
                    UsuarioCreacion = request.UsuarioCreacion
                };
                _Db.JuegoRecreativo.Add(nuevo);
                _Db.SaveChanges();
                return Task.FromResult(new JuegoRecreativoDto
                {
                    Id = nuevo.Id,
                    Nombre = nuevo.Nombre,
                    Tipo = nuevo.Tipo,
                    Estado = nuevo.Estado,
                    FechaCreacion = nuevo.FechaCreacion,
                    UsuarioCreacion = nuevo.UsuarioCreacion
                });
            }

            // UPDATE: Actualiza un juego existente
            public Task<JuegoRecreativoDto> Update(int id, JuegoRecreativoActualizarDto request)
            {
                var entidad = _Db.JuegoRecreativo.Find(id);
                if (entidad == null)
                    return Task.FromResult<JuegoRecreativoDto>(null!);
                entidad.Nombre = request.Nombre;
                entidad.Tipo = request.Tipo;
                entidad.Estado = request.Estado;
                entidad.FechaModificacion = DateTime.Now; // Se genera automáticamente
                entidad.UsuarioModificacion = request.UsuarioModificacion;
                _Db.JuegoRecreativo.Update(entidad);
                _Db.SaveChanges();
                return Task.FromResult(new JuegoRecreativoDto
                {
                    Id = entidad.Id,
                    Nombre = entidad.Nombre,
                    Tipo = entidad.Tipo,
                    Estado = entidad.Estado,
                    FechaCreacion = entidad.FechaCreacion,
                    FechaModificacion = entidad.FechaModificacion,
                    UsuarioCreacion = entidad.UsuarioCreacion,
                    UsuarioModificacion = entidad.UsuarioModificacion
                });
            }

            // DELETE: Elimina un juego por su ID
            public Task<bool> Delete(int id)
            {
                var entidad = _Db.JuegoRecreativo.Find(id);
                if (entidad == null)
                    return Task.FromResult(false);
                _Db.JuegoRecreativo.Remove(entidad);
                _Db.SaveChanges();
                return Task.FromResult(true);
            }

            // Libera los recursos de la conexión
            public void Dispose()
            {
                _Db?.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
