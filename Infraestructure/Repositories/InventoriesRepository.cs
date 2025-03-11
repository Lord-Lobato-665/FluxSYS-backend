using FluxSYS_backend.Application.DTOs.Inventories;
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
    public class InventoriesRepository : IInventories
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public InventoriesRepository(ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<InventoryReadDTO>> GetAllAsyncInventories()
        {
            try
            {
                var inventories = await _context.Inventories
                    .Include(i => i.CategoriesProducts)
                    .Include(i => i.States)
                    .Include(i => i.MovementsTypes)
                    .Include(i => i.Suppliers)
                    .Include(i => i.Departments)
                    .Include(i => i.Modules)
                    .Include(i => i.Companies)
                    .Include(i => i.Users)
                    .Select(i => new InventoryReadDTO
                    {
                        Id_inventory_product = i.Id_inventory_product,
                        Name_product = i.Name_product,
                        Stock_product = i.Stock_product,
                        Price_product = i.Price_product,
                        Name_category_product = i.CategoriesProducts.Name_category_product,
                        Name_state = i.States.Name_state,
                        Name_movement_type = i.MovementsTypes.Name_movement_type,
                        Name_supplier = i.Suppliers.Name_supplier,
                        Name_department = i.Departments.Name_deparment,
                        Name_module = i.Modules.Name_module,
                        Name_company = i.Companies.Name_company,
                        Name_user = i.Users.Name_user,
                        Date_insert = i.Date_insert.HasValue ? i.Date_insert.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A",
                        Date_update = i.Date_update.HasValue ? i.Date_update.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A",
                        Date_delete = i.Date_delete.HasValue ? i.Date_delete.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A",
                        Date_restore = i.Date_restore.HasValue ? i.Date_restore.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A",
                        Delete_log_inventory = i.Delete_log_inventory
                    })
                    .ToListAsync();

                return inventories;
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllInventories");
                return new List<InventoryReadDTO>();
            }
        }

        public async Task AddAsyncInventory(InventoryCreateDTO dto)
        {
            try
            {
                var inventory = new Inventories
                {
                    Name_product = dto.Name_product,
                    Stock_product = dto.Stock_product,
                    Price_product = dto.Price_product,
                    Id_category_product_Id = dto.Id_category_product_Id,
                    Id_state_Id = dto.Id_state_Id,
                    Id_movement_type_Id = dto.Id_movement_type_Id,
                    Id_supplier_Id = dto.Id_supplier_Id,
                    Id_department_Id = dto.Id_department_Id,
                    Id_module_Id = dto.Id_module_Id,
                    Id_company_Id = dto.Id_company_Id,
                    Id_user_Id = dto.Id_user_Id,
                    Date_insert = DateTime.UtcNow,
                    Delete_log_inventory = false
                };
                _context.Inventories.Add(inventory);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateInventory");
            }
        }

        public async Task UpdateAsyncInventory(int id, InventoryUpdateDTO dto)
        {
            try
            {
                var inventory = await _context.Inventories.FindAsync(id);
                if (inventory == null || inventory.Delete_log_inventory)
                {
                    throw new KeyNotFoundException("Producto de inventario no encontrado.");
                }

                inventory.Name_product = dto.Name_product;
                inventory.Stock_product = dto.Stock_product;
                inventory.Price_product = dto.Price_product;
                inventory.Id_category_product_Id = dto.Id_category_product_Id;
                inventory.Id_state_Id = dto.Id_state_Id;
                inventory.Id_movement_type_Id = dto.Id_movement_type_Id;
                inventory.Id_supplier_Id = dto.Id_supplier_Id;
                inventory.Id_department_Id = dto.Id_department_Id;
                inventory.Id_module_Id = dto.Id_module_Id;
                inventory.Id_company_Id = dto.Id_company_Id;
                inventory.Id_user_Id = dto.Id_user_Id;
                inventory.Date_update = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateInventory");
            }
        }

        public async Task SoftDeleteAsyncInventory(int id)
        {
            try
            {
                var inventory = await _context.Inventories.FindAsync(id);
                if (inventory != null)
                {
                    inventory.Delete_log_inventory = true;
                    inventory.Date_delete = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Producto de inventario no encontrado para eliminar.");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteInventory");
            }
        }

        public async Task RestoreAsyncInventory(int id)
        {
            try
            {
                var inventory = await _context.Inventories
                    .FirstOrDefaultAsync(i => i.Id_inventory_product == id && i.Delete_log_inventory);

                if (inventory != null)
                {
                    inventory.Delete_log_inventory = false;
                    inventory.Date_restore = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Producto de inventario no encontrado para restaurar.");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreInventory");
            }
        }
    }
}