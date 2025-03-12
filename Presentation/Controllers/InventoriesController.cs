using FluxSYS_backend.Application.DTOs.Inventories;
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

        [HttpPost("create-inventory")]
        public async Task<IActionResult> Create([FromBody] InventoryViewModel model, [FromQuery] int userId, [FromQuery] int departmentId)
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
                await _service.AddAsyncInventory(dto, userId, departmentId);
                return Ok(new { message = "Producto de inventario creado correctamente." });
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateInventoryController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPut("update-inventory/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] InventoryViewModel model, [FromQuery] int userId, [FromQuery] int departmentId)
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
                await _service.UpdateAsyncInventory(id, dto, userId, departmentId);
                return Ok(new { message = "Producto de inventario actualizado correctamente." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Producto de inventario no encontrado.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateInventoryController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpDelete("delete-inventory/{id}")]
        public async Task<IActionResult> SoftDelete(int id, [FromQuery] int userId, [FromQuery] int departmentId)
        {
            try
            {
                await _service.SoftDeleteAsyncInventory(id, userId, departmentId);
                return Ok(new { message = "Producto de inventario eliminado correctamente." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Producto de inventario no encontrado para eliminar.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteInventoryController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPatch("restore-inventory/{id}")]
        public async Task<IActionResult> Restore(int id, [FromQuery] int userId, [FromQuery] int departmentId)
        {
            try
            {
                await _service.RestoreAsyncInventory(id, userId, departmentId);
                return Ok(new { message = "Producto de inventario restaurado correctamente." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Producto de inventario no encontrado para restaurar.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreInventoryController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}