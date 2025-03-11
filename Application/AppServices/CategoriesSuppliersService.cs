using FluxSYS_backend.Application.DTOs.CategoriesSuppliers;
using FluxSYS_backend.Domain.IServices;

namespace FluxSYS_backend.Application.Services
{
    public class CategoriesSuppliersService : ICategoriesSuppliers
    {
        private readonly ICategoriesSuppliers _repository;

        public CategoriesSuppliersService(ICategoriesSuppliers repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategorySuppliersReadDTO>> GetAllAsyncCategoriesSuppliers()
        {
            return await _repository.GetAllAsyncCategoriesSuppliers();
        }

        public async Task AddAsyncCategorySupplier(CategorySuppliersCreateDTO dto)
        {
            await _repository.AddAsyncCategorySupplier(dto);
        }

        public async Task UpdateAsyncCategorySupplier(int id, CategorySuppliersUpdateDTO dto)
        {
            await _repository.UpdateAsyncCategorySupplier(id, dto);
        }

        public async Task SoftDeleteAsyncCategorySupplier(int id)
        {
            await _repository.SoftDeleteAsyncCategorySupplier(id);
        }

        public async Task RestoreAsyncCategorySupplier(int id)
        {
            await _repository.RestoreAsyncCategorySupplier(id);
        }
    }
}