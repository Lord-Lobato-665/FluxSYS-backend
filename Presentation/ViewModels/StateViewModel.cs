using System.ComponentModel.DataAnnotations;

namespace FluxSYS_backend.Application.ViewModels
{
    public class StateViewModel
    {
        [Required(ErrorMessage = "El nombre del estado es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre del estado solo puede contener letras y espacios.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres.")]
        public string Name_state { get; set; }

        [Required(ErrorMessage = "El ID de la compañía es obligatorio.")]
        public int Id_company_Id { get; set; }
    }
}