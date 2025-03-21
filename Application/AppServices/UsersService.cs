using FluxSYS_backend.Application.DTOs.Users;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Application.Services
{
    public class UsersService : IUsers
    {
        private readonly IUsers _repository;
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public UsersService(
            IUsers repository,
            ApplicationDbContext context,
            ErrorLogService errorLogService)
        {
            _repository = repository;
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<UserReadDTO>> GetAllAsyncUsers()
        {
            return await _repository.GetAllAsyncUsers();
        }

        public async Task<IEnumerable<UserReadDTO>> GetUsersByCompanyIdAsync(int companyId)
        {
            return await _repository.GetUsersByCompanyIdAsync(companyId);
        }

        public async Task AddAsyncUser(UserCreateDTO dto, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.AddAsyncUser(dto, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "AddAsyncUser");
                    throw;
                }
            }
        }

        public async Task UpdateAsyncUser(int id, UserUpdateDTO dto, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.UpdateAsyncUser(id, dto, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateAsyncUser");
                    throw;
                }
            }
        }

        public async Task SoftDeleteAsyncUser(int id, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.SoftDeleteAsyncUser(id, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteAsyncUser");
                    throw;
                }
            }
        }

        public async Task RestoreAsyncUser(int id, string nameUser, string nameDepartment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.RestoreAsyncUser(id, nameUser, nameDepartment);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreAsyncUser");
                    throw;
                }
            }
        }
    }
}