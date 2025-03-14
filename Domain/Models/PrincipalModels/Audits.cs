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
    public string Name_user { get; set; }
    public string Name_department { get; set; }
    public string Name_module { get; set; }
    public string Name_company { get; set; }
    public bool Delete_log_audits { get; set; } = false;
}
