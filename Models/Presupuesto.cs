using System.ComponentModel.DataAnnotations;

namespace Sintronico.Models;

public class Presupuesto
{  
    [Display(Name = "Cod. Presupuesto")]
    [Key]
    public int IdPresupuesto {get;set;}

    public int IdBicicleta {get;set;}

    public Propietario Bicicleta {get;set;}

    public int IdUsuario {get;set;}

    public Propietario Usuario {get;set;}

    public string FechaInicio {get;set;}

    public string FechaEntrega {get;set;}

    public string Monto {get;set;}

    public string Estado {get;set;}
}