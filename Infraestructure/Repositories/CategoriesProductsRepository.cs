using FluxSYS_backend.Application.DTOs.CategoriesProducts;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Infrastructure.Repositories
{
    public class CategoriesProductsRepository : ICategoriesProducts
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public CategoriesProductsRepository(ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<CategoryProductsReadDTO>> GetAllAsyncCategoriesProducts()
        {
            try
            {
                return await _context.CategoriesProducts
                    .Select(cp => new CategoryProductsReadDTO
                    {
                        Id_category_product = cp.Id_category_product,
                        Name_category_product = cp.Name_category_product,
                        Name_company = cp.Companies.Name_company,
                        Delete_log_category_product = cp.Delete_log_category_product
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllCategoriesProducts");
                return new List<CategoryProductsReadDTO>();
            }
        }

        public async Task<IEnumerable<CategoryProductsReadDTO>> GetCategoriesByCompanyIdAsync(int idCompany)
        {
            try
            {
                return await _context.CategoriesProducts
                    .Include(cp => cp.Companies) // Incluye la compañía
                    .Where(cp => cp.Id_company_Id == idCompany) // Filtra por Id_company y excluye eliminados lógicamente
                    .Select(cp => new CategoryProductsReadDTO
                    {
                        Id_category_product = cp.Id_category_product,
                        Name_category_product = cp.Name_category_product,
                        Name_company = cp.Companies.Name_company,
                        Delete_log_category_product = cp.Delete_log_category_product
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetCategoriesByCompanyIdAsync");
                return new List<CategoryProductsReadDTO>();
            }
        }

        public async Task AddAsyncCategoryProduct(CategoryProductsCreateDTO dto)
        {
            try
            {
                var categoryProduct = new CategoriesProducts
                {
                    Name_category_product = dto.Name_category_product,
                    Id_company_Id = dto.Id_company_Id
                };
                _context.CategoriesProducts.Add(categoryProduct);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateCategoryProduct");
            }
        }

        public async Task UpdateAsyncCategoryProduct(int id, CategoryProductsUpdateDTO dto)
        {
            try
            {
                var categoryProduct = await _context.CategoriesProducts.FindAsync(id);
                if (categoryProduct == null)
                {
                    throw new KeyNotFoundException("Categoría de producto no encontrada para actualizar");
                }

                categoryProduct.Name_category_product = dto.Name_category_product;
                categoryProduct.Id_company_Id = dto.Id_company_Id;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateCategoryProduct");
            }
        }

        public async Task SoftDeleteAsyncCategoryProduct(int id)
        {
            try
            {
                var categoryProduct = await _context.CategoriesProducts.FindAsync(id);
                if (categoryProduct != null)
                {
                    categoryProduct.Delete_log_category_product = true;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Categoría de producto no encontrada para eliminar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteCategoryProduct");
            }
        }

        public async Task RestoreAsyncCategoryProduct(int id)
        {
            try
            {
                var categoryProduct = await _context.CategoriesProducts
                    .FirstOrDefaultAsync(cp => cp.Id_category_product == id && cp.Delete_log_category_product);

                if (categoryProduct != null)
                {
                    categoryProduct.Delete_log_category_product = false;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Categoría de producto no encontrada para restaurar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreCategoryProduct");
            }
        }
    }
}