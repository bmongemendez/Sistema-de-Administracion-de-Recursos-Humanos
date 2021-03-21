using System;
using System.ComponentModel.DataAnnotations;

namespace SARH___JMéndez_Constructora.Models.PagosViewModels
{
    public class BoletaPagoViewModel
    {
        [Display (Name = "Boleta No.")]
        public int IdPago { get; set; }
        // 
        // Periodo Pago
        [Display (Name = "Del:")]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime FechaInicio { get; set; }
        [Display (Name = "Al:")]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime FechaFin { get; set; }
        [Display (Name = "Total Días:")]
        public double TotalDias { get; set; }
        [Display (Name = "Fecha Actual:")]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime FechaHoy { get; set; }
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
        // Contrato
        [Display(Name = "Inicio de Labores")]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime InicioLabores { get; set; }
        [Display(Name = "Días desde contrato")]
        public double DiasContrato { get; set; }
        //
        // Desgloce Salario
        [Display (Name = "Horas Salario Normal")]
        public decimal HorasNormal { get; set; }
        [Display (Name = "Monto Salario Normal")]
        public decimal SalarioNormal { get; set; }
        [Display(Name = "Horas Extra")]
        public decimal HorasExtra { get; set; }
        [Display (Name = "Monto Salario Extras")]
        public decimal SalarioExtras { get; set; }
        [Display(Name = "Dia Descanso")]
        public decimal DiaDescanso { get; set; }
        [Display (Name = "Monto Dia Descanso")]
        public decimal SalarioDiaDescanso { get; set; }
        [Display(Name = "CCSS y Aporte Banco Popular")]
        public decimal Deducciones { get; set; }
        //
        //Cuentas por cobrar is missing
        //
        // Totales
        [Display(Name = "Salario Bruto")]
        public decimal SalarioBruto { get; set; }
        [Display(Name = "Salario Neto")]
        public decimal SalarioNeto { get; set; }
        [Display(Name = "Total Deducciones (Patrono)")]
        public decimal TotalDeduccionesPatrono { get; set; }
        [Display(Name = "Observaciones")]
        [StringLength(128)]
        public string Observaciones { get; set; }
    }
}