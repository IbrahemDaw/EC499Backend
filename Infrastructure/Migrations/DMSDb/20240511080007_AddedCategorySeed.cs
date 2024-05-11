using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Infrastructure.Migrations.DMSDb
{
    /// <inheritdoc />
    public partial class AddedCategorySeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_GradustionProjects_GradustionProjectId",
                table: "Documents");

            migrationBuilder.DropTable(
                name: "GradustionProjects");

            migrationBuilder.DropIndex(
                name: "IX_Documents_GradustionProjectId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "GradustionProjectId",
                table: "Documents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GradustionProjectId",
                table: "Documents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GradustionProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradustionProjects", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_GradustionProjectId",
                table: "Documents",
                column: "GradustionProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_GradustionProjects_GradustionProjectId",
                table: "Documents",
                column: "GradustionProjectId",
                principalTable: "GradustionProjects",
                principalColumn: "Id");
        }
    }
}
