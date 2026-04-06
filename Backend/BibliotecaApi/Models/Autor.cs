using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BibliotecaApi.Models;

public class Autor
{
    [Key]
    public int IdAutor { get; set; }

    [StringLength(50)]
    public string? Nombre { get; set; }

    [StringLength(100)]
    public string? Apellidos { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    [StringLength(50)]
    public string? Nacionalidad { get; set; }

    [JsonIgnore]
    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
