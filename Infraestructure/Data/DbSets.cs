using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Domain.Models.PrincipalModels;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Infraestructure.Data
{
    // Esta es una clase parcial separa la inyeccion de los dbsets en la clase principal de ApplicationDbContext
    public partial class ApplicationDbContext
    {
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<ClasificationMovements> ClasificationsMovements { get; set; }
        public DbSet<MovementsTypes> MovementsTypes { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Modules> Modules { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<CategoriesProducts> CategoriesProducts { get; set; }
        public DbSet<CategoriesPurchaseOrders> CategoriesPurchaseOrders { get; set; }
        public DbSet<CategoriesSuppliers> CategoriesSuppliers { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<Inventories> Inventories { get; set; }
        public DbSet<PurchaseOrders> PurchaseOrders { get; set; }
        public DbSet<OrdersProducts> OrdersProducts { get; set; }
        public DbSet<Invoices> Invoices { get; set; }
        public DbSet<InventoryMovements> InventoryMovements { get; set; }
        public DbSet<Audits> Audits { get; set; }
        public DbSet<InvoicesProducts> InvoicesProducts { get; set; }
        public DbSet<SuppliersProducts> SuppliersProducts { get; set; }
        public DbSet<ErrorLogs> ErrorLogs { get; set; }
    }
}