using FluxSYS_backend.Application.DTOs.Suppliers;
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
    public class SuppliersController : ControllerBase
    {
        private readonly SuppliersService _service;
        private readonly ErrorLogService _errorLogService;
        private readonly CategoriesSuppliersService _categoriesSuppliersService;
        private readonly ModulesService _modulesService;
        private readonly CompaniesService _companiesService;

        public SuppliersController(SuppliersService service, ErrorLogService errorLogService, CategoriesSuppliersService categoriesSuppliersService, ModulesService modulesService, CompaniesService companiesService)
        {
            _service = service;
            _errorLogService = errorLogService;
            _categoriesSuppliersService = categoriesSuppliersService;
            _modulesService = modulesService;
            _companiesService = companiesService;
        }

        [HttpGet("get-suppliers")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var suppliers = await _service.GetAllAsyncSuppliers();
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetSuppliersController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPost("create-supplier")]
        public async Task<IActionResult> Create([FromBody] SupplierViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Verificar si las entidades relacionadas existen
                var categorySupplier = await _categoriesSuppliersService.GetAllAsyncCategoriesSuppliers();
                if (!categorySupplier.Any(c => c.Id_category_supplier == model.Id_category_supplier_Id))
                {
                    return BadRequest("La categoría de proveedor especificada no existe.");
                }

                var module = await _modulesService.GetAllAsyncModules();
                if (!module.Any(m => m.Id_module == model.Id_module_Id))
                {
                    return BadRequest("El módulo especificado no existe.");
                }

                var company = await _companiesService.GetAllAsyncCompanies();
                if (!company.Any(c => c.Id_company == model.Id_company_Id))
                {
                    return BadRequest("La compañía especificada no existe.");
                }

                var dto = new SupplierCreateDTO
                {
                    Name_supplier = model.Name_supplier,
                    Mail_supplier = model.Mail_supplier,
                    Phone_supplier = model.Phone_supplier,
                    Id_category_supplier_Id = model.Id_category_supplier_Id,
                    Id_module_Id = model.Id_module_Id,
                    Id_company_Id = model.Id_company_Id,
                    Products = model.Products.Select(p => new SupplierProductCreateDTO
                    {
                        Id_inventory_product_Id = p.Id_inventory_product_Id,
                        Suggested_price = p.Suggested_price
                    }).ToList()
                };
                await _service.AddAsyncSupplier(dto);
                return Ok(new { message = "Proveedor creado correctamente" });
            }
            catch (SqlException ex) when (ex.Number == 547) // Error de clave foránea
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateSupplierController - ForeignKey");
                return BadRequest("Error de clave foránea: Verifica que los IDs de las entidades relacionadas sean correctos.");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateSupplierController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPut("update-supplier/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SupplierViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dto = new SupplierUpdateDTO
                {
                    Name_supplier = model.Name_supplier,
                    Mail_supplier = model.Mail_supplier,
                    Phone_supplier = model.Phone_supplier,
                    Id_category_supplier_Id = model.Id_category_supplier_Id,
                    Id_company_Id = model.Id_company_Id,
                    Products = model.Products.Select(p => new SupplierProductUpdateDTO
                    {
                        Id_inventory_product_Id = p.Id_inventory_product_Id,
                        Suggested_price = p.Suggested_price
                    }).ToList()
                };
                await _service.UpdateAsyncSupplier(id, dto);
                return Ok(new { message = "Proveedor actualizado correctamente" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Proveedor no encontrado");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateSupplierController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpDelete("delete-supplier/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                await _service.SoftDeleteAsyncSupplier(id);
                return Ok(new { message = "Proveedor eliminado correctamente" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Proveedor no encontrado para eliminar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteSupplierController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPatch("restore-supplier/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                await _service.RestoreAsyncSupplier(id);
                return Ok(new { message = "Proveedor restaurado correctamente" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Proveedor no encontrado para restaurar");
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreSupplierController");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}