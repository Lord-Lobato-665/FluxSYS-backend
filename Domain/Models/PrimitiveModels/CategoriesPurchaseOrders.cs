using FluxSYS_backend.Domain.Models.PrincipalModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluxSYS_backend.Domain.Models.PrimitiveModels;

[Table("categories_purchase_orders")]
public class CategoriesPurchaseOrders
{
    [Key]
    public int Id_category_purchase_order {  get; set; }
    public string Name_category_purchase_order { get; set; }

    [ForeignKey("Id_company_Id")]
    public int Id_company_Id { get; set; }
    public bool Delete_log_category_purchase_order { get; set; } = false;

    public virtual Companies Companies { get; set; }

    public virtual ICollection<PurchaseOrders> PurchaseOrders { get; set; }
}
