using System.ComponentModel.DataAnnotations;

namespace FluxSYS_backend.Application.ViewModels
{
    public class InventoryViewModel
    {
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        public string Name_product { get; set; }

        [Required(ErrorMessage = "El stock del producto es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0.")]
        public int Stock_product { get; set; }

        [Required(ErrorMessage = "El precio del producto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
        public decimal Price_product { get; set; }

        [Required(ErrorMessage = "El ID de la categoría del producto es obligatorio.")]
        public int Id_category_product_Id { get; set; }

        [Required(ErrorMessage = "El ID del estado es obligatorio.")]
        public int Id_state_Id { get; set; }

        [Required(ErrorMessage = "El ID del tipo de movimiento es obligatorio.")]
        public int Id_movement_type_Id { get; set; }

        [Required(ErrorMessage = "El ID del proveedor es obligatorio.")]
        public int Id_supplier_Id { get; set; }

        [Required(ErrorMessage = "El ID del departamento es obligatorio.")]
        public int Id_department_Id { get; set; }

        [Required(ErrorMessage = "El ID del módulo es obligatorio.")]
        public int Id_module_Id { get; set; }

        [Required(ErrorMessage = "El ID de la compañía es obligatorio.")]
        public int Id_company_Id { get; set; }

        [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
        public int Id_user_Id { get; set; }
    }
}