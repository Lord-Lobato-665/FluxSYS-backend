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

        public async Task<IEnumerable<InvoiceReadDTO>> GetInvoicesByCompanyIdAsync(int companyId)
        {
            return await _repository.GetInvoicesByCompanyIdAsync(companyId);
        }

        public async Task AddAsyncInvoice(InvoiceCreateDTO dto, string nameUser, string nameDepartment)
        {
            await _repository.AddAsyncInvoice(dto, nameUser, nameDepartment);
        }

        public async Task UpdateAsyncInvoice(int id, InvoiceUpdateDTO dto, string nameUser, string nameDepartment)
        {
            await _repository.UpdateAsyncInvoice(id, dto, nameUser, nameDepartment);
        }

        public async Task SoftDeleteAsyncInvoice(int id, string nameUser, string nameDepartment)
        {
            await _repository.SoftDeleteAsyncInvoice(id, nameUser, nameDepartment);
        }

        public async Task RestoreAsyncInvoice(int id, string nameUser, string nameDepartment)
        {
            await _repository.RestoreAsyncInvoice(id, nameUser, nameDepartment);
        }
    }
}