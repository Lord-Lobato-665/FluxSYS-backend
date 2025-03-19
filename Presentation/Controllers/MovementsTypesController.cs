using FluxSYS_backend.Application.DTOs.MovementsTypes;
using FluxSYS_backend.Application.ViewModels;
using FluxSYS_backend.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using FluxSYS_backend.Application.AppServices;
using Microsoft.Data.SqlClient;
using FluxSYS_backend.Application.Filters;

namespace FluxSYS_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementsTypesController : ControllerBase
    {
        private readonly MovementsTypesService _service;
        private readonly ErrorLogService _errorLogService;
        private readonly CompaniesService _companiesService;
        private readonly ClasificationMovementsService _clasificationMovementsService;

        public MovementsTypesController(MovementsTypesService service, ErrorLogService errorLogService, CompaniesService companiesService, ClasificationMovementsService clasificationMovementsService)
        {
            _service = service;
            _errorLogService = errorLogService;
            _companiesService = companiesService;
            _clasificationMovementsService = clasificationMovementsService;
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpGet("get-movements-types")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var movementsTypes = await _service.GetAllAsyncMovementsTypes();
                return Ok(movementsTypes);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetMovementsTypesController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpGet("get-movement-types-by-company/{companyId}")]
        public async Task<IActionResult> GetMovementTypesByCompanyId(int companyId)
        {
            try
            {
                var movementTypes = await _service.GetMovementTypesByCompanyIdAsync(companyId);
                return Ok(movementTypes);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetMovementTypesByCompanyIdController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento")]
        [HttpPost("create-movement-type")]
        public async Task<IActionResult> Create([FromBody] MovementTypeViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Verificar si la compañía existe
                var company = await _companiesService.GetAllAsyncCompanies();
                if (!company.Any(c => c.Id_company == model.Id_company_Id))
                {
                    return BadRequest("La compañía especificada no existe.");
                }

                // Verificar si la clasificación de movimiento existe
                var clasificationMovement = await _clasificationMovementsService.GetAllAsyncClasificationMovements();
                if (!clasificationMovement.Any(cm => cm.Id_clasification_movement == model.Id_clasification_movement_Id))
                {
                    return BadRequest("La clasificación de movimiento especificada no existe.");
                }

                var dto = new MovementTypeCreateDTO
                {
                    Name_movement_type = model.Name_movement_type,
                    Id_company_Id = model.Id_company_Id,
                    Id_clasification_movement_Id = model.Id_clasification_movement_Id
                };
                await _service.AddAsyncMovementType(dto);
                return Ok(new { message = "Tipo de movimiento creado correctamente" });
            }
            catch (SqlException ex) when (ex.Number == 547) // Error de clave foránea
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateMovementTypeController - ForeignKey");
                return BadRequest("Error de clave foránea: Verifica que los IDs de la compañía y clasificación de movimiento sean correctos.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateMovementTypeController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento")]
        [HttpPut("update-movement-type/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MovementTypeViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dto = new MovementTypeUpdateDTO
                {
                    Name_movement_type = model.Name_movement_type,
                    Id_company_Id = model.Id_company_Id,
                    Id_clasification_movement_Id = model.Id_clasification_movement_Id
                };
                await _service.UpdateAsyncMovementType(id, dto);
                return Ok(new { message = "Tipo de movimiento actualizado correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateMovementTypeController - KeyNotFound");
                return NotFound("Tipo de movimiento no encontrado");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateMovementTypeController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpDelete("delete-movement-type/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                await _service.SoftDeleteAsyncMovementType(id);
                return Ok(new { message = "Tipo de movimiento eliminado correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "DeleteMovementTypeController - KeyNotFound");
                return NotFound("Tipo de movimiento no encontrado para eliminar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteMovementTypeController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpPatch("restore-movement-type/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                await _service.RestoreAsyncMovementType(id);
                return Ok(new { message = "Tipo de movimiento restaurado correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreMovementTypeController - KeyNotFound");
                return NotFound("Tipo de movimiento no encontrado para restaurar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreMovementTypeController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}