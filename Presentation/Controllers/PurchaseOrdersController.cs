using FluxSYS_backend.Application.DTOs.PurchaseOrders;
using FluxSYS_backend.Application.ViewModels;
using FluxSYS_backend.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using FluxSYS_backend.Infraestructure.Data;
using FluxSYS_backend.Application.AppServices;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly PurchaseOrdersService _service;
        private readonly ErrorLogService _errorLogService;
        private readonly UsersService _usersService;
        private readonly CategoriesPurchaseOrdersService _categoriesPurchaseOrdersService;
        private readonly DepartmentsService _departmentsService;
        private readonly SuppliersService _suppliersService;
        private readonly StatesService _statesService;
        private readonly MovementsTypesService _movementsTypesService;
        private readonly ModulesService _modulesService;
        private readonly CompaniesService _companiesService;
        private readonly ApplicationDbContext _context;

        public PurchaseOrdersController(
            PurchaseOrdersService service,
            ErrorLogService errorLogService,
            UsersService usersService,
            CategoriesPurchaseOrdersService categoriesPurchaseOrdersService,
            DepartmentsService departmentsService,
            SuppliersService suppliersService,
            StatesService statesService,
            MovementsTypesService movementsTypesService,
            ModulesService modulesService,
            CompaniesService companiesService,
            ApplicationDbContext context)
        {
            _service = service;
            _errorLogService = errorLogService;
            _usersService = usersService;
            _categoriesPurchaseOrdersService = categoriesPurchaseOrdersService;
            _departmentsService = departmentsService;
            _suppliersService = suppliersService;
            _statesService = statesService;
            _movementsTypesService = movementsTypesService;
            _modulesService = modulesService;
            _companiesService = companiesService;
            _context = context;
        }

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

        [HttpPost("create-purchase-order")]
        public async Task<IActionResult> Create([FromBody] PurchaseOrderViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Verificar si las entidades relacionadas existen
                var userExists = await _usersService.GetAllAsyncUsers();
                if (!userExists.Any(u => u.Id_user == model.Id_user_Id))
                {
                    return BadRequest("El usuario especificado no existe.");
                }

                var categoryExists = await _categoriesPurchaseOrdersService.GetAllAsyncCategoriesPurchaseOrders();
                if (!categoryExists.Any(c => c.Id_category_purchase_order == model.Id_category_purchase_order_Id))
                {
                    return BadRequest("La categoría de orden de compra especificada no existe.");
                }

                var departmentExists = await _departmentsService.GetAllAsyncDepartments();
                if (!departmentExists.Any(d => d.Id_department == model.Id_department_Id))
                {
                    return BadRequest("El departamento especificado no existe.");
                }

                var supplierExists = await _suppliersService.GetAllAsyncSuppliers();
                if (!supplierExists.Any(s => s.Id_supplier == model.Id_supplier_Id))
                {
                    return BadRequest("El proveedor especificado no existe.");
                }

                var stateExists = await _statesService.GetAllAsyncStates();
                if (!stateExists.Any(s => s.Id_state == model.Id_state_Id))
                {
                    return BadRequest("El estado especificado no existe.");
                }

                var movementTypeExists = await _movementsTypesService.GetAllAsyncMovementsTypes();
                if (!movementTypeExists.Any(m => m.Id_movement_type == model.Id_movement_type_Id))
                {
                    return BadRequest("El tipo de movimiento especificado no existe.");
                }

                var moduleExists = await _modulesService.GetAllAsyncModules();
                if (!moduleExists.Any(m => m.Id_module == model.Id_module_Id))
                {
                    return BadRequest("El módulo especificado no existe.");
                }

                var companyExists = await _companiesService.GetAllAsyncCompanies();
                if (!companyExists.Any(c => c.Id_company == model.Id_company_Id))
                {
                    return BadRequest("La compañía especificada no existe.");
                }

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
                    Id_module_Id = model.Id_module_Id,
                    Id_company_Id = model.Id_company_Id,
                    Products = productsWithPrices
                };

                await _service.AddAsyncPurchaseOrder(dto);
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

        [HttpPut("update-purchase-order/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PurchaseOrderViewModel model)
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
                    Id_module_Id = model.Id_module_Id,
                    Id_company_Id = model.Id_company_Id,
                    Products = productsWithPrices
                };

                await _service.UpdateAsyncPurchaseOrder(id, dto);
                return Ok(new { message = "Orden de compra actualizada correctamente" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Orden de compra no encontrada");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdatePurchaseOrderController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpDelete("delete-purchase-order/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                await _service.SoftDeleteAsyncPurchaseOrder(id);
                return Ok(new { message = "Orden de compra eliminada correctamente" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Orden de compra no encontrada para eliminar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeletePurchaseOrderController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPatch("restore-purchase-order/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                await _service.RestoreAsyncPurchaseOrder(id);
                return Ok(new { message = "Orden de compra restaurada correctamente" });
            }
            catch (KeyNotFoundException)
            {
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