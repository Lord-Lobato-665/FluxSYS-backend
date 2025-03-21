using FluxSYS_backend.Application.DTOs.Companies;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Application.AppServices;

public class CompaniesService
{
    private readonly ICompanies _repository;
    private readonly ApplicationDbContext _context;
    private readonly ErrorLogService _errorLogService;

    public CompaniesService(ICompanies repository, ApplicationDbContext context, ErrorLogService errorLogService)
    {
        _repository = repository;
        _context = context;
        _errorLogService = errorLogService;
    }

    public async Task<IEnumerable<CompanyReadDTO>> GetAllAsyncCompanies()
    {
        var companies = await _repository.GetAllAsyncCompanies();
        return companies;
    }

    public async Task AddAsyncCompany(CompanyCreateDTO dto)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                await _repository.AddAsyncCompany(dto);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "AddAsyncCompany");
                throw;
            }
        }
    }

    public async Task UpdateAsyncCompany(int id, CompanyUpdateDTO dto)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                await _repository.UpdateAsyncCompany(id, dto);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "UpdateAsyncCompany");
                throw;
            }
        }
    }

    public async Task SoftDeleteAsync(int id)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                await _repository.SoftDeleteAsync(id);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "SoftDeleteAsync");
                throw;
            }
        }
    }

    public async Task RestoreAsync(int id)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                await _repository.RestoreAsync(id);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "RestoreAsync");
                throw;
            }
        }
    }
}