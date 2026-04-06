using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BibliotecaApi.Models;

public class Genero
{
    [Key]
    public int IdGenero { get; set; }

    [StringLength(100)]
    public string? Nombre { get; set; }

    [JsonIgnore]
    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
