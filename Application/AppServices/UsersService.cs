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

        public async Task AddAsyncUser(UserCreateDTO dto, string nameUser, string nameDepartment)
        {
            await _repository.AddAsyncUser(dto, nameUser, nameDepartment);
        }

        public async Task UpdateAsyncUser(int id, UserUpdateDTO dto, string nameUser, string nameDepartment)
        {
            await _repository.UpdateAsyncUser(id, dto, nameUser, nameDepartment);
        }

        public async Task SoftDeleteAsyncUser(int id, string nameUser, string nameDepartment)
        {
            await _repository.SoftDeleteAsyncUser(id, nameUser, nameDepartment);
        }

        public async Task RestoreAsyncUser(int id, string nameUser, string nameDepartment)
        {
            await _repository.RestoreAsyncUser(id, nameUser, nameDepartment);
        }
    }
}