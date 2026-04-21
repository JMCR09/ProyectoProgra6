/*
 * Creado por: Jose Miguel Salas Chacon
 *
 * Servicio para gestionar operaciones CRUD de usuarios
 */



using BibliotecaWeb.Models;
using System.Net.Http.Json;

namespace BibliotecaWeb.Services;

public class UsuarioService
{
    // Cliente HTTP para consumir el backend
    private readonly HttpClient _httpClient;

    public UsuarioService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("API");
    }

    // =========================
    // CRUD (CREATE - READ - UPDATE - DELETE)
    // =========================

    // Metodo para obtener todos los usuarios desde la API
    public async Task<List<Usuario>> GetAllAsync()
        => await _httpClient.GetFromJsonAsync<List<Usuario>>("/Usuarios") ?? [];

    // Metodo para obtener un usuario por Id
    public async Task<Usuario?> GetAsync(int id)
        => await _httpClient.GetFromJsonAsync<Usuario>($"/Usuarios/{id}");

    // Metodo para crear un nuevo usuario
    public async Task AddAsync(Usuario usuario)
    {
        var response = await _httpClient.PostAsJsonAsync("/Usuarios", usuario);
        response.EnsureSuccessStatusCode();
    }

    // Metodo para actualizar un usuario existente
    public async Task UpdateAsync(Usuario usuario)
    {
        var response = await _httpClient.PutAsJsonAsync($"/Usuarios/{usuario.IdUsuario}", usuario);
        response.EnsureSuccessStatusCode();
    }

    // Metodo para eliminar un usuario por Id
    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"/Usuarios/{id}");
        response.EnsureSuccessStatusCode();
    }
}
