using BibliotecaWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Servicios principales de Razor Pages
builder.Services.AddRazorPages();

// Registro de servicios HTTP para consumir la API del backend
builder.Services.AddHttpClient("API", client =>
{
    // URL base del backend configurada en appsettings.json
    client.BaseAddress = new Uri(builder.Configuration["ApiUrl"]!);
});

// Registro de servicios de la aplicacion
builder.Services.AddScoped<AutorService>();
builder.Services.AddScoped<GeneroService>();
builder.Services.AddScoped<LibroService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<PrestamoService>();

var app = builder.Build();

// Manejo de errores para ambientes no productivos
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Configuracion del pipeline HTTP
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Mapea las paginas Razor del proyecto
app.MapRazorPages();

app.Run();
