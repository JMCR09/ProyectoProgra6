using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaApi.Models;

public class Prestamo
{
    [Key]
    public int IdPrestamo { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdLibro { get; set; }

    public DateTime? FechaPrestamo { get; set; }

    public DateTime? FechaDevolucion { get; set; }

    [StringLength(20)]
    public string? Estado { get; set; }

    public string? Comentarios { get; set; }

    public DateTime? FechaDevolucionEfectiva { get; set; }

    [ForeignKey(nameof(IdUsuario))]
    public virtual Usuario? Usuario { get; set; }

    [ForeignKey(nameof(IdLibro))]
    public virtual Libro? Libro { get; set; }
}
