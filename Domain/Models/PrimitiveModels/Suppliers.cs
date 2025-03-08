using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluxSYS_backend.Domain.Models.PrincipalModels;

namespace FluxSYS_backend.Domain.Models.PrimitiveModels;

[Table("suppliers")]
public class Suppliers
{
    [Key]
    public int Id_supplier { get; set; }
    public string Name_supplier { get; set; }
    public string Mail_supplier { get; set; }
    public long Phone_supplier { get; set; }

    [ForeignKey("Id_category_supplier_Id")]
    public int Id_category_supplier_Id { get; set; }

    [ForeignKey("Id_module_Id")]
    public int Id_module_Id { get; set; }

    [ForeignKey("Id_company_Id")]
    public int Id_company_Id { get; set; }

    public DateTime? Date_insert { get; set; }
    public DateTime? Date_update { get; set; }
    public DateTime? Date_delete { get; set; }
    public DateTime? Date_restore { get; set; }
    public bool Delete_log_suppliers { get; set; } = false;

    public virtual CategoriesSuppliers CategoriesSuppliers { get; set; }
    public virtual Modules Modules { get; set; }
    public virtual Companies Companies { get; set; }

    public virtual ICollection<Inventories> Inventories { get; set; }
    public virtual ICollection<PurchaseOrders> PurchaseOrders { get; set; }
    public virtual ICollection<Invoices> Invoices { get; set; }
    public virtual ICollection<InventoryMovements> InventoryMovements { get; set; }
    public virtual ICollection<SuppliersProducts> SuppliersProducts { get; set; }
}
