using FluxSYS_backend.Application.DTOs.ClasificationMovements;
using FluxSYS_backend.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using FluxSYS_backend.Application.ViewModels;
using FluxSYS_backend.Application.Filters;

namespace FluxSYS_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasificationMovementsController : ControllerBase
    {
        private readonly ClasificationMovementsService _service;
        private readonly ErrorLogService _errorLogService;

        public ClasificationMovementsController(ClasificationMovementsService service, ErrorLogService errorLogService)
        {
            _service = service;
            _errorLogService = errorLogService;
        }

        [CustomAuthorize("Administrador")]
        [HttpGet("get-clasification-movements")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var clasificationMovements = await _service.GetAllAsyncClasificationMovements();
                return Ok(clasificationMovements);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetClasificationMovementsController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador")]
        [HttpPost("create-clasification-movement")]
        public async Task<IActionResult> Create([FromBody] ClasificationMovementViewModel model)
        {
            // Validación del ViewModel
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Mapear el ViewModel al DTO
                var dto = new ClasificationMovementCreateDTO
                {
                    Name_clasification_movement = model.Name_clasification_movement
                };

                // Llamada al servicio para crear la clasificación de movimiento
                await _service.AddAsyncClasificationMovement(dto);
                return Ok(new { message = "Clasificación de movimiento creada correctamente" });
            }
            catch (SqlException ex) when (ex.Number == 547) // Error de clave foránea
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateClasificationMovementController - ForeignKey");
                return BadRequest("Error de clave foránea: Verifica los datos.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateClasificationMovementController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador")]
        [HttpPut("update-clasification-movement/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClasificationMovementViewModel model)
        {
            // Validación del ViewModel
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Mapear el ViewModel al DTO
                var dto = new ClasificationMovementUpdateDTO
                {
                    Name_clasification_movement = model.Name_clasification_movement
                };

                // Llamada al servicio para actualizar la clasificación de movimiento
                await _service.UpdateAsyncClasificationMovement(id, dto);
                return Ok(new { message = "Clasificación de movimiento actualizada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateClasificationMovementController - KeyNotFound");
                return NotFound("Clasificación de movimiento no encontrada.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateClasificationMovementController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador")]
        [HttpDelete("delete-clasification-movement/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                await _service.SoftDeleteAsyncClasificationMovement(id);
                return Ok(new { message = "Clasificación de movimiento eliminada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "DeleteClasificationMovementController - KeyNotFound");
                return NotFound("Clasificación de movimiento no encontrada para eliminar.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteClasificationMovementController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador")]
        [HttpPatch("restore-clasification-movement/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                await _service.RestoreAsyncClasificationMovement(id);
                return Ok(new { message = "Clasificación de movimiento restaurada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreClasificationMovementController - KeyNotFound");
                return NotFound("Clasificación de movimiento no encontrada para restaurar.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreClasificationMovementController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}
