using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Infraestructure.Data;
using FluxSYS_backend.Application.DTOs.Companies;
using Microsoft.EntityFrameworkCore;
using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Application.Services;

namespace FluxSYS_backend.Infraestructure.Repositories;

public class CompaniesRepository : ICompanies
{
    private readonly ApplicationDbContext _context;
    private readonly ErrorLogService _errorLogService;

    public CompaniesRepository(ApplicationDbContext context, ErrorLogService errorLogService)
    {
        _context = context;
        _errorLogService = errorLogService;
    }

    public async Task<IEnumerable<CompanyReadDTO>> GetAllAsyncCompanies()
    {
        try
        {
            return await _context.Companies
                //.Where(c => !c.Delete_log_company)
                .Select(c => new CompanyReadDTO
                {
                    Id_company = c.Id_company,
                    Name_company = c.Name_company,
                    Delete_log_company = c.Delete_log_company
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllCompanies");
            return new List<CompanyReadDTO>();
        }
    }

    public async Task AddAsyncCompany(CompanyCreateDTO dto)
    {
        try
        {
            var company = new Companies
            {
                Name_company = dto.Name_company
            };

            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateCompany");
        }
    }

    public async Task UpdateAsyncCompany(int id, CompanyUpdateDTO dto)
    {
        try
        {
            var company = await _context.Companies
                .FirstOrDefaultAsync(c => c.Id_company == id);

            if (company == null)
            {
                throw new KeyNotFoundException("Compañía no encontrada");
            }

            company.Name_company = dto.Name_company;

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateCompany");
        }
    }

    public async Task SoftDeleteAsync(int id)
    {
        try
        {
            // Buscar la compañía por su ID
            var company = await _context.Companies
                .Include(c => c.States)
                .Include(c => c.MovementsTypes)
                .Include(c => c.Departments)
                .Include(c => c.Positions)
                .Include(c => c.CategoriesProducts)
                .Include(c => c.CategoriesPurchaseOrders)
                .Include(c => c.CategoriesSuppliers)
                .Include(c => c.Users)
                .Include(c => c.Suppliers)
                .Include(c => c.Inventories)
                .Include(c => c.PurchaseOrders)
                .Include(c => c.Invoices)
                .Include(c => c.InventoryMovements)
                .FirstOrDefaultAsync(c => c.Id_company == id);

            if (company == null)
            {
                throw new KeyNotFoundException("Compañía no encontrada para eliminar");
            }

            // Marcar la compañía como eliminada lógicamente
            company.Delete_log_company = true;

            // Marcar todas las entidades relacionadas como eliminadas lógicamente
            foreach (var state in company.States)
            {
                state.Delete_log_state = true;
            }

            foreach (var movementType in company.MovementsTypes)
            {
                movementType.Delete_log_movement_type = true;
            }

            foreach (var department in company.Departments)
            {
                department.Delete_log_department = true;
            }

            foreach (var position in company.Positions)
            {
                position.Delete_log_position = true;
            }

            foreach (var categoryProduct in company.CategoriesProducts)
            {
                categoryProduct.Delete_log_category_product = true;
            }

            foreach (var categoryPurchaseOrder in company.CategoriesPurchaseOrders)
            {
                categoryPurchaseOrder.Delete_log_category_purchase_order = true;
            }

            foreach (var categorySupplier in company.CategoriesSuppliers)
            {
                categorySupplier.Delete_log_category_supplier = true;
            }

            foreach (var user in company.Users)
            {
                user.Delete_log_user = true;
            }

            foreach (var supplier in company.Suppliers)
            {
                supplier.Delete_log_suppliers = true;
            }

            foreach (var inventory in company.Inventories)
            {
                inventory.Delete_log_inventory = true;
            }

            foreach (var purchaseOrder in company.PurchaseOrders)
            {
                purchaseOrder.Delete_log_purchase_orders = true;
            }

            foreach (var invoice in company.Invoices)
            {
                invoice.Delete_log_invoices = true;
            }

            foreach (var inventoryMovement in company.InventoryMovements)
            {
                inventoryMovement.Delete_log_inventory_movement = true;
            }

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteCompany");
        }
    }

    public async Task RestoreAsync(int id)
    {
        try
        {
            // Buscar la compañía por su ID, incluyendo todas las entidades relacionadas
            var company = await _context.Companies
                .Include(c => c.States)
                .Include(c => c.MovementsTypes)
                .Include(c => c.Departments)
                .Include(c => c.Positions)
                .Include(c => c.CategoriesProducts)
                .Include(c => c.CategoriesPurchaseOrders)
                .Include(c => c.CategoriesSuppliers)
                .Include(c => c.Users)
                .Include(c => c.Suppliers)
                .Include(c => c.Inventories)
                .Include(c => c.PurchaseOrders)
                .Include(c => c.Invoices)
                .Include(c => c.InventoryMovements)
                .FirstOrDefaultAsync(c => c.Id_company == id && c.Delete_log_company);

            if (company == null)
            {
                throw new KeyNotFoundException("Compañía no encontrada para restaurar");
            }

            // Restaurar la compañía
            company.Delete_log_company = false;

            // Restaurar todas las entidades relacionadas
            foreach (var state in company.States)
            {
                state.Delete_log_state = false;
            }

            foreach (var movementType in company.MovementsTypes)
            {
                movementType.Delete_log_movement_type = false;
            }

            foreach (var department in company.Departments)
            {
                department.Delete_log_department = false;
            }

            foreach (var position in company.Positions)
            {
                position.Delete_log_position = false;
            }

            foreach (var categoryProduct in company.CategoriesProducts)
            {
                categoryProduct.Delete_log_category_product = false;
            }

            foreach (var categoryPurchaseOrder in company.CategoriesPurchaseOrders)
            {
                categoryPurchaseOrder.Delete_log_category_purchase_order = false;
            }

            foreach (var categorySupplier in company.CategoriesSuppliers)
            {
                categorySupplier.Delete_log_category_supplier = false;
            }

            foreach (var user in company.Users)
            {
                user.Delete_log_user = false;
            }

            foreach (var supplier in company.Suppliers)
            {
                supplier.Delete_log_suppliers = false;
            }

            foreach (var inventory in company.Inventories)
            {
                inventory.Delete_log_inventory = false;
            }

            foreach (var purchaseOrder in company.PurchaseOrders)
            {
                purchaseOrder.Delete_log_purchase_orders = false;
            }

            foreach (var invoice in company.Invoices)
            {
                invoice.Delete_log_invoices = false;
            }

            foreach (var inventoryMovement in company.InventoryMovements)
            {
                inventoryMovement.Delete_log_inventory_movement = false;
            }

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreCompany");
        }
    }
}
