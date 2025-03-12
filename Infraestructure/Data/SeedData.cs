using FluxSYS_backend.Domain.Models.PrimitiveModels;
using FluxSYS_backend.Domain.Models.PrincipalModels;
using Microsoft.EntityFrameworkCore;

namespace FluxSYS_backend.Infraestructure.Data
{
    public partial class ApplicationDbContext
    {
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Companies
            modelBuilder.Entity<Companies>().HasData(
                new Companies { Id_company = 1, Name_company = "Tech Innovators" },
                new Companies { Id_company = 2, Name_company = "Global Solutions" },
                new Companies { Id_company = 3, Name_company = "NextGen Systems" },
                new Companies { Id_company = 4, Name_company = "Future Enterprises" }
            );

            // ClasificationMovements
            modelBuilder.Entity<ClasificationMovements>().HasData(
                new ClasificationMovements { Id_clasification_movement = 1, Name_clasification_movement = "Eliminacion" },
                new ClasificationMovements { Id_clasification_movement = 2, Name_clasification_movement = "Creacion" },
                new ClasificationMovements { Id_clasification_movement = 3, Name_clasification_movement = "Actualizacion" },
                new ClasificationMovements { Id_clasification_movement = 4, Name_clasification_movement = "Restauracion" }
            );

            // Modules
            modelBuilder.Entity<Modules>().HasData(
                new Modules { Id_module = 1, Name_module = "Proveedores" },
                new Modules { Id_module = 2, Name_module = "Auditorias" },
                new Modules { Id_module = 3, Name_module = "Inventario" },
                new Modules { Id_module = 4, Name_module = "Facturas" },
                new Modules { Id_module = 5, Name_module = "Ordenes de compra" },
                new Modules { Id_module = 6, Name_module = "Usuarios" }
            );

            // MovementsTypes
            modelBuilder.Entity<MovementsTypes>().HasData(
                new MovementsTypes { Id_movement_type = 1, Name_movement_type = "Entrada de Mercancía", Id_company_Id = 1, Id_clasification_movement_Id = 1, Delete_log_movement_type = false },
                new MovementsTypes { Id_movement_type = 2, Name_movement_type = "Salida por Venta", Id_company_Id = 2, Id_clasification_movement_Id = 2, Delete_log_movement_type = false },
                new MovementsTypes { Id_movement_type = 3, Name_movement_type = "Ajuste por Pérdida", Id_company_Id = 3, Id_clasification_movement_Id = 3, Delete_log_movement_type = false },
                new MovementsTypes { Id_movement_type = 4, Name_movement_type = "Transferencia de Almacén", Id_company_Id = 1, Id_clasification_movement_Id = 1, Delete_log_movement_type = false },
                new MovementsTypes { Id_movement_type = 5, Name_movement_type = "Devolución de Producto", Id_company_Id = 2, Id_clasification_movement_Id = 2, Delete_log_movement_type = false }
            );

            // CategoriesProducts
            modelBuilder.Entity<CategoriesProducts>().HasData(
                new CategoriesProducts { Id_category_product = 1, Name_category_product = "Electrónica", Id_company_Id = 1 },
                new CategoriesProducts { Id_category_product = 2, Name_category_product = "Muebles", Id_company_Id = 2 },
                new CategoriesProducts { Id_category_product = 3, Name_category_product = "Ropa", Id_company_Id = 3 },
                new CategoriesProducts { Id_category_product = 4, Name_category_product = "Alimentos", Id_company_Id = 4 },
                new CategoriesProducts { Id_category_product = 5, Name_category_product = "Herramientas", Id_company_Id = 1 }
            );

            // CategoriesPurchaseOrders
            modelBuilder.Entity<CategoriesPurchaseOrders>().HasData(
                new CategoriesPurchaseOrders { Id_category_purchase_order = 1, Name_category_purchase_order = "Interna", Id_company_Id = 1 },
                new CategoriesPurchaseOrders { Id_category_purchase_order = 2, Name_category_purchase_order = "Externa", Id_company_Id = 2 },
                new CategoriesPurchaseOrders { Id_category_purchase_order = 3, Name_category_purchase_order = "Urgente", Id_company_Id = 3 },
                new CategoriesPurchaseOrders { Id_category_purchase_order = 4, Name_category_purchase_order = "Planificada", Id_company_Id = 4 },
                new CategoriesPurchaseOrders { Id_category_purchase_order = 5, Name_category_purchase_order = "Especial", Id_company_Id = 1 }
            );

            // CategoriesSuppliers
            modelBuilder.Entity<CategoriesSuppliers>().HasData(
                new CategoriesSuppliers { Id_category_supplier = 1, Name_category_supplier = "Tecnología", Id_company_Id = 1, Delete_log_category_supplier = false },
                new CategoriesSuppliers { Id_category_supplier = 2, Name_category_supplier = "Alimentos", Id_company_Id = 2, Delete_log_category_supplier = false },
                new CategoriesSuppliers { Id_category_supplier = 3, Name_category_supplier = "Materiales de Construcción", Id_company_Id = 3, Delete_log_category_supplier = false },
                new CategoriesSuppliers { Id_category_supplier = 4, Name_category_supplier = "Suministros de Oficina", Id_company_Id = 4, Delete_log_category_supplier = false },
                new CategoriesSuppliers { Id_category_supplier = 5, Name_category_supplier = "Productos Químicos", Id_company_Id = 1, Delete_log_category_supplier = false }
            );

            // Departments
            modelBuilder.Entity<Departments>().HasData(
                new Departments { Id_department = 1, Name_deparment = "Recursos Humanos", Id_company_Id = 1 },
                new Departments { Id_department = 2, Name_deparment = "Tecnología", Id_company_Id = 2 },
                new Departments { Id_department = 3, Name_deparment = "Ventas", Id_company_Id = 3 },
                new Departments { Id_department = 4, Name_deparment = "Logística", Id_company_Id = 4 },
                new Departments { Id_department = 5, Name_deparment = "Finanzas", Id_company_Id = 1 }
            );

            // Positions
            modelBuilder.Entity<Positions>().HasData(
                new Positions { Id_position = 1, Name_position = "Gerente", Id_company_Id = 1 },
                new Positions { Id_position = 2, Name_position = "Supervisor", Id_company_Id = 2 },
                new Positions { Id_position = 3, Name_position = "Analista", Id_company_Id = 3 },
                new Positions { Id_position = 4, Name_position = "Técnico", Id_company_Id = 4 },
                new Positions { Id_position = 5, Name_position = "Asistente", Id_company_Id = 1 }
            );

            // Roles
            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id_role = 1, Name_role = "Administrador" },
                new Roles { Id_role = 2, Name_role = "Supervisor" },
                new Roles { Id_role = 3, Name_role = "Operador" },
                new Roles { Id_role = 4, Name_role = "Invitado" },
                new Roles { Id_role = 5, Name_role = "Gerente" }
            );

            // States
            modelBuilder.Entity<States>().HasData(
                new States { Id_state = 1, Name_state = "Activo", Id_company_Id = 1, Delete_log_state = false },
                new States { Id_state = 2, Name_state = "Inactivo", Id_company_Id = 2, Delete_log_state = false },
                new States { Id_state = 3, Name_state = "Pendiente", Id_company_Id = 3, Delete_log_state = false },
                new States { Id_state = 4, Name_state = "En Proceso", Id_company_Id = 1, Delete_log_state = false },
                new States { Id_state = 5, Name_state = "Finalizado", Id_company_Id = 2, Delete_log_state = false }
            );

            // Suppliers
            modelBuilder.Entity<Suppliers>().HasData(
                new Suppliers
                {
                    Id_supplier = 1,
                    Name_supplier = "Proveedor A",
                    Mail_supplier = "contacto@proveedora.com",
                    Phone_supplier = 5512345678,
                    Id_category_supplier_Id = 1,
                    Id_module_Id = 1,
                    Id_company_Id = 1,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_suppliers = false
                },
                new Suppliers
                {
                    Id_supplier = 2,
                    Name_supplier = "Proveedor B",
                    Mail_supplier = "ventas@proveedorb.com",
                    Phone_supplier = 5523456789,
                    Id_category_supplier_Id = 2,
                    Id_module_Id = 1,
                    Id_company_Id = 2,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_suppliers = false
                },
                new Suppliers
                {
                    Id_supplier = 3,
                    Name_supplier = "Distribuidora C",
                    Mail_supplier = "info@distribuidorac.com",
                    Phone_supplier = 5534567890,
                    Id_category_supplier_Id = 3,
                    Id_module_Id = 1,
                    Id_company_Id = 3,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_suppliers = false
                },
                new Suppliers
                {
                    Id_supplier = 4,
                    Name_supplier = "Empresa D",
                    Mail_supplier = "soporte@empresad.com",
                    Phone_supplier = 5545678901,
                    Id_category_supplier_Id = 4,
                    Id_module_Id = 1,
                    Id_company_Id = 1,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_suppliers = false
                },
                new Suppliers
                {
                    Id_supplier = 5,
                    Name_supplier = "Mayorista E",
                    Mail_supplier = "compras@mayoristae.com",
                    Phone_supplier = 5556789012,
                    Id_category_supplier_Id = 5,
                    Id_module_Id = 1,
                    Id_company_Id = 2,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_suppliers = false
                }
            );

            // Inventories
            modelBuilder.Entity<Inventories>().HasData(
                new Inventories
                {
                    Id_inventory_product = 1,
                    Name_product = "Laptop Dell XPS 15",
                    Stock_product = 10,
                    Price_product = 25000.00m,
                    Id_category_product_Id = 1,
                    Id_state_Id = 1,
                    Id_movement_type_Id = 1,
                    Id_supplier_Id = 1,
                    Id_department_Id = 1,
                    Id_module_Id = 3,
                    Id_company_Id = 1,
                    Id_user_Id = 1,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_inventory = false
                },
                new Inventories
                {
                    Id_inventory_product = 2,
                    Name_product = "Monitor Samsung 27\"",
                    Stock_product = 20,
                    Price_product = 7500.00m,
                    Id_category_product_Id = 2,
                    Id_state_Id = 2,
                    Id_movement_type_Id = 2,
                    Id_supplier_Id = 2,
                    Id_department_Id = 2,
                    Id_module_Id = 3,
                    Id_company_Id = 2,
                    Id_user_Id = 2,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_inventory = false
                },
                new Inventories
                {
                    Id_inventory_product = 3,
                    Name_product = "Teclado Mecánico HyperX",
                    Stock_product = 15,
                    Price_product = 3500.00m,
                    Id_category_product_Id = 3,
                    Id_state_Id = 1,
                    Id_movement_type_Id = 1,
                    Id_supplier_Id = 3,
                    Id_department_Id = 3,
                    Id_module_Id = 3,
                    Id_company_Id = 3,
                    Id_user_Id = 3,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_inventory = false
                },
                new Inventories
                {
                    Id_inventory_product = 4,
                    Name_product = "Mouse Logitech MX Master 3",
                    Stock_product = 25,
                    Price_product = 2500.00m,
                    Id_category_product_Id = 4,
                    Id_state_Id = 2,
                    Id_movement_type_Id = 2,
                    Id_supplier_Id = 4,
                    Id_department_Id = 4,
                    Id_module_Id = 3,
                    Id_company_Id = 1,
                    Id_user_Id = 4,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_inventory = false
                },
                new Inventories
                {
                    Id_inventory_product = 5,
                    Name_product = "Silla Ergonómica",
                    Stock_product = 5,
                    Price_product = 12000.00m,
                    Id_category_product_Id = 5,
                    Id_state_Id = 3,
                    Id_movement_type_Id = 3,
                    Id_supplier_Id = 5,
                    Id_department_Id = 5,
                    Id_module_Id = 3,
                    Id_company_Id = 2,
                    Id_user_Id = 5,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_inventory = false
                }
            );

            // Invoices
            modelBuilder.Entity<Invoices>().HasData(
                new Invoices
                {
                    Id_invoice = 1,
                    Name_invoice = "Factura 001",
                    Amount_items_in_the_invoice = 5,
                    Id_purchase_order_Id = 1,
                    Id_supplier_Id = 1,
                    Id_department_Id = 1,
                    Id_module_Id = 4,
                    Id_company_Id = 1,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_invoices = false
                },
                new Invoices
                {
                    Id_invoice = 2,
                    Name_invoice = "Factura 002",
                    Amount_items_in_the_invoice = 8,
                    Id_purchase_order_Id = 2,
                    Id_supplier_Id = 2,
                    Id_department_Id = 2,
                    Id_module_Id = 4,
                    Id_company_Id = 1,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_invoices = false
                },
                new Invoices
                {
                    Id_invoice = 3,
                    Name_invoice = "Factura 003",
                    Amount_items_in_the_invoice = 3,
                    Id_purchase_order_Id = 3,
                    Id_supplier_Id = 3,
                    Id_department_Id = 3,
                    Id_module_Id = 4,
                    Id_company_Id = 2,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_invoices = false
                },
                new Invoices
                {
                    Id_invoice = 4,
                    Name_invoice = "Factura 004",
                    Amount_items_in_the_invoice = 12,
                    Id_purchase_order_Id = 4,
                    Id_supplier_Id = 4,
                    Id_department_Id = 4,
                    Id_module_Id = 4,
                    Id_company_Id = 2,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_invoices = false
                },
                new Invoices
                {
                    Id_invoice = 5,
                    Name_invoice = "Factura 005",
                    Amount_items_in_the_invoice = 7,
                    Id_purchase_order_Id = 5,
                    Id_supplier_Id = 5,
                    Id_department_Id = 5,
                    Id_module_Id = 4,
                    Id_company_Id = 3,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_invoices = false
                }
            );

            // PurchaseOrders
            modelBuilder.Entity<PurchaseOrders>().HasData(
                new PurchaseOrders
                {
                    Id_purchase_order = 1,
                    Name_purchase_order = "Orden de Compra 001",
                    Amount_items_in_the_order = 5,
                    Total_price_products = 1500.50m,
                    Id_user_Id = 1,
                    Id_category_purchase_order_Id = 1,
                    Id_department_Id = 1,
                    Id_supplier_Id = 1,
                    Id_state_Id = 1,
                    Id_movement_type_Id = 1,
                    Id_module_Id = 5,
                    Id_company_Id = 1,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_purchase_orders = false
                },
                new PurchaseOrders
                {
                    Id_purchase_order = 2,
                    Name_purchase_order = "Orden de Compra 002",
                    Amount_items_in_the_order = 8,
                    Total_price_products = 2450.75m,
                    Id_user_Id = 2,
                    Id_category_purchase_order_Id = 2,
                    Id_department_Id = 2,
                    Id_supplier_Id = 2,
                    Id_state_Id = 2,
                    Id_movement_type_Id = 2,
                    Id_module_Id = 5,
                    Id_company_Id = 1,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_purchase_orders = false
                },
                new PurchaseOrders
                {
                    Id_purchase_order = 3,
                    Name_purchase_order = "Orden de Compra 003",
                    Amount_items_in_the_order = 3,
                    Total_price_products = 850.25m,
                    Id_user_Id = 3,
                    Id_category_purchase_order_Id = 3,
                    Id_department_Id = 3,
                    Id_supplier_Id = 3,
                    Id_state_Id = 3,
                    Id_movement_type_Id = 3,
                    Id_module_Id = 5,
                    Id_company_Id = 2,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_purchase_orders = false
                },
                new PurchaseOrders
                {
                    Id_purchase_order = 4,
                    Name_purchase_order = "Orden de Compra 004",
                    Amount_items_in_the_order = 12,
                    Total_price_products = 3750.00m,
                    Id_user_Id = 4,
                    Id_category_purchase_order_Id = 4,
                    Id_department_Id = 4,
                    Id_supplier_Id = 4,
                    Id_state_Id = 4,
                    Id_movement_type_Id = 4,
                    Id_module_Id = 5,
                    Id_company_Id = 2,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_purchase_orders = false
                },
                new PurchaseOrders
                {
                    Id_purchase_order = 5,
                    Name_purchase_order = "Orden de Compra 005",
                    Amount_items_in_the_order = 7,
                    Total_price_products = 1980.99m,
                    Id_user_Id = 5,
                    Id_category_purchase_order_Id = 5,
                    Id_department_Id = 5,
                    Id_supplier_Id = 5,
                    Id_state_Id = 5,
                    Id_movement_type_Id = 5,
                    Id_module_Id = 5,
                    Id_company_Id = 3,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_purchase_orders = false
                }
            );

            // Users
            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    Id_user = 1,
                    Name_user = "Sánchez Lobato Gael",
                    Mail_user = "gael.sanchez@example.com",
                    Phone_user = 1234567890,
                    Password_user = "password123",
                    Id_rol_Id = 1,
                    Id_position_Id = 1,
                    Id_department_Id = 1,
                    Id_company_Id = 1,
                    Id_module_Id = 6,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_user = false
                },
                new Users
                {
                    Id_user = 2,
                    Name_user = "Quintero Escobar Carlos Máximo",
                    Mail_user = "carlos.quintero@example.com",
                    Phone_user = 9876543210,
                    Password_user = "password123",
                    Id_rol_Id = 2,
                    Id_position_Id = 2,
                    Id_department_Id = 2,
                    Id_company_Id = 1,
                    Id_module_Id = 6,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_user = false
                },
                new Users
                {
                    Id_user = 3,
                    Name_user = "Gutiérrez Canul Gustavo",
                    Mail_user = "gustavo.gutierrez@example.com",
                    Phone_user = 1122334455,
                    Password_user = "password123",
                    Id_rol_Id = 3,
                    Id_position_Id = 3,
                    Id_department_Id = 3,
                    Id_company_Id = 2,
                    Id_module_Id = 6,
                    Date_insert = new DateTime(2025, 3, 12), 
                    Delete_log_user = false
                },
                new Users
                {
                    Id_user = 4,
                    Name_user = "Raymundo Mata Isha Mia",
                    Mail_user = "isha.mata@example.com",
                    Phone_user = 2233445566,
                    Password_user = "password123",
                    Id_rol_Id = 4,
                    Id_position_Id = 4,
                    Id_department_Id = 4,
                    Id_company_Id = 2,
                    Id_module_Id = 6,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_user = false
                },
                new Users
                {
                    Id_user = 5,
                    Name_user = "Velázquez De La Cruz Carlos Yahir",
                    Mail_user = "carlos.velazquez@example.com",
                    Phone_user = 3344556677,
                    Password_user = "password123",
                    Id_rol_Id = 5,
                    Id_position_Id = 5,
                    Id_department_Id = 5,
                    Id_company_Id = 3,
                    Id_module_Id = 6,
                    Date_insert = new DateTime(2025, 3, 12),
                    Delete_log_user = false
                }
            );
        }
    }
}
