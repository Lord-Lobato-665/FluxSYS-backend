﻿using FluxSYS_backend.Application.DTOs.Inventories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluxSYS_backend.Domain.IServices
{
    public interface IInventories
    {
        Task<IEnumerable<InventoryReadDTO>> GetAllAsyncInventories();
        Task<IEnumerable<InventoryReadDTO>> GetInventoriesByCompanyIdAsync(int companyId);
        Task<InventoryReadByIdDTO> GetInventoryByIdAsync(int id);
        Task AddAsyncInventory(InventoryCreateDTO dto, string nameUser, string nameDepartment);
        Task UpdateAsyncInventory(int id, InventoryUpdateDTO dto, string nameUser, string nameDepartment);
        Task SoftDeleteAsyncInventory(int id, string nameUser, string nameDepartment);
        Task RestoreAsyncInventory(int id, string nameUser, string nameDepartment);
        Task<byte[]> GetPDF(string companyName, string departmentName);
    }
}