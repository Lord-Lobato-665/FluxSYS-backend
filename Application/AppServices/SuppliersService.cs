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

        public async Task AddAsyncSupplier(SupplierCreateDTO dto, string nameUser, string nameDepartment)
        {
            // Verificar si la categoría del proveedor existe
            var categories = await _categoriesSuppliersRepository.GetAllAsyncCategoriesSuppliers();
            if (!categories.Any(c => c.Id_category_supplier == dto.Id_category_supplier_Id))
                throw new KeyNotFoundException("La categoría de proveedor especificada no existe.");

            // Verificar si la compañía existe
            var companies = await _companiesRepository.GetAllAsyncCompanies();
            if (!companies.Any(c => c.Id_company == dto.Id_company_Id))
                throw new KeyNotFoundException("La compañía especificada no existe.");

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
