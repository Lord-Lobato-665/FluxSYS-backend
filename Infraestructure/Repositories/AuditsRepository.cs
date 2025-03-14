using FluxSYS_backend.Application.DTOs.Audits;
using FluxSYS_backend.Application.Services;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Domain.Models.PrincipalModels;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluxSYS_backend.Infrastructure.Repositories
{
    public class AuditsRepository : IAudits
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorLogService _errorLogService;

        public AuditsRepository(ApplicationDbContext context, ErrorLogService errorLogService)
        {
            _context = context;
            _errorLogService = errorLogService;
        }

        public async Task<IEnumerable<AuditReadDTO>> GetAllAsyncAudits()
        {
            try
            {
                var audits = await _context.Audits
                    .Select(a => new AuditReadDTO
                    {
                        Id_audit = a.Id_audit,
                        Date_insert = a.Date_insert.HasValue
                            ? a.Date_insert.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Date_update = a.Date_update.HasValue
                            ? a.Date_update.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Date_delete = a.Date_delete.HasValue
                            ? a.Date_delete.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Date_restore = a.Date_restore.HasValue
                            ? a.Date_restore.Value.ToString("yyyy-MM-dd HH:mm:ss")
                            : "N/A",
                        Amount_modify = a.Amount_modify,
                        Name_user = a.Name_user,
                        Name_department = a.Name_department,
                        Name_module = a.Name_module,
                        Name_company = a.Name_company,
                        Delete_log_audits = a.Delete_log_audits
                    })
                    .ToListAsync();

                return audits;
            }
            catch (Exception ex)
            {
                await _errorLogService.SaveErrorAsync(ex.Message, ex.StackTrace, "GetAllAudits");
                return new List<AuditReadDTO>();
            }
        }
    }
}