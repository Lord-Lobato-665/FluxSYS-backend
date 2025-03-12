using FluxSYS_backend.Application.DTOs.Users;
using FluxSYS_backend.Domain.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task AddAsyncUser(UserCreateDTO dto, int userId, int departmentId)
        {
            await _repository.AddAsyncUser(dto, userId, departmentId);
        }

        public async Task UpdateAsyncUser(int id, UserUpdateDTO dto, int userId, int departmentId)
        {
            await _repository.UpdateAsyncUser(id, dto, userId, departmentId);
        }

        public async Task SoftDeleteAsyncUser(int id, int userId, int departmentId)
        {
            await _repository.SoftDeleteAsyncUser(id, userId, departmentId);
        }

        public async Task RestoreAsyncUser(int id, int userId, int departmentId)
        {
            await _repository.RestoreAsyncUser(id, userId, departmentId);
        }
    }
}