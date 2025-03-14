using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FluxSYS_backend.Migrations
{
    /// <inheritdoc />
    public partial class NameInconsistency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Name_user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delete_log_audits = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audits", x => x.Id_audit);
                });

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
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTokens_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id_user",
                        onDelete: ReferentialAction.Cascade);
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
                    { 3, false, "Actualizacion" },
                    { 4, false, "Restauracion" }
                });

            migrationBuilder.InsertData(
                table: "companies",
                columns: new[] { "Id_company", "Delete_log_company", "Name_company" },
                values: new object[,]
                {
                    { 1, false, "FluxSYS" },
                    { 2, false, "Global Solutions" },
                    { 3, false, "NextGen Systems" },
                    { 4, false, "Future Enterprises" }
                });

            migrationBuilder.InsertData(
                table: "modules",
                columns: new[] { "Id_module", "Delete_log_module", "Name_module" },
                values: new object[,]
                {
                    { 1, false, "Proveedores" },
                    { 2, false, "Auditorias" },
                    { 3, false, "Inventario" },
                    { 4, false, "Facturas" },
                    { 5, false, "Ordenes de compra" },
                    { 6, false, "Usuarios" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id_role", "Delete_log_role", "Name_role" },
                values: new object[,]
                {
                    { 1, false, "Administrador" },
                    { 2, false, "Administrador Empresarial" },
                    { 3, false, "Jefe de Departamento" },
                    { 4, false, "Subjefe de Departamento" },
                    { 5, false, "Colaborador" }
                });

            migrationBuilder.InsertData(
                table: "categories_products",
                columns: new[] { "Id_category_product", "Delete_log_category_product", "Id_company_Id", "Name_category_product" },
                values: new object[,]
                {
                    { 1, false, 1, "Electrónica" },
                    { 2, false, 2, "Muebles" },
                    { 3, false, 3, "Ropa" },
                    { 4, false, 4, "Alimentos" },
                    { 5, false, 1, "Herramientas" }
                });

            migrationBuilder.InsertData(
                table: "categories_purchase_orders",
                columns: new[] { "Id_category_purchase_order", "Delete_log_category_purchase_order", "Id_company_Id", "Name_category_purchase_order" },
                values: new object[,]
                {
                    { 1, false, 1, "Interna" },
                    { 2, false, 2, "Externa" },
                    { 3, false, 3, "Urgente" },
                    { 4, false, 4, "Planificada" },
                    { 5, false, 1, "Especial" }
                });

            migrationBuilder.InsertData(
                table: "categories_suppliers",
                columns: new[] { "Id_category_supplier", "Delete_log_category_supplier", "Id_company_Id", "Name_category_supplier" },
                values: new object[,]
                {
                    { 1, false, 1, "Tecnología" },
                    { 2, false, 2, "Alimentos" },
                    { 3, false, 3, "Materiales de Construcción" },
                    { 4, false, 4, "Suministros de Oficina" },
                    { 5, false, 1, "Productos Químicos" }
                });

            migrationBuilder.InsertData(
                table: "departments",
                columns: new[] { "Id_department", "Delete_log_department", "Id_company_Id", "Name_deparment" },
                values: new object[,]
                {
                    { 1, false, 1, "System" },
                    { 2, false, 2, "Tecnología" },
                    { 3, false, 3, "Ventas" },
                    { 4, false, 4, "Logística" },
                    { 5, false, 1, "Finanzas" }
                });

            migrationBuilder.InsertData(
                table: "movements_types",
                columns: new[] { "Id_movement_type", "Delete_log_movement_type", "Id_clasification_movement_Id", "Id_company_Id", "Name_movement_type" },
                values: new object[,]
                {
                    { 1, false, 1, 1, "Entrada de Mercancía" },
                    { 2, false, 2, 2, "Salida por Venta" },
                    { 3, false, 3, 3, "Ajuste por Pérdida" },
                    { 4, false, 1, 1, "Transferencia de Almacén" },
                    { 5, false, 2, 2, "Devolución de Producto" }
                });

            migrationBuilder.InsertData(
                table: "positions",
                columns: new[] { "Id_position", "Delete_log_position", "Id_company_Id", "Name_position" },
                values: new object[,]
                {
                    { 1, false, 1, "System" },
                    { 2, false, 2, "Supervisor" },
                    { 3, false, 3, "Analista" },
                    { 4, false, 4, "Técnico" },
                    { 5, false, 1, "Asistente" }
                });

            migrationBuilder.InsertData(
                table: "states",
                columns: new[] { "Id_state", "Delete_log_state", "Id_company_Id", "Name_state" },
                values: new object[,]
                {
                    { 1, false, 1, "Activo" },
                    { 2, false, 2, "Inactivo" },
                    { 3, false, 3, "Pendiente" },
                    { 4, false, 1, "En Proceso" },
                    { 5, false, 2, "Finalizado" }
                });

            migrationBuilder.InsertData(
                table: "suppliers",
                columns: new[] { "Id_supplier", "Date_delete", "Date_insert", "Date_restore", "Date_update", "Delete_log_suppliers", "Id_category_supplier_Id", "Id_company_Id", "Id_module_Id", "Mail_supplier", "Name_supplier", "Phone_supplier" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 1, 1, 1, "contacto@proveedora.com", "Proveedor A", 5512345678L },
                    { 2, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 2, 2, 1, "ventas@proveedorb.com", "Proveedor B", 5523456789L },
                    { 3, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 3, 3, 1, "info@distribuidorac.com", "Distribuidora C", 5534567890L },
                    { 4, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 4, 1, 1, "soporte@empresad.com", "Empresa D", 5545678901L },
                    { 5, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 5, 2, 1, "compras@mayoristae.com", "Mayorista E", 5556789012L }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id_user", "Date_delete", "Date_insert", "Date_restore", "Date_update", "Delete_log_user", "Id_company_Id", "Id_department_Id", "Id_module_Id", "Id_position_Id", "Id_rol_Id", "Mail_user", "Name_user", "Password_user", "Phone_user" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 1, 1, 6, 1, 1, "gael@example.com", "Sánchez Lobato Gael", "$2y$10$5E5W.r3psQwlD7qFPQMqx.X55Tm8YuTWfirUaSQO5XTk0t5v0CJ/S", 1234567890L },
                    { 2, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 1, 1, 6, 1, 1, "maximo@example.com", "Quintero Escobar Carlos Máximo", "$2y$10$5E5W.r3psQwlD7qFPQMqx.X55Tm8YuTWfirUaSQO5XTk0t5v0CJ/S", 9876543210L },
                    { 3, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 1, 1, 6, 1, 1, "gustavo@example.com", "Gutiérrez Canul Gustavo", "$2y$10$5E5W.r3psQwlD7qFPQMqx.X55Tm8YuTWfirUaSQO5XTk0t5v0CJ/S", 1122334455L },
                    { 4, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 1, 1, 6, 1, 1, "mia@example.com", "Raymundo Mata Isha Mia", "$2y$10$5E5W.r3psQwlD7qFPQMqx.X55Tm8YuTWfirUaSQO5XTk0t5v0CJ/S", 2233445566L },
                    { 5, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 1, 1, 6, 1, 1, "carlos@example.com", "Velázquez De La Cruz Carlos Yahir", "$2y$10$5E5W.r3psQwlD7qFPQMqx.X55Tm8YuTWfirUaSQO5XTk0t5v0CJ/S", 3344556677L }
                });

            migrationBuilder.InsertData(
                table: "inventory",
                columns: new[] { "Id_inventory_product", "Date_delete", "Date_insert", "Date_restore", "Date_update", "Delete_log_inventory", "Id_category_product_Id", "Id_company_Id", "Id_department_Id", "Id_module_Id", "Id_movement_type_Id", "Id_state_Id", "Id_supplier_Id", "Id_user_Id", "Name_product", "Price_product", "Stock_product" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 1, 1, 1, 3, 1, 1, 1, 1, "Laptop Dell XPS 15", 25000.00m, 10 },
                    { 2, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 2, 2, 2, 3, 2, 2, 2, 2, "Monitor Samsung 27\"", 7500.00m, 20 },
                    { 3, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 3, 3, 3, 3, 1, 1, 3, 3, "Teclado Mecánico HyperX", 3500.00m, 15 },
                    { 4, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 4, 1, 4, 3, 2, 2, 4, 4, "Mouse Logitech MX Master 3", 2500.00m, 25 },
                    { 5, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 5, 2, 5, 3, 3, 3, 5, 5, "Silla Ergonómica", 12000.00m, 5 }
                });

            migrationBuilder.InsertData(
                table: "purchase_orders",
                columns: new[] { "Id_purchase_order", "Amount_items_in_the_order", "Date_delete", "Date_insert", "Date_restore", "Date_update", "Delete_log_purchase_orders", "Id_category_purchase_order_Id", "Id_company_Id", "Id_department_Id", "Id_module_Id", "Id_movement_type_Id", "Id_state_Id", "Id_supplier_Id", "Id_user_Id", "Name_purchase_order", "Total_price_products" },
                values: new object[,]
                {
                    { 1, 5, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 1, 1, 1, 5, 1, 1, 1, 1, "Orden de Compra 001", 1500.50m },
                    { 2, 8, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 2, 1, 2, 5, 2, 2, 2, 2, "Orden de Compra 002", 2450.75m },
                    { 3, 3, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 3, 2, 3, 5, 3, 3, 3, 3, "Orden de Compra 003", 850.25m },
                    { 4, 12, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 4, 2, 4, 5, 4, 4, 4, 4, "Orden de Compra 004", 3750.00m },
                    { 5, 7, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 5, 3, 5, 5, 5, 5, 5, 5, "Orden de Compra 005", 1980.99m }
                });

            migrationBuilder.InsertData(
                table: "invoices",
                columns: new[] { "Id_invoice", "Amount_items_in_the_invoice", "Date_delete", "Date_insert", "Date_restore", "Date_update", "Delete_log_invoices", "Id_company_Id", "Id_department_Id", "Id_module_Id", "Id_purchase_order_Id", "Id_supplier_Id", "Name_invoice" },
                values: new object[,]
                {
                    { 1, 5, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 1, 1, 4, 1, 1, "Factura 001" },
                    { 2, 8, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 1, 2, 4, 2, 2, "Factura 002" },
                    { 3, 3, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 2, 3, 4, 3, 3, "Factura 003" },
                    { 4, 12, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 2, 4, 4, 4, 4, "Factura 004" },
                    { 5, 7, null, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, 3, 5, 4, 5, 5, "Factura 005" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                table: "UserTokens",
                column: "UserId");
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
                name: "UserTokens");

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
