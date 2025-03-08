using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FluxSYS_backend.Domain.Models.PrimitiveModels;

[Table("roles")]
public class Roles
{
    [Key]
    public int Id_role { get; set; }
    public required string Name_role { get; set; }
    public bool Delete_log_role { get; set; } = false;

    public virtual ICollection<Users> Users { get; set; }

}
