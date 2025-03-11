using FluxSYS_backend.Application.DTOs.Users;
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
    public class UsersController : ControllerBase
    {
        private readonly UsersService _service;
        private readonly ErrorLogService _errorLogService;
        private readonly RolesService _rolesService;
        private readonly PositionsService _positionsService;
        private readonly DepartmentsService _departmentsService;
        private readonly CompaniesService _companiesService;
        private readonly ModulesService _modulesService;

        public UsersController(UsersService service, ErrorLogService errorLogService, RolesService rolesService, PositionsService positionsService, DepartmentsService departmentsService, CompaniesService companiesService, ModulesService modulesService)
        {
            _service = service;
            _errorLogService = errorLogService;
            _rolesService = rolesService;
            _positionsService = positionsService;
            _departmentsService = departmentsService;
            _companiesService = companiesService;
            _modulesService = modulesService;
        }

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

        [HttpPost("create-user")]
        public async Task<IActionResult> Create([FromBody] UserViewModel model)
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

                var module = await _modulesService.GetAllAsyncModules();
                if (!module.Any(m => m.Id_module == model.Id_module_Id))
                {
                    return BadRequest("El módulo especificado no existe.");
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
                    Id_company_Id = model.Id_company_Id,
                    Id_module_Id = model.Id_module_Id
                };
                await _service.AddAsyncUser(dto);
                return Ok(new { message = "Usuario creado correctamente" });
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

        [HttpPut("update-user/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dto = new UserUpdateDTO
                {
                    Name_user = model.Name_user,
                    Mail_user = model.Mail_user,
                    Phone_user = model.Phone_user,
                    Password_user = model.Password_user,
                    Id_rol_Id = model.Id_rol_Id,
                    Id_position_Id = model.Id_position_Id,
                    Id_department_Id = model.Id_department_Id,
                    Id_company_Id = model.Id_company_Id,
                    Id_module_Id = model.Id_module_Id
                };
                await _service.UpdateAsyncUser(id, dto);
                return Ok(new { message = "Usuario actualizado correctamente" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Usuario no encontrado");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateUserController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                await _service.SoftDeleteAsyncUser(id);
                return Ok(new { message = "Usuario eliminado correctamente" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Usuario no encontrado para eliminar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteUserController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPatch("restore-user/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                await _service.RestoreAsyncUser(id);
                return Ok(new { message = "Usuario restaurado correctamente" });
            }
            catch (KeyNotFoundException)
            {
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