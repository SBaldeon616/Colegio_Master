using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ColegioMaster.Repositorio.BDColegioMaster;

[Table("estado_cliente")]
[Index("Codigo", Name = "UQ_EstadoCliente_Codigo", IsUnique = true)]
public partial class EstadoCliente
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("codigo")]
    [StringLength(30)]
    public string Codigo { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(100)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("IdEstadoNavigation")]
    public virtual ICollection<Cliente> Cliente { get; set; } = new List<Cliente>();
}
