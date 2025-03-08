using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Infraestructure.Data
{
    // Convertimos el DbContext en una clase parcial para dividirla en partes manejables
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplicar configuraciones de los modelos
            ConfigureEntities(modelBuilder);

            // Aplicar semillas para los modelos
            SeedData(modelBuilder);
        }
    }
}
