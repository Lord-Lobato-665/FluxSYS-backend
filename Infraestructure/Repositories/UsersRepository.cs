using FluxSYS_backend.Application.DTOs.Users;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Domain.Models.PrincipalModels;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task AddAsyncUser(UserCreateDTO dto, int userId, int departmentId)
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
                    Id_module_Id = 6,
                    Date_insert = DateTime.Now // Marca de tiempo para la creación
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Registrar en la tabla de auditoría
                var audit = new Audits
                {
                    Date_insert = DateTime.Now,
                    Amount_modify = 1, // Creación de 1 usuario
                    Id_user_Id = userId, // Usar el ID del usuario desde los parámetros
                    Id_department_Id = departmentId, // Usar el ID del departamento desde los parámetros
                    Id_module_Id = 6, // Módulo de usuarios
                    Id_company_Id = dto.Id_company_Id, // Usar el ID de la compañía desde el DTO
                    Delete_log_audits = false
                };
                _context.Audits.Add(audit);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateUser");
            }
        }

        public async Task UpdateAsyncUser(int id, UserUpdateDTO dto, int userId, int departmentId)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    throw new KeyNotFoundException("Usuario no encontrado para actualizar");
                }

                // Registrar en la tabla de auditoría
                var audit = new Audits
                {
                    Date_update = DateTime.Now,
                    Amount_modify = 1, // Actualización de 1 usuario
                    Id_user_Id = userId, // Usar el ID del usuario desde los parámetros
                    Id_department_Id = departmentId, // Usar el ID del departamento desde los parámetros
                    Id_module_Id = 6, // Módulo de usuarios
                    Id_company_Id = dto.Id_company_Id, // Usar el ID de la compañía desde el DTO
                    Delete_log_audits = false
                };
                _context.Audits.Add(audit);
                await _context.SaveChangesAsync();

                // Actualizar datos del usuario
                user.Name_user = dto.Name_user;
                user.Mail_user = dto.Mail_user;
                user.Phone_user = dto.Phone_user;
                user.Password_user = dto.Password_user;
                user.Id_rol_Id = dto.Id_rol_Id;
                user.Id_position_Id = dto.Id_position_Id;
                user.Id_department_Id = dto.Id_department_Id;
                user.Id_company_Id = dto.Id_company_Id;
                user.Id_module_Id = 6;
                user.Date_update = DateTime.Now; // Marca de tiempo para la actualización

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateUser");
            }
        }

        public async Task SoftDeleteAsyncUser(int id, int userId, int departmentId)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    // Registrar en la tabla de auditoría
                    var audit = new Audits
                    {
                        Date_delete = DateTime.Now,
                        Amount_modify = -1, // Eliminación de 1 usuario
                        Id_user_Id = userId, // Usar el ID del usuario desde los parámetros
                        Id_department_Id = departmentId, // Usar el ID del departamento desde los parámetros
                        Id_module_Id = user.Id_module_Id, // Módulo de usuarios
                        Id_company_Id = user.Id_company_Id, // Usar el ID de la compañía desde el usuario
                        Delete_log_audits = false
                    };
                    _context.Audits.Add(audit);
                    await _context.SaveChangesAsync();

                    // Eliminación lógica
                    user.Delete_log_user = true;
                    user.Date_delete = DateTime.Now; // Marca de tiempo para la eliminación
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

        public async Task RestoreAsyncUser(int id, int userId, int departmentId)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id_user == id && u.Delete_log_user);

                if (user != null)
                {
                    // Registrar en la tabla de auditoría
                    var audit = new Audits
                    {
                        Date_restore = DateTime.Now,
                        Amount_modify = 1, // Restauración de 1 usuario
                        Id_user_Id = userId, // Usar el ID del usuario desde los parámetros
                        Id_department_Id = departmentId, // Usar el ID del departamento desde los parámetros
                        Id_module_Id = user.Id_module_Id, // Módulo de usuarios
                        Id_company_Id = user.Id_company_Id, // Usar el ID de la compañía desde el usuario
                        Delete_log_audits = false
                    };
                    _context.Audits.Add(audit);
                    await _context.SaveChangesAsync();

                    // Restauración
                    user.Delete_log_user = false;
                    user.Date_restore = DateTime.Now; // Marca de tiempo para la restauración
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