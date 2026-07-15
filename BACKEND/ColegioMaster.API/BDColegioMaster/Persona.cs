using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ColegioMaster.API.BDColegioMaster;

[Table("persona")]
public partial class Persona
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("tipo_documento")]
    [StringLength(50)]
    public string? TipoDocumento { get; set; }

    [Column("numero_documento")]
    [StringLength(50)]
    public string? NumeroDocumento { get; set; }

    [Column("nombres")]
    [StringLength(50)]
    public string? Nombres { get; set; }

    [Column("apellido_paterno")]
    [StringLength(50)]
    public string? ApellidoPaterno { get; set; }

    [Column("apellido_materno")]
    [StringLength(50)]
    public string? ApellidoMaterno { get; set; }

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
}
