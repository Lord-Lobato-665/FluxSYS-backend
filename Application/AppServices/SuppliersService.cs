using FluxSYS_backend.Application.DTOs.Suppliers;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Application.Services
{
    public class SuppliersService : ISuppliers
    {
        private readonly ISuppliers _repository;
        private readonly ICategoriesSuppliers _categoriesSuppliersRepository;
        private readonly ICompanies _companiesRepository;
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public SuppliersService(
            ISuppliers repository,
            ICategoriesSuppliers categoriesSuppliersRepository,
            ICompanies companiesRepository,
            ApplicationDbContext context,
            ErrorLogService errorLogService)
        {
            _repository = repository;
            _categoriesSuppliersRepository = categoriesSuppliersRepository;
            _companiesRepository = companiesRepository;
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<SupplierReadDTO>> GetAllAsyncSuppliers()
        {
            return await _repository.GetAllAsyncSuppliers();
        }

        public async Task<IEnumerable<SupplierReadDTO>> GetSuppliersByCompanyIdAsync(int companyId)
        {
            return await _repository.GetSuppliersByCompanyIdAsync(companyId);
        }

        public async Task AddAsyncSupplier(SupplierCreateDTO dto, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.AddAsyncSupplier(dto, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "AddAsyncSupplier");
                    throw;
                }
            }
        }

        public async Task UpdateAsyncSupplier(int id, SupplierUpdateDTO dto, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.UpdateAsyncSupplier(id, dto, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateAsyncSupplier");
                    throw;
                }
            }
        }

        public async Task SoftDeleteAsyncSupplier(int id, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.SoftDeleteAsyncSupplier(id, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteAsyncSupplier");
                    throw;
                }
            }
        }

        public async Task RestoreAsyncSupplier(int id, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.RestoreAsyncSupplier(id, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreAsyncSupplier");
                    throw;
                }
            }
        }
    }
}