using System.ComponentModel.DataAnnotations;

namespace FluxSYS_backend.Application.DTOs.Suppliers
{
    public class SupplierUpdateDTO
    {
        public string Name_supplier { get; set; }
        public string Mail_supplier { get; set; }
        public long Phone_supplier { get; set; }
        public int Id_category_supplier_Id { get; set; }
        public int Id_company_Id { get; set; }

        public List<SupplierProductUpdateDTO> Products { get; set; }
    }

    public class SupplierProductUpdateDTO
    {
        public int Id_inventory_product_Id { get; set; }
        public decimal Suggested_price { get; set; }
    }
}