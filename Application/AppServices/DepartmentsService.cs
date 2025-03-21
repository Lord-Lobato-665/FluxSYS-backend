using FluxSYS_backend.Application.DTOs.Departments;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Application.Services
{
    public class DepartmentsService : IDepartments
    {
        private readonly IDepartments _repository;
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public DepartmentsService(
            IDepartments repository,
            ApplicationDbContext context,
            ErrorLogService errorLogService)
        {
            _repository = repository;
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<DepartmentReadDTO>> GetAllAsyncDepartments()
        {
            return await _repository.GetAllAsyncDepartments();
        }

        public async Task<IEnumerable<DepartmentReadDTO>> GetDepartmentsByCompanyIdAsync(int companyId)
        {
            return await _repository.GetDepartmentsByCompanyIdAsync(companyId);
        }

        public async Task AddAsyncDepartment(DepartmentCreateDTO dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.AddAsyncDepartment(dto);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "AddAsyncDepartment");
                    throw;
                }
            }
        }

        public async Task UpdateAsyncDepartment(int id, DepartmentUpdateDTO dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.UpdateAsyncDepartment(id, dto);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateAsyncDepartment");
                    throw;
                }
            }
        }

        public async Task SoftDeleteAsyncDepartment(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.SoftDeleteAsyncDepartment(id);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteAsyncDepartment");
                    throw;
                }
            }
        }

        public async Task RestoreAsyncDepartment(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.RestoreAsyncDepartment(id);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreAsyncDepartment");
                    throw;
                }
            }
        }
    }
}