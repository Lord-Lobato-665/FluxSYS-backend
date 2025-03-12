using FluxSYS_backend.Application.DTOs.InventoryMovements;
using FluxSYS_backend.Domain.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Application.Services
{
    public class InventoryMovementsService : IInventoryMovements
    {
        private readonly IInventoryMovements _repository;

        public InventoryMovementsService(IInventoryMovements repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<InventoryMovementReadDTO>> GetAllAsyncInventoryMovements()
        {
            return await _repository.GetAllAsyncInventoryMovements();
        }
    }
}