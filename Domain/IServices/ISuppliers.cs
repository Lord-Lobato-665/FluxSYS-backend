using FluxSYS_backend.Application.DTOs.Suppliers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Domain.IServices
{
    public interface ISuppliers
    {
        Task<IEnumerable<SupplierReadDTO>> GetAllAsyncSuppliers();
        Task AddAsyncSupplier(SupplierCreateDTO dto, string nameUser, string nameDepartment);
        Task UpdateAsyncSupplier(int id, SupplierUpdateDTO dto, string nameUser, string nameDepartment);
        Task SoftDeleteAsyncSupplier(int id, string nameUser, string nameDepartment);
        Task RestoreAsyncSupplier(int id, string nameUser, string nameDepartment);
    }
}