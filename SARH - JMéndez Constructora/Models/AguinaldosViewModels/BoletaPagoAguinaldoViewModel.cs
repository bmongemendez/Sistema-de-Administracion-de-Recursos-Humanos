using System;
using System.ComponentModel.DataAnnotations;

namespace SARH___JMéndez_Constructora.Models.AguinaldosViewModels
{
    public class BoletaPagoAguinaldoViewModel
    {
        [Display (Name = "Boleta No.")]
        public int IdPago { get; set; }
        // 
        // Periodo Pago
        [Display (Name = "Inicio del Contrato")]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime FechaInicioContrato { get; set; }
        [Display (Name = "Fecha Actual")]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime FechaActual { get; set; }
        [Display (Name = "Días desde contrato")]
        public double DiasContrato { get; set; }
        [Display (Name = "Meses laborados")]
        public double MesesContrato { get; set; }
        //
        // Informacion Empleado
        [Display(Name = "Cedula: ")]
        public int Cedula { get; set; }
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [StringLength(50)]
        [Display(Name = "Primer Apellido")]
        public string Apellido1 { get; set; }
        [StringLength(50)]
        [Display(Name = "Segundo Apellido")]
        public string Apellido2 { get; set; }
        //
        // Puesto
        [Display(Name = "Puesto:")]
        public string NombrePuesto { get; set; }
        //
        // Desgloce Aguinaldo
        [Display(Name = "Sumatoria Salarios Brutos")]
        public decimal SumatoriaSalarioBrutos { get; set; }
        [Display(Name = "Monto Aguinaldo")]
        public decimal MontoAguinaldo { get; set; }
        [Display(Name = "Anotaciones")]
        [StringLength(128)]
        public string Anotaciones { get; set; }
    }
}