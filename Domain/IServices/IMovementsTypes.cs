using FluxSYS_backend.Application.DTOs.MovementsTypes;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IMovementsTypes
    {
        Task<IEnumerable<MovementTypeReadDTO>> GetAllAsyncMovementsTypes();
        Task<IEnumerable<MovementTypeReadDTO>> GetMovementTypesByCompanyIdAsync(int companyId);
        Task AddAsyncMovementType(MovementTypeCreateDTO dto);
        Task UpdateAsyncMovementType(int id, MovementTypeUpdateDTO dto);
        Task SoftDeleteAsyncMovementType(int id);
        Task RestoreAsyncMovementType(int id);
    }
}