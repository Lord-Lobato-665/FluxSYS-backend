using FluxSYS_backend.Application.DTOs.Positions;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Infrastructure.Repositories
{
    public class PositionsRepository : IPositions
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public PositionsRepository(ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<PositionReadDTO>> GetAllAsyncPositions()
        {
            try
            {
                return await _context.Positions
                    .Select(p => new PositionReadDTO
                    {
                        Name_position = p.Name_position,
                        Name_company = p.Companies.Name_company,
                        Delete_log_position = p.Delete_log_position
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllPositions");
                return new List<PositionReadDTO>();
            }
        }

        public async Task AddAsyncPosition(PositionCreateDTO dto)
        {
            try
            {
                var position = new Positions
                {
                    Name_position = dto.Name_position,
                    Id_company_Id = dto.Id_company_Id
                };
                _context.Positions.Add(position);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreatePosition");
            }
        }

        public async Task UpdateAsyncPosition(int id, PositionUpdateDTO dto)
        {
            try
            {
                var position = await _context.Positions.FindAsync(id);
                if (position == null)
                {
                    throw new KeyNotFoundException("Posición no encontrada para actualizar");
                }

                position.Name_position = dto.Name_position;
                position.Id_company_Id = dto.Id_company_Id;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdatePosition");
            }
        }

        public async Task SoftDeleteAsyncPosition(int id)
        {
            try
            {
                var position = await _context.Positions.FindAsync(id);
                if (position != null)
                {
                    position.Delete_log_position = true;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Posición no encontrada para eliminar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeletePosition");
            }
        }

        public async Task RestoreAsyncPosition(int id)
        {
            try
            {
                var position = await _context.Positions
                    .FirstOrDefaultAsync(p => p.Id_position == id && p.Delete_log_position);

                if (position != null)
                {
                    position.Delete_log_position = false;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Posición no encontrada para restaurar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestorePosition");
            }
        }
    }
}