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

        public async Task AddAsyncPurchaseOrder(PurchaseOrderCreateDTO dto, int userId, int departmentId)
        {
            await _repository.AddAsyncPurchaseOrder(dto, userId, departmentId);
        }

        public async Task UpdateAsyncPurchaseOrder(int id, PurchaseOrderUpdateDTO dto, int userId, int departmentId)
        {
            await _repository.UpdateAsyncPurchaseOrder(id, dto, userId, departmentId);
        }

        public async Task SoftDeleteAsyncPurchaseOrder(int id, int userId, int departmentId)
        {
            await _repository.SoftDeleteAsyncPurchaseOrder(id, userId, departmentId);
        }

        public async Task RestoreAsyncPurchaseOrder(int id, int userId, int departmentId)
        {
            await _repository.RestoreAsyncPurchaseOrder(id, userId, departmentId);
        }
    }
}