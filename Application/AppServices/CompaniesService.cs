using FluxSYS_backend.Application.DTOs.Companies;
using FluxSYS_backend.Domain.IServices;

namespace FluxSYS_backend.Application.AppServices;

public class CompaniesService
{
    private readonly ICompanies _repository;

    public CompaniesService(ICompanies repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CompanyReadDTO>> GetAllAsyncCompanies()
    {
        var companies = await _repository.GetAllAsyncCompanies();
        return companies;
    }

    public async Task AddAsyncCompany(CompanyCreateDTO dto)
    {
        await _repository.AddAsyncCompany(dto);
    }

    public async Task UpdateAsyncCompany(int id, CompanyUpdateDTO dto)
    {
        await _repository.UpdateAsyncCompany(id, dto);
    }

    public async Task SoftDeleteAsync(int id)
    {
        await _repository.SoftDeleteAsync(id);
    }

    public async Task RestoreAsync(int id)
    {
        await _repository.RestoreAsync(id);
    }
}
