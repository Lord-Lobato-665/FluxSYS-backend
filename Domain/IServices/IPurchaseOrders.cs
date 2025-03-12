using FluxSYS_backend.Application.DTOs.PurchaseOrders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IPurchaseOrders
    {
        Task<IEnumerable<PurchaseOrderReadDTO>> GetAllAsyncPurchaseOrders();
        Task AddAsyncPurchaseOrder(PurchaseOrderCreateDTO dto, int userId, int departmentId);
        Task UpdateAsyncPurchaseOrder(int id, PurchaseOrderUpdateDTO dto, int userId, int departmentId);
        Task SoftDeleteAsyncPurchaseOrder(int id, int userId, int departmentId);
        Task RestoreAsyncPurchaseOrder(int id, int userId, int departmentId);
    }
}