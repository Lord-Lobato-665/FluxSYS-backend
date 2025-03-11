using FluxSYS_backend.Application.DTOs.ClasificationMovements;
using FluxSYS_backend.Domain.IServices;

namespace FluxSYS_backend.Application.Services
{
    public class ClasificationMovementsService : IClasificationMovements
    {
        private readonly IClasificationMovements _repository;

        public ClasificationMovementsService(IClasificationMovements repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ClasificationMovementReadDTO>> GetAllAsyncClasificationMovements()
        {
            return await _repository.GetAllAsyncClasificationMovements();
        }

        public async Task AddAsyncClasificationMovement(ClasificationMovementCreateDTO dto)
        {
            await _repository.AddAsyncClasificationMovement(dto);
        }

        public async Task UpdateAsyncClasificationMovement(int id, ClasificationMovementUpdateDTO dto)
        {
            await _repository.UpdateAsyncClasificationMovement(id, dto);
        }

        public async Task SoftDeleteAsyncClasificationMovement(int id)
        {
            await _repository.SoftDeleteAsyncClasificationMovement(id);
        }

        public async Task RestoreAsyncClasificationMovement(int id)
        {
            await _repository.RestoreAsyncClasificationMovement(id);
        }
    }
}
