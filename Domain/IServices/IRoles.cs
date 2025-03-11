using FluxSYS_backend.Application.DTOs.Roles;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IRoles
    {
        Task<IEnumerable<RoleReadDTO>> GetAllAsyncRoles();
        Task AddAsyncRole(RoleCreateDTO dto);
        Task UpdateAsyncRole(int id, RoleUpdateDTO dto);
        Task SoftDeleteAsyncRole(int id);
        Task RestoreAsyncRole(int id);
    }
}