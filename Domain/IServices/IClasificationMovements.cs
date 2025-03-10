using FluxSYS_backend.Application.DTOs.ClasificationMovements;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IClasificationMovements
    {
        Task<IEnumerable<ClasificationMovementReadDTO>> GetAllAsyncClasificationMovements();
        Task AddAsyncClasificationMovement(ClasificationMovementCreateDTO dto);
        Task UpdateAsyncClasificationMovement(int id, ClasificationMovementUpdateDTO dto);
        Task SoftDeleteAsyncClasificationMovement(int id);
        Task RestoreAsyncClasificationMovement(int id);
    }
}
