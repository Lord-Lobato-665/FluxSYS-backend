using FluxSYS_backend.Application.DTOs.Roles;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Infrastructure.Repositories
{
    public class RolesRepository : IRoles
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public RolesRepository(ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<RoleReadDTO>> GetAllAsyncRoles()
        {
            try
            {
                return await _context.Roles
                    .Select(r => new RoleReadDTO
                    {
                        Id_role = r.Id_role,
                        Name_role = r.Name_role,
                        Delete_log_role = r.Delete_log_role
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllRoles");
                return new List<RoleReadDTO>();
            }
        }

        public async Task AddAsyncRole(RoleCreateDTO dto)
        {
            try
            {
                var role = new Roles
                {
                    Name_role = dto.Name_role
                };
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateRole");
            }
        }

        public async Task UpdateAsyncRole(int id, RoleUpdateDTO dto)
        {
            try
            {
                var role = await _context.Roles.FindAsync(id);
                if (role == null)
                {
                    throw new KeyNotFoundException("Rol no encontrado para actualizar");
                }

                role.Name_role = dto.Name_role;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateRole");
            }
        }

        public async Task SoftDeleteAsyncRole(int id)
        {
            try
            {
                var role = await _context.Roles.FindAsync(id);
                if (role != null)
                {
                    role.Delete_log_role = true;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Rol no encontrado para eliminar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteRole");
            }
        }

        public async Task RestoreAsyncRole(int id)
        {
            try
            {
                var role = await _context.Roles
                    .FirstOrDefaultAsync(r => r.Id_role == id && r.Delete_log_role);

                if (role != null)
                {
                    role.Delete_log_role = false;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Rol no encontrado para restaurar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreRole");
            }
        }
    }
}