using System.ComponentModel.DataAnnotations;

namespace FluxSYS_backend.Application.ViewModels
{
    public class SupplierViewModel
    {
        [Required(ErrorMessage = "El nombre del proveedor es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres.")]
        public string Name_supplier { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Mail_supplier { get; set; }

        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de teléfono debe tener 10 dígitos.")]
        public long Phone_supplier { get; set; }

        [Required(ErrorMessage = "El ID de la categoría de proveedor es obligatorio.")]
        public int Id_category_supplier_Id { get; set; }

        [Required(ErrorMessage = "El ID de la compañía es obligatorio.")]
        public int Id_company_Id { get; set; }

        public List<SupplierProductViewModel> Products { get; set; }
    }

    public class SupplierProductViewModel
    {
        [Required(ErrorMessage = "El ID del producto de inventario es obligatorio.")]
        public int Id_inventory_product_Id { get; set; }

        [Required(ErrorMessage = "El precio sugerido es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio sugerido debe ser mayor que 0.")]
        public decimal Suggested_price { get; set; }
    }
}