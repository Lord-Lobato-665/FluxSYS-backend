using FluxSYS_backend.Domain.Models.PrincipalModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluxSYS_backend.Domain.Models.PrimitiveModels;

[Table("orders_products")]
public class OrdersProducts
{
    [Key]
    public int Id_order_product {  get; set; }
    public int Quantity {  get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [ForeignKey("Id_purchase_order_Id")]
    public int Id_purchase_order_Id { get; set; }

    [ForeignKey("Id_inventory_product_Id")]
    public int Id_inventory_product_Id { get; set; }

    public virtual PurchaseOrders PurchaseOrders { get; set; }
    public virtual Inventories Inventories { get; set; }
}
