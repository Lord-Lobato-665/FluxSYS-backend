using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluxSYS_backend.Domain.Models.PrincipalModels;

namespace FluxSYS_backend.Domain.Models.PrimitiveModels;

[Table("states")]
public class States
{
    [Key]
    public int Id_state { get; set; }
    public string Name_state { get; set; }
    public bool Delete_log_state { get; set; } = false;

    [ForeignKey("Id_company_Id")]
    public int Id_company_Id { get; set; }

    public virtual Companies Companies { get; set; }

    public virtual ICollection<Inventories> Inventories { get; set; }
    public virtual ICollection<PurchaseOrders> PurchaseOrders { get; set; }
}
