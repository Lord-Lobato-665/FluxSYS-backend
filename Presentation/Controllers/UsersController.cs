using FluxSYS_backend.Application.DTOs.Users;
using FluxSYS_backend.Application.ViewModels;
using FluxSYS_backend.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using FluxSYS_backend.Application.AppServices;
using FluxSYS_backend.Application.Filters;

namespace FluxSYS_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _service;
        private readonly ErrorLogService _errorLogService;
        private readonly RolesService _rolesService;
        private readonly PositionsService _positionsService;
        private readonly DepartmentsService _departmentsService;
        private readonly CompaniesService _companiesService;
        private readonly ModulesService _modulesService;

        public UsersController(
            UsersService service,
            ErrorLogService errorLogService,
            RolesService rolesService,
            PositionsService positionsService,
            DepartmentsService departmentsService,
            CompaniesService companiesService,
            ModulesService modulesService)
        {
            _service = service;
            _errorLogService = errorLogService;
            _rolesService = rolesService;
            _positionsService = positionsService;
            _departmentsService = departmentsService;
            _companiesService = companiesService;
            _modulesService = modulesService;
        }

        [CustomAuthorize("Administrador")]
        [HttpGet("get-users")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _service.GetAllAsyncUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetUsersController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador")]
        [HttpPost("create-user")]
        public async Task<IActionResult> Create([FromBody] UserViewModel model, [FromQuery] int userId, [FromQuery] int departmentId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Verificar si las entidades relacionadas existen
                var role = await _rolesService.GetAllAsyncRoles();
                if (!role.Any(r => r.Id_role == model.Id_rol_Id))
                {
                    return BadRequest("El rol especificado no existe.");
                }

                var position = await _positionsService.GetAllAsyncPositions();
                if (!position.Any(p => p.Id_position == model.Id_position_Id))
                {
                    return BadRequest("La posición especificada no existe.");
                }

                var department = await _departmentsService.GetAllAsyncDepartments();
                if (!department.Any(d => d.Id_department == model.Id_department_Id))
                {
                    return BadRequest("El departamento especificado no existe.");
                }

                var company = await _companiesService.GetAllAsyncCompanies();
                if (!company.Any(c => c.Id_company == model.Id_company_Id))
                {
                    return BadRequest("La compañía especificada no existe.");
                }

                var dto = new UserCreateDTO
                {
                    Name_user = model.Name_user,
                    Mail_user = model.Mail_user,
                    Phone_user = model.Phone_user,
                    Password_user = model.Password_user,
                    Id_rol_Id = model.Id_rol_Id,
                    Id_position_Id = model.Id_position_Id,
                    Id_department_Id = model.Id_department_Id,
                    Id_company_Id = model.Id_company_Id
                };

                await _service.AddAsyncUser(dto, userId, departmentId);
                return Ok(new { message = "Usuario creado correctamente" });
            }
            catch (InvalidOperationException ex)
            {
                // Registrar el error en el servicio de errores
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateUserController - InvalidOperation");
                return BadRequest(ex.Message);
            }
            catch (SqlException ex) when (ex.Number == 547) // Error de clave foránea
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateUserController - ForeignKey");
                return BadRequest("Error de clave foránea: Verifica que los IDs de las entidades relacionadas sean correctos.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateUserController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador")]
        [HttpPut("update-user/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDTO dto, [FromQuery] int userId, [FromQuery] int departmentId)
        {
            try
            {
                // Llamar al servicio para actualizar el usuario
                await _service.UpdateAsyncUser(id, dto, userId, departmentId);
                return Ok(new { message = "Usuario actualizado correctamente" });
            }
            catch (InvalidOperationException ex)
            {
                // Registrar el error en el servicio de errores
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateUserController - InvalidOperation");
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                // Registrar el error en el servicio de errores
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateUserController - KeyNotFound");
                return NotFound("Usuario no encontrado");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateUserController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador")]
        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> SoftDelete(int id, [FromQuery] int userId, [FromQuery] int departmentId)
        {
            try
            {
                await _service.SoftDeleteAsyncUser(id, userId, departmentId);
                return Ok(new { message = "Usuario eliminado correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                // Registrar el error en el servicio de errores
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteUserController - KeyNotFound");
                return NotFound("Usuario no encontrado para eliminar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteUserController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador")]
        [HttpPatch("restore-user/{id}")]
        public async Task<IActionResult> Restore(int id, [FromQuery] int userId, [FromQuery] int departmentId)
        {
            try
            {
                await _service.RestoreAsyncUser(id, userId, departmentId);
                return Ok(new { message = "Usuario restaurado correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                // Registrar el error en el servicio de errores
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreUserController - KeyNotFound");
                return NotFound("Usuario no encontrado para restaurar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreUserController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}