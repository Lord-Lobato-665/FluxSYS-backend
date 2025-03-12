namespace FluxSYS_backend.Application.DTOs.PurchaseOrders
{
    public class PurchaseOrderCreateDTO
    {
        public string Name_purchase_order { get; set; }
        public int Id_user_Id { get; set; }
        public int Id_category_purchase_order_Id { get; set; }
        public int Id_department_Id { get; set; }
        public int Id_supplier_Id { get; set; }
        public int Id_state_Id { get; set; }
        public int Id_movement_type_Id { get; set; }
        public int Id_company_Id { get; set; }
        public List<OrderProductCreateDTO> Products { get; set; }
    }

    public class OrderProductCreateDTO
    {
        public int Id_inventory_product_Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}