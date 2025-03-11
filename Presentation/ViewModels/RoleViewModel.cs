using System.ComponentModel.DataAnnotations;

namespace FluxSYS_backend.Application.ViewModels
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre del rol solo puede contener letras y espacios.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres.")]
        public string Name_role { get; set; }
    }
}