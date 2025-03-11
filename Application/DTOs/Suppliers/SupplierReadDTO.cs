namespace FluxSYS_backend.Application.DTOs.Suppliers
{
    public class SupplierReadDTO
    {
        public int Id_supplier { get; set; }
        public string Name_supplier { get; set; }
        public string Mail_supplier { get; set; }
        public long Phone_supplier { get; set; }
        public string Name_category_supplier { get; set; }
        public string Name_module { get; set; }
        public string Name_company { get; set; }
        public string Date_insert { get; set; }
        public string Date_update { get; set; }
        public string Date_delete { get; set; }
        public string Date_restore { get; set; }
        public bool Delete_log_suppliers { get; set; }
        public int Products_from_supplier { get; set; }
        public List<SupplierProductReadDTO> Products { get; set; }
    }

    public class SupplierProductReadDTO
    {
        public int Id_inventory_product { get; set; }
        public string Name_product { get; set; }
        public decimal Suggested_price { get; set; }
    }
}