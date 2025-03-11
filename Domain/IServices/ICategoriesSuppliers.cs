using FluxSYS_backend.Application.DTOs.CategoriesSuppliers;

namespace FluxSYS_backend.Domain.IServices
{
    public interface ICategoriesSuppliers
    {
        Task<IEnumerable<CategorySuppliersReadDTO>> GetAllAsyncCategoriesSuppliers();
        Task AddAsyncCategorySupplier(CategorySuppliersCreateDTO dto);
        Task UpdateAsyncCategorySupplier(int id, CategorySuppliersUpdateDTO dto);
        Task SoftDeleteAsyncCategorySupplier(int id);
        Task RestoreAsyncCategorySupplier(int id);
    }
}