using System.ComponentModel.DataAnnotations;

namespace Sintronico.Models;

public class Propietario
{  
    [Display(Name = "Cod. Propietario")]
    [Key]
    public int IdPropietario {get;set;}

    public string Nombre {get;set;}

    public string Apellido {get;set;}

    public string Dni {get;set;}

    public string Telefono {get;set;}

    public string Direccion {get;set;}

    public string Email {get;set;}
    
    public string Avatar {get;set;}

    public string Clave {get;set;}
}