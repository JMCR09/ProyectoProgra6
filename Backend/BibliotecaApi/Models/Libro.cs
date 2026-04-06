using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BibliotecaApi.Models;

public class Libro
{
    [Key]
    public int IdLibro { get; set; }

    [StringLength(200)]
    public string? Titulo { get; set; }

    public int? IdAutor { get; set; }

    public int? NumeroPaginas { get; set; }

    public int? IdGenero { get; set; }

    public DateTime? FechaPublicacion { get; set; }

    [ForeignKey(nameof(IdAutor))]
    public virtual Autor? Autor { get; set; }

    [ForeignKey(nameof(IdGenero))]
    public virtual Genero? Genero { get; set; }

    [JsonIgnore]
    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
