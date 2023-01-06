using System.ComponentModel.DataAnnotations;

namespace Sintronico.Models;

public class Pagos
{  
    [Display(Name = "Cod. Pagos")]
    [Key]
    public int IdPagos {get;set;}

    public int IdPresupuesto {get;set;}

    public Presupuesto Presupuesto {get;set;}

    public string Monto {get;set;}

    public string FechaEmision {get;set;}
}