using FluxSYS_backend.Application.DTOs.PurchaseOrders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IPurchaseOrders
    {
        Task<IEnumerable<PurchaseOrderReadDTO>> GetAllAsyncPurchaseOrders();
        Task<IEnumerable<PurchaseOrderReadDTO>> GetPurchaseOrdersByCompanyIdAsync(int companyId);
        Task AddAsyncPurchaseOrder(PurchaseOrderCreateDTO dto, string nameUser, string nameDepartment);
        Task UpdateAsyncPurchaseOrder(int id, PurchaseOrderUpdateDTO dto, string nameUser, string nameDepartment);
        Task SoftDeleteAsyncPurchaseOrder(int id, string nameUser, string nameDepartment);
        Task RestoreAsyncPurchaseOrder(int id, string nameUser, string nameDepartment);
    }
}