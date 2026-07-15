using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ColegioMaster.Repositorio.BDColegioMaster;

[Table("usuario_plataforma")]
[Index("Correo", Name = "UQ_UsuarioPlataforma_Correo", IsUnique = true)]
public partial class UsuarioPlataforma
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("nombres")]
    [StringLength(120)]
    public string Nombres { get; set; } = null!;

    [Column("apellidos")]
    [StringLength(120)]
    public string Apellidos { get; set; } = null!;

    [Column("correo")]
    [StringLength(150)]
    public string Correo { get; set; } = null!;

    [Column("clave_cifrada")]
    [StringLength(500)]
    public string ClaveCifrada { get; set; } = null!;

    [Column("intentos_fallidos")]
    public int IntentosFallidos { get; set; }

    [Column("bloqueado_hasta", TypeName = "datetime")]
    public DateTime? BloqueadoHasta { get; set; }

    [Column("ultimo_acceso", TypeName = "datetime")]
    public DateTime? UltimoAcceso { get; set; }

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
}
