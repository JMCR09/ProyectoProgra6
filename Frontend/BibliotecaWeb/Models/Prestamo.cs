namespace BibliotecaWeb.Models;

public class Prestamo
{
    public int IdPrestamo { get; set; }
    public int? IdUsuario { get; set; }
    public int? IdLibro { get; set; }
    public DateTime? FechaPrestamo { get; set; }
    public DateTime? FechaDevolucion { get; set; }
    public string? Estado { get; set; }
    public string? Comentarios { get; set; }
    public DateTime? FechaDevolucionEfectiva { get; set; }
    public Usuario? Usuario { get; set; }
    public Libro? Libro { get; set; }
}
