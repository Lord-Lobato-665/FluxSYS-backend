using FluxSYS_backend.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers();

// Inyecci�n del ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserApiDb")));

// Llamar al m�todo de extensi�n para registrar dependencias
builder.Services.AddApplicationServices();

// Configuraci�n de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Reemplaza con el dominio de tu frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Inyecci�n de servicios de swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FluxSYS-backend", Version = "v1" });
    c.DocumentFilter<SwaggerIgnoreFilter>(); // Agregar el llamado del filtro personalizado
});

var app = builder.Build();

// Configuraci�n del pipeline de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirigir a Swagger si acceden a la ra�z
app.MapGet("/", () => Results.Redirect("/swagger"));

// Aplicar CORS
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Filtro personalizado para ignorar el endpoint GET / (IGNORAR TODO ESTO)
public class SwaggerIgnoreFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var pathToRemove = swaggerDoc.Paths.FirstOrDefault(p => p.Key == "/");
        if (pathToRemove.Key != null)
        {
            swaggerDoc.Paths.Remove(pathToRemove.Key);
        }
    }
}
