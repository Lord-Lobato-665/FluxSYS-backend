﻿using FluxSYS_backend.Application.DTOs.ErrorLogs; // Importa el DTO
using FluxSYS_backend.Application.Filters;
using FluxSYS_backend.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluxSYS_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        private readonly ErrorLogService _errorLogService;

        public ErrorsController(ErrorLogService errorLogService)
        {
            _errorLogService = errorLogService;
        }

        [CustomAuthorize("Administrador")]
        [HttpGet]
        public async Task<IActionResult> GetAllErrors()
        {
            try
            {
                var errors = await _errorLogService.GetAllErrorsAsync();
                return Ok(errors);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllErrorsController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // Nuevo método para crear un ErrorLog desde el frontend usando el DTO
        [HttpPost("log")]
        public async Task<IActionResult> LogError([FromBody] ErrorLogCreateDTO request)
        {
            try
            {
                await _errorLogService.SaveErrorAsync(request.Message, request.StackTrace, request.Source);
                return Ok("Error registrado correctamente.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "LogErrorController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}