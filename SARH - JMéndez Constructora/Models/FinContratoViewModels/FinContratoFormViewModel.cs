using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SARH___JMéndez_Constructora.Models.FinContratoViewModels
{
    public class FinContratoFormViewModel
    {
        public int IdInicioContrato { get; set; }
        [Required (ErrorMessage = "Este espacio es requerido")]
        [Display (Name = "Se ha ejercido preaviso")]
        public int? PreavisoEjercido { get; set; }
        [Display (Name = "Dias Pendientes Preaviso")]
        public int? DiasPendientesPreaviso { get; set; }
        [Required (ErrorMessage = "Este espacio es requerido")]
        [Display (Name = "Motivo de Salida")]
        public int MotivoSalida { get; set; }
        [Display (Name = "Saldo de Vacaciones")]
        public int SaldoVacaciones { get; set; }
    }
}
