using FluxSYS_backend.Application.DTOs.Suppliers;
using FluxSYS_backend.Domain.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluxSYS_backend.Application.Services
{
    public class SuppliersService : ISuppliers
    {
        private readonly ISuppliers _repository;
        private readonly ICategoriesSuppliers _categoriesSuppliersRepository;
        private readonly ICompanies _companiesRepository;

        public SuppliersService(ISuppliers repository, ICategoriesSuppliers categoriesSuppliersRepository, ICompanies companiesRepository)
        {
            _repository = repository;
            _categoriesSuppliersRepository = categoriesSuppliersRepository;
            _companiesRepository = companiesRepository;
        }

        public async Task<IEnumerable<SupplierReadDTO>> GetAllAsyncSuppliers()
        {
            return await _repository.GetAllAsyncSuppliers();
        }

        public async Task<IEnumerable<SupplierReadDTO>> GetSuppliersByCompanyIdAsync(int companyId)
        {
            return await _repository.GetSuppliersByCompanyIdAsync(companyId);
        }

        public async Task AddAsyncSupplier(SupplierCreateDTO dto, string nameUser, string nameDepartment)
        {
            await _repository.AddAsyncSupplier(dto, nameUser, nameDepartment);
        }

        public async Task UpdateAsyncSupplier(int id, SupplierUpdateDTO dto, string nameUser, string nameDepartment)
        {
            await _repository.UpdateAsyncSupplier(id, dto, nameUser, nameDepartment);
        }

        public async Task SoftDeleteAsyncSupplier(int id, string nameUser, string nameDepartment)
        {
            await _repository.SoftDeleteAsyncSupplier(id, nameUser, nameDepartment);
        }

        public async Task RestoreAsyncSupplier(int id, string nameUser, string nameDepartment)
        {
            await _repository.RestoreAsyncSupplier(id, nameUser, nameDepartment);
        }
    }
}
