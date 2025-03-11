namespace FluxSYS_backend.Application.DTOs.Inventories
{
    public class InventoryCreateDTO
    {
        public string Name_product { get; set; }
        public int Stock_product { get; set; }
        public decimal Price_product { get; set; }
        public int Id_category_product_Id { get; set; }
        public int Id_state_Id { get; set; }
        public int Id_movement_type_Id { get; set; }
        public int Id_supplier_Id { get; set; }
        public int Id_department_Id { get; set; }
        public int Id_module_Id { get; set; }
        public int Id_company_Id { get; set; }
        public int Id_user_Id { get; set; }
    }
}