using FluxSYS_backend.Application.DTOs.CategoriesSuppliers;
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
    public class CategoriesSuppliersController : ControllerBase
    {
        private readonly CategoriesSuppliersService _service;
        private readonly ErrorLogService _errorLogService;
        private readonly CompaniesService _companiesService;

        public CategoriesSuppliersController(CategoriesSuppliersService service, ErrorLogService errorLogService, CompaniesService companiesService)
        {
            _service = service;
            _errorLogService = errorLogService;
            _companiesService = companiesService;
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpGet("get-categories-suppliers")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categoriesSuppliers = await _service.GetAllAsyncCategoriesSuppliers();
                return Ok(categoriesSuppliers);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetCategoriesSuppliersController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento")]
        [HttpPost("create-category-supplier")]
        public async Task<IActionResult> Create([FromBody] CategorySuppliersViewModel model)
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

                var dto = new CategorySuppliersCreateDTO
                {
                    Name_category_supplier = model.Name_category_supplier,
                    Id_company_Id = model.Id_company_Id
                };
                await _service.AddAsyncCategorySupplier(dto);
                return Ok(new { message = "Categoría de proveedor creada correctamente" });
            }
            catch (SqlException ex) when (ex.Number == 547) // Error de clave foránea
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateCategorySupplierController - ForeignKey");
                return BadRequest("Error de clave foránea: Verifica que el ID de la compañía sea correcto.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateCategorySupplierController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento")]
        [HttpPut("update-category-supplier/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategorySuppliersViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dto = new CategorySuppliersUpdateDTO
                {
                    Name_category_supplier = model.Name_category_supplier,
                    Id_company_Id = model.Id_company_Id
                };
                await _service.UpdateAsyncCategorySupplier(id, dto);
                return Ok(new { message = "Categoría de proveedor actualizada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateCategorySupplierController - KeyNotFound");
                return NotFound("Categoría de proveedor no encontrada");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateCategorySupplierController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpDelete("delete-category-supplier/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                await _service.SoftDeleteAsyncCategorySupplier(id);
                return Ok(new { message = "Categoría de proveedor eliminada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "DeleteCategorySupplierController - KeyNotFound");
                return NotFound("Categoría de proveedor no encontrada para eliminar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteCategorySupplierController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpPatch("restore-category-supplier/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                await _service.RestoreAsyncCategorySupplier(id);
                return Ok(new { message = "Categoría de proveedor restaurada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreCategorySupplierController - KeyNotFound");
                return NotFound("Categoría de proveedor no encontrada para restaurar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreCategorySupplierController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}