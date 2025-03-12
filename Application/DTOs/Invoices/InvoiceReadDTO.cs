namespace FluxSYS_backend.Application.DTOs.Invoices;

public class InvoiceReadDTO
{
    public int Id_invoice { get; set; }
    public string Name_invoice { get; set; }
    public int Amount_items_in_the_invoice { get; set; }
    public decimal Total_price_invoice { get; set; }
    public string Name_purchase_order { get; set; }
    public string Name_supplier { get; set; }
    public string Name_department { get; set; }
    public string Name_module { get; set; }
    public string Name_company { get; set; }
    public string Date_insert { get; set; }
    public string Date_update { get; set; }
    public string Date_delete { get; set; }
    public string Date_restore { get; set; }
    public bool Delete_log_invoices { get; set; }
    public List<InvoiceProductReadDTO> Products { get; set; }
}

public class InvoiceProductReadDTO
{
    public int Id_inventory_product { get; set; }
    public string Name_product { get; set; }
    public int Quantity { get; set; }
    public decimal Unit_price { get; set; }
}
