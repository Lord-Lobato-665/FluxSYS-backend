namespace FluxSYS_backend.Application.DTOs.PurchaseOrders
{
    public class PurchaseOrderReadDTO
    {
        public int Id_purchase_order { get; set; }
        public string Name_purchase_order { get; set; }
        public int Amount_items_in_the_order { get; set; }
        public decimal Total_price_products { get; set; }
        public string Name_user { get; set; }
        public string Name_category_purchase_order { get; set; }
        public string Name_department { get; set; }
        public string Name_supplier { get; set; }
        public string Name_state { get; set; }
        public string Name_movement_type { get; set; }
        public string Name_module { get; set; }
        public string Name_company { get; set; }
        public string Date_insert { get; set; }
        public string Date_update { get; set; }
        public string Date_delete { get; set; }
        public string Date_restore { get; set; }
        public bool Delete_log_purchase_orders { get; set; }
        public List<OrderProductReadDTO> Products { get; set; }
    }

    public class OrderProductReadDTO
    {
        public int Id_inventory_product { get; set; }
        public string Name_product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}