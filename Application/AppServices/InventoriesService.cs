using FluxSYS_backend.Application.DTOs.Inventories;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Application.Services
{
    public class InventoriesService : IInventories
    {
        private readonly IInventories _repository;
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public InventoriesService(
            IInventories repository,
            ApplicationDbContext context,
            ErrorLogService errorLogService)
        {
            _repository = repository;
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<InventoryReadDTO>> GetAllAsyncInventories()
        {
            return await _repository.GetAllAsyncInventories();
        }

        public async Task<IEnumerable<InventoryReadDTO>> GetInventoriesByCompanyIdAsync(int companyId)
        {
            return await _repository.GetInventoriesByCompanyIdAsync(companyId);
        }

        public async Task<InventoryReadByIdDTO> GetInventoryByIdAsync(int id)
        {
            return await _repository.GetInventoryByIdAsync(id);
        }

        public async Task AddAsyncInventory(InventoryCreateDTO dto, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.AddAsyncInventory(dto, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "AddAsyncInventory");
                    throw;
                }
            }
        }

        public async Task UpdateAsyncInventory(int id, InventoryUpdateDTO dto, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.UpdateAsyncInventory(id, dto, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateAsyncInventory");
                    throw;
                }
            }
        }

        public async Task SoftDeleteAsyncInventory(int id, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.SoftDeleteAsyncInventory(id, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteAsyncInventory");
                    throw;
                }
            }
        }

        public async Task RestoreAsyncInventory(int id, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.RestoreAsyncInventory(id, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreAsyncInventory");
                    throw;
                }
            }
        }

        public async Task<byte[]> GetPDF(string companyName, string departmentName)
        {
            return await _repository.GetPDF(companyName, departmentName);
        }
    }
}