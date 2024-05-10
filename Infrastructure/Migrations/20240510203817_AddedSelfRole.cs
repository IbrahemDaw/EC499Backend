using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedSelfRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelfRole",
                table: "Roles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "IsSelfRole", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 10, 22, 38, 16, 605, DateTimeKind.Local).AddTicks(3248), false, new DateTime(2024, 5, 10, 22, 38, 16, 605, DateTimeKind.Local).AddTicks(3263) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 10, 22, 38, 16, 605, DateTimeKind.Local).AddTicks(5686), new DateTime(2024, 5, 10, 22, 38, 16, 605, DateTimeKind.Local).AddTicks(6571) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelfRole",
                table: "Roles");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 4, 29, 10, 36, 53, 372, DateTimeKind.Local).AddTicks(8608), new DateTime(2024, 4, 29, 10, 36, 53, 372, DateTimeKind.Local).AddTicks(8625) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 4, 29, 10, 36, 53, 373, DateTimeKind.Local).AddTicks(1407), new DateTime(2024, 4, 29, 10, 36, 53, 373, DateTimeKind.Local).AddTicks(2062) });
        }
    }
}
