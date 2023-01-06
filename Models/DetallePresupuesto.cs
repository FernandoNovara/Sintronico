using System.ComponentModel.DataAnnotations;

namespace Sintronico.Models;

public class DetallePresupuesto
{  
    [Display(Name = "Cod. Detalle Presupuesto")]
    [Key]
    public int IdDetallePresupuesto {get;set;}

    public int IdPresupuesto {get;set;}

    public Presupuesto Presupuesto {get;set;}

    public int IdRepuestos {get;set;}

    public Repuestos Repuestos {get;set;}

    public string Nombre {get;set;}

    public string Apellido {get;set;}

    public string Dni {get;set;}

    public string Telefono {get;set;}

    public string Direccion {get;set;}

    public string Email {get;set;}
    
    public string Avatar {get;set;}

    public string Clave {get;set;}
}