using FluxSYS_backend.Application.DTOs.Invoices;
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
    public class InvoicesRepository : IInvoices
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public InvoicesRepository(ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<InvoiceReadDTO>> GetAllAsyncInvoices()
        {
            try
            {
                var invoices = await _context.Invoices
                    .Include(i => i.PurchaseOrders)
                    .Include(i => i.Suppliers)
                    .Include(i => i.Departments)
                    .Include(i => i.Modules)
                    .Include(i => i.Companies)
                    .Include(i => i.InvoicesProducts)
                        .ThenInclude(ip => ip.Inventories)
                    .Select(i => new InvoiceReadDTO
                    {
                        Id_invoice = i.Id_invoice,
                        Name_invoice = i.Name_invoice,
                        Amount_items_in_the_invoice = i.InvoicesProducts.Count(),
                        Total_price_invoice = i.InvoicesProducts.Sum(ip => ip.Unit_price * ip.Quantity),
                        Name_purchase_order = i.PurchaseOrders.Name_purchase_order,
                        Name_supplier = i.Suppliers.Name_supplier,
                        Name_department = i.Departments.Name_deparment,
                        Name_module = i.Modules.Name_module,
                        Name_company = i.Companies.Name_company,
                        Date_insert = i.Date_insert.HasValue
                            ? i.Date_insert.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Date_update = i.Date_update.HasValue
                            ? i.Date_update.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Date_delete = i.Date_delete.HasValue
                            ? i.Date_delete.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Date_restore = i.Date_restore.HasValue
                            ? i.Date_restore.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Delete_log_invoices = i.Delete_log_invoices,
                        Products = i.InvoicesProducts.Any()
                            ? i.InvoicesProducts.Select(ip => new InvoiceProductReadDTO
                            {
                                Id_inventory_product = ip.Inventories.Id_inventory_product,
                                Name_product = ip.Inventories.Name_product,
                                Quantity = ip.Quantity,
                                Unit_price = ip.Unit_price
                            }).ToList()
                            : new List<InvoiceProductReadDTO>()
                    })
                    .ToListAsync();

                // Manejar "Sin productos asociados" fuera de la consulta LINQ
                foreach (var invoice in invoices)
                {
                    if (!invoice.Products.Any())
                    {
                        invoice.Products = new List<InvoiceProductReadDTO>
                        {
                            new InvoiceProductReadDTO { Name_product = "Sin productos asociados" }
                        };
                    }
                }

                return invoices;
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllInvoices");
                return new List<InvoiceReadDTO>();
            }
        }

        public async Task AddAsyncInvoice(InvoiceCreateDTO dto, int userId, int departmentId)
        {
            try
            {
                var invoice = new Invoices
                {
                    Name_invoice = dto.Name_invoice,
                    Id_purchase_order_Id = dto.Id_purchase_order_Id,
                    Id_supplier_Id = dto.Id_supplier_Id,
                    Id_department_Id = dto.Id_department_Id,
                    Id_module_Id = 4,
                    Id_company_Id = dto.Id_company_Id,
                    Date_insert = DateTime.Now,
                    Delete_log_invoices = false
                };
                _context.Invoices.Add(invoice);
                await _context.SaveChangesAsync(); // Guardar para generar el ID de la factura

                // Si hay productos asociados, añadirlos
                if (dto.Products != null && dto.Products.Any())
                {
                    var invoiceProducts = new List<InvoicesProducts>();
                    foreach (var product in dto.Products)
                    {
                        // Obtener el precio del producto desde el inventario
                        var inventoryProduct = await _context.Inventories
                            .FirstOrDefaultAsync(i => i.Id_inventory_product == product.Id_inventory_product_Id);

                        if (inventoryProduct == null)
                        {
                            throw new KeyNotFoundException($"El producto con ID {product.Id_inventory_product_Id} no existe en el inventario.");
                        }

                        invoiceProducts.Add(new InvoicesProducts
                        {
                            Id_invoice_Id = invoice.Id_invoice,
                            Id_inventory_product_Id = product.Id_inventory_product_Id,
                            Quantity = product.Quantity,
                            Unit_price = inventoryProduct.Price_product // Tomar el precio del inventario
                        });
                    }

                    await _context.InvoicesProducts.AddRangeAsync(invoiceProducts);
                    await _context.SaveChangesAsync();
                }

                // Registrar en la tabla de auditoría
                var audit = new Audits
                {
                    Date_insert = DateTime.Now,
                    Amount_modify = dto.Products?.Count ?? 0, // Cantidad de ítems en la factura
                    Id_user_Id = userId, // Usar el ID del usuario desde los parámetros
                    Id_department_Id = departmentId, // Usar el ID del departamento desde los parámetros
                    Id_module_Id = 4, // Módulo de facturas
                    Id_company_Id = dto.Id_company_Id, // Usar el ID de la compañía desde el DTO
                    Delete_log_audits = false
                };
                _context.Audits.Add(audit);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateInvoice");
            }
        }

        public async Task UpdateAsyncInvoice(int id, InvoiceUpdateDTO dto, int userId, int departmentId)
        {
            try
            {
                var invoice = await _context.Invoices
                    .Include(i => i.InvoicesProducts)
                    .FirstOrDefaultAsync(i => i.Id_invoice == id && !i.Delete_log_invoices);

                if (invoice == null)
                {
                    throw new KeyNotFoundException("Factura no encontrada.");
                }

                // Calcular la cantidad de ítems en la factura antes y después de la actualización
                int itemsBeforeUpdate = invoice.InvoicesProducts.Count;
                int itemsAfterUpdate = dto.Products?.Count ?? 0;

                // Registrar en la tabla de auditoría
                var audit = new Audits
                {
                    Date_update = DateTime.Now,
                    Amount_modify = itemsAfterUpdate - itemsBeforeUpdate, // Diferencia en la cantidad de ítems
                    Id_user_Id = userId, // Usar el ID del usuario desde los parámetros
                    Id_department_Id = departmentId, // Usar el ID del departamento desde los parámetros
                    Id_module_Id = 4, // Módulo de facturas
                    Id_company_Id = dto.Id_company_Id, // Usar el ID de la compañía desde el DTO
                    Delete_log_audits = false
                };
                _context.Audits.Add(audit);
                await _context.SaveChangesAsync();

                // Actualizar datos de la factura
                invoice.Name_invoice = dto.Name_invoice;
                invoice.Id_purchase_order_Id = dto.Id_purchase_order_Id;
                invoice.Id_supplier_Id = dto.Id_supplier_Id;
                invoice.Id_department_Id = dto.Id_department_Id;
                invoice.Id_module_Id = 4;
                invoice.Id_company_Id = dto.Id_company_Id;
                invoice.Date_update = DateTime.Now;

                // Manejo de productos asociados
                var existingProducts = invoice.InvoicesProducts.ToList();

                // Productos eliminados
                var removedProducts = existingProducts
                    .Where(ep => !dto.Products.Any(p => p.Id_inventory_product_Id == ep.Id_inventory_product_Id))
                    .ToList();
                _context.InvoicesProducts.RemoveRange(removedProducts);

                // Productos actualizados o nuevos
                foreach (var productDto in dto.Products)
                {
                    var existingProduct = existingProducts
                        .FirstOrDefault(ep => ep.Id_inventory_product_Id == productDto.Id_inventory_product_Id);

                    if (existingProduct != null)
                    {
                        // Actualizar cantidad
                        existingProduct.Quantity = productDto.Quantity;

                        // Obtener el precio actualizado desde el inventario
                        var inventoryProduct = await _context.Inventories
                            .FirstOrDefaultAsync(i => i.Id_inventory_product == productDto.Id_inventory_product_Id);

                        if (inventoryProduct != null)
                        {
                            existingProduct.Unit_price = inventoryProduct.Price_product; // Actualizar el precio
                        }
                    }
                    else
                    {
                        // Agregar nuevo producto
                        var inventoryProduct = await _context.Inventories
                            .FirstOrDefaultAsync(i => i.Id_inventory_product == productDto.Id_inventory_product_Id);

                        if (inventoryProduct == null)
                        {
                            throw new KeyNotFoundException($"El producto con ID {productDto.Id_inventory_product_Id} no existe en el inventario.");
                        }

                        var newProduct = new InvoicesProducts
                        {
                            Id_invoice_Id = invoice.Id_invoice,
                            Id_inventory_product_Id = productDto.Id_inventory_product_Id,
                            Quantity = productDto.Quantity,
                            Unit_price = inventoryProduct.Price_product // Tomar el precio del inventario
                        };
                        await _context.InvoicesProducts.AddAsync(newProduct);
                    }
                }

                // Guardar cambios
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateInvoice");
            }
        }

        public async Task SoftDeleteAsyncInvoice(int id, int userId, int departmentId)
        {
            try
            {
                var invoice = await _context.Invoices
                    .Include(i => i.InvoicesProducts)
                    .FirstOrDefaultAsync(i => i.Id_invoice == id);

                if (invoice != null)
                {
                    // Registrar en la tabla de auditoría
                    var audit = new Audits
                    {
                        Date_delete = DateTime.Now,
                        Amount_modify = -invoice.InvoicesProducts.Count, // Cantidad de ítems eliminados
                        Id_user_Id = userId, // Usar el ID del usuario desde los parámetros
                        Id_department_Id = departmentId, // Usar el ID del departamento desde los parámetros
                        Id_module_Id = invoice.Id_module_Id, // Módulo de facturas
                        Id_company_Id = invoice.Id_company_Id, // Usar el ID de la compañía desde la factura
                        Delete_log_audits = false
                    };
                    _context.Audits.Add(audit);
                    await _context.SaveChangesAsync();

                    // Eliminación lógica
                    invoice.Delete_log_invoices = true;
                    invoice.Date_delete = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Factura no encontrada para eliminar.");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteInvoice");
            }
        }

        public async Task RestoreAsyncInvoice(int id, int userId, int departmentId)
        {
            try
            {
                var invoice = await _context.Invoices
                    .Include(i => i.InvoicesProducts)
                    .FirstOrDefaultAsync(i => i.Id_invoice == id && i.Delete_log_invoices);

                if (invoice != null)
                {
                    // Registrar en la tabla de auditoría
                    var audit = new Audits
                    {
                        Date_restore = DateTime.Now,
                        Amount_modify = invoice.InvoicesProducts.Count, // Cantidad de ítems restaurados
                        Id_user_Id = userId, // Usar el ID del usuario desde los parámetros
                        Id_department_Id = departmentId, // Usar el ID del departamento desde los parámetros
                        Id_module_Id = invoice.Id_module_Id, // Módulo de facturas
                        Id_company_Id = invoice.Id_company_Id, // Usar el ID de la compañía desde la factura
                        Delete_log_audits = false
                    };
                    _context.Audits.Add(audit);
                    await _context.SaveChangesAsync();

                    // Restauración
                    invoice.Delete_log_invoices = false;
                    invoice.Date_restore = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Factura no encontrada para restaurar.");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreInvoice");
            }
        }
    }
}