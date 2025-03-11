using FluxSYS_backend.Application.DTOs.Suppliers;
using FluxSYS_backend.Domain.IServices;

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

        public async Task AddAsyncSupplier(SupplierCreateDTO dto)
        {
            await _repository.AddAsyncSupplier(dto);
        }

        public async Task UpdateAsyncSupplier(int id, SupplierUpdateDTO dto)
        {
            await _repository.UpdateAsyncSupplier(id, dto);
        }

        public async Task SoftDeleteAsyncSupplier(int id)
        {
            await _repository.SoftDeleteAsyncSupplier(id);
        }

        public async Task RestoreAsyncSupplier(int id)
        {
            await _repository.RestoreAsyncSupplier(id);
        }
    }
}