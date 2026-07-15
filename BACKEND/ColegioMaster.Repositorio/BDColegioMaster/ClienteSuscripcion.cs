using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ColegioMaster.Repositorio.BDColegioMaster;

[Table("cliente_suscripcion")]
[Index("IdCliente", Name = "FK_ClienteSuscripcion_Cliente")]
[Index("IdEstado", Name = "FK_ClienteSuscripcion_EstadoSuscripcion")]
[Index("IdPlan", Name = "FK_ClienteSuscripcion_Plan")]
public partial class ClienteSuscripcion
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [Column("id_plan")]
    public int IdPlan { get; set; }

    [Column("fecha_inicio")]
    public DateOnly FechaInicio { get; set; }

    [Column("fecha_fin")]
    public DateOnly? FechaFin { get; set; }

    [Column("modalidad")]
    [StringLength(20)]
    public string Modalidad { get; set; } = null!;

    [Column("monto_pactado")]
    [Precision(12, 2)]
    public decimal MontoPactado { get; set; }

    [Column("id_estado")]
    public int IdEstado { get; set; }

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

    [ForeignKey("IdCliente")]
    [InverseProperty("ClienteSuscripcion")]
    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    [ForeignKey("IdEstado")]
    [InverseProperty("ClienteSuscripcion")]
    public virtual EstadoSuscripcion IdEstadoNavigation { get; set; } = null!;

    [ForeignKey("IdPlan")]
    [InverseProperty("ClienteSuscripcion")]
    public virtual Plan IdPlanNavigation { get; set; } = null!;
}
