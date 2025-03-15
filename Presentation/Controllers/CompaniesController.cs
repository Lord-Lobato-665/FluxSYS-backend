using FluxSYS_backend.Application.AppServices;
using FluxSYS_backend.Application.ViewModels;
using FluxSYS_backend.Application.DTOs.Companies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using FluxSYS_backend.Application.Services;
using Microsoft.AspNetCore.Authorization;
using FluxSYS_backend.Application.Filters;

namespace FluxSYS_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly CompaniesService _service;
        private readonly ErrorLogService _errorLogService;

        public CompaniesController(CompaniesService service, ErrorLogService errorLogService)
        {
            _service = service;
            _errorLogService = errorLogService;
        }

        [CustomAuthorize("Administrador")]
        [HttpGet("get-companies")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var companies = await _service.GetAllAsyncCompanies();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetCompaniesController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador")]
        [HttpPost("create-company")]
        public async Task<IActionResult> Create([FromBody] CompanyViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dto = new CompanyCreateDTO { Name_company = model.Name_company };
                await _service.AddAsyncCompany(dto);
                return CreatedAtAction(nameof(GetAll), new { message = "Compañía creada correctamente" });
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateCompanyController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador")]
        [HttpPut("update-company/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CompanyViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dto = new CompanyUpdateDTO { Name_company = model.Name_company };
                await _service.UpdateAsyncCompany(id, dto);
                return Ok(new { message = "Compañía actualizada correctamente" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Compañía no encontrada");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateCompanyController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador")]
        [HttpDelete("delete-company/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                await _service.SoftDeleteAsync(id);
                return Ok(new { message = "Compañía eliminada correctamente" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Compañía no encontrada para eliminar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteCompanyController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador")]
        [HttpPatch("restore-company/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                await _service.RestoreAsync(id);
                return Ok(new { message = "Compañía restaurada correctamente" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Compañía no encontrada para restaurar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreCompanyController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}
