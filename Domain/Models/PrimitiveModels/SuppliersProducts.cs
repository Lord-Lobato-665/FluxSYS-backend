using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluxSYS_backend.Domain.Models.PrincipalModels;

namespace FluxSYS_backend.Domain.Models.PrimitiveModels;

[Table("suppliers_products")]
public class SuppliersProducts
{
    [Key]
    public int Id_supplier_product { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Suggested_price { get; set; }

    [ForeignKey("Id_inventory_product_Id")]
    public int Id_inventory_product_Id { get; set; }

    [ForeignKey("Id_supplier_Id")]
    public int Id_supplier_Id { get; set; }

    public virtual Inventories Inventories { get; set; }
    public virtual Suppliers Suppliers { get; set; }
}
