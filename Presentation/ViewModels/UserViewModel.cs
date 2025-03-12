using System.ComponentModel.DataAnnotations;

namespace FluxSYS_backend.Application.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "El nombre del usuario es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres.")]
        public string Name_user { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Mail_user { get; set; }

        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de teléfono debe tener 10 dígitos.")]
        public long Phone_user { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 50 caracteres.")]
        public string Password_user { get; set; }

        [Required(ErrorMessage = "El ID del rol es obligatorio.")]
        public int Id_rol_Id { get; set; }

        [Required(ErrorMessage = "El ID de la posición es obligatorio.")]
        public int Id_position_Id { get; set; }

        [Required(ErrorMessage = "El ID del departamento es obligatorio.")]
        public int Id_department_Id { get; set; }

        [Required(ErrorMessage = "El ID de la compañía es obligatorio.")]
        public int Id_company_Id { get; set; }
    }
}