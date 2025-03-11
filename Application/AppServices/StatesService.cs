using FluxSYS_backend.Application.DTOs.States;
using FluxSYS_backend.Domain.IServices;

namespace FluxSYS_backend.Application.Services
{
    public class StatesService : IStates
    {
        private readonly IStates _repository;

        public StatesService(IStates repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StateReadDTO>> GetAllAsyncStates()
        {
            return await _repository.GetAllAsyncStates();
        }

        public async Task AddAsyncState(StateCreateDTO dto)
        {
            await _repository.AddAsyncState(dto);
        }

        public async Task UpdateAsyncState(int id, StateUpdateDTO dto)
        {
            await _repository.UpdateAsyncState(id, dto);
        }

        public async Task SoftDeleteAsyncState(int id)
        {
            await _repository.SoftDeleteAsyncState(id);
        }

        public async Task RestoreAsyncState(int id)
        {
            await _repository.RestoreAsyncState(id);
        }
    }
}