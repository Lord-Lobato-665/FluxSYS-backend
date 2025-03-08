using FluxSYS_backend.Domain.Models.PrincipalModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluxSYS_backend.Domain.Models.PrimitiveModels;

[Table("movements_types")]
public class MovementsTypes
{
    [Key]
    public int Id_movement_type { get; set; }
    public string Name_movement_type { get; set; }

    [ForeignKey("Id_company_Id")]
    public int Id_company_Id {  get; set; }

    [ForeignKey("Id_clasification_movement_Id")]
    public int Id_clasification_movement_Id { get; set; }
    public bool Delete_log_movement_type { get; set; } = false;

    public virtual Companies Companies { get; set; }
    public virtual ClasificationMovements ClasificationsMovements { get; set; }

    public virtual ICollection<Inventories> Inventories { get; set; }
    public virtual ICollection<PurchaseOrders> PurchaseOrders { get; set; }
    public virtual ICollection<InventoryMovements> InventoryMovements { get; set; }
}
