using FluxSYS_backend.Application.DTOs.CategoriesProducts;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Application.Services
{
    public class CategoriesProductsService : ICategoriesProducts
    {
        private readonly ICategoriesProducts _repository;
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public CategoriesProductsService(ICategoriesProducts repository, ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _repository = repository;
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<CategoryProductsReadDTO>> GetAllAsyncCategoriesProducts()
        {
            return await _repository.GetAllAsyncCategoriesProducts();
        }

        public async Task<IEnumerable<CategoryProductsReadDTO>> GetCategoriesByCompanyIdAsync(int idCompany)
        {
            return await _repository.GetCategoriesByCompanyIdAsync(idCompany);
        }

        public async Task AddAsyncCategoryProduct(CategoryProductsCreateDTO dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.AddAsyncCategoryProduct(dto);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "AddAsyncCategoryProduct");
                    throw;
                }
            }
        }

        public async Task UpdateAsyncCategoryProduct(int id, CategoryProductsUpdateDTO dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.UpdateAsyncCategoryProduct(id, dto);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateAsyncCategoryProduct");
                    throw;
                }
            }
        }

        public async Task SoftDeleteAsyncCategoryProduct(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.SoftDeleteAsyncCategoryProduct(id);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteAsyncCategoryProduct");
                    throw;
                }
            }
        }

        public async Task RestoreAsyncCategoryProduct(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.RestoreAsyncCategoryProduct(id);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreAsyncCategoryProduct");
                    throw;
                }
            }
        }
    }
}