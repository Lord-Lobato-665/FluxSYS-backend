using FluxSYS_backend.Application.DTOs.Users;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Infrastructure.Repositories
{
    public class UsersRepository : IUsers
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public UsersRepository(ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<UserReadDTO>> GetAllAsyncUsers()
        {
            try
            {
                return await _context.Users
                    .Select(u => new UserReadDTO
                    {
                        Id_user = u.Id_user,
                        Name_user = u.Name_user,
                        Mail_user = u.Mail_user,
                        Phone_user = u.Phone_user,
                        Name_role = u.Roles.Name_role,
                        Name_position = u.Positions.Name_position,
                        Name_department = u.Departments.Name_deparment,
                        Name_company = u.Companies.Name_company,
                        Name_module = u.Modules.Name_module,
                        Date_insert = u.Date_insert,
                        Date_update = u.Date_update,
                        Date_delete = u.Date_delete,
                        Date_restore = u.Date_restore,
                        Delete_log_user = u.Delete_log_user
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllUsers");
                return new List<UserReadDTO>();
            }
        }

        public async Task AddAsyncUser(UserCreateDTO dto)
        {
            try
            {
                var user = new Users
                {
                    Name_user = dto.Name_user,
                    Mail_user = dto.Mail_user,
                    Phone_user = dto.Phone_user,
                    Password_user = dto.Password_user,
                    Id_rol_Id = dto.Id_rol_Id,
                    Id_position_Id = dto.Id_position_Id,
                    Id_department_Id = dto.Id_department_Id,
                    Id_company_Id = dto.Id_company_Id,
                    Id_module_Id = dto.Id_module_Id,
                    Date_insert = DateTime.UtcNow // Marca de tiempo para la creación
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateUser");
            }
        }

        public async Task UpdateAsyncUser(int id, UserUpdateDTO dto)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    throw new KeyNotFoundException("Usuario no encontrado para actualizar");
                }

                user.Name_user = dto.Name_user;
                user.Mail_user = dto.Mail_user;
                user.Phone_user = dto.Phone_user;
                user.Password_user = dto.Password_user;
                user.Id_rol_Id = dto.Id_rol_Id;
                user.Id_position_Id = dto.Id_position_Id;
                user.Id_department_Id = dto.Id_department_Id;
                user.Id_company_Id = dto.Id_company_Id;
                user.Id_module_Id = dto.Id_module_Id;
                user.Date_update = DateTime.UtcNow; // Marca de tiempo para la actualización

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateUser");
            }
        }

        public async Task SoftDeleteAsyncUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    user.Delete_log_user = true;
                    user.Date_delete = DateTime.UtcNow; // Marca de tiempo para la eliminación
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Usuario no encontrado para eliminar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteUser");
            }
        }

        public async Task RestoreAsyncUser(int id)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id_user == id && u.Delete_log_user);

                if (user != null)
                {
                    user.Delete_log_user = false;
                    user.Date_restore = DateTime.UtcNow; // Marca de tiempo para la restauración
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Usuario no encontrado para restaurar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreUser");
            }
        }
    }
}