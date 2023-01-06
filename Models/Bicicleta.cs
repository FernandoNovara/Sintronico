using System.ComponentModel.DataAnnotations;

namespace Sintronico.Models;

public class Bicicleta
{  
    [Display(Name = "Cod. Bicicleta")]
    [Key]
    public int IdBicicleta {get;set;}

    public int IdPropietario {get;set;}

    public Propietario Dueño {get;set;}

    public string Marca {get;set;}

    public string Color {get;set;}

    public string NumeroSerie {get;set;}

    public string Tipo {get;set;}

    public string Imagen {get;set;}
}