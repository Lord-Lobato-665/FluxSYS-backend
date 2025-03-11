using FluxSYS_backend.Application.DTOs.PurchaseOrders;
using FluxSYS_backend.Domain.IServices;

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

        public async Task AddAsyncPurchaseOrder(PurchaseOrderCreateDTO dto)
        {
            await _repository.AddAsyncPurchaseOrder(dto);
        }

        public async Task UpdateAsyncPurchaseOrder(int id, PurchaseOrderUpdateDTO dto)
        {
            await _repository.UpdateAsyncPurchaseOrder(id, dto);
        }

        public async Task SoftDeleteAsyncPurchaseOrder(int id)
        {
            await _repository.SoftDeleteAsyncPurchaseOrder(id);
        }

        public async Task RestoreAsyncPurchaseOrder(int id)
        {
            await _repository.RestoreAsyncPurchaseOrder(id);
        }
    }
}