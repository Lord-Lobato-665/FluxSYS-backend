using FluxSYS_backend.Application.DTOs.PurchaseOrders;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Application.Services
{
    public class PurchaseOrdersService : IPurchaseOrders
    {
        private readonly IPurchaseOrders _repository;
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public PurchaseOrdersService(
            IPurchaseOrders repository,
            ApplicationDbContext context,
            ErrorLogService errorLogService)
        {
            _repository = repository;
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<PurchaseOrderReadDTO>> GetAllAsyncPurchaseOrders()
        {
            return await _repository.GetAllAsyncPurchaseOrders();
        }

        public async Task<IEnumerable<PurchaseOrderReadDTO>> GetPurchaseOrdersByCompanyIdAsync(int companyId)
        {
            return await _repository.GetPurchaseOrdersByCompanyIdAsync(companyId);
        }

        public async Task AddAsyncPurchaseOrder(PurchaseOrderCreateDTO dto, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.AddAsyncPurchaseOrder(dto, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "AddAsyncPurchaseOrder");
                    throw;
                }
            }
        }

        public async Task UpdateAsyncPurchaseOrder(int id, PurchaseOrderUpdateDTO dto, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.UpdateAsyncPurchaseOrder(id, dto, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateAsyncPurchaseOrder");
                    throw;
                }
            }
        }

        public async Task SoftDeleteAsyncPurchaseOrder(int id, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.SoftDeleteAsyncPurchaseOrder(id, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteAsyncPurchaseOrder");
                    throw;
                }
            }
        }

        public async Task RestoreAsyncPurchaseOrder(int id, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.RestoreAsyncPurchaseOrder(id, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreAsyncPurchaseOrder");
                    throw;
                }
            }
        }
    }
}