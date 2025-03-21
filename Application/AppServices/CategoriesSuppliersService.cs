using FluxSYS_backend.Application.DTOs.CategoriesSuppliers;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Application.Services
{
    public class CategoriesSuppliersService : ICategoriesSuppliers
    {
        private readonly ICategoriesSuppliers _repository;
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public CategoriesSuppliersService(
            ICategoriesSuppliers repository,
            ApplicationDbContext context,
            ErrorLogService errorLogService)
        {
            _repository = repository;
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<CategorySuppliersReadDTO>> GetAllAsyncCategoriesSuppliers()
        {
            return await _repository.GetAllAsyncCategoriesSuppliers();
        }

        public async Task<IEnumerable<CategorySuppliersReadDTO>> GetCategoriesSuppliersByCompanyIdAsync(int companyId)
        {
            return await _repository.GetCategoriesSuppliersByCompanyIdAsync(companyId);
        }

        public async Task AddAsyncCategorySupplier(CategorySuppliersCreateDTO dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.AddAsyncCategorySupplier(dto);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "AddAsyncCategorySupplier");
                    throw;
                }
            }
        }

        public async Task UpdateAsyncCategorySupplier(int id, CategorySuppliersUpdateDTO dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.UpdateAsyncCategorySupplier(id, dto);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateAsyncCategorySupplier");
                    throw;
                }
            }
        }

        public async Task SoftDeleteAsyncCategorySupplier(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.SoftDeleteAsyncCategorySupplier(id);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteAsyncCategorySupplier");
                    throw;
                }
            }
        }

        public async Task RestoreAsyncCategorySupplier(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.RestoreAsyncCategorySupplier(id);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreAsyncCategorySupplier");
                    throw;
                }
            }
        }
    }
}