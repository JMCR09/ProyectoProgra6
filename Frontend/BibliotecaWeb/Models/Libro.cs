namespace BibliotecaWeb.Models;

public class Libro
{
    public int IdLibro { get; set; }
    public string? Titulo { get; set; }
    public int? IdAutor { get; set; }
    public int? NumeroPaginas { get; set; }
    public int? IdGenero { get; set; }
    public DateTime? FechaPublicacion { get; set; }
    public Autor? Autor { get; set; }
    public Genero? Genero { get; set; }
}
