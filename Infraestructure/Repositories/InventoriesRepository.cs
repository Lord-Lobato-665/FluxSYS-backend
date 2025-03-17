using DevExpress.DataAccess.ObjectBinding;
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

        public async Task<InventoryReadByIdDTO> GetInventoryByIdAsync(int id)
        {
            try
            {
                var inventory = await _context.Inventories
                    .Include(i => i.CategoriesProducts)
                    .Include(i => i.States)
                    .Include(i => i.MovementsTypes)
                    .Include(i => i.Suppliers)
                    .Include(i => i.Departments)
                    .Include(i => i.Modules)
                    .Include(i => i.Companies)
                    .Include(i => i.Users)
                    .Include(i => i.SuppliersProducts)
                        .ThenInclude(sp => sp.Suppliers)
                    .Include(i => i.InvoicesProducts)
                        .ThenInclude(ip => ip.Invoices)
                    .Include(i => i.OrdersProducts)
                        .ThenInclude(op => op.PurchaseOrders)
                    .FirstOrDefaultAsync(i => i.Id_inventory_product == id);

                if (inventory == null)
                {
                    return null; // O podrías lanzar una excepción si lo prefieres
                }

                var inventoryDto = new InventoryReadByIdDTO
                {
                    Id_inventory_product = inventory.Id_inventory_product,
                    Name_product = inventory.Name_product,
                    Stock_product = inventory.Stock_product,
                    Price_product = inventory.Price_product,
                    Name_category_product = inventory.CategoriesProducts.Name_category_product,
                    Name_state = inventory.States.Name_state,
                    Name_movement_type = inventory.MovementsTypes.Name_movement_type,
                    Name_supplier = inventory.Suppliers.Name_supplier,
                    Name_department = inventory.Departments.Name_deparment,
                    Name_module = inventory.Modules.Name_module,
                    Name_company = inventory.Companies.Name_company,
                    Name_user = inventory.Users.Name_user,
                    Date_insert = inventory.Date_insert.HasValue ? inventory.Date_insert.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A",
                    Date_update = inventory.Date_update.HasValue ? inventory.Date_update.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A",
                    Date_delete = inventory.Date_delete.HasValue ? inventory.Date_delete.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A",
                    Date_restore = inventory.Date_restore.HasValue ? inventory.Date_restore.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A",
                    Delete_log_inventory = inventory.Delete_log_inventory,
                    Suppliers = inventory.SuppliersProducts
                        .Select(sp => new SupplierSimpleReadDTO
                        {
                            Id_supplier = sp.Suppliers.Id_supplier,
                            Name_supplier = sp.Suppliers.Name_supplier,
                            Mail_supplier = sp.Suppliers.Mail_supplier,
                            Phone_supplier = sp.Suppliers.Phone_supplier
                        }).ToList(),
                    Invoices = inventory.InvoicesProducts
                        .Select(ip => new InvoiceSimpleReadDTO
                        {
                            Id_invoice = ip.Invoices.Id_invoice,
                            Name_invoice = ip.Invoices.Name_invoice
                        }).ToList(),
                    PurchaseOrders = inventory.OrdersProducts
                        .Select(op => new PurchaseOrderSimpleReadDTO
                        {
                            Id_purchase_order = op.PurchaseOrders.Id_purchase_order,
                            Name_purchase_order = op.PurchaseOrders.Name_purchase_order
                        }).ToList()
                };

                return inventoryDto;
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetInventoryById");
                return null;
            }
        }

        public async Task AddAsyncInventory(InventoryCreateDTO dto, string nameUser, string nameDepartment)
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
                    Id_module_Id = 3, // Módulo de inventarios
                    Id_company_Id = dto.Id_company_Id,
                    Id_user_Id = dto.Id_user_Id,
                    Date_insert = DateTime.Now,
                    Delete_log_inventory = false
                };
                _context.Inventories.Add(inventory);
                await _context.SaveChangesAsync();

                // Registrar en la tabla de auditoría
                var audit = new Audits
                {
                    Date_insert = DateTime.Now,
                    Amount_modify = dto.Stock_product, // Cantidad de stock agregado
                    Name_user = nameUser, // Nombre del usuario desde el localStorage
                    Name_department = nameDepartment, // Nombre del departamento desde el localStorage
                    Name_module = "Inventarios", // Módulo de inventarios
                    Name_company = (await _context.Companies.FindAsync(dto.Id_company_Id))?.Name_company ?? "Desconocida",
                    Delete_log_audits = false
                };
                _context.Audits.Add(audit);
                await _context.SaveChangesAsync();

                // Registrar en la tabla de movimientos de inventario
                var movement = new InventoryMovements
                {
                    Amount_modify = dto.Stock_product, // Cantidad de stock agregado
                    Id_inventory_product_Id = inventory.Id_inventory_product,
                    Id_category_product_Id = dto.Id_category_product_Id,
                    Id_department_Id = dto.Id_department_Id,
                    Id_supplier_Id = dto.Id_supplier_Id,
                    Id_movements_types_Id = dto.Id_movement_type_Id,
                    Id_module_Id = 3,
                    Id_company_Id = dto.Id_company_Id,
                    Id_user_Id = dto.Id_user_Id,
                    Date_insert = DateTime.Now,
                    Delete_log_inventory_movement = false
                };
                _context.InventoryMovements.Add(movement);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateInventory");
            }
        }

        public async Task UpdateAsyncInventory(int id, InventoryUpdateDTO dto, string nameUser, string nameDepartment)
        {
            try
            {
                var inventory = await _context.Inventories.FindAsync(id);
                if (inventory == null || inventory.Delete_log_inventory)
                {
                    throw new KeyNotFoundException("Producto de inventario no encontrado.");
                }

                // Calcular la diferencia en el stock
                int stockDifference = dto.Stock_product - inventory.Stock_product;

                // Registrar en la tabla de auditoría
                var audit = new Audits
                {
                    Date_update = DateTime.Now,
                    Amount_modify = stockDifference, // Diferencia en el stock
                    Name_user = nameUser, // Nombre del usuario desde el localStorage
                    Name_department = nameDepartment, // Nombre del departamento desde el localStorage
                    Name_module = "Inventarios", // Módulo de inventarios
                    Name_company = (await _context.Companies.FindAsync(dto.Id_company_Id))?.Name_company ?? "Desconocida",
                    Delete_log_audits = false
                };
                _context.Audits.Add(audit);
                await _context.SaveChangesAsync();

                // Registrar en la tabla de movimientos de inventario
                var movement = new InventoryMovements
                {
                    Amount_modify = stockDifference, // Diferencia en el stock
                    Id_inventory_product_Id = inventory.Id_inventory_product,
                    Id_category_product_Id = dto.Id_category_product_Id,
                    Id_department_Id = dto.Id_department_Id,
                    Id_supplier_Id = dto.Id_supplier_Id,
                    Id_movements_types_Id = dto.Id_movement_type_Id,
                    Id_module_Id = 3,
                    Id_company_Id = dto.Id_company_Id,
                    Id_user_Id = dto.Id_user_Id,
                    Date_update = DateTime.Now,
                    Delete_log_inventory_movement = false
                };
                _context.InventoryMovements.Add(movement);
                await _context.SaveChangesAsync();

                // Actualizar datos del producto
                inventory.Name_product = dto.Name_product;
                inventory.Stock_product = dto.Stock_product;
                inventory.Price_product = dto.Price_product;
                inventory.Id_category_product_Id = dto.Id_category_product_Id;
                inventory.Id_state_Id = dto.Id_state_Id;
                inventory.Id_movement_type_Id = dto.Id_movement_type_Id;
                inventory.Id_supplier_Id = dto.Id_supplier_Id;
                inventory.Id_department_Id = dto.Id_department_Id;
                inventory.Id_module_Id = 3;
                inventory.Id_company_Id = dto.Id_company_Id;
                inventory.Id_user_Id = dto.Id_user_Id;
                inventory.Date_update = DateTime.Now;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateInventory");
            }
        }

        public async Task SoftDeleteAsyncInventory(int id, string nameUser, string nameDepartment)
        {
            try
            {
                var inventory = await _context.Inventories.FindAsync(id);
                if (inventory != null)
                {
                    // Registrar en la tabla de auditoría
                    var audit = new Audits
                    {
                        Date_delete = DateTime.Now,
                        Amount_modify = -inventory.Stock_product, // Se elimina todo el stock
                        Name_user = nameUser, // Nombre del usuario desde el localStorage
                        Name_department = nameDepartment, // Nombre del departamento desde el localStorage
                        Name_module = "Inventarios", // Módulo de inventarios
                        Name_company = (await _context.Companies.FindAsync(inventory.Id_company_Id))?.Name_company ?? "Desconocida",
                        Delete_log_audits = false
                    };
                    _context.Audits.Add(audit);
                    await _context.SaveChangesAsync();

                    // Registrar en la tabla de movimientos de inventario
                    var movement = new InventoryMovements
                    {
                        Amount_modify = -inventory.Stock_product, // Se elimina todo el stock
                        Id_inventory_product_Id = inventory.Id_inventory_product,
                        Id_category_product_Id = inventory.Id_category_product_Id,
                        Id_department_Id = inventory.Id_department_Id,
                        Id_supplier_Id = inventory.Id_supplier_Id,
                        Id_movements_types_Id = inventory.Id_movement_type_Id,
                        Id_module_Id = inventory.Id_module_Id,
                        Id_company_Id = inventory.Id_company_Id,
                        Id_user_Id = inventory.Id_user_Id,
                        Date_delete = DateTime.Now,
                        Delete_log_inventory_movement = false
                    };
                    _context.InventoryMovements.Add(movement);
                    await _context.SaveChangesAsync();

                    // Eliminación lógica
                    inventory.Delete_log_inventory = true;
                    inventory.Date_delete = DateTime.Now;
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

        public async Task RestoreAsyncInventory(int id, string nameUser, string nameDepartment)
        {
            try
            {
                var inventory = await _context.Inventories
                    .FirstOrDefaultAsync(i => i.Id_inventory_product == id && i.Delete_log_inventory);

                if (inventory != null)
                {
                    // Registrar en la tabla de auditoría
                    var audit = new Audits
                    {
                        Date_restore = DateTime.Now,
                        Amount_modify = inventory.Stock_product, // Se restaura todo el stock
                        Name_user = nameUser, // Nombre del usuario desde el localStorage
                        Name_department = nameDepartment, // Nombre del departamento desde el localStorage
                        Name_module = "Inventarios", // Módulo de inventarios
                        Name_company = (await _context.Companies.FindAsync(inventory.Id_company_Id))?.Name_company ?? "Desconocida",
                        Delete_log_audits = false
                    };
                    _context.Audits.Add(audit);
                    await _context.SaveChangesAsync();

                    // Registrar en la tabla de movimientos de inventario
                    var movement = new InventoryMovements
                    {
                        Amount_modify = inventory.Stock_product, // Se restaura todo el stock
                        Id_inventory_product_Id = inventory.Id_inventory_product,
                        Id_category_product_Id = inventory.Id_category_product_Id,
                        Id_department_Id = inventory.Id_department_Id,
                        Id_supplier_Id = inventory.Id_supplier_Id,
                        Id_movements_types_Id = inventory.Id_movement_type_Id,
                        Id_module_Id = inventory.Id_module_Id,
                        Id_company_Id = inventory.Id_company_Id,
                        Id_user_Id = inventory.Id_user_Id,
                        Date_restore = DateTime.Now,
                        Delete_log_inventory_movement = false
                    };
                    _context.InventoryMovements.Add(movement);
                    await _context.SaveChangesAsync();

                    // Restauración
                    inventory.Delete_log_inventory = false;
                    inventory.Date_restore = DateTime.Now;
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

        public async Task<byte[]> GetPDF(string companyName, string departmentName)
        {
            // Filtra los datos de inventarios por el nombre de la compañía y departamento
            var inventories = await _context.Inventories
                .Include(i => i.CategoriesProducts)
                .Include(i => i.States)
                .Include(i => i.MovementsTypes)
                .Include(i => i.Suppliers)
                .Include(i => i.Departments)
                .Include(i => i.Modules)
                .Include(i => i.Companies)
                .Include(i => i.Users)
                .Where(i => i.Companies.Name_company.Contains(companyName) && i.Departments.Name_deparment.Contains(departmentName)) // Filtra por nombre de compañía y departamento
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

            // Crear el objeto DTO para el reporte
            var reportePdf = new InventoryReadDTOPDF
            {
                Fecha = DateTime.Now.ToString("dddd dd MMMM yyyy", new System.Globalization.CultureInfo("es-ES")),
                Hora = DateTime.Now.ToString("HH:mm:ss"),
                Inventories = inventories
            };

            // Configura el ObjectDataSource
            ObjectDataSource source = new ObjectDataSource { DataSource = reportePdf };

            // Crea el reporte
            var report = new FluxSYS_backend.Application.PDFs.Inventory.InventoryReport
            {
                DataSource = source,
                DataMember = "Inventories"
            };

            using (var memory = new MemoryStream())
            {
                // Exporta el reporte a PDF
                await report.ExportToPdfAsync(memory);
                return memory.ToArray();
            }
        }
    }
}