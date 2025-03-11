using FluxSYS_backend.Application.DTOs.CategoriesPurchaseOrders;

namespace FluxSYS_backend.Domain.IServices
{
    public interface ICategoriesPurchaseOrders
    {
        Task<IEnumerable<CategoryPurchaseOrderReadDTO>> GetAllAsyncCategoriesPurchaseOrders();
        Task AddAsyncCategoryPurchaseOrder(CategoryPurchaseOrderCreateDTO dto);
        Task UpdateAsyncCategoryPurchaseOrder(int id, CategoryPurchaseOrderUpdateDTO dto);
        Task SoftDeleteAsyncCategoryPurchaseOrder(int id);
        Task RestoreAsyncCategoryPurchaseOrder(int id);
    }
}