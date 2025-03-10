using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FluxSYS_backend.Migrations
{
    /// <inheritdoc />
    public partial class ModelsConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clasification_movements",
                columns: table => new
                {
                    Id_clasification_movement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_clasification_movement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delete_log_clasification_movement = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clasification_movements", x => x.Id_clasification_movement);
                });

            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    Id_company = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delete_log_company = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.Id_company);
                });

            migrationBuilder.CreateTable(
                name: "error_logs",
                columns: table => new
                {
                    Id_error_log = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message_error = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stacktrace_error = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source_error = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_error_logs", x => x.Id_error_log);
                });

            migrationBuilder.CreateTable(
                name: "modules",
                columns: table => new
                {
                    Id_module = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delete_log_module = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modules", x => x.Id_module);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id_role = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delete_log_role = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id_role);
                });

            migrationBuilder.CreateTable(
                name: "categories_products",
                columns: table => new
                {
                    Id_category_product = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_category_product = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_company_Id = table.Column<int>(type: "int", nullable: false),
                    Delete_log_category_product = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories_products", x => x.Id_category_product);
                    table.ForeignKey(
                        name: "FK_categories_products_companies_Id_company_Id",
                        column: x => x.Id_company_Id,
                        principalTable: "companies",
                        principalColumn: "Id_company",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "categories_purchase_orders",
                columns: table => new
                {
                    Id_category_purchase_order = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_category_purchase_order = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_company_Id = table.Column<int>(type: "int", nullable: false),
                    Delete_log_category_purchase_order = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories_purchase_orders", x => x.Id_category_purchase_order);
                    table.ForeignKey(
                        name: "FK_categories_purchase_orders_companies_Id_company_Id",
                        column: x => x.Id_company_Id,
                        principalTable: "companies",
                        principalColumn: "Id_company",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "categories_suppliers",
                columns: table => new
                {
                    Id_category_supplier = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_category_supplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_company_Id = table.Column<int>(type: "int", nullable: false),
                    Delete_log_category_supplier = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories_suppliers", x => x.Id_category_supplier);
                    table.ForeignKey(
                        name: "FK_categories_suppliers_companies_Id_company_Id",
                        column: x => x.Id_company_Id,
                        principalTable: "companies",
                        principalColumn: "Id_company",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    Id_department = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_deparment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_company_Id = table.Column<int>(type: "int", nullable: false),
                    Delete_log_department = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.Id_department);
                    table.ForeignKey(
                        name: "FK_departments_companies_Id_company_Id",
                        column: x => x.Id_company_Id,
                        principalTable: "companies",
                        principalColumn: "Id_company");
                });

            migrationBuilder.CreateTable(
                name: "movements_types",
                columns: table => new
                {
                    Id_movement_type = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_movement_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_company_Id = table.Column<int>(type: "int", nullable: false),
                    Id_clasification_movement_Id = table.Column<int>(type: "int", nullable: false),
                    Delete_log_movement_type = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movements_types", x => x.Id_movement_type);
                    table.ForeignKey(
                        name: "FK_movements_types_clasification_movements_Id_clasification_movement_Id",
                        column: x => x.Id_clasification_movement_Id,
                        principalTable: "clasification_movements",
                        principalColumn: "Id_clasification_movement");
                    table.ForeignKey(
                        name: "FK_movements_types_companies_Id_company_Id",
                        column: x => x.Id_company_Id,
                        principalTable: "companies",
                        principalColumn: "Id_company",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "positions",
                columns: table => new
                {
                    Id_position = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_company_Id = table.Column<int>(type: "int", nullable: false),
                    Delete_log_position = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_positions", x => x.Id_position);
                    table.ForeignKey(
                        name: "FK_positions_companies_Id_company_Id",
                        column: x => x.Id_company_Id,
                        principalTable: "companies",
                        principalColumn: "Id_company",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "states",
                columns: table => new
                {
                    Id_state = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delete_log_state = table.Column<bool>(type: "bit", nullable: false),
                    Id_company_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_states", x => x.Id_state);
                    table.ForeignKey(
                        name: "FK_states_companies_Id_company_Id",
                        column: x => x.Id_company_Id,
                        principalTable: "companies",
                        principalColumn: "Id_company",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    Id_supplier = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_supplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail_supplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_supplier = table.Column<long>(type: "bigint", nullable: false),
                    Id_category_supplier_Id = table.Column<int>(type: "int", nullable: false),
                    Id_module_Id = table.Column<int>(type: "int", nullable: false),
                    Id_company_Id = table.Column<int>(type: "int", nullable: false),
                    Date_insert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_update = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_delete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_restore = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delete_log_suppliers = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suppliers", x => x.Id_supplier);
                    table.ForeignKey(
                        name: "FK_suppliers_categories_suppliers_Id_category_supplier_Id",
                        column: x => x.Id_category_supplier_Id,
                        principalTable: "categories_suppliers",
                        principalColumn: "Id_category_supplier");
                    table.ForeignKey(
                        name: "FK_suppliers_companies_Id_company_Id",
                        column: x => x.Id_company_Id,
                        principalTable: "companies",
                        principalColumn: "Id_company",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_suppliers_modules_Id_module_Id",
                        column: x => x.Id_module_Id,
                        principalTable: "modules",
                        principalColumn: "Id_module");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id_user = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail_user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_user = table.Column<long>(type: "bigint", nullable: false),
                    Password_user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_rol_Id = table.Column<int>(type: "int", nullable: false),
                    Id_position_Id = table.Column<int>(type: "int", nullable: false),
                    Id_department_Id = table.Column<int>(type: "int", nullable: false),
                    Id_company_Id = table.Column<int>(type: "int", nullable: false),
                    Id_module_Id = table.Column<int>(type: "int", nullable: false),
                    Date_insert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_update = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_delete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_restore = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delete_log_user = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id_user);
                    table.ForeignKey(
                        name: "FK_users_companies_Id_company_Id",
                        column: x => x.Id_company_Id,
                        principalTable: "companies",
                        principalColumn: "Id_company",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_departments_Id_department_Id",
                        column: x => x.Id_department_Id,
                        principalTable: "departments",
                        principalColumn: "Id_department");
                    table.ForeignKey(
                        name: "FK_users_modules_Id_module_Id",
                        column: x => x.Id_module_Id,
                        principalTable: "modules",
                        principalColumn: "Id_module");
                    table.ForeignKey(
                        name: "FK_users_positions_Id_position_Id",
                        column: x => x.Id_position_Id,
                        principalTable: "positions",
                        principalColumn: "Id_position");
                    table.ForeignKey(
                        name: "FK_users_roles_Id_rol_Id",
                        column: x => x.Id_rol_Id,
                        principalTable: "roles",
                        principalColumn: "Id_role");
                });

            migrationBuilder.CreateTable(
                name: "audits",
                columns: table => new
                {
                    Id_audit = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date_insert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_update = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_delete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_restore = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Amount_modify = table.Column<int>(type: "int", nullable: false),
                    Id_user_Id = table.Column<int>(type: "int", nullable: false),
                    Id_department_Id = table.Column<int>(type: "int", nullable: false),
                    Id_module_Id = table.Column<int>(type: "int", nullable: false),
                    Id_company_Id = table.Column<int>(type: "int", nullable: false),
                    Delete_log_audits = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audits", x => x.Id_audit);
                    table.ForeignKey(
                        name: "FK_audits_companies_Id_company_Id",
                        column: x => x.Id_company_Id,
                        principalTable: "companies",
                        principalColumn: "Id_company");
                    table.ForeignKey(
                        name: "FK_audits_departments_Id_department_Id",
                        column: x => x.Id_department_Id,
                        principalTable: "departments",
                        principalColumn: "Id_department");
                    table.ForeignKey(
                        name: "FK_audits_modules_Id_module_Id",
                        column: x => x.Id_module_Id,
                        principalTable: "modules",
                        principalColumn: "Id_module");
                    table.ForeignKey(
                        name: "FK_audits_users_Id_user_Id",
                        column: x => x.Id_user_Id,
                        principalTable: "users",
                        principalColumn: "Id_user");
                });

            migrationBuilder.CreateTable(
                name: "inventory",
                columns: table => new
                {
                    Id_inventory_product = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_product = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stock_product = table.Column<int>(type: "int", nullable: false),
                    Price_product = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Id_category_product_Id = table.Column<int>(type: "int", nullable: false),
                    Id_state_Id = table.Column<int>(type: "int", nullable: false),
                    Id_movement_type_Id = table.Column<int>(type: "int", nullable: false),
                    Id_supplier_Id = table.Column<int>(type: "int", nullable: false),
                    Id_department_Id = table.Column<int>(type: "int", nullable: false),
                    Id_module_Id = table.Column<int>(type: "int", nullable: false),
                    Id_company_Id = table.Column<int>(type: "int", nullable: false),
                    Id_user_Id = table.Column<int>(type: "int", nullable: false),
                    Date_insert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_update = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_delete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_restore = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delete_log_inventory = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory", x => x.Id_inventory_product);
                    table.ForeignKey(
                        name: "FK_inventory_categories_products_Id_category_product_Id",
                        column: x => x.Id_category_product_Id,
                        principalTable: "categories_products",
                        principalColumn: "Id_category_product");
                    table.ForeignKey(
                        name: "FK_inventory_companies_Id_company_Id",
                        column: x => x.Id_company_Id,
                        principalTable: "companies",
                        principalColumn: "Id_company",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventory_departments_Id_department_Id",
                        column: x => x.Id_department_Id,
                        principalTable: "departments",
                        principalColumn: "Id_department");
                    table.ForeignKey(
                        name: "FK_inventory_modules_Id_module_Id",
                        column: x => x.Id_module_Id,
                        principalTable: "modules",
                        principalColumn: "Id_module");
                    table.ForeignKey(
                        name: "FK_inventory_movements_types_Id_movement_type_Id",
                        column: x => x.Id_movement_type_Id,
                        principalTable: "movements_types",
                        principalColumn: "Id_movement_type");
                    table.ForeignKey(
                        name: "FK_inventory_states_Id_state_Id",
                        column: x => x.Id_state_Id,
                        principalTable: "states",
                        principalColumn: "Id_state");
                    table.ForeignKey(
                        name: "FK_inventory_suppliers_Id_supplier_Id",
                        column: x => x.Id_supplier_Id,
                        principalTable: "suppliers",
                        principalColumn: "Id_supplier");
                    table.ForeignKey(
                        name: "FK_inventory_users_Id_user_Id",
                        column: x => x.Id_user_Id,
                        principalTable: "users",
                        principalColumn: "Id_user");
                });

            migrationBuilder.CreateTable(
                name: "purchase_orders",
                columns: table => new
                {
                    Id_purchase_order = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_purchase_order = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount_items_in_the_order = table.Column<int>(type: "int", nullable: false),
                    Total_price_products = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Id_user_Id = table.Column<int>(type: "int", nullable: false),
                    Id_category_purchase_order_Id = table.Column<int>(type: "int", nullable: false),
                    Id_department_Id = table.Column<int>(type: "int", nullable: false),
                    Id_supplier_Id = table.Column<int>(type: "int", nullable: false),
                    Id_state_Id = table.Column<int>(type: "int", nullable: false),
                    Id_movement_type_Id = table.Column<int>(type: "int", nullable: false),
                    Id_module_Id = table.Column<int>(type: "int", nullable: false),
                    Id_company_Id = table.Column<int>(type: "int", nullable: false),
                    Date_insert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_update = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_delete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_restore = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delete_log_purchase_orders = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_orders", x => x.Id_purchase_order);
                    table.ForeignKey(
                        name: "FK_purchase_orders_categories_purchase_orders_Id_category_purchase_order_Id",
                        column: x => x.Id_category_purchase_order_Id,
                        principalTable: "categories_purchase_orders",
                        principalColumn: "Id_category_purchase_order");
                    table.ForeignKey(
                        name: "FK_purchase_orders_companies_Id_company_Id",
                        column: x => x.Id_company_Id,
                        principalTable: "companies",
                        principalColumn: "Id_company",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_orders_departments_Id_department_Id",
                        column: x => x.Id_department_Id,
                        principalTable: "departments",
                        principalColumn: "Id_department");
                    table.ForeignKey(
                        name: "FK_purchase_orders_modules_Id_module_Id",
                        column: x => x.Id_module_Id,
                        principalTable: "modules",
                        principalColumn: "Id_module");
                    table.ForeignKey(
                        name: "FK_purchase_orders_movements_types_Id_movement_type_Id",
                        column: x => x.Id_movement_type_Id,
                        principalTable: "movements_types",
                        principalColumn: "Id_movement_type");
                    table.ForeignKey(
                        name: "FK_purchase_orders_states_Id_state_Id",
                        column: x => x.Id_state_Id,
                        principalTable: "states",
                        principalColumn: "Id_state");
                    table.ForeignKey(
                        name: "FK_purchase_orders_suppliers_Id_supplier_Id",
                        column: x => x.Id_supplier_Id,
                        principalTable: "suppliers",
                        principalColumn: "Id_supplier");
                    table.ForeignKey(
                        name: "FK_purchase_orders_users_Id_user_Id",
                        column: x => x.Id_user_Id,
                        principalTable: "users",
                        principalColumn: "Id_user");
                });

            migrationBuilder.CreateTable(
                name: "inventory_movements",
                columns: table => new
                {
                    Id_inventory_movement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount_modify = table.Column<int>(type: "int", nullable: false),
                    Id_inventory_product_Id = table.Column<int>(type: "int", nullable: false),
                    Id_category_product_Id = table.Column<int>(type: "int", nullable: false),
                    Id_department_Id = table.Column<int>(type: "int", nullable: false),
                    Id_supplier_Id = table.Column<int>(type: "int", nullable: false),
                    Id_movements_types_Id = table.Column<int>(type: "int", nullable: false),
                    Id_module_Id = table.Column<int>(type: "int", nullable: false),
                    Id_company_Id = table.Column<int>(type: "int", nullable: false),
                    Id_user_Id = table.Column<int>(type: "int", nullable: false),
                    Date_insert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_update = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_delete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_restore = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delete_log_inventory_movement = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_movements", x => x.Id_inventory_movement);
                    table.ForeignKey(
                        name: "FK_inventory_movements_categories_products_Id_category_product_Id",
                        column: x => x.Id_category_product_Id,
                        principalTable: "categories_products",
                        principalColumn: "Id_category_product");
                    table.ForeignKey(
                        name: "FK_inventory_movements_companies_Id_company_Id",
                        column: x => x.Id_company_Id,
                        principalTable: "companies",
                        principalColumn: "Id_company",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventory_movements_departments_Id_department_Id",
                        column: x => x.Id_department_Id,
                        principalTable: "departments",
                        principalColumn: "Id_department");
                    table.ForeignKey(
                        name: "FK_inventory_movements_inventory_Id_inventory_product_Id",
                        column: x => x.Id_inventory_product_Id,
                        principalTable: "inventory",
                        principalColumn: "Id_inventory_product");
                    table.ForeignKey(
                        name: "FK_inventory_movements_modules_Id_module_Id",
                        column: x => x.Id_module_Id,
                        principalTable: "modules",
                        principalColumn: "Id_module");
                    table.ForeignKey(
                        name: "FK_inventory_movements_movements_types_Id_movements_types_Id",
                        column: x => x.Id_movements_types_Id,
                        principalTable: "movements_types",
                        principalColumn: "Id_movement_type");
                    table.ForeignKey(
                        name: "FK_inventory_movements_suppliers_Id_supplier_Id",
                        column: x => x.Id_supplier_Id,
                        principalTable: "suppliers",
                        principalColumn: "Id_supplier");
                    table.ForeignKey(
                        name: "FK_inventory_movements_users_Id_user_Id",
                        column: x => x.Id_user_Id,
                        principalTable: "users",
                        principalColumn: "Id_user");
                });

            migrationBuilder.CreateTable(
                name: "suppliers_products",
                columns: table => new
                {
                    Id_supplier_product = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Suggested_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Id_inventory_product_Id = table.Column<int>(type: "int", nullable: false),
                    Id_supplier_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suppliers_products", x => x.Id_supplier_product);
                    table.ForeignKey(
                        name: "FK_suppliers_products_inventory_Id_inventory_product_Id",
                        column: x => x.Id_inventory_product_Id,
                        principalTable: "inventory",
                        principalColumn: "Id_inventory_product",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_suppliers_products_suppliers_Id_supplier_Id",
                        column: x => x.Id_supplier_Id,
                        principalTable: "suppliers",
                        principalColumn: "Id_supplier");
                });

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    Id_invoice = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_invoice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount_items_in_the_invoice = table.Column<int>(type: "int", nullable: false),
                    Id_purchase_order_Id = table.Column<int>(type: "int", nullable: false),
                    Id_supplier_Id = table.Column<int>(type: "int", nullable: false),
                    Id_department_Id = table.Column<int>(type: "int", nullable: false),
                    Id_module_Id = table.Column<int>(type: "int", nullable: false),
                    Id_company_Id = table.Column<int>(type: "int", nullable: false),
                    Date_insert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_update = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_delete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_restore = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delete_log_invoices = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoices", x => x.Id_invoice);
                    table.ForeignKey(
                        name: "FK_invoices_companies_Id_company_Id",
                        column: x => x.Id_company_Id,
                        principalTable: "companies",
                        principalColumn: "Id_company",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_invoices_departments_Id_department_Id",
                        column: x => x.Id_department_Id,
                        principalTable: "departments",
                        principalColumn: "Id_department");
                    table.ForeignKey(
                        name: "FK_invoices_modules_Id_module_Id",
                        column: x => x.Id_module_Id,
                        principalTable: "modules",
                        principalColumn: "Id_module");
                    table.ForeignKey(
                        name: "FK_invoices_purchase_orders_Id_purchase_order_Id",
                        column: x => x.Id_purchase_order_Id,
                        principalTable: "purchase_orders",
                        principalColumn: "Id_purchase_order");
                    table.ForeignKey(
                        name: "FK_invoices_suppliers_Id_supplier_Id",
                        column: x => x.Id_supplier_Id,
                        principalTable: "suppliers",
                        principalColumn: "Id_supplier");
                });

            migrationBuilder.CreateTable(
                name: "orders_products",
                columns: table => new
                {
                    Id_order_product = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Id_purchase_order_Id = table.Column<int>(type: "int", nullable: false),
                    Id_inventory_product_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders_products", x => x.Id_order_product);
                    table.ForeignKey(
                        name: "FK_orders_products_inventory_Id_inventory_product_Id",
                        column: x => x.Id_inventory_product_Id,
                        principalTable: "inventory",
                        principalColumn: "Id_inventory_product",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_products_purchase_orders_Id_purchase_order_Id",
                        column: x => x.Id_purchase_order_Id,
                        principalTable: "purchase_orders",
                        principalColumn: "Id_purchase_order");
                });

            migrationBuilder.CreateTable(
                name: "invoices_products",
                columns: table => new
                {
                    Id_invoice_product = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Unit_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Id_invoice_Id = table.Column<int>(type: "int", nullable: false),
                    Id_inventory_product_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoices_products", x => x.Id_invoice_product);
                    table.ForeignKey(
                        name: "FK_invoices_products_inventory_Id_inventory_product_Id",
                        column: x => x.Id_inventory_product_Id,
                        principalTable: "inventory",
                        principalColumn: "Id_inventory_product",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_invoices_products_invoices_Id_invoice_Id",
                        column: x => x.Id_invoice_Id,
                        principalTable: "invoices",
                        principalColumn: "Id_invoice");
                });

            migrationBuilder.InsertData(
                table: "clasification_movements",
                columns: new[] { "Id_clasification_movement", "Delete_log_clasification_movement", "Name_clasification_movement" },
                values: new object[,]
                {
                    { 1, false, "Eliminacion" },
                    { 2, false, "Creacion" },
                    { 3, false, "Actualizacion" }
                });

            migrationBuilder.InsertData(
                table: "companies",
                columns: new[] { "Id_company", "Delete_log_company", "Name_company" },
                values: new object[,]
                {
                    { 1, false, "Tech Innovators" },
                    { 2, false, "Global Solutions" },
                    { 3, false, "NextGen Systems" },
                    { 4, false, "Future Enterprises" },
                    { 5, false, "Pioneer Tech" },
                    { 6, false, "Visionary Corp" },
                    { 7, false, "Nexus Industries" },
                    { 8, false, "Elite Technologies" },
                    { 9, false, "Synergy Networks" },
                    { 10, false, "Vertex Solutions" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_audits_Id_company_Id",
                table: "audits",
                column: "Id_company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_audits_Id_department_Id",
                table: "audits",
                column: "Id_department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_audits_Id_module_Id",
                table: "audits",
                column: "Id_module_Id");

            migrationBuilder.CreateIndex(
                name: "IX_audits_Id_user_Id",
                table: "audits",
                column: "Id_user_Id");

            migrationBuilder.CreateIndex(
                name: "IX_categories_products_Id_company_Id",
                table: "categories_products",
                column: "Id_company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_categories_purchase_orders_Id_company_Id",
                table: "categories_purchase_orders",
                column: "Id_company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_categories_suppliers_Id_company_Id",
                table: "categories_suppliers",
                column: "Id_company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_departments_Id_company_Id",
                table: "departments",
                column: "Id_company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_Id_category_product_Id",
                table: "inventory",
                column: "Id_category_product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_Id_company_Id",
                table: "inventory",
                column: "Id_company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_Id_department_Id",
                table: "inventory",
                column: "Id_department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_Id_module_Id",
                table: "inventory",
                column: "Id_module_Id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_Id_movement_type_Id",
                table: "inventory",
                column: "Id_movement_type_Id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_Id_state_Id",
                table: "inventory",
                column: "Id_state_Id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_Id_supplier_Id",
                table: "inventory",
                column: "Id_supplier_Id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_Id_user_Id",
                table: "inventory",
                column: "Id_user_Id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_movements_Id_category_product_Id",
                table: "inventory_movements",
                column: "Id_category_product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_movements_Id_company_Id",
                table: "inventory_movements",
                column: "Id_company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_movements_Id_department_Id",
                table: "inventory_movements",
                column: "Id_department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_movements_Id_inventory_product_Id",
                table: "inventory_movements",
                column: "Id_inventory_product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_movements_Id_module_Id",
                table: "inventory_movements",
                column: "Id_module_Id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_movements_Id_movements_types_Id",
                table: "inventory_movements",
                column: "Id_movements_types_Id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_movements_Id_supplier_Id",
                table: "inventory_movements",
                column: "Id_supplier_Id");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_movements_Id_user_Id",
                table: "inventory_movements",
                column: "Id_user_Id");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_Id_company_Id",
                table: "invoices",
                column: "Id_company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_Id_department_Id",
                table: "invoices",
                column: "Id_department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_Id_module_Id",
                table: "invoices",
                column: "Id_module_Id");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_Id_purchase_order_Id",
                table: "invoices",
                column: "Id_purchase_order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_Id_supplier_Id",
                table: "invoices",
                column: "Id_supplier_Id");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_products_Id_inventory_product_Id",
                table: "invoices_products",
                column: "Id_inventory_product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_products_Id_invoice_Id",
                table: "invoices_products",
                column: "Id_invoice_Id");

            migrationBuilder.CreateIndex(
                name: "IX_movements_types_Id_clasification_movement_Id",
                table: "movements_types",
                column: "Id_clasification_movement_Id");

            migrationBuilder.CreateIndex(
                name: "IX_movements_types_Id_company_Id",
                table: "movements_types",
                column: "Id_company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_products_Id_inventory_product_Id",
                table: "orders_products",
                column: "Id_inventory_product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_products_Id_purchase_order_Id",
                table: "orders_products",
                column: "Id_purchase_order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_positions_Id_company_Id",
                table: "positions",
                column: "Id_company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_orders_Id_category_purchase_order_Id",
                table: "purchase_orders",
                column: "Id_category_purchase_order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_orders_Id_company_Id",
                table: "purchase_orders",
                column: "Id_company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_orders_Id_department_Id",
                table: "purchase_orders",
                column: "Id_department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_orders_Id_module_Id",
                table: "purchase_orders",
                column: "Id_module_Id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_orders_Id_movement_type_Id",
                table: "purchase_orders",
                column: "Id_movement_type_Id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_orders_Id_state_Id",
                table: "purchase_orders",
                column: "Id_state_Id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_orders_Id_supplier_Id",
                table: "purchase_orders",
                column: "Id_supplier_Id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_orders_Id_user_Id",
                table: "purchase_orders",
                column: "Id_user_Id");

            migrationBuilder.CreateIndex(
                name: "IX_states_Id_company_Id",
                table: "states",
                column: "Id_company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_Id_category_supplier_Id",
                table: "suppliers",
                column: "Id_category_supplier_Id");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_Id_company_Id",
                table: "suppliers",
                column: "Id_company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_Id_module_Id",
                table: "suppliers",
                column: "Id_module_Id");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_products_Id_inventory_product_Id",
                table: "suppliers_products",
                column: "Id_inventory_product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_products_Id_supplier_Id",
                table: "suppliers_products",
                column: "Id_supplier_Id");

            migrationBuilder.CreateIndex(
                name: "IX_users_Id_company_Id",
                table: "users",
                column: "Id_company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_users_Id_department_Id",
                table: "users",
                column: "Id_department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_users_Id_module_Id",
                table: "users",
                column: "Id_module_Id");

            migrationBuilder.CreateIndex(
                name: "IX_users_Id_position_Id",
                table: "users",
                column: "Id_position_Id");

            migrationBuilder.CreateIndex(
                name: "IX_users_Id_rol_Id",
                table: "users",
                column: "Id_rol_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audits");

            migrationBuilder.DropTable(
                name: "error_logs");

            migrationBuilder.DropTable(
                name: "inventory_movements");

            migrationBuilder.DropTable(
                name: "invoices_products");

            migrationBuilder.DropTable(
                name: "orders_products");

            migrationBuilder.DropTable(
                name: "suppliers_products");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "inventory");

            migrationBuilder.DropTable(
                name: "purchase_orders");

            migrationBuilder.DropTable(
                name: "categories_products");

            migrationBuilder.DropTable(
                name: "categories_purchase_orders");

            migrationBuilder.DropTable(
                name: "movements_types");

            migrationBuilder.DropTable(
                name: "states");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "clasification_movements");

            migrationBuilder.DropTable(
                name: "categories_suppliers");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "modules");

            migrationBuilder.DropTable(
                name: "positions");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "companies");
        }
    }
}
