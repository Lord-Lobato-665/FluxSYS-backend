using FluxSYS_backend.Application.DTOs.States;
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
    public class StatesController : ControllerBase
    {
        private readonly StatesService _service;
        private readonly ErrorLogService _errorLogService;
        private readonly CompaniesService _companiesService;

        public StatesController(StatesService service, ErrorLogService errorLogService, CompaniesService companiesService)
        {
            _service = service;
            _errorLogService = errorLogService;
            _companiesService = companiesService;
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpGet("get-states")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var states = await _service.GetAllAsyncStates();
                return Ok(states);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetStatesController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento")]
        [HttpPost("create-state")]
        public async Task<IActionResult> Create([FromBody] StateViewModel model)
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

                var dto = new StateCreateDTO
                {
                    Name_state = model.Name_state,
                    Id_company_Id = model.Id_company_Id
                };
                await _service.AddAsyncState(dto);
                return Ok(new { message = "Estado creado correctamente" });
            }
            catch (SqlException ex) when (ex.Number == 547) // Error de clave foránea
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateStateController - ForeignKey");
                return BadRequest("Error de clave foránea: Verifica que el ID de la compañía sea correcto.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateStateController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento")]
        [HttpPut("update-state/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dto = new StateUpdateDTO
                {
                    Name_state = model.Name_state,
                    Id_company_Id = model.Id_company_Id
                };
                await _service.UpdateAsyncState(id, dto);
                return Ok(new { message = "Estado actualizado correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateStateController - KeyNotFound");
                return NotFound("Estado no encontrado");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateStateController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpDelete("delete-state/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                await _service.SoftDeleteAsyncState(id);
                return Ok(new { message = "Estado eliminado correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "DeleteStateController - KeyNotFound");
                return NotFound("Estado no encontrado para eliminar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteStateController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpPatch("restore-state/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                await _service.RestoreAsyncState(id);
                return Ok(new { message = "Estado restaurado correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreStateController - KeyNotFound");
                return NotFound("Estado no encontrado para restaurar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreStateController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}