using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Domain.Models.PrincipalModels;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Infraestructure.Data
{
    public partial class ApplicationDbContext
    {
        private void ConfigureEntities(ModelBuilder modelBuilder)
        {
            // Configuracion de eliminaciones en cascada del modelo de Suppliers
            modelBuilder.Entity<Suppliers>()
                .HasOne(s => s.CategoriesSuppliers)
                .WithMany()
                .HasForeignKey(s => s.Id_category_supplier_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Suppliers>()
                .HasOne(s => s.Modules)
                .WithMany()
                .HasForeignKey(s => s.Id_module_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Suppliers>()
                .HasOne(s => s.Companies)
                .WithMany()
                .HasForeignKey(s => s.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);


            // Configuracion de elimaciones en cascada del modelo Users
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Companies)
                .WithMany()
                .HasForeignKey(u => u.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Departments)
                .WithMany()
                .HasForeignKey(u => u.Id_department_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Modules)
                .WithMany()
                .HasForeignKey(u => u.Id_module_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Positions)
                .WithMany()
                .HasForeignKey(u => u.Id_position_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Roles)
                .WithMany()
                .HasForeignKey(u => u.Id_rol_Id)
                .OnDelete(DeleteBehavior.NoAction);


            // Configuracion de elimaciones en cascada del modelo Audits
            modelBuilder.Entity<Audits>()
                .HasOne(a => a.Companies)
                .WithMany()
                .HasForeignKey(a => a.Id_company_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Audits>()
                .HasOne(a => a.Departments)
                .WithMany()
                .HasForeignKey(a => a.Id_department_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Audits>()
                .HasOne(a => a.Modules)
                .WithMany()
                .HasForeignKey(a => a.Id_module_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Audits>()
                .HasOne(a => a.Users)
                .WithMany()
                .HasForeignKey(a => a.Id_user_Id)
                .OnDelete(DeleteBehavior.NoAction);


            // Configuracion de elimaciones en cascada del modelo Inventories
            modelBuilder.Entity<Inventories>()
                .HasOne(a => a.CategoriesProducts)
                .WithMany()
                .HasForeignKey(a => a.Id_category_product_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inventories>()
                .HasOne(a => a.States)
                .WithMany()
                .HasForeignKey(a => a.Id_state_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inventories>()
                .HasOne(a => a.MovementsTypes)
                .WithMany()
                .HasForeignKey(a => a.Id_movement_type_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inventories>()
                .HasOne(a => a.States)
                .WithMany()
                .HasForeignKey(a => a.Id_state_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inventories>()
                .HasOne(a => a.Suppliers)
                .WithMany()
                .HasForeignKey(a => a.Id_supplier_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inventories>()
                .HasOne(a => a.Departments)
                .WithMany()
                .HasForeignKey(a => a.Id_department_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inventories>()
                .HasOne(a => a.Modules)
                .WithMany()
                .HasForeignKey(a => a.Id_module_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inventories>()
                .HasOne(a => a.Companies)
                .WithMany()
                .HasForeignKey(a => a.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Inventories>()
                .HasOne(a => a.Users)
                .WithMany()
                .HasForeignKey(a => a.Id_user_Id)
                .OnDelete(DeleteBehavior.NoAction);


            // Configuracion de elimaciones en cascada del modelo PurchaseOrders
            modelBuilder.Entity<PurchaseOrders>()
                .HasOne(a => a.Users)
                .WithMany()
                .HasForeignKey(a => a.Id_user_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseOrders>()
                .HasOne(a => a.CategoryPurchaseOrders)
                .WithMany()
                .HasForeignKey(a => a.Id_category_purchase_order_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseOrders>()
                .HasOne(a => a.Departments)
                .WithMany()
                .HasForeignKey(a => a.Id_department_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseOrders>()
                .HasOne(a => a.Suppliers)
                .WithMany()
                .HasForeignKey(a => a.Id_supplier_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseOrders>()
                .HasOne(a => a.States)
                .WithMany()
                .HasForeignKey(a => a.Id_state_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseOrders>()
                .HasOne(a => a.MovementsTypes)
                .WithMany()
                .HasForeignKey(a => a.Id_movement_type_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseOrders>()
                .HasOne(a => a.Modules)
                .WithMany()
                .HasForeignKey(a => a.Id_module_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseOrders>()
                .HasOne(a => a.Companies)
                .WithMany()
                .HasForeignKey(a => a.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);


            // Configuracion de elimaciones en cascada del modelo InventoryMovements
            modelBuilder.Entity<InventoryMovements>()
                .HasOne(a => a.Inventories)
                .WithMany()
                .HasForeignKey(a => a.Id_inventory_product_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InventoryMovements>()
                .HasOne(a => a.CategoriesProducts)
                .WithMany()
                .HasForeignKey(a => a.Id_category_product_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InventoryMovements>()
                .HasOne(a => a.Departments)
                .WithMany()
                .HasForeignKey(a => a.Id_department_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InventoryMovements>()
                .HasOne(a => a.Suppliers)
                .WithMany()
                .HasForeignKey(a => a.Id_supplier_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InventoryMovements>()
                .HasOne(a => a.MovementsTypes)
                .WithMany()
                .HasForeignKey(a => a.Id_movements_types_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InventoryMovements>()
                .HasOne(a => a.Modules)
                .WithMany()
                .HasForeignKey(a => a.Id_module_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InventoryMovements>()
                .HasOne(a => a.Companies)
                .WithMany()
                .HasForeignKey(a => a.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InventoryMovements>()
                .HasOne(a => a.Users)
                .WithMany()
                .HasForeignKey(a => a.Id_user_Id)
                .OnDelete(DeleteBehavior.NoAction);


            // Configuracion de elimaciones en cascada del modelo SuppliersProducts
            modelBuilder.Entity<SuppliersProducts>()
                .HasOne(a => a.Inventories)
                .WithMany()
                .HasForeignKey(a => a.Id_inventory_product_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SuppliersProducts>()
                .HasOne(a => a.Suppliers)
                .WithMany()
                .HasForeignKey(a => a.Id_supplier_Id)
                .OnDelete(DeleteBehavior.NoAction);


            // Configuracion de elimaciones en cascada del modelo Invoices
            modelBuilder.Entity<Invoices>()
                .HasOne(a => a.PurchaseOrders)
                .WithMany()
                .HasForeignKey(a => a.Id_purchase_order_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Invoices>()
                .HasOne(a => a.Suppliers)
                .WithMany()
                .HasForeignKey(a => a.Id_supplier_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Invoices>()
                .HasOne(a => a.Departments)
                .WithMany()
                .HasForeignKey(a => a.Id_department_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Invoices>()
                .HasOne(a => a.Modules)
                .WithMany()
                .HasForeignKey(a => a.Id_module_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Invoices>()
                .HasOne(a => a.Companies)
                .WithMany()
                .HasForeignKey(a => a.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);


            // Configuracion de elimaciones en cascada del modelo OrdersProducts
            modelBuilder.Entity<OrdersProducts>()
                .HasOne(a => a.PurchaseOrders)
                .WithMany()
                .HasForeignKey(a => a.Id_purchase_order_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrdersProducts>()
                .HasOne(a => a.Inventories)
                .WithMany()
                .HasForeignKey(a => a.Id_inventory_product_Id)
                .OnDelete(DeleteBehavior.Cascade);


            // Configuracion de elimaciones en cascada del modelo InvoicesProducts
            modelBuilder.Entity<InvoicesProducts>()
                .HasOne(a => a.Invoices)
                .WithMany()
                .HasForeignKey(a => a.Id_invoice_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InvoicesProducts>()
                .HasOne(a => a.Inventories)
                .WithMany()
                .HasForeignKey(a => a.Id_inventory_product_Id)
                .OnDelete(DeleteBehavior.Cascade);


            // Configuracion de elimaciones en cascada del modelo Departments
            modelBuilder.Entity<Departments>()
                .HasOne(d => d.Companies)
                .WithMany(c => c.Departments)
                .HasForeignKey(d => d.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);


            // Configuracion de elimaciones en cascada del modelo CategoriesProducts
            modelBuilder.Entity<CategoriesProducts>()
                .HasOne(cp => cp.Companies)
                .WithMany(c => c.CategoriesProducts)
                .HasForeignKey(cp => cp.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);



            // Configuracion de elimaciones en cascada del modelo



            // Configuracion de elimaciones en cascada del modelo



            // Configuracion de elimaciones en cascada del modelo



            // Configuracion de elimaciones en cascada del modelo



            // Configuracion de elimaciones en cascada del modelo



            // Configuracion de elimaciones en cascada del modelo



            // Configuracion de elimaciones en cascada del modelo



            // Configuracion de elimaciones en cascada del modelo



            // Configuracion de elimaciones en cascada del modelo
        }
    }
}
