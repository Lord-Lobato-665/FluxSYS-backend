using FluxSYS_backend.Application.DTOs.Departments;
using FluxSYS_backend.Domain.IServices;

namespace FluxSYS_backend.Application.Services
{
    public class DepartmentsService : IDepartments
    {
        private readonly IDepartments _repository;

        public DepartmentsService(IDepartments repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DepartmentReadDTO>> GetAllAsyncDepartments()
        {
            return await _repository.GetAllAsyncDepartments();
        }

        public async Task AddAsyncDepartment(DepartmentCreateDTO dto)
        {
            await _repository.AddAsyncDepartment(dto);
        }

        public async Task UpdateAsyncDepartment(int id, DepartmentUpdateDTO dto)
        {
            await _repository.UpdateAsyncDepartment(id, dto);
        }

        public async Task SoftDeleteAsyncDepartment(int id)
        {
            await _repository.SoftDeleteAsyncDepartment(id);
        }

        public async Task RestoreAsyncDepartment(int id)
        {
            await _repository.RestoreAsyncDepartment(id);
        }
    }
}