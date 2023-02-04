using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintronico.Models;

public class Presupuesto
{  
    [Display(Name = "Cod. Presupuesto")]
    [Key]
    public int IdPresupuesto {get;set;}

    [Display(Name = "Bicicleta")]
    public int IdBicicleta {get;set;}

    [Display(Name = "Bicicleta")]
    [ForeignKey(nameof(IdBicicleta))]
    public Bicicleta? Bicicleta {get;set;}

    [Display(Name = "Usuario")]
    public int IdUsuario {get;set;}

    [Display(Name = "Usuario")]
    [ForeignKey(nameof(IdUsuario))]
    public Usuario? Usuario {get;set;}

    [Display(Name = "Fecha de Inicio")]
    public DateTime FechaInicio {get;set;}

    [Display(Name = "Fecha de Entrega")]
    public DateTime FechaEntrega {get;set;}

    public Double Monto {get;set;}

    public string Estado {get;set;}
}