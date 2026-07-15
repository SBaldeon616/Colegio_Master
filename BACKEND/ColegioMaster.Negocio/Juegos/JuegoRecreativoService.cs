using ColegioMaster.DtoModels.JuegoRecreativo;
using ColegioMaster.Repositorio.Implement;

namespace ColegioMaster.Negocio.Juegos
{
    public class JuegoRecreativoService : IJuegoRecreativoService
    {
        private readonly IJuegoRecreativoRepositorio _repository;

        public JuegoRecreativoService(IJuegoRecreativoRepositorio repository)
        {
            _repository = repository;
        }

        public async Task<JuegoRecreativoDto> Create(JuegoRecreativoCrearDto request)
        {
            JuegoRecreativoDto result = await _repository.Create(request);
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

        public async Task<List<JuegoRecreativoDto>> GetAll()
        {
            List<JuegoRecreativoDto> result = await _repository.GetAll();
            return result;
        }

        public async Task<JuegoRecreativoDto> GetById(int id)
        {
            JuegoRecreativoDto result = await _repository.GetById(id);
            return result;
        }

        public async Task<JuegoRecreativoDto> Update(int id, JuegoRecreativoActualizarDto request)
        {
            JuegoRecreativoDto result = await _repository.Update(id, request);
            return result;
        }
    }
}