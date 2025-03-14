using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Application.ViewModels;
using FluxSYS_backend.Domain.Models.PrincipalModels;
using FluxSYS_backend.Infraestructure.Data;

namespace FluxSYS_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public AuthController(AuthService authService, ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _authService = authService;
            _context = context;
            _errorLogService = errorLogService;
        }

        // Endpoint de registro de usuario
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = new Users
                {
                    Name_user = model.Name_user,
                    Mail_user = model.Mail_user,
                    Phone_user = model.Phone_user,
                    Password_user = BCrypt.Net.BCrypt.HashPassword(model.Password_user),
                    Id_rol_Id = model.Id_rol_Id,
                    Id_position_Id = model.Id_position_Id,
                    Id_department_Id = model.Id_department_Id,
                    Id_company_Id = model.Id_company_Id,
                    Id_module_Id = 6,
                    Date_insert = DateTime.Now
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Usuario registrado correctamente" });
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RegisterUserController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // Endpoint de login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _authService.Authenticate(model.Email, model.Password);

                if (user == null)
                    return Unauthorized(new { message = "Credenciales inválidas" });

                var token = await _authService.GenerateToken(user);
                var expirationDate = DateTime.Now.AddMinutes(120);

                await _authService.SaveToken(user.Id_user, token, expirationDate);

                return Ok(new { token, expirationDate });
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "LoginUserController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // Endpoint de validación de token
        [HttpGet("validate-token")]
        public async Task<IActionResult> ValidateToken()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();

                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    return Unauthorized(new { message = "Token no proporcionado o con formato incorrecto" });
                }

                var token = authHeader.Substring("Bearer ".Length).Trim();

                var userToken = await _authService.ValidateToken(token);

                if (userToken == null)
                    return Unauthorized(new { message = "Token inválido o expirado" });

                return Ok(new { user = userToken.User });
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "ValidateTokenController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }


        // Endpoint de logout
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();

                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    return Unauthorized(new { message = "Token no proporcionado o inválido" });
                }

                var token = authHeader.Replace("Bearer ", "").Trim();
                await _authService.RemoveToken(token);

                return Ok(new { message = "Sesión cerrada correctamente" });
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "LogoutUserController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

    }
}