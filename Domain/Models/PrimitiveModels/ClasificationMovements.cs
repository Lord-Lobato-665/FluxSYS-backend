using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluxSYS_backend.Domain.Models.PrimitiveModels;


[Table("clasification_movements")]
public class ClasificationMovements
{
    [Key]
    public int Id_clasification_movement { get; set; }
    public string Name_clasification_movement { get; set; }
    public bool Delete_log_clasification_movement { get; set; } = false;

    public virtual ICollection<MovementsTypes> MoventsTypes { get; set; }
}
