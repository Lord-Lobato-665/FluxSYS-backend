using FluxSYS_backend.Application.DTOs.CategoriesPurchaseOrders;
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
    public class CategoriesPurchaseOrdersController : ControllerBase
    {
        private readonly CategoriesPurchaseOrdersService _service;
        private readonly ErrorLogService _errorLogService;
        private readonly CompaniesService _companiesService;

        public CategoriesPurchaseOrdersController(CategoriesPurchaseOrdersService service, ErrorLogService errorLogService, CompaniesService companiesService)
        {
            _service = service;
            _errorLogService = errorLogService;
            _companiesService = companiesService;
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpGet("get-categories-purchase-orders")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categoriesPurchaseOrders = await _service.GetAllAsyncCategoriesPurchaseOrders();
                return Ok(categoriesPurchaseOrders);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetCategoriesPurchaseOrdersController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento")]
        [HttpPost("create-category-purchase-order")]
        public async Task<IActionResult> Create([FromBody] CategoryPurchaseOrderViewModel model)
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

                var dto = new CategoryPurchaseOrderCreateDTO
                {
                    Name_category_purchase_order = model.Name_category_purchase_order,
                    Id_company_Id = model.Id_company_Id
                };
                await _service.AddAsyncCategoryPurchaseOrder(dto);
                return Ok(new { message = "Categoría de orden de compra creada correctamente" });
            }
            catch (SqlException ex) when (ex.Number == 547) // Error de clave foránea
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateCategoryPurchaseOrderController - ForeignKey");
                return BadRequest("Error de clave foránea: Verifica que el ID de la compañía sea correcto.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateCategoryPurchaseOrderController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento")]
        [HttpPut("update-category-purchase-order/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryPurchaseOrderViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dto = new CategoryPurchaseOrderUpdateDTO
                {
                    Name_category_purchase_order = model.Name_category_purchase_order,
                    Id_company_Id = model.Id_company_Id
                };
                await _service.UpdateAsyncCategoryPurchaseOrder(id, dto);
                return Ok(new { message = "Categoría de orden de compra actualizada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateCategoryPurchaseOrdertController - KeyNotFound");
                return NotFound("Categoría de orden de compra no encontrada");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateCategoryPurchaseOrderController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpDelete("delete-category-purchase-order/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                await _service.SoftDeleteAsyncCategoryPurchaseOrder(id);
                return Ok(new { message = "Categoría de orden de compra eliminada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "DeleteCategoryPurchaseOrdertController - KeyNotFound");
                return NotFound("Categoría de orden de compra no encontrada para eliminar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteCategoryPurchaseOrderController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [CustomAuthorize("Administrador", "Administrador Empresarial", "Jefe de Departamento", "Subjefe de Departamento", "Colaborador")]
        [HttpPatch("restore-category-purchase-order/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                await _service.RestoreAsyncCategoryPurchaseOrder(id);
                return Ok(new { message = "Categoría de orden de compra restaurada correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreCategoryPurchaseOrdertController - KeyNotFound");
                return NotFound("Categoría de orden de compra no encontrada para restaurar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreCategoryPurchaseOrderController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}