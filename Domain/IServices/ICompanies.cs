using FluxSYS_backend.Application.DTOs.Companies;

namespace FluxSYS_backend.Domain.IServices;

public interface ICompanies
{
    Task<IEnumerable<CompanyReadDTO>> GetAllAsyncCompanies();
    Task AddAsyncCompany(CompanyCreateDTO dto);
    Task UpdateAsyncCompany(int id, CompanyUpdateDTO dto);
    Task SoftDeleteAsync(int id);
    Task RestoreAsync(int id);
}
