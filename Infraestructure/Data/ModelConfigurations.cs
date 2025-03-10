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
                .WithMany(s => s.Suppliers)
                .HasForeignKey(s => s.Id_category_supplier_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Suppliers>()
                .HasOne(s => s.Modules)
                .WithMany(s => s.Suppliers)
                .HasForeignKey(s => s.Id_module_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Suppliers>()
                .HasOne(s => s.Companies)
                .WithMany(s => s.Suppliers)
                .HasForeignKey(s => s.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);


            // Configuracion de elimaciones en cascada del modelo Users
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Companies)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Departments)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.Id_department_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Modules)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.Id_module_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Positions)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.Id_position_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Roles)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.Id_rol_Id)
                .OnDelete(DeleteBehavior.NoAction);


            // Configuracion de elimaciones en cascada del modelo Audits
            modelBuilder.Entity<Audits>()
                .HasOne(a => a.Companies)
                .WithMany(a => a.Audits)
                .HasForeignKey(a => a.Id_company_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Audits>()
                .HasOne(a => a.Departments)
                .WithMany(a => a.Audits)
                .HasForeignKey(a => a.Id_department_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Audits>()
                .HasOne(a => a.Modules)
                .WithMany(a => a.Audits)
                .HasForeignKey(a => a.Id_module_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Audits>()
                .HasOne(a => a.Users)
                .WithMany(a => a.Audits)
                .HasForeignKey(a => a.Id_user_Id)
                .OnDelete(DeleteBehavior.NoAction);


            // Configuracion de elimaciones en cascada del modelo Inventories
            modelBuilder.Entity<Inventories>()
                .HasOne(a => a.CategoriesProducts)
                .WithMany(a => a.Inventories)
                .HasForeignKey(a => a.Id_category_product_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inventories>()
                .HasOne(a => a.States)
                .WithMany(a => a.Inventories)
                .HasForeignKey(a => a.Id_state_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inventories>()
                .HasOne(a => a.MovementsTypes)
                .WithMany(a => a.Inventories)
                .HasForeignKey(a => a.Id_movement_type_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inventories>()
                .HasOne(a => a.States)
                .WithMany(a => a.Inventories)
                .HasForeignKey(a => a.Id_state_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inventories>()
                .HasOne(a => a.Suppliers)
                .WithMany(a => a.Inventories)
                .HasForeignKey(a => a.Id_supplier_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inventories>()
                .HasOne(a => a.Departments)
                .WithMany(a => a.Inventories)
                .HasForeignKey(a => a.Id_department_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inventories>()
                .HasOne(a => a.Modules)
                .WithMany(a => a.Inventories)
                .HasForeignKey(a => a.Id_module_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inventories>()
                .HasOne(a => a.Companies)
                .WithMany(a => a.Inventories)
                .HasForeignKey(a => a.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Inventories>()
                .HasOne(a => a.Users)
                .WithMany(a => a.Inventories)
                .HasForeignKey(a => a.Id_user_Id)
                .OnDelete(DeleteBehavior.NoAction);


            // Configuracion de elimaciones en cascada del modelo PurchaseOrders
            modelBuilder.Entity<PurchaseOrders>()
                .HasOne(a => a.Users)
                .WithMany(a => a.PurchaseOrders)
                .HasForeignKey(a => a.Id_user_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseOrders>()
                .HasOne(a => a.CategoryPurchaseOrders)
                .WithMany(a => a.PurchaseOrders)
                .HasForeignKey(a => a.Id_category_purchase_order_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseOrders>()
                .HasOne(a => a.Departments)
                .WithMany(a => a.PurchaseOrders)
                .HasForeignKey(a => a.Id_department_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseOrders>()
                .HasOne(a => a.Suppliers)
                .WithMany(a => a.PurchaseOrders)
                .HasForeignKey(a => a.Id_supplier_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseOrders>()
                .HasOne(a => a.States)
                .WithMany(a => a.PurchaseOrders)
                .HasForeignKey(a => a.Id_state_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseOrders>()
                .HasOne(a => a.MovementsTypes)
                .WithMany(a => a.PurchaseOrders)
                .HasForeignKey(a => a.Id_movement_type_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseOrders>()
                .HasOne(a => a.Modules)
                .WithMany(a => a.PurchaseOrders)
                .HasForeignKey(a => a.Id_module_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseOrders>()
                .HasOne(a => a.Companies)
                .WithMany(a => a.PurchaseOrders)
                .HasForeignKey(a => a.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);


            // Configuracion de elimaciones en cascada del modelo InventoryMovements
            modelBuilder.Entity<InventoryMovements>()
                .HasOne(a => a.Inventories)
                .WithMany(a => a.InventoryMovements)
                .HasForeignKey(a => a.Id_inventory_product_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InventoryMovements>()
                .HasOne(a => a.CategoriesProducts)
                .WithMany(a => a.InventoryMovements)
                .HasForeignKey(a => a.Id_category_product_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InventoryMovements>()
                .HasOne(a => a.Departments)
                .WithMany(a => a.InventoryMovements)
                .HasForeignKey(a => a.Id_department_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InventoryMovements>()
                .HasOne(a => a.Suppliers)
                .WithMany(a => a.InventoryMovements)
                .HasForeignKey(a => a.Id_supplier_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InventoryMovements>()
                .HasOne(a => a.MovementsTypes)
                .WithMany(a => a.InventoryMovements)
                .HasForeignKey(a => a.Id_movements_types_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InventoryMovements>()
                .HasOne(a => a.Modules)
                .WithMany(a => a.InventoryMovements)
                .HasForeignKey(a => a.Id_module_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InventoryMovements>()
                .HasOne(a => a.Companies)
                .WithMany(a => a.InventoryMovements)
                .HasForeignKey(a => a.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InventoryMovements>()
                .HasOne(a => a.Users)
                .WithMany(a => a.InventoryMovements)
                .HasForeignKey(a => a.Id_user_Id)
                .OnDelete(DeleteBehavior.NoAction);


            // Configuracion de elimaciones en cascada del modelo SuppliersProducts
            modelBuilder.Entity<SuppliersProducts>()
                .HasOne(a => a.Inventories)
                .WithMany(a => a.SuppliersProducts)
                .HasForeignKey(a => a.Id_inventory_product_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SuppliersProducts>()
                .HasOne(a => a.Suppliers)
                .WithMany(a => a.SuppliersProducts)
                .HasForeignKey(a => a.Id_supplier_Id)
                .OnDelete(DeleteBehavior.NoAction);


            // Configuracion de elimaciones en cascada del modelo Invoices
            modelBuilder.Entity<Invoices>()
                .HasOne(a => a.PurchaseOrders)
                .WithMany(a => a.Invoices)
                .HasForeignKey(a => a.Id_purchase_order_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Invoices>()
                .HasOne(a => a.Suppliers)
                .WithMany(a => a.Invoices)
                .HasForeignKey(a => a.Id_supplier_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Invoices>()
                .HasOne(a => a.Departments)
                .WithMany(a => a.Invoices)
                .HasForeignKey(a => a.Id_department_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Invoices>()
                .HasOne(a => a.Modules)
                .WithMany(a => a.Invoices)
                .HasForeignKey(a => a.Id_module_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Invoices>()
                .HasOne(a => a.Companies)
                .WithMany(a => a.Invoices)
                .HasForeignKey(a => a.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);


            // Configuracion de elimaciones en cascada del modelo OrdersProducts
            modelBuilder.Entity<OrdersProducts>()
                .HasOne(a => a.PurchaseOrders)
                .WithMany(a => a.OrdersProducts)
                .HasForeignKey(a => a.Id_purchase_order_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrdersProducts>()
                .HasOne(a => a.Inventories)
                .WithMany(a => a.OrdersProducts)
                .HasForeignKey(a => a.Id_inventory_product_Id)
                .OnDelete(DeleteBehavior.Cascade);


            // Configuracion de elimaciones en cascada del modelo InvoicesProducts
            modelBuilder.Entity<InvoicesProducts>()
                .HasOne(a => a.Invoices)
                .WithMany(a => a.InvoicesProducts)
                .HasForeignKey(a => a.Id_invoice_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InvoicesProducts>()
                .HasOne(a => a.Inventories)
                .WithMany(a => a.InvoicesProducts)
                .HasForeignKey(a => a.Id_inventory_product_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuracion de elimaciones en cascada del modelo Deparments
            modelBuilder.Entity<Departments>()
                .HasOne(d => d.Companies)
                .WithMany(d => d.Departments)
                .HasForeignKey(d => d.Id_company_Id)
                .OnDelete(DeleteBehavior.NoAction);


            // Configuracion de elimaciones en cascada del modelo CategoriesProducts
            modelBuilder.Entity<CategoriesProducts>()
                .HasOne(a => a.Companies)
                .WithMany(a => a.CategoriesProducts)
                .HasForeignKey(a => a.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);


            // Configuracion de elimaciones en cascada del modelo CategoriesPurchaseOrders
            modelBuilder.Entity<CategoriesPurchaseOrders>()
                .HasOne(a => a.Companies)
                .WithMany(a => a.CategoriesPurchaseOrders)
                .HasForeignKey(a => a.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);


            // Configuracion de elimaciones en cascada del modelo CategoriesSuppliers
            modelBuilder.Entity<CategoriesSuppliers>()
                .HasOne(a => a.Companies)
                .WithMany(a => a.CategoriesSuppliers)
                .HasForeignKey(a => a.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);


            // Configuracion de elimaciones en cascada del modelo MovementsTypes 
            modelBuilder.Entity<MovementsTypes>()
                .HasOne(a => a.Companies)
                .WithMany(a => a.MovementsTypes)
                .HasForeignKey(a => a.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MovementsTypes>()
                .HasOne(a => a.ClasificationsMovements)
                .WithMany(a => a.MoventsTypes)
                .HasForeignKey(a => a.Id_clasification_movement_Id)
                .OnDelete(DeleteBehavior.NoAction);


            // Configuracion de elimaciones en cascada del modelo Positions
            modelBuilder.Entity<Positions>()
                .HasOne(a => a.Companies)
                .WithMany(a => a.Positions)
                .HasForeignKey(a => a.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);


            // Configuracion de elimaciones en cascada del modelo States
            modelBuilder.Entity<States>()
                .HasOne(a => a.Companies)
                .WithMany(a => a.States)
                .HasForeignKey(a => a.Id_company_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
