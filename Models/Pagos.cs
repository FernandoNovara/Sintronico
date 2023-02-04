using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintronico.Models;

public class Pago
{  
    [Display(Name = "Cod. Pagos")]
    [Key]
    public int IdPago {get;set;}

    [Display(Name = "Presupuesto")]
    public int IdPresupuesto {get;set;}

    [Display(Name = "Presupuesto")]
    [ForeignKey(nameof(IdPresupuesto))]
    public Presupuesto? Presupuesto {get;set;}

    public Double Monto {get;set;}

    [Display(Name = "Fecha de Emision")]
    public DateTime FechaEmision {get;set;}
}