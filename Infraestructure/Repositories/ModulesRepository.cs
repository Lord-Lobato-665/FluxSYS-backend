using FluxSYS_backend.Application.DTOs.Modules;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Infrastructure.Repositories
{
    public class ModulesRepository : IModules
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public ModulesRepository(ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<ModuleReadDTO>> GetAllAsyncModules()
        {
            try
            {
                return await _context.Modules
                    .Select(m => new ModuleReadDTO
                    {
                        Id_module = m.Id_module,
                        Name_module = m.Name_module,
                        Delete_log_module = m.Delete_log_module
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllModules");
                return new List<ModuleReadDTO>();
            }
        }

        public async Task AddAsyncModule(ModuleCreateDTO dto)
        {
            try
            {
                var module = new Modules
                {
                    Name_module = dto.Name_module
                };
                _context.Modules.Add(module);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateModule");
            }
        }

        public async Task UpdateAsyncModule(int id, ModuleUpdateDTO dto)
        {
            try
            {
                var module = await _context.Modules.FindAsync(id);
                if (module == null)
                {
                    throw new KeyNotFoundException("Módulo no encontrado para actualizar");
                }

                module.Name_module = dto.Name_module;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateModule");
            }
        }

        public async Task SoftDeleteAsyncModule(int id)
        {
            try
            {
                var module = await _context.Modules.FindAsync(id);
                if (module != null)
                {
                    module.Delete_log_module = true;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Módulo no encontrado para eliminar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteModule");
            }
        }

        public async Task RestoreAsyncModule(int id)
        {
            try
            {
                var module = await _context.Modules
                    .FirstOrDefaultAsync(m => m.Id_module == id && m.Delete_log_module);

                if (module != null)
                {
                    module.Delete_log_module = false;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Módulo no encontrado para restaurar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreModule");
            }
        }
    }
}