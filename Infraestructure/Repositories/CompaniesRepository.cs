using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Infraestructure.Data;
using FluxSYS_backend.Application.DTOs.Companies;
using Microsoft.EntityFrameworkCore;
using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Application.Services;

namespace FluxSYS_backend.Infraestructure.Repositories;

public class CompaniesRepository : ICompanies
{
    private readonly ApplicationDbContext _context;
    private readonly ErrorLogService _errorLogService;

    public CompaniesRepository(ApplicationDbContext context, ErrorLogService errorLogService)
    {
        _context = context;
        _errorLogService = errorLogService;
    }

    public async Task<IEnumerable<CompanyReadDTO>> GetAllAsyncCompanies()
    {
        try
        {
            return await _context.Companies
                //.Where(c => !c.Delete_log_company)
                .Select(c => new CompanyReadDTO
                {
                    Id_company = c.Id_company,
                    Name_company = c.Name_company,
                    Delete_log_company = c.Delete_log_company
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllCompanies");
            return new List<CompanyReadDTO>();
        }
    }

    public async Task AddAsyncCompany(CompanyCreateDTO dto)
    {
        try
        {
            var company = new Companies
            {
                Name_company = dto.Name_company
            };

            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "CreateCompany");
        }
    }

    public async Task UpdateAsyncCompany(int id, CompanyUpdateDTO dto)
    {
        try
        {
            var company = await _context.Companies
                .FirstOrDefaultAsync(c => c.Id_company == id);

            if (company == null)
            {
                throw new KeyNotFoundException("Compañía no encontrada");
            }

            company.Name_company = dto.Name_company;

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateCompany");
        }
    }

    public async Task SoftDeleteAsync(int id)
    {
        try
        {
            var company = await _context.Companies
                .FirstOrDefaultAsync(c => c.Id_company == id);

            if (company != null)
            {
                company.Delete_log_company = true;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Compañía no encontrada para eliminar");
            }
        }
        catch (Exception ex)
        {
            await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteCompany");
        }
    }

    public async Task RestoreAsync(int id)
    {
        try
        {
            var company = await _context.Companies
                .FirstOrDefaultAsync(c => c.Id_company == id && c.Delete_log_company);

            if (company != null)
            {
                company.Delete_log_company = false;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Compañía no encontrada para restaurar");
            }
        }
        catch (Exception ex)
        {
            await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreCompany");
        }
    }
}
