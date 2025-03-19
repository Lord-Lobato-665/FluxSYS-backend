using FluxSYS_backend.Application.DTOs.Positions;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IPositions
    {
        Task<IEnumerable<PositionReadDTO>> GetAllAsyncPositions();
        Task<IEnumerable<PositionReadDTO>> GetPositionsByCompanyIdAsync(int companyId);
        Task AddAsyncPosition(PositionCreateDTO dto);
        Task UpdateAsyncPosition(int id, PositionUpdateDTO dto);
        Task SoftDeleteAsyncPosition(int id);
        Task RestoreAsyncPosition(int id);
    }
}