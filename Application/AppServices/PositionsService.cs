using FluxSYS_backend.Application.DTOs.Positions;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Application.Services
{
    public class PositionsService : IPositions
    {
        private readonly IPositions _repository;
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public PositionsService(
            IPositions repository,
            ApplicationDbContext context,
            ErrorLogService errorLogService)
        {
            _repository = repository;
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<PositionReadDTO>> GetAllAsyncPositions()
        {
            return await _repository.GetAllAsyncPositions();
        }

        public async Task<IEnumerable<PositionReadDTO>> GetPositionsByCompanyIdAsync(int companyId)
        {
            return await _repository.GetPositionsByCompanyIdAsync(companyId);
        }

        public async Task AddAsyncPosition(PositionCreateDTO dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.AddAsyncPosition(dto);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "AddAsyncPosition");
                    throw;
                }
            }
        }

        public async Task UpdateAsyncPosition(int id, PositionUpdateDTO dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.UpdateAsyncPosition(id, dto);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateAsyncPosition");
                    throw;
                }
            }
        }

        public async Task SoftDeleteAsyncPosition(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.SoftDeleteAsyncPosition(id);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteAsyncPosition");
                    throw;
                }
            }
        }

        public async Task RestoreAsyncPosition(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _repository.RestoreAsyncPosition(id);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreAsyncPosition");
                    throw;
                }
            }
        }
    }
}