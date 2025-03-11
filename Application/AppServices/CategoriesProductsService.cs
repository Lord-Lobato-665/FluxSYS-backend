using FluxSYS_backend.Application.DTOs.CategoriesProducts;
using FluxSYS_backend.Domain.IServices;

namespace FluxSYS_backend.Application.Services
{
    public class CategoriesProductsService : ICategoriesProducts
    {
        private readonly ICategoriesProducts _repository;

        public CategoriesProductsService(ICategoriesProducts repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryProductsReadDTO>> GetAllAsyncCategoriesProducts()
        {
            return await _repository.GetAllAsyncCategoriesProducts();
        }

        public async Task AddAsyncCategoryProduct(CategoryProductsCreateDTO dto)
        {
            await _repository.AddAsyncCategoryProduct(dto);
        }

        public async Task UpdateAsyncCategoryProduct(int id, CategoryProductsUpdateDTO dto)
        {
            await _repository.UpdateAsyncCategoryProduct(id, dto);
        }

        public async Task SoftDeleteAsyncCategoryProduct(int id)
        {
            await _repository.SoftDeleteAsyncCategoryProduct(id);
        }

        public async Task RestoreAsyncCategoryProduct(int id)
        {
            await _repository.RestoreAsyncCategoryProduct(id);
        }
    }
}