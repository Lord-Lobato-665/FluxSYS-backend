using FluxSYS_backend.Application.DTOs.Roles;
using FluxSYS_backend.Domain.IServices;

namespace FluxSYS_backend.Application.Services
{
    public class RolesService : IRoles
    {
        private readonly IRoles _repository;

        public RolesService(IRoles repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RoleReadDTO>> GetAllAsyncRoles()
        {
            return await _repository.GetAllAsyncRoles();
        }

        public async Task AddAsyncRole(RoleCreateDTO dto)
        {
            await _repository.AddAsyncRole(dto);
        }

        public async Task UpdateAsyncRole(int id, RoleUpdateDTO dto)
        {
            await _repository.UpdateAsyncRole(id, dto);
        }

        public async Task SoftDeleteAsyncRole(int id)
        {
            await _repository.SoftDeleteAsyncRole(id);
        }

        public async Task RestoreAsyncRole(int id)
        {
            await _repository.RestoreAsyncRole(id);
        }
    }
}