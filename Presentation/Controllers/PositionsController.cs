using FluxSYS_backend.Application.DTOs.Positions;
using FluxSYS_backend.Application.ViewModels;
using FluxSYS_backend.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using FluxSYS_backend.Application.AppServices;
using Microsoft.Data.SqlClient;

namespace FluxSYS_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly PositionsService _service;
        private readonly ErrorLogService _errorLogService;
        private readonly CompaniesService _companiesService;

        public PositionsController(PositionsService service, ErrorLogService errorLogService, CompaniesService companiesService)
        {
            _service = service;
            _errorLogService = errorLogService;
            _companiesService = companiesService;
        }

        [HttpGet("get-positions")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var positions = await _service.GetAllAsyncPositions();
                return Ok(positions);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetPositionsController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPost("create-position")]
        public async Task<IActionResult> Create([FromBody] PositionViewModel model)
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

                var dto = new PositionCreateDTO
                {
                    Name_position = model.Name_position,
                    Id_company_Id = model.Id_company_Id
                };
                await _service.AddAsyncPosition(dto);
                return Ok(new { message = "Posición creada correctamente" });
            }
            catch (SqlException ex) when (ex.Number == 547) // Error de clave foránea
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreatePositionController - ForeignKey");
                return BadRequest("Error de clave foránea: Verifica que el ID de la compañía sea correcto.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreatePositionController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPut("update-position/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PositionViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dto = new PositionUpdateDTO
                {
                    Name_position = model.Name_position,
                    Id_company_Id = model.Id_company_Id
                };
                await _service.UpdateAsyncPosition(id, dto);
                return Ok(new { message = "Posición actualizada correctamente" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Posición no encontrada");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdatePositionController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpDelete("delete-position/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                await _service.SoftDeleteAsyncPosition(id);
                return Ok(new { message = "Posición eliminada correctamente" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Posición no encontrada para eliminar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeletePositionController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPatch("restore-position/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                await _service.RestoreAsyncPosition(id);
                return Ok(new { message = "Posición restaurada correctamente" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Posición no encontrada para restaurar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestorePositionController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}