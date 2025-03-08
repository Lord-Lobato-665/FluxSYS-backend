using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluxSYS_backend.Domain.Models.PrimitiveModels;

[Table("positions")]
public class Positions
{
    [Key]
    public int Id_position { get; set; }
    public string Name_position { get; set; }

    [ForeignKey("Id_company_Id")]
    public int Id_company_Id { get; set; }
    public bool Delete_log_position { get; set; } = false;

    public virtual Companies Companies { get; set; }

    public virtual ICollection<Users> Users { get; set; }
}
