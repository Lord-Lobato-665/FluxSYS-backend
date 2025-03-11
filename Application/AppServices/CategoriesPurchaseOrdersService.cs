using FluxSYS_backend.Application.DTOs.CategoriesPurchaseOrders;
using FluxSYS_backend.Domain.IServices;

namespace FluxSYS_backend.Application.Services
{
    public class CategoriesPurchaseOrdersService : ICategoriesPurchaseOrders
    {
        private readonly ICategoriesPurchaseOrders _repository;

        public CategoriesPurchaseOrdersService(ICategoriesPurchaseOrders repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryPurchaseOrderReadDTO>> GetAllAsyncCategoriesPurchaseOrders()
        {
            return await _repository.GetAllAsyncCategoriesPurchaseOrders();
        }

        public async Task AddAsyncCategoryPurchaseOrder(CategoryPurchaseOrderCreateDTO dto)
        {
            await _repository.AddAsyncCategoryPurchaseOrder(dto);
        }

        public async Task UpdateAsyncCategoryPurchaseOrder(int id, CategoryPurchaseOrderUpdateDTO dto)
        {
            await _repository.UpdateAsyncCategoryPurchaseOrder(id, dto);
        }

        public async Task SoftDeleteAsyncCategoryPurchaseOrder(int id)
        {
            await _repository.SoftDeleteAsyncCategoryPurchaseOrder(id);
        }

        public async Task RestoreAsyncCategoryPurchaseOrder(int id)
        {
            await _repository.RestoreAsyncCategoryPurchaseOrder(id);
        }
    }
}