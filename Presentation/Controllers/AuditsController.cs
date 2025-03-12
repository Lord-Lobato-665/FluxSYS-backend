using FluxSYS_backend.Application.DTOs.Audits;
using FluxSYS_backend.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluxSYS_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditsController : ControllerBase
    {
        private readonly AuditsService _service;
        private readonly ErrorLogService _errorLogService;

        public AuditsController(AuditsService service, ErrorLogService errorLogService)
        {
            _service = service;
            _errorLogService = errorLogService;
        }

        [HttpGet("get-audits")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var audits = await _service.GetAllAsyncAudits();
                return Ok(audits);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAuditsController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}