using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SARH___JMéndez_Constructora.ViewModels
{
    public class Edcn_n_LicncFormViewModel
    {
        public int IdEmpleado { get; set; }
        [Display(Name = "Grado Bachiller")]
        public bool TieneBachiller { get; set; }
        [Display(Name = "Grado Licenciatura")]
        public bool TieneLicenciatura { get; set; }
        [Display(Name = "Grado Tecnico")]
        public bool TieneTecnico { get; set; }
        [Display(Name = "Licencia A3")]
        public bool TieneLicenciaA3 { get; set; }
        [Display(Name = "Licencia B1")]
        public bool TieneLicenciaB1 { get; set; }
        [Display(Name ="Licencia B2")]
        public bool TieneLicenciaB2 { get; set; }
        [Display(Name ="Licencia B3")]
        public bool TieneLicenciaB3 { get; set; }
        [Display(Name ="Licencia D")]
        public bool TieneLicenciaD { get; set; }
        [Display(Name ="Licencia E")]
        public bool TieneLicenciaE { get; set; }
    }
}