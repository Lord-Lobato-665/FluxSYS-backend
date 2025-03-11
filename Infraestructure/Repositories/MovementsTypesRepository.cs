using FluxSYS_backend.Application.DTOs.MovementsTypes;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Infrastructure.Repositories
{
    public class MovementsTypesRepository : IMovementsTypes
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public MovementsTypesRepository(ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<MovementTypeReadDTO>> GetAllAsyncMovementsTypes()
        {
            try
            {
                return await _context.MovementsTypes
                    .Select(mt => new MovementTypeReadDTO
                    {
                        Name_movement_type = mt.Name_movement_type,
                        Name_company = mt.Companies.Name_company,
                        Name_clasification_movement = mt.ClasificationsMovements.Name_clasification_movement,
                        Delete_log_movement_type = mt.Delete_log_movement_type
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllMovementsTypes");
                return new List<MovementTypeReadDTO>();
            }
        }

        public async Task AddAsyncMovementType(MovementTypeCreateDTO dto)
        {
            try
            {
                var movementType = new MovementsTypes
                {
                    Name_movement_type = dto.Name_movement_type,
                    Id_company_Id = dto.Id_company_Id,
                    Id_clasification_movement_Id = dto.Id_clasification_movement_Id
                };
                _context.MovementsTypes.Add(movementType);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateMovementType");
            }
        }

        public async Task UpdateAsyncMovementType(int id, MovementTypeUpdateDTO dto)
        {
            try
            {
                var movementType = await _context.MovementsTypes.FindAsync(id);
                if (movementType == null)
                {
                    throw new KeyNotFoundException("Tipo de movimiento no encontrado para actualizar");
                }

                movementType.Name_movement_type = dto.Name_movement_type;
                movementType.Id_company_Id = dto.Id_company_Id;
                movementType.Id_clasification_movement_Id = dto.Id_clasification_movement_Id;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateMovementType");
            }
        }

        public async Task SoftDeleteAsyncMovementType(int id)
        {
            try
            {
                var movementType = await _context.MovementsTypes.FindAsync(id);
                if (movementType != null)
                {
                    movementType.Delete_log_movement_type = true;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Tipo de movimiento no encontrado para eliminar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteMovementType");
            }
        }

        public async Task RestoreAsyncMovementType(int id)
        {
            try
            {
                var movementType = await _context.MovementsTypes
                    .FirstOrDefaultAsync(mt => mt.Id_movement_type == id && mt.Delete_log_movement_type);

                if (movementType != null)
                {
                    movementType.Delete_log_movement_type = false;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Tipo de movimiento no encontrado para restaurar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreMovementType");
            }
        }
    }
}