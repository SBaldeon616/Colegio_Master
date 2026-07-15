using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ColegioMaster.Repositorio.BDColegioMaster;

[Table("plan")]
[Index("Codigo", Name = "UQ_Plan_Codigo", IsUnique = true)]
public partial class Plan
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("codigo")]
    [StringLength(30)]
    public string Codigo { get; set; } = null!;

    [Column("nombre")]
    [StringLength(120)]
    public string Nombre { get; set; } = null!;

    [Column("precio_mensual")]
    [Precision(12, 2)]
    public decimal PrecioMensual { get; set; }

    [Column("precio_anual")]
    [Precision(12, 2)]
    public decimal PrecioAnual { get; set; }

    [Column("max_estudiante")]
    public int? MaxEstudiante { get; set; }

    [Column("max_usuario")]
    public int? MaxUsuario { get; set; }

    [Required]
    [Column("estado")]
    public bool? Estado { get; set; }

    [Column("fecha_creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [Column("fecha_modificacion", TypeName = "datetime")]
    public DateTime? FechaModificacion { get; set; }

    [Column("usuario_creacion")]
    [StringLength(100)]
    public string UsuarioCreacion { get; set; } = null!;

    [Column("usuario_modificacion")]
    [StringLength(100)]
    public string? UsuarioModificacion { get; set; }

    [InverseProperty("IdPlanNavigation")]
    public virtual ICollection<ClienteSuscripcion> ClienteSuscripcion { get; set; } = new List<ClienteSuscripcion>();
}
