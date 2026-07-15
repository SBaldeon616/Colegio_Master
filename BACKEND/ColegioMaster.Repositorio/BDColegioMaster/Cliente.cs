using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ColegioMaster.Repositorio.BDColegioMaster;

[Table("cliente")]
[Index("IdEstado", Name = "FK_Cliente_EstadoCliente")]
[Index("BdNombre", Name = "UQ_Cliente_BdNombre", IsUnique = true)]
[Index("Codigo", Name = "UQ_Cliente_Codigo", IsUnique = true)]
[Index("Ruc", Name = "UQ_Cliente_Ruc", IsUnique = true)]
public partial class Cliente
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("ruc")]
    [StringLength(11)]
    public string Ruc { get; set; } = null!;

    [Column("codigo")]
    [StringLength(30)]
    public string Codigo { get; set; } = null!;

    [Column("razon_social")]
    [StringLength(200)]
    public string RazonSocial { get; set; } = null!;

    [Column("nombre_comercial")]
    [StringLength(200)]
    public string NombreComercial { get; set; } = null!;

    [Column("direccion")]
    [StringLength(250)]
    public string? Direccion { get; set; }

    [Column("telefono")]
    [StringLength(30)]
    public string? Telefono { get; set; }

    [Column("correo_contacto")]
    [StringLength(120)]
    public string? CorreoContacto { get; set; }

    [Column("servidor_sql")]
    [StringLength(120)]
    public string ServidorSql { get; set; } = null!;

    [Column("bd_nombre")]
    [StringLength(120)]
    public string BdNombre { get; set; } = null!;

    [Column("bd_usuario")]
    [StringLength(120)]
    public string? BdUsuario { get; set; }

    [Column("bd_password_cifrada")]
    [StringLength(400)]
    public string? BdPasswordCifrada { get; set; }

    [Column("id_estado")]
    public int IdEstado { get; set; }

    [Column("fecha_activacion")]
    public DateOnly? FechaActivacion { get; set; }

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

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<ClienteSuscripcion> ClienteSuscripcion { get; set; } = new List<ClienteSuscripcion>();

    [ForeignKey("IdEstado")]
    [InverseProperty("Cliente")]
    public virtual EstadoCliente IdEstadoNavigation { get; set; } = null!;
}
