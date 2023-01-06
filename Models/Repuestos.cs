using System.ComponentModel.DataAnnotations;

namespace Sintronico.Models;

public class Repuestos
{  
    [Display(Name = "Cod. Repuestos")]
    [Key]
    public int IdRepuestos {get;set;}

    public string Nombre {get;set;}

    public string Tipo {get;set;}

    public string Monto {get;set;}

    public string Detalle {get;set;}

    public string Imagen {get;set;}
}