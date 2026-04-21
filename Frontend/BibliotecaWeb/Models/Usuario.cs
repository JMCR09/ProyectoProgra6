/*
 * Creado por: Jose Miguel Salas Chacon
 * Usuarios.cs - se encarga de representar la entidad de usuario en el sistema, con sus propiedades y validaciones correspondientes.
 * 
 */



using System.ComponentModel.DataAnnotations;

namespace BibliotecaWeb.Models;

public class Usuario
{
    public int IdUsuario { get; set; }

    [StringLength(20)]
    [RegularExpression("^[0-9]+$", ErrorMessage = "La cedula solo debe contener numeros.")]
    public string? Cedula { get; set; }

    [StringLength(50)]
    [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "El nombre solo debe contener letras.")]
    public string? Nombre { get; set; }

    [StringLength(100)]
    [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$", ErrorMessage = "Los apellidos solo deben contener letras.")]
    public string? Apellidos { get; set; }

    [StringLength(20)]
    [RegularExpression("^[0-9]+$", ErrorMessage = "El telefono solo debe contener numeros")]
    public string? Telefono { get; set; }

    [StringLength(200)]
    public string? Direccion { get; set; }

    public DateTime? FechaNacimiento { get; set; }
}
