using FluxSYS_backend.Application.DTOs.InventoryMovements;
using FluxSYS_backend.Application.Filters;
using FluxSYS_backend.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluxSYS_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryMovementsController : ControllerBase
    {
        private readonly InventoryMovementsService _service;
        private readonly ErrorLogService _errorLogService;

        public InventoryMovementsController(InventoryMovementsService service, ErrorLogService errorLogService)
        {
            _service = service;
            _errorLogService = errorLogService;
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento")]
        [HttpGet("get-inventory-movements")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var inventoryMovements = await _service.GetAllAsyncInventoryMovements();
                return Ok(inventoryMovements);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetInventoryMovementsController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento")]
        [HttpGet("get-inventory-movements-by-company/{idCompany}")]
        public async Task<IActionResult> GetAllByCompanyId(int idCompany)
        {
            try
            {
                var inventoryMovements = await _service.GetAllByCompanyIdAsync(idCompany);
                return Ok(inventoryMovements);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetInventoryMovementsByCompanyController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}