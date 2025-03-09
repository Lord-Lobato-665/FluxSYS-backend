using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluxSYS_backend.Domain.IServices;
using FluxSYS_backend.Domain.Models.PrincipalModels;
using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Infraestructure.Repositories
{
    public class ErrorLogRepository : IErrorLog
    {
        private readonly ApplicationDbContext _context;

        public ErrorLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveErrorAsync(string message, string stackTrace, string source)
        {
            var errorLog = new ErrorLogs
            {
                Message_error = message,
                Stacktrace_error = stackTrace,
                Source_error = source,
                Timestamp = DateTime.Now
            };

            await _context.ErrorLogs.AddAsync(errorLog);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ErrorLogs>> GetAllErrorsAsync()
        {
            return await _context.ErrorLogs
                .OrderByDescending(e => e.Timestamp)
                .ToListAsync();
        }
    }
}
