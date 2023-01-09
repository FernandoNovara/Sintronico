using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintronico.Models;

public class DetallePresupuesto
{  
    [Display(Name = "Cod. Detalle Presupuesto")]
    [Key]
    public int IdDetalle {get;set;}

    [NotMapped]
    public int IdPresupuesto {get;set;}

    [NotMapped]
    public Presupuesto Presupuesto {get;set;}

    [NotMapped]
    public int IdRepuesto {get;set;}

    [NotMapped]
    public Repuestos Repuestos {get;set;}

    public Double Total {get;set;}

    public Int32 Cantidad {get;set;}
}