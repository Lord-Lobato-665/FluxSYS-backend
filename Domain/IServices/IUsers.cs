using FluxSYS_backend.Application.DTOs.Users;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IUsers
    {
        Task<IEnumerable<UserReadDTO>> GetAllAsyncUsers();
        Task AddAsyncUser(UserCreateDTO dto);
        Task UpdateAsyncUser(int id, UserUpdateDTO dto);
        Task SoftDeleteAsyncUser(int id);
        Task RestoreAsyncUser(int id);
    }
}