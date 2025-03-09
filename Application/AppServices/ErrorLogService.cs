using System.Collections.Generic;
using System.Threading.Tasks;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Domain.Models.PrincipalModels;
using FluxSYS_backend.Infraestructure.Repositories;

namespace FluxSYS_backend.Application.Services
{
    public class ErrorLogService
    {
        private readonly IErrorLog _errorLogRepository;

        public ErrorLogService(IErrorLog errorLogRepository)
        {
            _errorLogRepository = errorLogRepository;
        }

        public async Task SaveErrorAsync(string message, string stackTrace, string source)
        {
            await _errorLogRepository.SaveErrorAsync(message, stackTrace, source);
        }

        public async Task<List<ErrorLogs>> GetAllErrorsAsync()
        {
            return await _errorLogRepository.GetAllErrorsAsync();
        }
    }
}
