using FluxSYS_backend.Application.DTOs.Suppliers;

namespace FluxSYS_backend.Domain.IServices
{
    public interface ISuppliers
    {
        Task<IEnumerable<SupplierReadDTO>> GetAllAsyncSuppliers();
        Task AddAsyncSupplier(SupplierCreateDTO dto);
        Task UpdateAsyncSupplier(int id, SupplierUpdateDTO dto);
        Task SoftDeleteAsyncSupplier(int id);
        Task RestoreAsyncSupplier(int id);
    }
}