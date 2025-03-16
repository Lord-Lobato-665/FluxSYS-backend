using FluxSYS_backend.Application.DTOs.InventoryMovements;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Domain.Models.PrincipalModels;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluxSYS_backend.Infrastructure.Repositories
{
    public class InventoryMovementsRepository : IInventoryMovements
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public InventoryMovementsRepository(ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<InventoryMovementReadDTO>> GetAllAsyncInventoryMovements()
        {
            try
            {
                var inventoryMovements = await _context.InventoryMovements
                    .Include(im => im.Inventories)
                    .Include(im => im.CategoriesProducts)
                    .Include(im => im.Departments)
                    .Include(im => im.Suppliers)
                    .Include(im => im.MovementsTypes)
                    .Include(im => im.Modules)
                    .Include(im => im.Companies)
                    .Include(im => im.Users)
                    .Select(im => new InventoryMovementReadDTO
                    {
                        Id_inventory_movement = im.Id_inventory_movement,
                        Amount_modify = im.Amount_modify,
                        Name_product = im.Inventories.Name_product,
                        Name_category_product = im.CategoriesProducts.Name_category_product,
                        Name_department = im.Departments.Name_deparment,
                        Name_supplier = im.Suppliers.Name_supplier,
                        Name_movement_type = im.MovementsTypes.Name_movement_type,
                        Name_module = im.Modules.Name_module,
                        Name_company = im.Companies.Name_company,
                        Name_user = im.Users.Name_user,
                        Date_insert = im.Date_insert.HasValue
                            ? im.Date_insert.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Date_update = im.Date_update.HasValue
                            ? im.Date_update.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Date_delete = im.Date_delete.HasValue
                            ? im.Date_delete.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Date_restore = im.Date_restore.HasValue
                            ? im.Date_restore.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Delete_log_inventory_movement = im.Delete_log_inventory_movement
                    })
                    .ToListAsync();

                return inventoryMovements;
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllInventoryMovements");
                return new List<InventoryMovementReadDTO>();
            }
        }

        public async Task<IEnumerable<InventoryMovementReadDTO>> GetAllByCompanyIdAsync(int idCompany)
        {
            try
            {
                var inventoryMovements = await _context.InventoryMovements
                    .Include(im => im.Inventories)
                    .Include(im => im.CategoriesProducts)
                    .Include(im => im.Departments)
                    .Include(im => im.Suppliers)
                    .Include(im => im.MovementsTypes)
                    .Include(im => im.Modules)
                    .Include(im => im.Companies)
                    .Include(im => im.Users)
                    .Where(im => im.Id_company_Id == idCompany) // Filtramos por Id_company
                    .Select(im => new InventoryMovementReadDTO
                    {
                        Id_inventory_movement = im.Id_inventory_movement,
                        Amount_modify = im.Amount_modify,
                        Name_product = im.Inventories.Name_product,
                        Name_category_product = im.CategoriesProducts.Name_category_product,
                        Name_department = im.Departments.Name_deparment,
                        Name_supplier = im.Suppliers.Name_supplier,
                        Name_movement_type = im.MovementsTypes.Name_movement_type,
                        Name_module = im.Modules.Name_module,
                        Name_company = im.Companies.Name_company,
                        Name_user = im.Users.Name_user,
                        Date_insert = im.Date_insert.HasValue
                            ? im.Date_insert.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Date_update = im.Date_update.HasValue
                            ? im.Date_update.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Date_delete = im.Date_delete.HasValue
                            ? im.Date_delete.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Date_restore = im.Date_restore.HasValue
                            ? im.Date_restore.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Delete_log_inventory_movement = im.Delete_log_inventory_movement
                    })
                    .ToListAsync();

                return inventoryMovements;
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllByCompanyIdAsync");
                return new List<InventoryMovementReadDTO>();
            }
        }
    }
}