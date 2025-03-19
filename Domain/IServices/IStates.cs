using FluxSYS_backend.Application.DTOs.States;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IStates
    {
        Task<IEnumerable<StateReadDTO>> GetAllAsyncStates();
        Task<IEnumerable<StateReadDTO>> GetStatesByCompanyIdAsync(int companyId);
        Task AddAsyncState(StateCreateDTO dto);
        Task UpdateAsyncState(int id, StateUpdateDTO dto);
        Task SoftDeleteAsyncState(int id);
        Task RestoreAsyncState(int id);
    }
}