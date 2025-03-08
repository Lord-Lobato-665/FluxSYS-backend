using FluxSYS_backend.Domain.Models.PrimitiveModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FluxSYS_backend.Domain.Models.PrincipalModels;

[Table("purchase_orders")]
public class PurchaseOrders
{
    [Key]
    public int Id_purchase_order { get; set; }
    public string Name_purchase_order { get; set; }
    public int Amount_items_in_the_order { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Total_price_products { get; set; }

    [ForeignKey("Id_user_Id")]
    public int Id_user_Id { get; set; }

    [ForeignKey("Id_category_purchase_order_Id")]
    public int Id_category_purchase_order_Id { get; set; }

    [ForeignKey("Id_department_Id")]
    public int Id_department_Id { get; set; }

    [ForeignKey("Id_supplier_Id")]
    public int Id_supplier_Id { get; set; }

    [ForeignKey("Id_state_Id")]
    public int Id_state_Id { get; set; }

    [ForeignKey("Id_movement_type_Id")]
    public int Id_movement_type_Id { get; set; }

    [ForeignKey("Id_module_Id")]
    public int Id_module_Id { get; set; }

    [ForeignKey("Id_company_Id")]
    public int Id_company_Id { get; set; }
    public DateTime? Date_insert { get; set; }
    public DateTime? Date_update { get; set; }
    public DateTime? Date_delete { get; set; }
    public DateTime? Date_restore { get; set; }
    public bool Delete_log_purchase_orders { get; set; } = false;

    public virtual Users Users { get; set; }
    public virtual CategoriesPurchaseOrders CategoryPurchaseOrders { get; set; }
    public virtual Departments Departments { get; set; }
    public virtual Suppliers Suppliers { get; set; }
    public virtual States States { get; set; }
    public virtual MovementsTypes MovementsTypes { get; set; }
    public virtual Modules Modules { get; set; }
    public virtual Companies Companies { get; set; }

    public virtual ICollection<OrdersProducts> OrdersProducts { get; set; }
    public virtual ICollection<Invoices> Invoices { get; set; }
}
