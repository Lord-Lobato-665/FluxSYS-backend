using FluxSYS_backend.Application.DTOs.Invoices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IInvoices
    {
        Task<IEnumerable<InvoiceReadDTO>> GetAllAsyncInvoices();
        Task AddAsyncInvoice(InvoiceCreateDTO dto, string nameUser, string nameDepartment);
        Task UpdateAsyncInvoice(int id, InvoiceUpdateDTO dto, string nameUser, string nameDepartment);
        Task SoftDeleteAsyncInvoice(int id, string nameUser, string nameDepartment);
        Task RestoreAsyncInvoice(int id, string nameUser, string nameDepartment);
    }
}