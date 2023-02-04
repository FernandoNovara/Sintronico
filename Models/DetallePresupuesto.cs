using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintronico.Models;

public class DetallePresupuesto
{  
    [Display(Name = "Cod. Detalle Presupuesto")]
    [Key]
    public int IdDetalle {get;set;}

    [Display(Name = "Presupuesto")]
    public int IdPresupuesto {get;set;}

    [Display(Name = "Presupuesto")]
    [ForeignKey(nameof(IdPresupuesto))]
    public Presupuesto? Presupuesto {get;set;}

    [Display(Name = "Repuestos")]
    public int IdRepuesto {get;set;}

    [Display(Name = "Repuestos")]
    [ForeignKey(nameof(IdRepuesto))]
    public Repuestos? Repuestos {get;set;}

    public Double Total {get;set;}

    public Int32 Cantidad {get;set;}
}