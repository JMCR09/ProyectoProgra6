namespace BibliotecaWeb.Models;

public class Autor
{
    public int IdAutor { get; set; }
    public string? Nombre { get; set; }
    public string? Apellidos { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string? Nacionalidad { get; set; }
}
