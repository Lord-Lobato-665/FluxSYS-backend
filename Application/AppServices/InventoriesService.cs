using FluxSYS_backend.Application.DTOs.Inventories;
using FluxSYS_backend.Domain.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Application.Services
{
    public class InventoriesService : IInventories
    {
        private readonly IInventories _repository;

        public InventoriesService(IInventories repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<InventoryReadDTO>> GetAllAsyncInventories()
        {
            return await _repository.GetAllAsyncInventories();
        }

        public async Task AddAsyncInventory(InventoryCreateDTO dto)
        {
            await _repository.AddAsyncInventory(dto);
        }

        public async Task UpdateAsyncInventory(int id, InventoryUpdateDTO dto)
        {
            await _repository.UpdateAsyncInventory(id, dto);
        }

        public async Task SoftDeleteAsyncInventory(int id)
        {
            await _repository.SoftDeleteAsyncInventory(id);
        }

        public async Task RestoreAsyncInventory(int id)
        {
            await _repository.RestoreAsyncInventory(id);
        }
    }
}