using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SARH___JMéndez_Constructora.Models
{
    public class SetPasswordViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "El campo de 'Contraseña' es requerido")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} de extension.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña**")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme la contraseña**")]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas deben coincidir.")]
        public string ConfirmPassword { get; set; }
    }
}
