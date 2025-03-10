using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluxSYS_backend.Domain.Models.PrimitiveModels;

[Table("categories_suppliers")]
public class CategoriesSuppliers
{
    [Key]
    public int Id_category_supplier {  get; set; }
    public string Name_category_supplier { get; set; }

    [ForeignKey("Id_company_Id")]
    public int Id_company_Id { get; set; }
    public bool Delete_log_category_supplier { get; set; } = false;

    public virtual Companies Companies { get; set; }

    public virtual ICollection<Suppliers> Suppliers { get; set; }
}
