using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FluxSYS-backend", Version = "v1" });
    c.DocumentFilter<SwaggerIgnoreFilter>(); // Agregar el llamado del filtro personalizado
});

var app = builder.Build();

// Configuración del pipeline de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirigir a Swagger si acceden a la raíz
app.MapGet("/", () => Results.Redirect("/swagger"));

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