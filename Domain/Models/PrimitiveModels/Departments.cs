using FluxSYS_backend.Domain.Models.PrincipalModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluxSYS_backend.Domain.Models.PrimitiveModels;

[Table("departments")]
public class Departments
{
    [Key]
    public int Id_department {  get; set; }
    public string Name_deparment { get; set; }

    [ForeignKey("Id_company_Id")]
    public int Id_company_Id { get; set; }
    public bool Delete_log_department { get; set; } = false;

    public virtual Companies Companies { get; set; }

    public virtual ICollection<Users> Users { get; set; }
    public virtual ICollection<Inventories> Inventories { get; set; }
    public virtual ICollection<PurchaseOrders> PurchaseOrders { get; set; }
    public virtual ICollection<Invoices> Invoices { get; set; }
    public virtual ICollection<InventoryMovements> InventoryMovements { get; set; }
    public virtual ICollection<Audits> Audits { get; set; }
}
