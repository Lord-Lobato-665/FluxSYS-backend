using FluxSYS_backend.Application.DTOs.Inventories;
using FluxSYS_backend.Application.Filters;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluxSYS_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly InventoriesService _service;
        private readonly ErrorLogService _errorLogService;

        public InventoriesController(InventoriesService service, ErrorLogService errorLogService)
        {
            _service = service;
            _errorLogService = errorLogService;
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpGet("get-inventories")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var inventories = await _service.GetAllAsyncInventories();
                return Ok(inventories);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetInventoriesController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpGet("get-inventory-by-id/{id}")]
        public async Task<IActionResult> GetInventoryById(int id)
        {
            try
            {
                var inventory = await _service.GetInventoryByIdAsync(id);
                if (inventory == null)
                {
                    return NotFound("Producto de inventario no encontrado.");
                }
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetInventoryByIdController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento")]
        [HttpPost("create-inventory")]
        public async Task<IActionResult> Create(
            [FromBody] InventoryViewModel model,
            [FromQuery] string nameUser, // Nombre del usuario desde el localStorage
            [FromQuery] string nameDepartment) // Nombre del departamento desde el localStorage
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dto = new InventoryCreateDTO
                {
                    Name_product = model.Name_product,
                    Stock_product = model.Stock_product,
                    Price_product = model.Price_product,
                    Id_category_product_Id = model.Id_category_product_Id,
                    Id_state_Id = model.Id_state_Id,
                    Id_movement_type_Id = model.Id_movement_type_Id,
                    Id_supplier_Id = model.Id_supplier_Id,
                    Id_department_Id = model.Id_department_Id,
                    Id_company_Id = model.Id_company_Id,
                    Id_user_Id = model.Id_user_Id
                };
                await _service.AddAsyncInventory(dto, nameUser, nameDepartment);
                return Ok(new { message = "Producto de inventario creado correctamente." });
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateInventoryController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento")]
        [HttpPut("update-inventory/{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] InventoryViewModel model,
            [FromQuery] string nameUser, // Nombre del usuario desde el localStorage
            [FromQuery] string nameDepartment) // Nombre del departamento desde el localStorage
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dto = new InventoryUpdateDTO
                {
                    Name_product = model.Name_product,
                    Stock_product = model.Stock_product,
                    Price_product = model.Price_product,
                    Id_category_product_Id = model.Id_category_product_Id,
                    Id_state_Id = model.Id_state_Id,
                    Id_movement_type_Id = model.Id_movement_type_Id,
                    Id_supplier_Id = model.Id_supplier_Id,
                    Id_department_Id = model.Id_department_Id,
                    Id_company_Id = model.Id_company_Id,
                    Id_user_Id = model.Id_user_Id
                };
                await _service.UpdateAsyncInventory(id, dto, nameUser, nameDepartment);
                return Ok(new { message = "Producto de inventario actualizado correctamente." });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateInventoryController - KeyNotFound");
                return NotFound("Producto de inventario no encontrado.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateInventoryController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpDelete("delete-inventory/{id}")]
        public async Task<IActionResult> SoftDelete(
            int id,
            [FromQuery] string nameUser, // Nombre del usuario desde el localStorage
            [FromQuery] string nameDepartment) // Nombre del departamento desde el localStorage
        {
            try
            {
                await _service.SoftDeleteAsyncInventory(id, nameUser, nameDepartment);
                return Ok(new { message = "Producto de inventario eliminado correctamente." });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "DeleteInventoryController - KeyNotFound");
                return NotFound("Producto de inventario no encontrado para eliminar.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteInventoryController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpPatch("restore-inventory/{id}")]
        public async Task<IActionResult> Restore(
            int id,
            [FromQuery] string nameUser, // Nombre del usuario desde el localStorage
            [FromQuery] string nameDepartment) // Nombre del departamento desde el localStorage
        {
            try
            {
                await _service.RestoreAsyncInventory(id, nameUser, nameDepartment);
                return Ok(new { message = "Producto de inventario restaurado correctamente." });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreInventoryController - KeyNotFound");
                return NotFound("Producto de inventario no encontrado para restaurar.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreInventoryController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento")]
        [HttpGet("inventory-pdf")]
        public async Task<IActionResult> GetPDF([FromQuery] string companyName, [FromQuery] string departmentName)
        {
            if (string.IsNullOrEmpty(companyName))
            {
                return BadRequest("El parámetro 'Compañía' es requerido.");
            }
            if (string.IsNullOrEmpty(departmentName))
            {
                return BadRequest("El parámetro 'Departamento' es requerido.");
            }

            // Pasar ambos parámetros al servicio
            var pdfFile = await _service.GetPDF(companyName, departmentName);

            if (pdfFile == null || pdfFile.Length == 0)
            {
                return NotFound("No se encontraron productos para la compañía y departamento especificados.");
            }

            return File(pdfFile, "application/pdf", "InventoryReport.pdf");
        }
    }
}