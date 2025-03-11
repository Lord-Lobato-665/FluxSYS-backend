namespace FluxSYS_backend.Application.DTOs.Inventories;

public class InventoryReadDTO
{
    public int Id_inventory_product { get; set; }
    public string Name_product { get; set; }
    public int Stock_product { get; set; }
    public decimal Price_product { get; set; }
    public string Name_category_product { get; set; }
    public string Name_state { get; set; }
    public string Name_movement_type { get; set; }
    public string Name_supplier { get; set; }
    public string Name_department { get; set; }
    public string Name_module { get; set; }
    public string Name_company { get; set; }
    public string Name_user { get; set; }
    public string Date_insert { get; set; }
    public string Date_update { get; set; }
    public string Date_delete { get; set; }
    public string Date_restore { get; set; }
    public bool Delete_log_inventory { get; set; }
}