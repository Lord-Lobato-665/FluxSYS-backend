using FluxSYS_backend.Application.DTOs.CategoriesPurchaseOrders;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Infrastructure.Repositories
{
    public class CategoriesPurchaseOrdersRepository : ICategoriesPurchaseOrders
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public CategoriesPurchaseOrdersRepository(ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<CategoryPurchaseOrderReadDTO>> GetAllAsyncCategoriesPurchaseOrders()
        {
            try
            {
                return await _context.CategoriesPurchaseOrders
                    .Select(cpo => new CategoryPurchaseOrderReadDTO
                    {
                        Id_category_purchase_order = cpo.Id_category_purchase_order,
                        Name_category_purchase_order = cpo.Name_category_purchase_order,
                        Name_company = cpo.Companies.Name_company,
                        Delete_log_category_purchase_order = cpo.Delete_log_category_purchase_order
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllCategoriesPurchaseOrders");
                return new List<CategoryPurchaseOrderReadDTO>();
            }
        }

        public async Task<IEnumerable<CategoryPurchaseOrderReadDTO>> GetCategoriesPurchaseOrdersByCompanyIdAsync(int companyId)
        {
            try
            {
                return await _context.CategoriesPurchaseOrders
                    .Where(cpo => cpo.Companies.Id_company == companyId) // Filtra por ID de la compañía
                    .Select(cpo => new CategoryPurchaseOrderReadDTO
                    {
                        Id_category_purchase_order = cpo.Id_category_purchase_order,
                        Name_category_purchase_order = cpo.Name_category_purchase_order,
                        Name_company = cpo.Companies.Name_company,
                        Delete_log_category_purchase_order = cpo.Delete_log_category_purchase_order
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetCategoriesPurchaseOrdersByCompanyIdAsync");
                return new List<CategoryPurchaseOrderReadDTO>();
            }
        }

        public async Task AddAsyncCategoryPurchaseOrder(CategoryPurchaseOrderCreateDTO dto)
        {
            try
            {
                var categoryPurchaseOrder = new CategoriesPurchaseOrders
                {
                    Name_category_purchase_order = dto.Name_category_purchase_order,
                    Id_company_Id = dto.Id_company_Id
                };
                _context.CategoriesPurchaseOrders.Add(categoryPurchaseOrder);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateCategoryPurchaseOrder");
            }
        }

        public async Task UpdateAsyncCategoryPurchaseOrder(int id, CategoryPurchaseOrderUpdateDTO dto)
        {
            try
            {
                var categoryPurchaseOrder = await _context.CategoriesPurchaseOrders.FindAsync(id);
                if (categoryPurchaseOrder == null)
                {
                    throw new KeyNotFoundException("Categoría de orden de compra no encontrada para actualizar");
                }

                categoryPurchaseOrder.Name_category_purchase_order = dto.Name_category_purchase_order;
                categoryPurchaseOrder.Id_company_Id = dto.Id_company_Id;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateCategoryPurchaseOrder");
            }
        }

        public async Task SoftDeleteAsyncCategoryPurchaseOrder(int id)
        {
            try
            {
                var categoryPurchaseOrder = await _context.CategoriesPurchaseOrders.FindAsync(id);
                if (categoryPurchaseOrder != null)
                {
                    categoryPurchaseOrder.Delete_log_category_purchase_order = true;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Categoría de orden de compra no encontrada para eliminar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteCategoryPurchaseOrder");
            }
        }

        public async Task RestoreAsyncCategoryPurchaseOrder(int id)
        {
            try
            {
                var categoryPurchaseOrder = await _context.CategoriesPurchaseOrders
                    .FirstOrDefaultAsync(cpo => cpo.Id_category_purchase_order == id && cpo.Delete_log_category_purchase_order);

                if (categoryPurchaseOrder != null)
                {
                    categoryPurchaseOrder.Delete_log_category_purchase_order = false;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Categoría de orden de compra no encontrada para restaurar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreCategoryPurchaseOrder");
            }
        }
    }
}