using FluxSYS_backend.Domain.Models.PrincipalModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluxSYS_backend.Domain.Models.PrimitiveModels;

[Table("modules")]
public class Modules
{
    [Key]
    public int Id_module {  get; set; }
    public string Name_module {  get; set; }
    public bool Delete_log_module { get; set; } = false;

    public virtual ICollection<Users> Users { get; set; }
    public virtual ICollection<Suppliers> Suppliers { get; set; }
    public virtual ICollection<Inventories> Inventories { get; set; }
    public virtual ICollection<PurchaseOrders> PurchaseOrders { get; set; }
    public virtual ICollection<Invoices> Invoices { get; set; }
    public virtual ICollection<InventoryMovements> InventoryMovements { get; set; }
}
