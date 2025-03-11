using System.ComponentModel.DataAnnotations;

namespace FluxSYS_backend.Application.ViewModels
{
    public class MovementTypeViewModel
    {
        [Required(ErrorMessage = "El nombre del tipo de movimiento es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre del tipo de movimiento solo puede contener letras y espacios.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres.")]
        public string Name_movement_type { get; set; }

        [Required(ErrorMessage = "El ID de la compañía es obligatorio.")]
        public int Id_company_Id { get; set; }

        [Required(ErrorMessage = "El ID de la clasificación de movimiento es obligatorio.")]
        public int Id_clasification_movement_Id { get; set; }
    }
}