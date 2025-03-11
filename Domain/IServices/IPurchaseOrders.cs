using FluxSYS_backend.Application.DTOs.PurchaseOrders;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IPurchaseOrders
    {
        Task<IEnumerable<PurchaseOrderReadDTO>> GetAllAsyncPurchaseOrders();
        Task AddAsyncPurchaseOrder(PurchaseOrderCreateDTO dto);
        Task UpdateAsyncPurchaseOrder(int id, PurchaseOrderUpdateDTO dto);
        Task SoftDeleteAsyncPurchaseOrder(int id);
        Task RestoreAsyncPurchaseOrder(int id);
    }
}