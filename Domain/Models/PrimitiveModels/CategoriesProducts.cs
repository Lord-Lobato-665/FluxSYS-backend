using FluxSYS_backend.Domain.Models.PrincipalModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluxSYS_backend.Domain.Models.PrimitiveModels;

[Table("categories_products")]
public class CategoriesProducts
{
    [Key]
    public int Id_category_product { get; set; }
    public string Name_category_product { get; set; }

    [ForeignKey("Id_company_Id")]
    public int Id_company_Id { get; set; }
    public bool Delete_log_category_product { get; set; } = false;

    public virtual Companies Companies { get; set; }

    public virtual ICollection<Inventories> Inventories { get; set; }
    public virtual ICollection<InventoryMovements> InventoryMovements { get; set; }
}
