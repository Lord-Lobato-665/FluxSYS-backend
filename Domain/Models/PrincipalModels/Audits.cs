using FluxSYS_backend.Domain.Models.PrimitiveModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FluxSYS_backend.Domain.Models.PrincipalModels;

[Table("audits")]
public class Audits
{
    [Key]
    public int Id_audit { get; set; }
    public DateTime? Date_insert { get; set; }
    public DateTime? Date_update { get; set; }
    public DateTime? Date_delete { get; set; }
    public DateTime? Date_restore { get; set; }
    public int Amount_modify { get; set; }

    [ForeignKey("Id_user_Id")]
    public int Id_user_Id { get; set; }

    [ForeignKey("Id_department_Id")]
    public int Id_department_Id { get; set; }

    [ForeignKey("Id_module_Id")]
    public int Id_module_Id { get; set; }

    [ForeignKey("Id_company_Id")]
    public int Id_company_Id { get; set; }
    public bool Delete_log_audits { get; set; } = false;

    public virtual Users Users { get; set; }
    public virtual Departments Departments { get; set; }
    public virtual Modules Modules { get; set; }
    public virtual Companies Companies { get; set; }
}
