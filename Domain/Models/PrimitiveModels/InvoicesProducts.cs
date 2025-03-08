using FluxSYS_backend.Domain.Models.PrincipalModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FluxSYS_backend;

[Table("invoices_products")]
public class InvoicesProducts
{

    [Key]
    public int Id_invoice_product { get; set; }
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Unit_price { get; set; }

    [ForeignKey("Id_invoice_Id")]
    public int Id_invoice_Id { get; set; }

    [ForeignKey("Id_inventory_product_Id")]
    public int Id_inventory_product_Id { get; set; }

    public virtual Invoices Invoices { get; set; }
    public virtual Inventories Inventories { get; set; }
}
