using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluxSYS_backend.Migrations
{
    /// <inheritdoc />
    public partial class ErrorLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "error_logs");
        }
    }
}
