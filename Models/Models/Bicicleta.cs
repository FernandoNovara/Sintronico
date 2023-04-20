using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintronico.Models;

public class Bicicleta
{  
    [Display(Name = "Cod. Bicicleta")]
    [Key]
    public int IdBicicleta {get;set;}

    [Display(Name = "Propietario")]
    public int IdPropietario {get;set;}

    [Display(Name = "Dueño")]
    [ForeignKey(nameof(IdPropietario))]
    public Propietario? Dueño {get;set;}

    public string Marca {get;set;}

    public string Color {get;set;}

    [Display(Name = "Num. de Serie")]
    public string NumeroSerie {get;set;}

    public string Tipo {get;set;}

    public string? Imagen {get;set;}
}