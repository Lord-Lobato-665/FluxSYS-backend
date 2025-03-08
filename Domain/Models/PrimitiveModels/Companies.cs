using FluxSYS_backend.Domain.Models.PrincipalModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluxSYS_backend.Domain.Models.PrimitiveModels;

[Table("companies")]
public class Companies
{
    [Key]
    public int Id_company { get; set; }
    public string Name_company { get; set; }
    public bool Delete_log_company { get; set; } = false;

    public virtual ICollection<States> States { get; set; }
    public virtual ICollection<MovementsTypes> MovementsTypes { get; set; }
    public virtual ICollection<Departments> Departments { get; set; }
    public virtual ICollection<Positions> Positions { get; set; }
    public virtual ICollection<CategoriesProducts> CategoriesProducts { get; set; }
    public virtual ICollection<CategoriesPurchaseOrders> CategoriesPurchaseOrders { get; set; }
    public virtual ICollection<CategoriesSuppliers> CategoriesSuppliers { get; set; }
    public virtual ICollection<Users> Users { get; set; }
    public virtual ICollection<Suppliers> Suppliers { get; set; }
    public virtual ICollection<Inventories> Inventories { get; set; }
    public virtual ICollection<PurchaseOrders> PurchaseOrders { get; set; }
    public virtual ICollection<Invoices> Invoices { get; set; }
    public virtual ICollection<InventoryMovements> InventoryMovements { get; set; }
    public virtual ICollection<Audits> Audits { get; set; }
}
