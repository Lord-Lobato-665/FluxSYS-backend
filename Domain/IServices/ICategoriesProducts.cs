using FluxSYS_backend.Application.DTOs.CategoriesProducts;

namespace FluxSYS_backend.Domain.IServices
{
    public interface ICategoriesProducts
    {
        Task<IEnumerable<CategoryProductsReadDTO>> GetAllAsyncCategoriesProducts();
        Task AddAsyncCategoryProduct(CategoryProductsCreateDTO dto);
        Task UpdateAsyncCategoryProduct(int id, CategoryProductsUpdateDTO dto);
        Task SoftDeleteAsyncCategoryProduct(int id);
        Task RestoreAsyncCategoryProduct(int id);
    }
}