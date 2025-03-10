using FluxSYS_backend.Application.DTOs.Departments;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Infrastructure.Repositories
{
    public class DepartmentsRepository : IDepartments
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public DepartmentsRepository(ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<DepartmentReadDTO>> GetAllAsyncDepartments()
        {
            try
            {
                return await _context.Departments
                    .Select(d => new DepartmentReadDTO
                    {
                        Id_department = d.Id_department,
                        Name_deparment = d.Name_deparment,
                        Name_company = d.Companies.Name_company,
                        Delete_log_department = d.Delete_log_department
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllDepartments");
                return new List<DepartmentReadDTO>();
            }
        }

        public async Task<DepartmentReadDTO> GetByIdAsyncDepartment(int id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null) return null;

                return new DepartmentReadDTO
                {
                    Id_department = department.Id_department,
                    Name_deparment = department.Name_deparment,
                    Name_company = department.Companies.Name_company,
                    Delete_log_department = department.Delete_log_department
                };
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetByIdDepartment");
                return null;
            }
        }

        public async Task AddAsyncDepartment(DepartmentCreateDTO dto)
        {
            try
            {
                var department = new Departments
                {
                    Name_deparment = dto.Name_deparment,
                    Id_company_Id = dto.Id_company_Id
                };
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateDepartment");
            }
        }

        public async Task UpdateAsyncDepartment(int id, DepartmentUpdateDTO dto)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null)
                {
                    throw new KeyNotFoundException("Departamento no encontrado para actualizar");
                }

                department.Name_deparment = dto.Name_deparment;
                department.Id_company_Id = dto.Id_company_Id;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateDepartment");
            }
        }

        public async Task SoftDeleteAsyncDepartment(int id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department != null)
                {
                    department.Delete_log_department = true;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Departamento no encontrado para eliminar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteDepartment");
            }
        }

        public async Task RestoreAsyncDepartment(int id)
        {
            try
            {
                var department = await _context.Departments
                    .FirstOrDefaultAsync(d => d.Id_department == id && d.Delete_log_department);

                if (department != null)
                {
                    department.Delete_log_department = false;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Departamento no encontrado para restaurar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreDepartment");
            }
        }
    }
}