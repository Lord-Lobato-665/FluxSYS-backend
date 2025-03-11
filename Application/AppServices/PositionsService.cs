using FluxSYS_backend.Application.DTOs.Positions;
using FluxSYS_backend.Domain.IServices;

namespace FluxSYS_backend.Application.Services
{
    public class PositionsService : IPositions
    {
        private readonly IPositions _repository;

        public PositionsService(IPositions repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PositionReadDTO>> GetAllAsyncPositions()
        {
            return await _repository.GetAllAsyncPositions();
        }

        public async Task AddAsyncPosition(PositionCreateDTO dto)
        {
            await _repository.AddAsyncPosition(dto);
        }

        public async Task UpdateAsyncPosition(int id, PositionUpdateDTO dto)
        {
            await _repository.UpdateAsyncPosition(id, dto);
        }

        public async Task SoftDeleteAsyncPosition(int id)
        {
            await _repository.SoftDeleteAsyncPosition(id);
        }

        public async Task RestoreAsyncPosition(int id)
        {
            await _repository.RestoreAsyncPosition(id);
        }
    }
}