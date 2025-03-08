using FluxSYS_backend.Domain.Models.PrimitiveModels;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Infraestructure.Data
{
    public partial class ApplicationDbContext
    {
        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Companies>().HasData(
            new Companies { Id_company = 1, Name_company = "Tech Innovators" },
            new Companies { Id_company = 2, Name_company = "Global Solutions" },
            new Companies { Id_company = 3, Name_company = "NextGen Systems" },
            new Companies { Id_company = 4, Name_company = "Future Enterprises" },
            new Companies { Id_company = 5, Name_company = "Pioneer Tech" },
            new Companies { Id_company = 6, Name_company = "Visionary Corp" },
            new Companies { Id_company = 7, Name_company = "Nexus Industries" },
            new Companies { Id_company = 8, Name_company = "Elite Technologies" },
            new Companies { Id_company = 9, Name_company = "Synergy Networks" },
            new Companies { Id_company = 10, Name_company = "Vertex Solutions" }
            );

        }
    }
}
