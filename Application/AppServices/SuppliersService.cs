using FluxSYS_backend.Application.DTOs.Suppliers;
using FluxSYS_backend.Domain.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Application.Services
{
    public class SuppliersService : ISuppliers
    {
        private readonly ISuppliers _repository;

        public SuppliersService(ISuppliers repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SupplierReadDTO>> GetAllAsyncSuppliers()
        {
            return await _repository.GetAllAsyncSuppliers();
        }

        public async Task AddAsyncSupplier(SupplierCreateDTO dto, int userId, int departmentId)
        {
            await _repository.AddAsyncSupplier(dto, userId, departmentId);
        }

        public async Task UpdateAsyncSupplier(int id, SupplierUpdateDTO dto, int userId, int departmentId)
        {
            await _repository.UpdateAsyncSupplier(id, dto, userId, departmentId);
        }

        public async Task SoftDeleteAsyncSupplier(int id, int userId, int departmentId)
        {
            await _repository.SoftDeleteAsyncSupplier(id, userId, departmentId);
        }

        public async Task RestoreAsyncSupplier(int id, int userId, int departmentId)
        {
            await _repository.RestoreAsyncSupplier(id, userId, departmentId);
        }
    }
}