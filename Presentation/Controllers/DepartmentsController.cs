using FluxSYS_backend.Application.DTOs.Departments;
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
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentsService _service;
        private readonly ErrorLogService _errorLogService;
        private readonly CompaniesService _companiesService;

        public DepartmentsController(DepartmentsService service, ErrorLogService errorLogService, CompaniesService companiesService)
        {
            _service = service;
            _errorLogService = errorLogService;
            _companiesService = companiesService;
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial")]
        [HttpGet("get-departments")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var departments = await _service.GetAllAsyncDepartments();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetDepartmentsController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpGet("get-departments-by-company/{companyId}")]
        public async Task<IActionResult> GetDepartmentsByCompanyId(int companyId)
        {
            try
            {
                var departments = await _service.GetDepartmentsByCompanyIdAsync(companyId);
                return Ok(departments);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetDepartmentsByCompanyIdController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial")]
        [HttpPost("create-department")]
        public async Task<IActionResult> Create([FromBody] DepartmentViewModel model)
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

                var dto = new DepartmentCreateDTO
                {
                    Name_deparment = model.Name_deparment,
                    Id_company_Id = model.Id_company_Id
                };
                await _service.AddAsyncDepartment(dto);
                return Ok(new { message = "Departamento creado correctamente" });
            }
            catch (SqlException ex) when (ex.Number == 547) // Error de clave foránea
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateDepartmentController - ForeignKey");
                return BadRequest("Error de clave foránea: Verifica que el ID de la compañía sea correcto.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateDepartmentController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial")]
        [HttpPut("update-department/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DepartmentViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dto = new DepartmentUpdateDTO
                {
                    Name_deparment = model.Name_deparment,
                    Id_company_Id = model.Id_company_Id
                };
                await _service.UpdateAsyncDepartment(id, dto);
                return Ok(new { message = "Departamento actualizado correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateDepartmentController - KeyNotFound");
                return NotFound("Departamento no encontrado");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateDepartmentController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial")]
        [HttpDelete("delete-department/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                await _service.SoftDeleteAsyncDepartment(id);
                return Ok(new { message = "Departamento eliminado correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "DeleteDepartmentController - KeyNotFound");
                return NotFound("Departamento no encontrado para eliminar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteDepartmentController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial")]
        [HttpPatch("restore-department/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                await _service.RestoreAsyncDepartment(id);
                return Ok(new { message = "Departamento restaurado correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreDepartmentController - KeyNotFound");
                return NotFound("Departamento no encontrado para restaurar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreDepartmentController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}