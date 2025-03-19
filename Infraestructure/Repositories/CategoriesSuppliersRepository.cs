using FluxSYS_backend.Application.DTOs.CategoriesSuppliers;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Infrastructure.Repositories
{
    public class CategoriesSuppliersRepository : ICategoriesSuppliers
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public CategoriesSuppliersRepository(ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<CategorySuppliersReadDTO>> GetAllAsyncCategoriesSuppliers()
        {
            try
            {
                return await _context.CategoriesSuppliers
                    .Select(cs => new CategorySuppliersReadDTO
                    {
                        Id_category_supplier = cs.Id_category_supplier,
                        Name_category_supplier = cs.Name_category_supplier,
                        Name_company = cs.Companies.Name_company,
                        Delete_log_category_supplier = cs.Delete_log_category_supplier
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllCategoriesSuppliers");
                return new List<CategorySuppliersReadDTO>();
            }
        }

        public async Task<IEnumerable<CategorySuppliersReadDTO>> GetCategoriesSuppliersByCompanyIdAsync(int companyId)
        {
            try
            {
                return await _context.CategoriesSuppliers
                    .Where(cs => cs.Companies.Id_company == companyId) // Filtra por ID de la compañía
                    .Select(cs => new CategorySuppliersReadDTO
                    {
                        Id_category_supplier = cs.Id_category_supplier,
                        Name_category_supplier = cs.Name_category_supplier,
                        Name_company = cs.Companies.Name_company,
                        Delete_log_category_supplier = cs.Delete_log_category_supplier
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetCategoriesSuppliersByCompanyIdAsync");
                return new List<CategorySuppliersReadDTO>();
            }
        }

        public async Task AddAsyncCategorySupplier(CategorySuppliersCreateDTO dto)
        {
            try
            {
                var categorySupplier = new CategoriesSuppliers
                {
                    Name_category_supplier = dto.Name_category_supplier,
                    Id_company_Id = dto.Id_company_Id
                };
                _context.CategoriesSuppliers.Add(categorySupplier);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateCategorySupplier");
            }
        }

        public async Task UpdateAsyncCategorySupplier(int id, CategorySuppliersUpdateDTO dto)
        {
            try
            {
                var categorySupplier = await _context.CategoriesSuppliers.FindAsync(id);
                if (categorySupplier == null)
                {
                    throw new KeyNotFoundException("Categoría de proveedor no encontrada para actualizar");
                }

                categorySupplier.Name_category_supplier = dto.Name_category_supplier;
                categorySupplier.Id_company_Id = dto.Id_company_Id;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateCategorySupplier");
            }
        }

        public async Task SoftDeleteAsyncCategorySupplier(int id)
        {
            try
            {
                var categorySupplier = await _context.CategoriesSuppliers.FindAsync(id);
                if (categorySupplier != null)
                {
                    categorySupplier.Delete_log_category_supplier = true;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Categoría de proveedor no encontrada para eliminar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteCategorySupplier");
            }
        }

        public async Task RestoreAsyncCategorySupplier(int id)
        {
            try
            {
                var categorySupplier = await _context.CategoriesSuppliers
                    .FirstOrDefaultAsync(cs => cs.Id_category_supplier == id && cs.Delete_log_category_supplier);

                if (categorySupplier != null)
                {
                    categorySupplier.Delete_log_category_supplier = false;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Categoría de proveedor no encontrada para restaurar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreCategorySupplier");
            }
        }
    }
}