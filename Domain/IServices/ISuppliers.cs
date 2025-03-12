using FluxSYS_backend.Application.DTOs.Suppliers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Domain.IServices
{
    public interface ISuppliers
    {
        Task<IEnumerable<SupplierReadDTO>> GetAllAsyncSuppliers();
        Task AddAsyncSupplier(SupplierCreateDTO dto, int userId, int departmentId); // Nuevos parámetros
        Task UpdateAsyncSupplier(int id, SupplierUpdateDTO dto, int userId, int departmentId); // Nuevos parámetros
        Task SoftDeleteAsyncSupplier(int id, int userId, int departmentId); // Nuevos parámetros
        Task RestoreAsyncSupplier(int id, int userId, int departmentId); // Nuevos parámetros
    }
}