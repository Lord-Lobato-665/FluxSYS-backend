using FluxSYS_backend.Application.DTOs.Roles;
using FluxSYS_backend.Application.ViewModels;
using FluxSYS_backend.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using FluxSYS_backend.Application.AppServices;
using FluxSYS_backend.Application.Filters;

namespace FluxSYS_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RolesService _service;
        private readonly ErrorLogService _errorLogService;

        public RolesController(RolesService service, ErrorLogService errorLogService)
        {
            _service = service;
            _errorLogService = errorLogService;
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial")]
        [HttpGet("get-roles")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var roles = await _service.GetAllAsyncRoles();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetRolesController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial")]
        [HttpPost("create-role")]
        public async Task<IActionResult> Create([FromBody] RoleViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dto = new RoleCreateDTO
                {
                    Name_role = model.Name_role
                };
                await _service.AddAsyncRole(dto);
                return Ok(new { message = "Rol creado correctamente" });
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateRoleController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial")]
        [HttpPut("update-role/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoleViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dto = new RoleUpdateDTO
                {
                    Name_role = model.Name_role
                };
                await _service.UpdateAsyncRole(id, dto);
                return Ok(new { message = "Rol actualizado correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateRoleController - KeyNotFound");
                return NotFound("Rol no encontrado");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateRoleController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial")]
        [HttpDelete("delete-role/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                await _service.SoftDeleteAsyncRole(id);
                return Ok(new { message = "Rol eliminado correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "DeleteRoleController - KeyNotFound");
                return NotFound("Rol no encontrado para eliminar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteRoleController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial")]
        [HttpPatch("restore-role/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                await _service.RestoreAsyncRole(id);
                return Ok(new { message = "Rol restaurado correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreRoleController - KeyNotFound");
                return NotFound("Rol no encontrado para restaurar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreRoleController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}