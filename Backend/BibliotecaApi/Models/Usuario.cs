/*
 * Creado por: Jose Miguel Salas Chacon
 * Usuarios.cs - Modelo de datos para la entidad Usuario
 * 
 */



using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BibliotecaApi.Models;

public class Usuario
{
    [Key]
    public int IdUsuario { get; set; }

    [StringLength(20)]
    public string? Cedula { get; set; }

    [StringLength(50)]
    public string? Nombre { get; set; }

    [StringLength(100)]
    public string? Apellidos { get; set; }

    [StringLength(20)]
    public string? Telefono { get; set; }

    [StringLength(200)]
    public string? Direccion { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    [JsonIgnore]
    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
