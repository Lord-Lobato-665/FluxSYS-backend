using FluxSYS_backend.Application.DTOs.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IUsers
    {
        Task<IEnumerable<UserReadDTO>> GetAllAsyncUsers();
        Task AddAsyncUser(UserCreateDTO dto, int userId, int departmentId);
        Task UpdateAsyncUser(int id, UserUpdateDTO dto, int userId, int departmentId);
        Task SoftDeleteAsyncUser(int id, int userId, int departmentId);
        Task RestoreAsyncUser(int id, int userId, int departmentId);
    }
}