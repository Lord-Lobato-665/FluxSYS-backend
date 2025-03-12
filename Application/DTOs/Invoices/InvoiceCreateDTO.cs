namespace FluxSYS_backend.Application.DTOs.Invoices;

public class InvoiceCreateDTO
{
    public string Name_invoice { get; set; }
    public int Id_purchase_order_Id { get; set; }
    public int Id_supplier_Id { get; set; }
    public int Id_department_Id { get; set; }
    public int Id_company_Id { get; set; }
    public List<InvoiceProductCreateDTO> Products { get; set; }
}

public class InvoiceProductCreateDTO
{
    public int Id_inventory_product_Id { get; set; }
    public int Quantity { get; set; }
}
