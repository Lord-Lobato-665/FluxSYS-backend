using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluxSYS_backend.Migrations
{
    /// <inheritdoc />
    public partial class DepartmentsError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_products_companies_CompaniesId_company",
                table: "categories_products");

            migrationBuilder.DropForeignKey(
                name: "FK_departments_companies_CompaniesId_company",
                table: "departments");

            migrationBuilder.DropIndex(
                name: "IX_departments_CompaniesId_company",
                table: "departments");

            migrationBuilder.DropIndex(
                name: "IX_categories_products_CompaniesId_company",
                table: "categories_products");

            migrationBuilder.DropColumn(
                name: "CompaniesId_company",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "CompaniesId_company",
                table: "categories_products");

            migrationBuilder.CreateIndex(
                name: "IX_departments_Id_company_Id",
                table: "departments",
                column: "Id_company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_categories_products_Id_company_Id",
                table: "categories_products",
                column: "Id_company_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_products_companies_Id_company_Id",
                table: "categories_products",
                column: "Id_company_Id",
                principalTable: "companies",
                principalColumn: "Id_company",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_departments_companies_Id_company_Id",
                table: "departments",
                column: "Id_company_Id",
                principalTable: "companies",
                principalColumn: "Id_company",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_products_companies_Id_company_Id",
                table: "categories_products");

            migrationBuilder.DropForeignKey(
                name: "FK_departments_companies_Id_company_Id",
                table: "departments");

            migrationBuilder.DropIndex(
                name: "IX_departments_Id_company_Id",
                table: "departments");

            migrationBuilder.DropIndex(
                name: "IX_categories_products_Id_company_Id",
                table: "categories_products");

            migrationBuilder.AddColumn<int>(
                name: "CompaniesId_company",
                table: "departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompaniesId_company",
                table: "categories_products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_departments_CompaniesId_company",
                table: "departments",
                column: "CompaniesId_company");

            migrationBuilder.CreateIndex(
                name: "IX_categories_products_CompaniesId_company",
                table: "categories_products",
                column: "CompaniesId_company");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_products_companies_CompaniesId_company",
                table: "categories_products",
                column: "CompaniesId_company",
                principalTable: "companies",
                principalColumn: "Id_company",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_departments_companies_CompaniesId_company",
                table: "departments",
                column: "CompaniesId_company",
                principalTable: "companies",
                principalColumn: "Id_company",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
