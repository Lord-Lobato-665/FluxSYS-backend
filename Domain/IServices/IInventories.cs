using FluxSYS_backend.Application.DTOs.Inventories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IInventories
    {
        Task<IEnumerable<InventoryReadDTO>> GetAllAsyncInventories();
        Task AddAsyncInventory(InventoryCreateDTO dto);
        Task UpdateAsyncInventory(int id, InventoryUpdateDTO dto);
        Task SoftDeleteAsyncInventory(int id);
        Task RestoreAsyncInventory(int id);
    }
}