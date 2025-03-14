using FluxSYS_backend.Application.DTOs.PurchaseOrders;
using FluxSYS_backend.Domain.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Application.Services
{
    public class PurchaseOrdersService : IPurchaseOrders
    {
        private readonly IPurchaseOrders _repository;

        public PurchaseOrdersService(IPurchaseOrders repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PurchaseOrderReadDTO>> GetAllAsyncPurchaseOrders()
        {
            return await _repository.GetAllAsyncPurchaseOrders();
        }

        public async Task AddAsyncPurchaseOrder(PurchaseOrderCreateDTO dto, string nameUser, string nameDepartment)
        {
            await _repository.AddAsyncPurchaseOrder(dto, nameUser, nameDepartment);
        }

        public async Task UpdateAsyncPurchaseOrder(int id, PurchaseOrderUpdateDTO dto, string nameUser, string nameDepartment)
        {
            await _repository.UpdateAsyncPurchaseOrder(id, dto, nameUser, nameDepartment);
        }

        public async Task SoftDeleteAsyncPurchaseOrder(int id, string nameUser, string nameDepartment)
        {
            await _repository.SoftDeleteAsyncPurchaseOrder(id, nameUser, nameDepartment);
        }

        public async Task RestoreAsyncPurchaseOrder(int id, string nameUser, string nameDepartment)
        {
            await _repository.RestoreAsyncPurchaseOrder(id, nameUser, nameDepartment);
        }
    }
}