using FluxSYS_backend.Application.DTOs.Invoices;
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
    public class InvoicesController : ControllerBase
    {
        private readonly InvoicesService _service;
        private readonly ErrorLogService _errorLogService;
        private readonly ApplicationDbContext _context;

        public InvoicesController(
            InvoicesService service,
            ErrorLogService errorLogService,
            ApplicationDbContext context)
        {
            _service = service;
            _errorLogService = errorLogService;
            _context = context;
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpGet("get-invoices")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var invoices = await _service.GetAllAsyncInvoices();
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetInvoicesController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpGet("get-invoices-by-company/{companyId}")]
        public async Task<IActionResult> GetInvoicesByCompanyId(int companyId)
        {
            try
            {
                var invoices = await _service.GetInvoicesByCompanyIdAsync(companyId);
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetInvoicesByCompanyIdController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento")]
        [HttpPost("create-invoice")]
        public async Task<IActionResult> Create(
            [FromBody] InvoiceViewModel model,
            [FromQuery] string nameUser, // Nombre del usuario desde el localStorage
            [FromQuery] string nameDepartment) // Nombre del departamento desde el localStorage
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Obtener los precios de los productos desde el inventario
                var productsWithPrices = new List<InvoiceProductCreateDTO>();
                foreach (var product in model.Products)
                {
                    var inventoryProduct = await _context.Inventories
                        .FirstOrDefaultAsync(i => i.Id_inventory_product == product.Id_inventory_product_Id);

                    if (inventoryProduct == null)
                    {
                        return BadRequest($"El producto con ID {product.Id_inventory_product_Id} no existe en el inventario.");
                    }

                    productsWithPrices.Add(new InvoiceProductCreateDTO
                    {
                        Id_inventory_product_Id = product.Id_inventory_product_Id,
                        Quantity = product.Quantity
                        // El Unit_price se asignará en el repositorio desde el inventario
                    });
                }

                var dto = new InvoiceCreateDTO
                {
                    Name_invoice = model.Name_invoice,
                    Id_purchase_order_Id = model.Id_purchase_order_Id,
                    Id_supplier_Id = model.Id_supplier_Id,
                    Id_department_Id = model.Id_department_Id,
                    Id_company_Id = model.Id_company_Id,
                    Products = productsWithPrices
                };

                await _service.AddAsyncInvoice(dto, nameUser, nameDepartment);
                return Ok(new { message = "Factura creada correctamente" });
            }
            catch (SqlException ex) when (ex.Number == 547) // Error de clave foránea
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateInvoiceController - ForeignKey");
                return BadRequest("Error de clave foránea: Verifica que los IDs de las entidades relacionadas sean correctos.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateInvoiceController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento")]
        [HttpPut("update-invoice/{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] InvoiceViewModel model,
            [FromQuery] string nameUser, // Nombre del usuario desde el localStorage
            [FromQuery] string nameDepartment) // Nombre del departamento desde el localStorage
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Obtener los precios de los productos desde el inventario
                var productsWithPrices = new List<InvoiceProductUpdateDTO>();
                foreach (var product in model.Products)
                {
                    var inventoryProduct = await _context.Inventories
                        .FirstOrDefaultAsync(i => i.Id_inventory_product == product.Id_inventory_product_Id);

                    if (inventoryProduct == null)
                    {
                        return BadRequest($"El producto con ID {product.Id_inventory_product_Id} no existe en el inventario.");
                    }

                    productsWithPrices.Add(new InvoiceProductUpdateDTO
                    {
                        Id_inventory_product_Id = product.Id_inventory_product_Id,
                        Quantity = product.Quantity
                        // El Unit_price se asignará en el repositorio desde el inventario
                    });
                }

                var dto = new InvoiceUpdateDTO
                {
                    Name_invoice = model.Name_invoice,
                    Id_purchase_order_Id = model.Id_purchase_order_Id,
                    Id_supplier_Id = model.Id_supplier_Id,
                    Id_department_Id = model.Id_department_Id,
                    Id_company_Id = model.Id_company_Id,
                    Products = productsWithPrices
                };

                await _service.UpdateAsyncInvoice(id, dto, nameUser, nameDepartment);
                return Ok(new { message = "Factura actualizada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateInvoiceController - KeyNotFound");
                return NotFound("Factura no encontrada");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateInvoiceController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpDelete("delete-invoice/{id}")]
        public async Task<IActionResult> SoftDelete(
            int id,
            [FromQuery] string nameUser, // Nombre del usuario desde el localStorage
            [FromQuery] string nameDepartment) // Nombre del departamento desde el localStorage
        {
            try
            {
                await _service.SoftDeleteAsyncInvoice(id, nameUser, nameDepartment);
                return Ok(new { message = "Factura eliminada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "DeleteInvoiceController - KeyNotFound");
                return NotFound("Factura no encontrada para eliminar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteInvoiceController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpPatch("restore-invoice/{id}")]
        public async Task<IActionResult> Restore(
            int id,
            [FromQuery] string nameUser, // Nombre del usuario desde el localStorage
            [FromQuery] string nameDepartment) // Nombre del departamento desde el localStorage
        {
            try
            {
                await _service.RestoreAsyncInvoice(id, nameUser, nameDepartment);
                return Ok(new { message = "Factura restaurada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreInvoiceController - KeyNotFound");
                return NotFound("Factura no encontrada para restaurar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreInvoiceController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}