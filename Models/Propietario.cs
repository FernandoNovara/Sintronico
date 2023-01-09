using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    
    [NotMapped]
    public string Avatar {get;set;}

    public string Clave {get;set;}
}