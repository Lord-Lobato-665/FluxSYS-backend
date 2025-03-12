using FluxSYS_backend.Application.DTOs.Audits;
using FluxSYS_backend.Domain.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Application.Services
{
    public class AuditsService : IAudits
    {
        private readonly IAudits _repository;

        public AuditsService(IAudits repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AuditReadDTO>> GetAllAsyncAudits()
        {
            return await _repository.GetAllAsyncAudits();
        }
    }
}