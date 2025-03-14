using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Domain.Models.PrincipalModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FluxSYS_backend;

[Table("users")]
public class Users
{
    [Key]
    public int Id_user { get; set; }
    public string Name_user { get; set; }
    public string Mail_user { get; set; }
    public long Phone_user { get; set; }
    public string Password_user { get; set; }

    [ForeignKey("Id_rol_Id")]
    public int Id_rol_Id { get; set; }

    [ForeignKey("Id_position_Id")]
    public int Id_position_Id { get; set; }

    [ForeignKey("Id_department_Id")]
    public int Id_department_Id { get; set; }

    [ForeignKey("Id_company_Id")]
    public int Id_company_Id { get; set; }

    [ForeignKey("Id_module_Id")]
    public int Id_module_Id { get; set; }
    public DateTime? Date_insert { get; set; }
    public DateTime? Date_update { get; set; }
    public DateTime? Date_delete { get; set; }
    public DateTime? Date_restore { get; set; }
    public bool Delete_log_user { get; set; } = false;

    public virtual Roles Roles { get; set; }
    public virtual Positions Positions { get; set; }
    public virtual Departments Departments { get; set; }
    public virtual Companies Companies { get; set; }
    public virtual Modules Modules { get; set; }

    public virtual ICollection<PurchaseOrders> PurchaseOrders { get; set; }
    public virtual ICollection<InventoryMovements> InventoryMovements { get; set; }
    public virtual ICollection<Inventories> Inventories { get; set; }
}
