using FluxSYS_backend.Application.DTOs.Modules;
using FluxSYS_backend.Domain.IServices;

namespace FluxSYS_backend.Application.Services
{
    public class ModulesService : IModules
    {
        private readonly IModules _repository;

        public ModulesService(IModules repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ModuleReadDTO>> GetAllAsyncModules()
        {
            return await _repository.GetAllAsyncModules();
        }

        public async Task AddAsyncModule(ModuleCreateDTO dto)
        {
            await _repository.AddAsyncModule(dto);
        }

        public async Task UpdateAsyncModule(int id, ModuleUpdateDTO dto)
        {
            await _repository.UpdateAsyncModule(id, dto);
        }

        public async Task SoftDeleteAsyncModule(int id)
        {
            await _repository.SoftDeleteAsyncModule(id);
        }

        public async Task RestoreAsyncModule(int id)
        {
            await _repository.RestoreAsyncModule(id);
        }
    }
}