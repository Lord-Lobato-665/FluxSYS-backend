using FluxSYS_backend.Application.DTOs.Suppliers;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Domain.Models.PrincipalModels;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluxSYS_backend.Infrastructure.Repositories
{
    public class SuppliersRepository : ISuppliers
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public SuppliersRepository(ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<SupplierReadDTO>> GetAllAsyncSuppliers()
        {
            try
            {
                var suppliers = await _context.Suppliers
                    .Include(s => s.SuppliersProducts)
                        .ThenInclude(sp => sp.Inventories)
                    .Include(s => s.CategoriesSuppliers)
                    .Include(s => s.Modules)
                    .Include(s => s.Companies)
                    .Select(s => new SupplierReadDTO
                    {
                        Id_supplier = s.Id_supplier,
                        Name_supplier = s.Name_supplier,
                        Mail_supplier = s.Mail_supplier,
                        Phone_supplier = s.Phone_supplier,
                        Name_category_supplier = s.CategoriesSuppliers.Name_category_supplier,
                        Name_module = s.Modules.Name_module,
                        Name_company = s.Companies.Name_company,
                        Date_insert = s.Date_insert.HasValue
                            ? s.Date_insert.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Date_update = s.Date_update.HasValue
                            ? s.Date_update.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Date_delete = s.Date_delete.HasValue
                            ? s.Date_delete.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Date_restore = s.Date_restore.HasValue
                            ? s.Date_restore.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Delete_log_suppliers = s.Delete_log_suppliers,
                        Products_from_supplier = s.SuppliersProducts.Count(),
                        Products = s.SuppliersProducts.Any()
                            ? s.SuppliersProducts.Select(sp => new SupplierProductReadDTO
                            {
                                Id_inventory_product = sp.Inventories.Id_inventory_product,
                                Name_product = sp.Inventories.Name_product,
                                Suggested_price = sp.Suggested_price
                            }).ToList()
                            : new List<SupplierProductReadDTO>()
                    })
                    .ToListAsync();

                // Manejar "Sin productos asociados" fuera de la consulta LINQ
                foreach (var supplier in suppliers)
                {
                    if (!supplier.Products.Any())
                    {
                        supplier.Products = new List<SupplierProductReadDTO>
                        {
                            new SupplierProductReadDTO { Name_product = "Sin productos asociados" }
                        };
                    }
                }

                return suppliers;
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllSuppliers");
                return new List<SupplierReadDTO>();
            }
        }

        public async Task AddAsyncSupplier(SupplierCreateDTO dto, int userId, int departmentId)
        {
            try
            {
                var supplier = new Suppliers
                {
                    Name_supplier = dto.Name_supplier,
                    Mail_supplier = dto.Mail_supplier,
                    Phone_supplier = dto.Phone_supplier,
                    Id_category_supplier_Id = dto.Id_category_supplier_Id,
                    Id_module_Id = 1,
                    Id_company_Id = dto.Id_company_Id,
                    Date_insert = DateTime.Now,
                    Delete_log_suppliers = false
                };
                _context.Suppliers.Add(supplier);
                await _context.SaveChangesAsync(); // Guardar para generar el ID del proveedor

                int amountModify = 0; // Cantidad de productos asociados al proveedor

                // Si hay productos asociados, añadirlos
                if (dto.Products != null && dto.Products.Any())
                {
                    var supplierProducts = dto.Products.Select(product => new SuppliersProducts
                    {
                        Id_supplier_Id = supplier.Id_supplier,
                        Id_inventory_product_Id = product.Id_inventory_product_Id,
                        Suggested_price = product.Suggested_price
                    }).ToList();

                    await _context.SuppliersProducts.AddRangeAsync(supplierProducts);
                    await _context.SaveChangesAsync();

                    amountModify = supplierProducts.Count; // Cantidad de productos asociados
                }

                // Registrar en la tabla de auditoría
                var audit = new Audits
                {
                    Date_insert = DateTime.Now,
                    Amount_modify = amountModify, // Cantidad de productos asociados
                    Id_user_Id = userId, // Usar el ID del usuario desde los parámetros
                    Id_department_Id = departmentId, // Usar el ID del departamento desde los parámetros
                    Id_module_Id = 1, // Módulo de proveedores
                    Id_company_Id = dto.Id_company_Id, // Usar el ID de la compañía desde el DTO
                    Delete_log_audits = false
                };
                _context.Audits.Add(audit);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateSupplier");
            }
        }

        public async Task UpdateAsyncSupplier(int id, SupplierUpdateDTO dto, int userId, int departmentId)
        {
            try
            {
                var supplier = await _context.Suppliers
                    .Include(s => s.SuppliersProducts)
                    .FirstOrDefaultAsync(s => s.Id_supplier == id && !s.Delete_log_suppliers);

                if (supplier == null)
                {
                    throw new KeyNotFoundException("Proveedor no encontrado.");
                }

                // Calcular la cantidad de productos modificados
                var existingProducts = supplier.SuppliersProducts.ToList();
                var removedProducts = existingProducts
                    .Where(ep => !dto.Products.Any(p => p.Id_inventory_product_Id == ep.Id_inventory_product_Id))
                    .ToList();
                var addedProducts = dto.Products
                    .Where(p => !existingProducts.Any(ep => ep.Id_inventory_product_Id == p.Id_inventory_product_Id))
                    .ToList();
                var updatedProducts = dto.Products
                    .Where(p => existingProducts.Any(ep => ep.Id_inventory_product_Id == p.Id_inventory_product_Id))
                    .ToList();

                int amountModify = removedProducts.Count + addedProducts.Count + updatedProducts.Count;

                // Registrar en la tabla de auditoría
                var audit = new Audits
                {
                    Date_update = DateTime.Now,
                    Amount_modify = amountModify, // Cantidad de productos modificados
                    Id_user_Id = userId, // Usar el ID del usuario desde los parámetros
                    Id_department_Id = departmentId, // Usar el ID del departamento desde los parámetros
                    Id_module_Id = 1, // Módulo de proveedores
                    Id_company_Id = dto.Id_company_Id, // Usar el ID de la compañía desde el DTO
                    Delete_log_audits = false
                };
                _context.Audits.Add(audit);
                await _context.SaveChangesAsync();

                // Actualizar datos del proveedor
                supplier.Name_supplier = dto.Name_supplier;
                supplier.Mail_supplier = dto.Mail_supplier;
                supplier.Phone_supplier = dto.Phone_supplier;
                supplier.Id_category_supplier_Id = dto.Id_category_supplier_Id;
                supplier.Id_company_Id = dto.Id_company_Id;
                supplier.Date_update = DateTime.Now;

                // Manejo de productos asociados
                // Productos eliminados
                _context.SuppliersProducts.RemoveRange(removedProducts);

                // Productos actualizados o nuevos
                foreach (var productDto in dto.Products)
                {
                    var existingProduct = existingProducts
                        .FirstOrDefault(ep => ep.Id_inventory_product_Id == productDto.Id_inventory_product_Id);

                    if (existingProduct != null)
                    {
                        // Actualizar precio sugerido
                        existingProduct.Suggested_price = productDto.Suggested_price;
                    }
                    else
                    {
                        // Agregar nuevo producto
                        var newProduct = new SuppliersProducts
                        {
                            Id_supplier_Id = supplier.Id_supplier,
                            Id_inventory_product_Id = productDto.Id_inventory_product_Id,
                            Suggested_price = productDto.Suggested_price
                        };
                        await _context.SuppliersProducts.AddAsync(newProduct);
                    }
                }

                // Guardar cambios
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateSupplier");
            }
        }

        public async Task SoftDeleteAsyncSupplier(int id, int userId, int departmentId)
        {
            try
            {
                var supplier = await _context.Suppliers
                    .Include(s => s.SuppliersProducts)
                    .FirstOrDefaultAsync(s => s.Id_supplier == id);

                if (supplier == null)
                {
                    throw new KeyNotFoundException("Proveedor no encontrado para eliminar");
                }

                int amountModify = supplier.SuppliersProducts.Count; // Cantidad de productos antes de eliminar

                supplier.Delete_log_suppliers = true;
                supplier.Date_delete = DateTime.Now;
                await _context.SaveChangesAsync();

                // Registrar en la tabla de auditoría
                var audit = new Audits
                {
                    Date_delete = DateTime.Now,
                    Amount_modify = amountModify, // Cantidad de productos antes de eliminar
                    Id_user_Id = userId, // Usar el ID del usuario desde los parámetros
                    Id_department_Id = departmentId, // Usar el ID del departamento desde los parámetros
                    Id_module_Id = supplier.Id_module_Id, // Módulo de proveedores
                    Id_company_Id = supplier.Id_company_Id, // Usar el ID de la compañía desde el proveedor
                    Delete_log_audits = false
                };
                _context.Audits.Add(audit);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteSupplier");
            }
        }

        public async Task RestoreAsyncSupplier(int id, int userId, int departmentId)
        {
            try
            {
                var supplier = await _context.Suppliers
                    .Include(s => s.SuppliersProducts)
                    .FirstOrDefaultAsync(s => s.Id_supplier == id && s.Delete_log_suppliers);

                if (supplier == null)
                {
                    throw new KeyNotFoundException("Proveedor no encontrado para restaurar");
                }

                int amountModify = supplier.SuppliersProducts.Count; // Cantidad de productos antes de restaurar

                supplier.Delete_log_suppliers = false;
                supplier.Date_restore = DateTime.Now;
                await _context.SaveChangesAsync();

                // Registrar en la tabla de auditoría
                var audit = new Audits
                {
                    Date_restore = DateTime.Now,
                    Amount_modify = amountModify, // Cantidad de productos antes de restaurar
                    Id_user_Id = userId, // Usar el ID del usuario desde los parámetros
                    Id_department_Id = departmentId, // Usar el ID del departamento desde los parámetros
                    Id_module_Id = supplier.Id_module_Id, // Módulo de proveedores
                    Id_company_Id = supplier.Id_company_Id, // Usar el ID de la compañía desde el proveedor
                    Delete_log_audits = false
                };
                _context.Audits.Add(audit);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreSupplier");
            }
        }
    }
}