using FluxSYS_backend.Application.DTOs.MovementsTypes;
using FluxSYS_backend.Domain.IServices;

namespace FluxSYS_backend.Application.Services
{
    public class MovementsTypesService : IMovementsTypes
    {
        private readonly IMovementsTypes _repository;

        public MovementsTypesService(IMovementsTypes repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MovementTypeReadDTO>> GetAllAsyncMovementsTypes()
        {
            return await _repository.GetAllAsyncMovementsTypes();
        }

        public async Task<IEnumerable<MovementTypeReadDTO>> GetMovementTypesByCompanyIdAsync(int companyId)
        {
            return await _repository.GetMovementTypesByCompanyIdAsync(companyId);
        }

        public async Task AddAsyncMovementType(MovementTypeCreateDTO dto)
        {
            await _repository.AddAsyncMovementType(dto);
        }

        public async Task UpdateAsyncMovementType(int id, MovementTypeUpdateDTO dto)
        {
            await _repository.UpdateAsyncMovementType(id, dto);
        }

        public async Task SoftDeleteAsyncMovementType(int id)
        {
            await _repository.SoftDeleteAsyncMovementType(id);
        }

        public async Task RestoreAsyncMovementType(int id)
        {
            await _repository.RestoreAsyncMovementType(id);
        }
    }
}