using FluxSYS_backend.Application.DTOs.Audits;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IAudits
    {
        Task<IEnumerable<AuditReadDTO>> GetAllAsyncAudits();
    }
}