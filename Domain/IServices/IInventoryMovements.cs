using FluxSYS_backend.Application.DTOs.InventoryMovements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IInventoryMovements
    {
        Task<IEnumerable<InventoryMovementReadDTO>> GetAllAsyncInventoryMovements();
        Task<IEnumerable<InventoryMovementReadDTO>> GetAllByCompanyIdAsync(int idCompany);
    }
}