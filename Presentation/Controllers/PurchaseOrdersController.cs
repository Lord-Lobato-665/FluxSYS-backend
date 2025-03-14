using FluxSYS_backend.Application.DTOs.PurchaseOrders;
using FluxSYS_backend.Application.ViewModels;
using FluxSYS_backend.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using FluxSYS_backend.Application.Filters;

namespace FluxSYS_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly PurchaseOrdersService _service;
        private readonly ErrorLogService _errorLogService;
        private readonly ApplicationDbContext _context;

        public PurchaseOrdersController(
            PurchaseOrdersService service,
            ErrorLogService errorLogService,
            ApplicationDbContext context)
        {
            _service = service;
            _errorLogService = errorLogService;
            _context = context;
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpGet("get-purchase-orders")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var purchaseOrders = await _service.GetAllAsyncPurchaseOrders();
                return Ok(purchaseOrders);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetPurchaseOrdersController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento")]
        [HttpPost("create-purchase-order")]
        public async Task<IActionResult> Create(
            [FromBody] PurchaseOrderViewModel model,
            [FromQuery] string nameUser, // Nombre del usuario desde el localStorage
            [FromQuery] string nameDepartment) // Nombre del departamento desde el localStorage
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Obtener los precios de los productos desde el inventario
                var productsWithPrices = new List<OrderProductCreateDTO>();
                foreach (var product in model.Products)
                {
                    var inventoryProduct = await _context.Inventories
                        .FirstOrDefaultAsync(i => i.Id_inventory_product == product.Id_inventory_product_Id);

                    if (inventoryProduct == null)
                    {
                        return BadRequest($"El producto con ID {product.Id_inventory_product_Id} no existe en el inventario.");
                    }

                    productsWithPrices.Add(new OrderProductCreateDTO
                    {
                        Id_inventory_product_Id = product.Id_inventory_product_Id,
                        Quantity = product.Quantity,
                        Price = inventoryProduct.Price_product // Tomar el precio del inventario
                    });
                }

                var dto = new PurchaseOrderCreateDTO
                {
                    Name_purchase_order = model.Name_purchase_order,
                    Id_user_Id = model.Id_user_Id,
                    Id_category_purchase_order_Id = model.Id_category_purchase_order_Id,
                    Id_department_Id = model.Id_department_Id,
                    Id_supplier_Id = model.Id_supplier_Id,
                    Id_state_Id = model.Id_state_Id,
                    Id_movement_type_Id = model.Id_movement_type_Id,
                    Id_company_Id = model.Id_company_Id,
                    Products = productsWithPrices
                };

                await _service.AddAsyncPurchaseOrder(dto, nameUser, nameDepartment);
                return Ok(new { message = "Orden de compra creada correctamente" });
            }
            catch (SqlException ex) when (ex.Number == 547) // Error de clave foránea
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreatePurchaseOrderController - ForeignKey");
                return BadRequest("Error de clave foránea: Verifica que los IDs de las entidades relacionadas sean correctos.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreatePurchaseOrderController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento")]
        [HttpPut("update-purchase-order/{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] PurchaseOrderViewModel model,
            [FromQuery] string nameUser, // Nombre del usuario desde el localStorage
            [FromQuery] string nameDepartment) // Nombre del departamento desde el localStorage
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Obtener los precios de los productos desde el inventario
                var productsWithPrices = new List<OrderProductUpdateDTO>();
                foreach (var product in model.Products)
                {
                    var inventoryProduct = await _context.Inventories
                        .FirstOrDefaultAsync(i => i.Id_inventory_product == product.Id_inventory_product_Id);

                    if (inventoryProduct == null)
                    {
                        return BadRequest($"El producto con ID {product.Id_inventory_product_Id} no existe en el inventario.");
                    }

                    productsWithPrices.Add(new OrderProductUpdateDTO
                    {
                        Id_inventory_product_Id = product.Id_inventory_product_Id,
                        Quantity = product.Quantity,
                        Price = inventoryProduct.Price_product // Tomar el precio del inventario
                    });
                }

                var dto = new PurchaseOrderUpdateDTO
                {
                    Name_purchase_order = model.Name_purchase_order,
                    Id_user_Id = model.Id_user_Id,
                    Id_category_purchase_order_Id = model.Id_category_purchase_order_Id,
                    Id_department_Id = model.Id_department_Id,
                    Id_supplier_Id = model.Id_supplier_Id,
                    Id_state_Id = model.Id_state_Id,
                    Id_movement_type_Id = model.Id_movement_type_Id,
                    Id_company_Id = model.Id_company_Id,
                    Products = productsWithPrices
                };

                await _service.UpdateAsyncPurchaseOrder(id, dto, nameUser, nameDepartment);
                return Ok(new { message = "Orden de compra actualizada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdatePurchaseOrderController - KeyNotFound");
                return NotFound("Orden de compra no encontrada");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdatePurchaseOrderController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpDelete("delete-purchase-order/{id}")]
        public async Task<IActionResult> SoftDelete(
            int id,
            [FromQuery] string nameUser, // Nombre del usuario desde el localStorage
            [FromQuery] string nameDepartment) // Nombre del departamento desde el localStorage
        {
            try
            {
                await _service.SoftDeleteAsyncPurchaseOrder(id, nameUser, nameDepartment);
                return Ok(new { message = "Orden de compra eliminada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "DeletePurchaseOrderController - KeyNotFound");
                return NotFound("Orden de compra no encontrada para eliminar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeletePurchaseOrderController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpPatch("restore-purchase-order/{id}")]
        public async Task<IActionResult> Restore(
            int id,
            [FromQuery] string nameUser, // Nombre del usuario desde el localStorage
            [FromQuery] string nameDepartment) // Nombre del departamento desde el localStorage
        {
            try
            {
                await _service.RestoreAsyncPurchaseOrder(id, nameUser, nameDepartment);
                return Ok(new { message = "Orden de compra restaurada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestorePurchaseOrderController - KeyNotFound");
                return NotFound("Orden de compra no encontrada para restaurar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestorePurchaseOrderController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}