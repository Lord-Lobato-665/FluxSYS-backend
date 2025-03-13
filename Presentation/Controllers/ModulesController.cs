using FluxSYS_backend.Application.DTOs.Modules;
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
    public class ModulesController : ControllerBase
    {
        private readonly ModulesService _service;
        private readonly ErrorLogService _errorLogService;

        public ModulesController(ModulesService service, ErrorLogService errorLogService)
        {
            _service = service;
            _errorLogService = errorLogService;
        }

        [CustomAuthorize("Administrador")]
        [HttpGet("get-modules")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var modules = await _service.GetAllAsyncModules();
                return Ok(modules);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetModulesController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador")]
        [HttpPost("create-module")]
        public async Task<IActionResult> Create([FromBody] ModuleViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dto = new ModuleCreateDTO
                {
                    Name_module = model.Name_module
                };
                await _service.AddAsyncModule(dto);
                return Ok(new { message = "Módulo creado correctamente" });
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateModuleController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador")]
        [HttpPut("update-module/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ModuleViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dto = new ModuleUpdateDTO
                {
                    Name_module = model.Name_module
                };
                await _service.UpdateAsyncModule(id, dto);
                return Ok(new { message = "Módulo actualizado correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateModuleController - KeyNotFound");
                return NotFound("Módulo no encontrado");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateModuleController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador")]
        [HttpDelete("delete-module/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                await _service.SoftDeleteAsyncModule(id);
                return Ok(new { message = "Módulo eliminado correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "DeleteModuleController - KeyNotFound");
                return NotFound("Módulo no encontrado para eliminar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteModuleController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador")]
        [HttpPatch("restore-module/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                await _service.RestoreAsyncModule(id);
                return Ok(new { message = "Módulo restaurado correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreModuleController - KeyNotFound");
                return NotFound("Módulo no encontrado para restaurar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreModuleController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}