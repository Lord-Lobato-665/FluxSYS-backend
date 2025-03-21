using FluxSYS_backend.Application.DTOs.CategoriesPurchaseOrders;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Application.Services
{
    public class CategoriesPurchaseOrdersService : ICategoriesPurchaseOrders
    {
        private readonly ICategoriesPurchaseOrders _repository;
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public CategoriesPurchaseOrdersService(
            ICategoriesPurchaseOrders repository,
            ApplicationDbContext context,
            ErrorLogService errorLogService)
        {
            _repository = repository;
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<CategoryPurchaseOrderReadDTO>> GetAllAsyncCategoriesPurchaseOrders()
        {
            return await _repository.GetAllAsyncCategoriesPurchaseOrders();
        }

        public async Task<IEnumerable<CategoryPurchaseOrderReadDTO>> GetCategoriesPurchaseOrdersByCompanyIdAsync(int companyId)
        {
            return await _repository.GetCategoriesPurchaseOrdersByCompanyIdAsync(companyId);
        }

        public async Task AddAsyncCategoryPurchaseOrder(CategoryPurchaseOrderCreateDTO dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.AddAsyncCategoryPurchaseOrder(dto);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "AddAsyncCategoryPurchaseOrder");
                    throw;
                }
            }
        }

        public async Task UpdateAsyncCategoryPurchaseOrder(int id, CategoryPurchaseOrderUpdateDTO dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.UpdateAsyncCategoryPurchaseOrder(id, dto);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateAsyncCategoryPurchaseOrder");
                    throw;
                }
            }
        }

        public async Task SoftDeleteAsyncCategoryPurchaseOrder(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.SoftDeleteAsyncCategoryPurchaseOrder(id);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteAsyncCategoryPurchaseOrder");
                    throw;
                }
            }
        }

        public async Task RestoreAsyncCategoryPurchaseOrder(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.RestoreAsyncCategoryPurchaseOrder(id);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreAsyncCategoryPurchaseOrder");
                    throw;
                }
            }
        }
    }
}