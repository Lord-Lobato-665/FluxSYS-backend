using FluxSYS_backend.Application.DTOs.Inventories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IInventories
    {
        Task<IEnumerable<InventoryReadDTO>> GetAllAsyncInventories();
        Task AddAsyncInventory(InventoryCreateDTO dto, int userId, int departmentId);
        Task UpdateAsyncInventory(int id, InventoryUpdateDTO dto, int userId, int departmentId);
        Task SoftDeleteAsyncInventory(int id, int userId, int departmentId);
        Task RestoreAsyncInventory(int id, int userId, int departmentId);
    }
}