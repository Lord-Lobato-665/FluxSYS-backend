

namespace FluxSYS_backend.Application.DTOs.Suppliers
{
    public class SupplierCreateDTO
    {
        public string Name_supplier { get; set; }
        public string Mail_supplier { get; set; }
        public long Phone_supplier { get; set; }
        public int Id_category_supplier_Id { get; set; }
        public int Id_module_Id { get; set; }
        public int Id_company_Id { get; set; }

        public List<SupplierProductCreateDTO> Products { get; set; }
    }

    public class SupplierProductCreateDTO
    {
        public int Id_inventory_product_Id { get; set; }
        public decimal Suggested_price { get; set; }
    }
}