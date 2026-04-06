using BibliotecaWeb.Models;
using System.Net.Http.Json;

namespace BibliotecaWeb.Services;

public class LibroService
{
    // Cliente HTTP para consumir el backend
    private readonly HttpClient _httpClient;

    public LibroService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("API");
    }

    // =========================
    // CRUD (CREATE - READ - UPDATE - DELETE)
    // =========================

    // Metodo para obtener todos los libros desde la API
    public async Task<List<Libro>> GetAllAsync()
        => await _httpClient.GetFromJsonAsync<List<Libro>>("/Libros") ?? [];

    // Metodo para obtener un libro por Id
    public async Task<Libro?> GetAsync(int id)
        => await _httpClient.GetFromJsonAsync<Libro>($"/Libros/{id}");

    // Metodo para crear un nuevo libro
    public async Task AddAsync(Libro libro)
        => await _httpClient.PostAsJsonAsync("/Libros", libro);

    // Metodo para actualizar un libro existente
    public async Task UpdateAsync(Libro libro)
        => await _httpClient.PutAsJsonAsync($"/Libros/{libro.IdLibro}", libro);

    // Metodo para eliminar un libro por Id
    public async Task DeleteAsync(int id)
        => await _httpClient.DeleteAsync($"/Libros/{id}");
}
