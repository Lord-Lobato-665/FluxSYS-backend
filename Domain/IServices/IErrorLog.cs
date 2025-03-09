using System.Collections.Generic;
using System.Threading.Tasks;
using FluxSYS_backend.Domain.Models.PrincipalModels;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IErrorLog
    {
        Task SaveErrorAsync(string message, string stackTrace, string source);
        Task<List<ErrorLogs>> GetAllErrorsAsync();
    }
}
