using System.ComponentModel.DataAnnotations;

namespace Sintronico.Models;

public class Pago
{  
    [Display(Name = "Cod. Pagos")]
    [Key]
    public int IdPago {get;set;}

    public int IdPresupuesto {get;set;}

    public Presupuesto Presupuesto {get;set;}

    public Double Monto {get;set;}

    public DateTime FechaEmision {get;set;}
}