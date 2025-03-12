using FluxSYS_backend.Application.DTOs.Invoices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IInvoices
    {
        Task<IEnumerable<InvoiceReadDTO>> GetAllAsyncInvoices();
        Task AddAsyncInvoice(InvoiceCreateDTO dto, int userId, int departmentId);
        Task UpdateAsyncInvoice(int id, InvoiceUpdateDTO dto, int userId, int departmentId);
        Task SoftDeleteAsyncInvoice(int id, int userId, int departmentId);
        Task RestoreAsyncInvoice(int id, int userId, int departmentId);
    }
}