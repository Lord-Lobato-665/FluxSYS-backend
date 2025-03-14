using FluxSYS_backend.Application.DTOs.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IUsers
    {
        Task<IEnumerable<UserReadDTO>> GetAllAsyncUsers();
        Task AddAsyncUser(UserCreateDTO dto, string nameUser, string nameDepartment);
        Task UpdateAsyncUser(int id, UserUpdateDTO dto, string nameUser, string nameDepartment);
        Task SoftDeleteAsyncUser(int id, string nameUser, string nameDepartment);
        Task RestoreAsyncUser(int id, string nameUser, string nameDepartment);
    }
}