using FluxSYS_backend.Application.DTOs.States;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Infrastructure.Repositories
{
    public class StatesRepository : IStates
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public StatesRepository(ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<StateReadDTO>> GetAllAsyncStates()
        {
            try
            {
                return await _context.States
                    .Select(s => new StateReadDTO
                    {
                        Id_state = s.Id_state,
                        Name_state = s.Name_state,
                        Name_company = s.Companies.Name_company,
                        Delete_log_state = s.Delete_log_state
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllStates");
                return new List<StateReadDTO>();
            }
        }

        public async Task AddAsyncState(StateCreateDTO dto)
        {
            try
            {
                var state = new States
                {
                    Name_state = dto.Name_state,
                    Id_company_Id = dto.Id_company_Id
                };
                _context.States.Add(state);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateState");
            }
        }

        public async Task UpdateAsyncState(int id, StateUpdateDTO dto)
        {
            try
            {
                var state = await _context.States.FindAsync(id);
                if (state == null)
                {
                    throw new KeyNotFoundException("Estado no encontrado para actualizar");
                }

                state.Name_state = dto.Name_state;
                state.Id_company_Id = dto.Id_company_Id;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateState");
            }
        }

        public async Task SoftDeleteAsyncState(int id)
        {
            try
            {
                var state = await _context.States.FindAsync(id);
                if (state != null)
                {
                    state.Delete_log_state = true;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Estado no encontrado para eliminar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteState");
            }
        }

        public async Task RestoreAsyncState(int id)
        {
            try
            {
                var state = await _context.States
                    .FirstOrDefaultAsync(s => s.Id_state == id && s.Delete_log_state);

                if (state != null)
                {
                    state.Delete_log_state = false;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Estado no encontrado para restaurar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreState");
            }
        }
    }
}