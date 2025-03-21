using FluxSYS_backend.Application.DTOs.Invoices;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Application.Services
{
    public class InvoicesService : IInvoices
    {
        private readonly IInvoices _repository;
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public InvoicesService(
            IInvoices repository,
            ApplicationDbContext context,
            ErrorLogService errorLogService)
        {
            _repository = repository;
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<InvoiceReadDTO>> GetAllAsyncInvoices()
        {
            return await _repository.GetAllAsyncInvoices();
        }

        public async Task<IEnumerable<InvoiceReadDTO>> GetInvoicesByCompanyIdAsync(int companyId)
        {
            return await _repository.GetInvoicesByCompanyIdAsync(companyId);
        }

        public async Task AddAsyncInvoice(InvoiceCreateDTO dto, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.AddAsyncInvoice(dto, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "AddAsyncInvoice");
                    throw;
                }
            }
        }

        public async Task UpdateAsyncInvoice(int id, InvoiceUpdateDTO dto, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.UpdateAsyncInvoice(id, dto, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateAsyncInvoice");
                    throw;
                }
            }
        }

        public async Task SoftDeleteAsyncInvoice(int id, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.SoftDeleteAsyncInvoice(id, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteAsyncInvoice");
                    throw;
                }
            }
        }

        public async Task RestoreAsyncInvoice(int id, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.RestoreAsyncInvoice(id, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreAsyncInvoice");
                    throw;
                }
            }
        }
    }
}