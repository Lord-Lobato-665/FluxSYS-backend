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

        public async Task<InventoryReadByIdDTO> GetInventoryByIdAsync(int id)
        {
            return await _repository.GetInventoryByIdAsync(id);
        }

        public async Task AddAsyncInventory(InventoryCreateDTO dto, string nameUser, string nameDepartment)
        {
            await _repository.AddAsyncInventory(dto, nameUser, nameDepartment);
        }

        public async Task UpdateAsyncInventory(int id, InventoryUpdateDTO dto, string nameUser, string nameDepartment)
        {
            await _repository.UpdateAsyncInventory(id, dto, nameUser, nameDepartment);
        }

        public async Task SoftDeleteAsyncInventory(int id, string nameUser, string nameDepartment)
        {
            await _repository.SoftDeleteAsyncInventory(id, nameUser, nameDepartment);
        }

        public async Task RestoreAsyncInventory(int id, string nameUser, string nameDepartment)
        {
            await _repository.RestoreAsyncInventory(id, nameUser, nameDepartment);
        }

        public async Task<byte[]> GetPDF(string companyName, string departmentName)
        {
            return await _repository.GetPDF(companyName, departmentName);
        }
    }
}