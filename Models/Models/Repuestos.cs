using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintronico.Models;

public class Repuestos
{  
    [Display(Name = "Cod. Repuestos")]
    [Key]
    public int IdRepuesto {get;set;}

    public string Nombre {get;set;}

    public string Tipo {get;set;}

    public Double Monto {get;set;}

    public string Detalle {get;set;}

    public string? Imagen {get;set;}

    [NotMapped]
    public IFormFile ImagenFile {get;set;}
}