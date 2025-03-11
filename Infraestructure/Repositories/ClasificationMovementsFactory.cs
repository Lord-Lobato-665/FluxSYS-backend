using FluxSYS_backend.Application.DTOs.ClasificationMovements;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Infrastructure.Repositories
{
    public class ClasificationMovementsRepository : IClasificationMovements
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public ClasificationMovementsRepository(ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<ClasificationMovementReadDTO>> GetAllAsyncClasificationMovements()
        {
            try
            {
                return await _context.ClasificationsMovements
                    .Select(c => new ClasificationMovementReadDTO
                    {
                        Id_clasification_movement = c.Id_clasification_movement,
                        Name_clasification_movement = c.Name_clasification_movement,
                        Delete_log_clasification_movement = c.Delete_log_clasification_movement
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllClasificationMovements");
                return new List<ClasificationMovementReadDTO>();
            }
        }

        public async Task<ClasificationMovementReadDTO> GetByIdAsyncClasificationMovement(int id)
        {
            try
            {
                var clasificationMovement = await _context.ClasificationsMovements.FindAsync(id);
                if (clasificationMovement == null) return null;

                return new ClasificationMovementReadDTO
                {
                    Id_clasification_movement = clasificationMovement.Id_clasification_movement,
                    Name_clasification_movement = clasificationMovement.Name_clasification_movement,
                    Delete_log_clasification_movement = clasificationMovement.Delete_log_clasification_movement
                };
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetByIdClasificationMovement");
                return null;
            }
        }

        public async Task AddAsyncClasificationMovement(ClasificationMovementCreateDTO dto)
        {
            try
            {
                var clasificationMovement = new ClasificationMovements
                {
                    Name_clasification_movement = dto.Name_clasification_movement
                };
                _context.ClasificationsMovements.Add(clasificationMovement);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateClasificationMovement");
            }
        }

        public async Task UpdateAsyncClasificationMovement(int id, ClasificationMovementUpdateDTO dto)
        {
            try
            {
                var clasificationMovement = await _context.ClasificationsMovements.FindAsync(id);
                if (clasificationMovement == null)
                {
                    throw new KeyNotFoundException("Clasificación de movimiento no encontrado para actualizar");
                }

                clasificationMovement.Name_clasification_movement = dto.Name_clasification_movement;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateClasificationMovement");
            }
        }

        public async Task SoftDeleteAsyncClasificationMovement(int id)
        {
            try
            {
                var clasificationMovement = await _context.ClasificationsMovements.FindAsync(id);
                if (clasificationMovement != null)
                {
                    clasificationMovement.Delete_log_clasification_movement = true;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Clasificación de movimiento no encontrado para eliminar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteClasificationMovement");
            }
        }

        public async Task RestoreAsyncClasificationMovement(int id)
        {
            try
            {
                var clasificationMovement = await _context.ClasificationsMovements
                    .FirstOrDefaultAsync(c => c.Id_clasification_movement == id && c.Delete_log_clasification_movement);

                if (clasificationMovement != null)
                {
                    clasificationMovement.Delete_log_clasification_movement = false;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Clasificación de movimiento no encontrado para restaurar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreClasificationMovement");
            }
        }
    }
}
