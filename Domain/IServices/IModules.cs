using FluxSYS_backend.Application.DTOs.Modules;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IModules
    {
        Task<IEnumerable<ModuleReadDTO>> GetAllAsyncModules();
        Task AddAsyncModule(ModuleCreateDTO dto);
        Task UpdateAsyncModule(int id, ModuleUpdateDTO dto);
        Task SoftDeleteAsyncModule(int id);
        Task RestoreAsyncModule(int id);
    }
}