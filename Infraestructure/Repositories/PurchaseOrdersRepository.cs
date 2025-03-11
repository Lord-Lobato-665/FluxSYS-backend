using FluxSYS_backend.Application.DTOs.PurchaseOrders;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Domain.Models.PrincipalModels;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Infrastructure.Repositories
{
    public class PurchaseOrdersRepository : IPurchaseOrders
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public PurchaseOrdersRepository(ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<PurchaseOrderReadDTO>> GetAllAsyncPurchaseOrders()
        {
            try
            {
                var purchaseOrders = await _context.PurchaseOrders
                    .Include(po => po.OrdersProducts)
                        .ThenInclude(op => op.Inventories) // Incluir los productos del inventario
                    .Include(po => po.Users)
                    .Include(po => po.CategoryPurchaseOrders)
                    .Include(po => po.Departments)
                    .Include(po => po.Suppliers)
                    .Include(po => po.States)
                    .Include(po => po.MovementsTypes)
                    .Include(po => po.Modules)
                    .Include(po => po.Companies)
                    .Select(po => new PurchaseOrderReadDTO
                    {
                        Id_purchase_order = po.Id_purchase_order,
                        Name_purchase_order = po.Name_purchase_order,
                        Amount_items_in_the_order = po.OrdersProducts.Count(),
                        Total_price_products = po.OrdersProducts.Sum(op => op.Inventories.Price_product * op.Quantity), // Usar el precio del inventario
                        Name_user = po.Users.Name_user,
                        Name_category_purchase_order = po.CategoryPurchaseOrders.Name_category_purchase_order,
                        Name_department = po.Departments.Name_deparment,
                        Name_supplier = po.Suppliers.Name_supplier,
                        Name_state = po.States.Name_state,
                        Name_movement_type = po.MovementsTypes.Name_movement_type,
                        Name_module = po.Modules.Name_module,
                        Name_company = po.Companies.Name_company,
                        Date_insert = po.Date_insert.HasValue
                            ? po.Date_insert.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Date_update = po.Date_update.HasValue
                            ? po.Date_update.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Date_delete = po.Date_delete.HasValue
                            ? po.Date_delete.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Date_restore = po.Date_restore.HasValue
                            ? po.Date_restore.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Delete_log_purchase_orders = po.Delete_log_purchase_orders,
                        Products = po.OrdersProducts.Any()
                            ? po.OrdersProducts.Select(op => new OrderProductReadDTO
                            {
                                Id_inventory_product = op.Inventories.Id_inventory_product,
                                Name_product = op.Inventories.Name_product,
                                Quantity = op.Quantity,
                                Price = op.Inventories.Price_product // Usar el precio del inventario
                            }).ToList()
                            : new List<OrderProductReadDTO>()
                    })
                    .ToListAsync();

                // Manejar "Sin productos asociados" fuera de la consulta LINQ
                foreach (var purchaseOrder in purchaseOrders)
                {
                    if (!purchaseOrder.Products.Any())
                    {
                        purchaseOrder.Products = new List<OrderProductReadDTO>
                {
                    new OrderProductReadDTO { Name_product = "Sin productos asociados" }
                };
                    }
                }

                return purchaseOrders;
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllPurchaseOrders");
                return new List<PurchaseOrderReadDTO>();
            }
        }

        public async Task AddAsyncPurchaseOrder(PurchaseOrderCreateDTO dto)
        {
            try
            {
                var purchaseOrder = new PurchaseOrders
                {
                    Name_purchase_order = dto.Name_purchase_order,
                    Id_user_Id = dto.Id_user_Id,
                    Id_category_purchase_order_Id = dto.Id_category_purchase_order_Id,
                    Id_department_Id = dto.Id_department_Id,
                    Id_supplier_Id = dto.Id_supplier_Id,
                    Id_state_Id = dto.Id_state_Id,
                    Id_movement_type_Id = dto.Id_movement_type_Id,
                    Id_module_Id = dto.Id_module_Id,
                    Id_company_Id = dto.Id_company_Id,
                    Date_insert = DateTime.UtcNow,
                    Delete_log_purchase_orders = false
                };
                _context.PurchaseOrders.Add(purchaseOrder);
                await _context.SaveChangesAsync(); // Guardar para generar el ID de la orden

                // Si hay productos asociados, añadirlos
                if (dto.Products != null && dto.Products.Any())
                {
                    var orderProducts = new List<OrdersProducts>();
                    foreach (var product in dto.Products)
                    {
                        // Obtener el precio del producto desde el inventario
                        var inventoryProduct = await _context.Inventories
                            .FirstOrDefaultAsync(i => i.Id_inventory_product == product.Id_inventory_product_Id);

                        if (inventoryProduct == null)
                        {
                            throw new KeyNotFoundException($"El producto con ID {product.Id_inventory_product_Id} no existe en el inventario.");
                        }

                        orderProducts.Add(new OrdersProducts
                        {
                            Id_purchase_order_Id = purchaseOrder.Id_purchase_order,
                            Id_inventory_product_Id = product.Id_inventory_product_Id,
                            Quantity = product.Quantity,
                            Price = inventoryProduct.Price_product // Tomar el precio del inventario
                        });
                    }

                    await _context.OrdersProducts.AddRangeAsync(orderProducts);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreatePurchaseOrder");
            }
        }

        public async Task UpdateAsyncPurchaseOrder(int id, PurchaseOrderUpdateDTO dto)
        {
            try
            {
                var purchaseOrder = await _context.PurchaseOrders
                    .Include(po => po.OrdersProducts)
                    .FirstOrDefaultAsync(po => po.Id_purchase_order == id && !po.Delete_log_purchase_orders);

                if (purchaseOrder == null)
                {
                    throw new KeyNotFoundException("Orden de compra no encontrada.");
                }

                // Actualizar datos de la orden
                purchaseOrder.Name_purchase_order = dto.Name_purchase_order;
                purchaseOrder.Id_user_Id = dto.Id_user_Id;
                purchaseOrder.Id_category_purchase_order_Id = dto.Id_category_purchase_order_Id;
                purchaseOrder.Id_department_Id = dto.Id_department_Id;
                purchaseOrder.Id_supplier_Id = dto.Id_supplier_Id;
                purchaseOrder.Id_state_Id = dto.Id_state_Id;
                purchaseOrder.Id_movement_type_Id = dto.Id_movement_type_Id;
                purchaseOrder.Id_module_Id = dto.Id_module_Id;
                purchaseOrder.Id_company_Id = dto.Id_company_Id;
                purchaseOrder.Date_update = DateTime.UtcNow;

                // Manejo de productos asociados
                var existingProducts = purchaseOrder.OrdersProducts.ToList();

                // Productos eliminados
                var removedProducts = existingProducts
                    .Where(ep => !dto.Products.Any(p => p.Id_inventory_product_Id == ep.Id_inventory_product_Id))
                    .ToList();
                _context.OrdersProducts.RemoveRange(removedProducts);

                // Productos actualizados o nuevos
                foreach (var productDto in dto.Products)
                {
                    var existingProduct = existingProducts
                        .FirstOrDefault(ep => ep.Id_inventory_product_Id == productDto.Id_inventory_product_Id);

                    if (existingProduct != null)
                    {
                        // Actualizar cantidad y precio
                        existingProduct.Quantity = productDto.Quantity;

                        // Obtener el precio actualizado desde el inventario
                        var inventoryProduct = await _context.Inventories
                            .FirstOrDefaultAsync(i => i.Id_inventory_product == productDto.Id_inventory_product_Id);

                        if (inventoryProduct != null)
                        {
                            existingProduct.Price = inventoryProduct.Price_product;
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

                        var newProduct = new OrdersProducts
                        {
                            Id_purchase_order_Id = purchaseOrder.Id_purchase_order,
                            Id_inventory_product_Id = productDto.Id_inventory_product_Id,
                            Quantity = productDto.Quantity,
                            Price = inventoryProduct.Price_product // Tomar el precio del inventario
                        };
                        await _context.OrdersProducts.AddAsync(newProduct);
                    }
                }

                // Guardar cambios
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdatePurchaseOrder");
            }
        }

        public async Task SoftDeleteAsyncPurchaseOrder(int id)
        {
            try
            {
                var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);
                if (purchaseOrder != null)
                {
                    purchaseOrder.Delete_log_purchase_orders = true;
                    purchaseOrder.Date_delete = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Orden de compra no encontrada para eliminar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeletePurchaseOrder");
            }
        }

        public async Task RestoreAsyncPurchaseOrder(int id)
        {
            try
            {
                var purchaseOrder = await _context.PurchaseOrders
                    .FirstOrDefaultAsync(po => po.Id_purchase_order == id && po.Delete_log_purchase_orders);

                if (purchaseOrder != null)
                {
                    purchaseOrder.Delete_log_purchase_orders = false;
                    purchaseOrder.Date_restore = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Orden de compra no encontrada para restaurar");
                }
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestorePurchaseOrder");
            }
        }
    }
}