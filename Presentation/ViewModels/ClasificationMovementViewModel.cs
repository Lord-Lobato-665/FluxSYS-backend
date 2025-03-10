using System.ComponentModel.DataAnnotations;

namespace FluxSYS_backend.Application.ViewModels
{
    public class ClasificationMovementViewModel
    {
        [Required(ErrorMessage = "El nombre de la clasificación de movimiento es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre de la compañía solo puede contener letras y espacios.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        public string Name_clasification_movement { get; set; }
    }
}
