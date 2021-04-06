using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SARH___JMéndez_Constructora.Models.TableroViewModels
{
    public class AuditRecentViewModel
    {
        [Display(Name = "Reg No.")]
        [StringLength(255)]
        public int IdTabla { get; set; }
        [StringLength(255)]
        [Display(Name = "Tabla")]
        public string Tabla { get; set; }
        [StringLength(255)]
        [Display(Name = "Dato")]
        public string Columna { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Hecho el")]
        public DateTime FechaHoraTransaccion { get; set; }
        [StringLength(255)]
        [Display(Name = "Valor Anterior")]
        public string ValorAnterior { get; set; }
        [StringLength(255)]
        [Display(Name = "Valor Nuevo")]
        public string ValorNuevo { get; set; }
        [StringLength(256)]
        [Display(Name = "Hecho por")]
        public string UserName { get; set; }
    }
}