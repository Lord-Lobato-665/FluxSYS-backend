using FluxSYS_backend.Application.DTOs.Invoices;
using FluxSYS_backend.Domain.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Application.Services
{
    public class InvoicesService : IInvoices
    {
        private readonly IInvoices _repository;

        public InvoicesService(IInvoices repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<InvoiceReadDTO>> GetAllAsyncInvoices()
        {
            return await _repository.GetAllAsyncInvoices();
        }

        public async Task AddAsyncInvoice(InvoiceCreateDTO dto, int userId, int departmentId)
        {
            await _repository.AddAsyncInvoice(dto, userId, departmentId);
        }

        public async Task UpdateAsyncInvoice(int id, InvoiceUpdateDTO dto, int userId, int departmentId)
        {
            await _repository.UpdateAsyncInvoice(id, dto, userId, departmentId);
        }

        public async Task SoftDeleteAsyncInvoice(int id, int userId, int departmentId)
        {
            await _repository.SoftDeleteAsyncInvoice(id, userId, departmentId);
        }

        public async Task RestoreAsyncInvoice(int id, int userId, int departmentId)
        {
            await _repository.RestoreAsyncInvoice(id, userId, departmentId);
        }
    }
}