using FluxSYS_backend.Application.DTOs.Departments;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IDepartments
    {
        Task<IEnumerable<DepartmentReadDTO>> GetAllAsyncDepartments();
        Task<IEnumerable<DepartmentReadDTO>> GetDepartmentsByCompanyIdAsync(int companyId);
        Task AddAsyncDepartment(DepartmentCreateDTO dto);
        Task UpdateAsyncDepartment(int id, DepartmentUpdateDTO dto);
        Task SoftDeleteAsyncDepartment(int id);
        Task RestoreAsyncDepartment(int id);
    }
}