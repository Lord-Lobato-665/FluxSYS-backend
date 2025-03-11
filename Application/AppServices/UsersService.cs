using FluxSYS_backend.Application.DTOs.Users;
using FluxSYS_backend.Domain.IServices;

namespace FluxSYS_backend.Application.Services
{
    public class UsersService : IUsers
    {
        private readonly IUsers _repository;

        public UsersService(IUsers repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserReadDTO>> GetAllAsyncUsers()
        {
            return await _repository.GetAllAsyncUsers();
        }

        public async Task AddAsyncUser(UserCreateDTO dto)
        {
            await _repository.AddAsyncUser(dto);
        }

        public async Task UpdateAsyncUser(int id, UserUpdateDTO dto)
        {
            await _repository.UpdateAsyncUser(id, dto);
        }

        public async Task SoftDeleteAsyncUser(int id)
        {
            await _repository.SoftDeleteAsyncUser(id);
        }

        public async Task RestoreAsyncUser(int id)
        {
            await _repository.RestoreAsyncUser(id);
        }
    }
}