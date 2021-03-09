using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SARH___JMéndez_Constructora.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El campo de 'Usuario' es requerido")]
        [Display(Name = "Nombre de Usuario**")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Rol del Usuario")]
        public string Role { get; set; }

        [Required(ErrorMessage = "El campo de 'Contraseña' es requerido")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} de extension.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña**")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme la contraseña**")]
        [Compare("Password", ErrorMessage = "Las contraseñas deben coincidir.")]
        public string ConfirmPassword { get; set; }
    }
}
