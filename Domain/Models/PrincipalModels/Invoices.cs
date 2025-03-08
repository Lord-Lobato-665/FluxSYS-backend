using FluxSYS_backend.Domain.Models.PrimitiveModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FluxSYS_backend.Domain.Models.PrincipalModels;

[Table("invoices")]
public class Invoices
{
    [Key]
    public int Id_invoice { get; set; }
    public string Name_invoice { get; set; }
    public int Amount_items_in_the_invoice { get; set; }

    [ForeignKey("Id_purchase_order_Id")]
    public int Id_purchase_order_Id { get; set; }

    [ForeignKey("Id_supplier_Id")]
    public int Id_supplier_Id { get; set; }

    [ForeignKey("Id_department_Id")]
    public int Id_department_Id { get; set; }

    [ForeignKey("Id_module_Id")]
    public int Id_module_Id { get; set; }

    [ForeignKey("Id_company_Id")]
    public int Id_company_Id { get; set; }
    public DateTime? Date_insert { get; set; }
    public DateTime? Date_update { get; set; }
    public DateTime? Date_delete { get; set; }
    public DateTime? Date_restore { get; set; }
    public bool Delete_log_invoices { get; set; } = false;

    public virtual PurchaseOrders PurchaseOrders { get; set; }
    public virtual Suppliers Suppliers { get; set; }
    public virtual Departments Departments { get; set; }
    public virtual Modules Modules { get; set; }
    public virtual Companies Companies { get; set; }

    public virtual ICollection<InvoicesProducts> InvoicesProducts { get; set; }
}
