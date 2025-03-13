using FluxSYS_backend.Application.DTOs.CategoriesProducts;
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
    public class CategoriesProductsController : ControllerBase
    {
        private readonly CategoriesProductsService _service;
        private readonly ErrorLogService _errorLogService;
        private readonly CompaniesService _companiesService;

        public CategoriesProductsController(CategoriesProductsService service, ErrorLogService errorLogService, CompaniesService companiesService)
        {
            _service = service;
            _errorLogService = errorLogService;
            _companiesService = companiesService;
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpGet("get-categories-products")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categoriesProducts = await _service.GetAllAsyncCategoriesProducts();
                return Ok(categoriesProducts);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetCategoriesProductsController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento")]
        [HttpPost("create-category-product")]
        public async Task<IActionResult> Create([FromBody] CategoryProductsViewModel model)
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

                var dto = new CategoryProductsCreateDTO
                {
                    Name_category_product = model.Name_category_product,
                    Id_company_Id = model.Id_company_Id
                };
                await _service.AddAsyncCategoryProduct(dto);
                return Ok(new { message = "Categoría de producto creada correctamente" });
            }
            catch (SqlException ex) when (ex.Number == 547) // Error de clave foránea
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateCategoryProductController - ForeignKey");
                return BadRequest("Error de clave foránea: Verifica que el ID de la compañía sea correcto.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateCategoryProductController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento")]
        [HttpPut("update-category-product/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryProductsViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dto = new CategoryProductsUpdateDTO
                {
                    Name_category_product = model.Name_category_product,
                    Id_company_Id = model.Id_company_Id
                };
                await _service.UpdateAsyncCategoryProduct(id, dto);
                return Ok(new { message = "Categoría de producto actualizada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateCategoryProductController - KeyNotFound");
                return NotFound("Categoría de producto no encontrada");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateCategoryProductController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpDelete("delete-category-product/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                await _service.SoftDeleteAsyncCategoryProduct(id);
                return Ok(new { message = "Categoría de producto eliminada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "DeleteCategoryProductController - KeyNotFound");
                return NotFound("Categoría de producto no encontrada para eliminar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteCategoryProductController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpPatch("restore-category-product/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                await _service.RestoreAsyncCategoryProduct(id);
                return Ok(new { message = "Categoría de producto restaurada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreCategoryProductController - KeyNotFound");
                return NotFound("Categoría de producto no encontrada para restaurar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreCategoryProductController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}