using FluxSYS_backend.Application.DTOs.MovementsTypes;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Application.Services
{
    public class MovementsTypesService : IMovementsTypes
    {
        private readonly IMovementsTypes _repository;
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public MovementsTypesService(
            IMovementsTypes repository,
            ApplicationDbContext context,
            ErrorLogService errorLogService)
        {
            _repository = repository;
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<MovementTypeReadDTO>> GetAllAsyncMovementsTypes()
        {
            return await _repository.GetAllAsyncMovementsTypes();
        }

        public async Task<IEnumerable<MovementTypeReadDTO>> GetMovementTypesByCompanyIdAsync(int companyId)
        {
            return await _repository.GetMovementTypesByCompanyIdAsync(companyId);
        }

        public async Task AddAsyncMovementType(MovementTypeCreateDTO dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.AddAsyncMovementType(dto);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "AddAsyncMovementType");
                    throw;
                }
            }
        }

        public async Task UpdateAsyncMovementType(int id, MovementTypeUpdateDTO dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.UpdateAsyncMovementType(id, dto);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateAsyncMovementType");
                    throw;
                }
            }
        }

        public async Task SoftDeleteAsyncMovementType(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.SoftDeleteAsyncMovementType(id);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteAsyncMovementType");
                    throw;
                }
            }
        }

        public async Task RestoreAsyncMovementType(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.RestoreAsyncMovementType(id);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreAsyncMovementType");
                    throw;
                }
            }
        }
    }
}