using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FluxSYS_backend.Application.ViewModels
{
    public class PurchaseOrderViewModel
    {
        [Required(ErrorMessage = "El nombre de la orden de compra es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        public string Name_purchase_order { get; set; }

        [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
        public int Id_user_Id { get; set; }

        [Required(ErrorMessage = "El ID de la categoría de orden de compra es obligatorio.")]
        public int Id_category_purchase_order_Id { get; set; }

        [Required(ErrorMessage = "El ID del departamento es obligatorio.")]
        public int Id_department_Id { get; set; }

        [Required(ErrorMessage = "El ID del proveedor es obligatorio.")]
        public int Id_supplier_Id { get; set; }

        [Required(ErrorMessage = "El ID del estado es obligatorio.")]
        public int Id_state_Id { get; set; }

        [Required(ErrorMessage = "El ID del tipo de movimiento es obligatorio.")]
        public int Id_movement_type_Id { get; set; }

        [Required(ErrorMessage = "El ID de la compañía es obligatorio.")]
        public int Id_company_Id { get; set; }

        public List<OrderProductViewModel> Products { get; set; }
    }

    public class OrderProductViewModel
    {
        [Required(ErrorMessage = "El ID del producto de inventario es obligatorio.")]
        public int Id_inventory_product_Id { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0.")]
        public int Quantity { get; set; }
    }
}