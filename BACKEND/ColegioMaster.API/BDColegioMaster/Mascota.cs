using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ColegioMaster.API.BDColegioMaster;

[Table("mascota")]
public partial class Mascota
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("categoria_mascota")]
    [StringLength(50)]
    public string? CategoriaMascota { get; set; }

    [Column("raza")]
    [StringLength(50)]
    public string? Raza { get; set; }

    [Column("edad")]
    public int? Edad { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string? Nombre { get; set; }

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
